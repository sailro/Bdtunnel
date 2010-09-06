/* BoutDuTunnel Copyright (c) 2007-2010 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

#region " Inclusions "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Windows.Forms;

using Bdt.Client.Configuration;
using Bdt.GuiClient.Logs;
using Bdt.GuiClient.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Protocol;
using Bdt.Shared.Service;
using Bdt.Shared.Request;
using Bdt.Shared.Response;
#endregion

namespace Bdt.GuiClient.Forms
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Fenêtre de configuration du client
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class AdminForm : Form
    {

        #region " Attributs "
        protected ITunnel m_tunnel;
        protected int m_sid;
        protected string m_sidhex;
        protected Session m_currentsession;
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="tunnel">le tunnel</param>
        /// <param name="sid">le jeton de session</param>
        /// -----------------------------------------------------------------------------
        public AdminForm (ITunnel tunnel, int sid)
        {
            InitializeComponent();
            m_tunnel = tunnel;
            m_sid = sid;
            m_sidhex = m_sid.ToString("x");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Chargement de la page
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void AdminForm_Load (object sender, EventArgs e)
        {
            RefreshSessions();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Traite une réponse du serveur
        /// </summary>
        /// <param name="response">la réponse</param>
        /// -----------------------------------------------------------------------------
        private void HandleResponse(IMinimalResponse response)
        {
            if (!response.Success)
            {
                MessageBox.Show(response.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualise la liste des sessions et connexions
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void RefreshSessions()
        {
            try
            {
                BtRefresh.Enabled = BtClose.Enabled = false;
                this.UseWaitCursor = true;

                Application.DoEvents();
                MonitorResponse response = m_tunnel.Monitor(new SessionContextRequest(m_sid));
                if (response.Success)
                {
                    m_currentsession = default(Session);
                    SessionsBindingSource.DataSource = response.Sessions;
                    SessionsBindingSource.ResetBindings(false);

                    if (Sessions.SelectedRows.Count > 0)
                    {
                        m_currentsession = (Session)Sessions.SelectedRows[0].DataBoundItem;
                        ConnectionsBindingSource.DataSource = m_currentsession.Connections;
                    }
                    else
                    {
                        ConnectionsBindingSource.DataSource = null;
                    }
                }
                else
                {
                    HandleResponse(response);
                }
            }
            finally
            {
                BtRefresh.Enabled = BtClose.Enabled = true;
                this.UseWaitCursor = false;
            }

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Clic sur un item de la liste des sessions
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void Sessions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < Sessions.Rows.Count)
            {
                m_currentsession = (Session)Sessions.Rows[e.RowIndex].DataBoundItem;
                ConnectionsBindingSource.DataSource = m_currentsession.Connections;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Termine une session
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void KillSessionItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Sessions.SelectedRows)
            {
                Session session = (Session)row.DataBoundItem;
                int targetsid = 0;

                if (int.TryParse(session.Sid, System.Globalization.NumberStyles.HexNumber, null, out targetsid))
                {
                    if (targetsid == m_sid)
                    {
                        MessageBox.Show(Strings.ADMINFORM_KILL_OWN_SESSION, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        IMinimalResponse response = m_tunnel.KillSession(new KillSessionRequest(targetsid, m_sid));
                        if (!response.Success)
                        {
                            HandleResponse(response);
                        }
                    }
                }
            }
            RefreshSessions();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Termine une connexion
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void KillConnectionItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Connections.SelectedRows)
            {
                Connection connection = (Connection)row.DataBoundItem;
                int targetsid = 0;
                int targetcid = 0;

                if (int.TryParse(m_currentsession.Sid, System.Globalization.NumberStyles.HexNumber, null, out targetsid)
                    && int.TryParse(connection.Cid, System.Globalization.NumberStyles.HexNumber, null, out targetcid))
                {
                    IMinimalResponse response = m_tunnel.KillConnection(new KillConnectionRequest(targetsid, m_sid, targetcid));
                    if (!response.Success)
                    {
                        HandleResponse(response);
                    }
                }
            }
            RefreshSessions();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualise la liste des sessions et connexions
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshSessions();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Détermine si le popup doit s'ouvrir pour la liste des sessions
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void SessionsMenu_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = Sessions.SelectedRows.Count == 0;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Détermine si le popup doit s'ouvrir pour la liste des connexions
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void ConnectionsMenu_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = Connections.SelectedRows.Count == 0;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Format des lignes de sessions (pour mettre en gras la session active)
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void Sessions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Session session = (Session)Sessions.Rows[e.RowIndex].DataBoundItem;
            if (session.Sid == m_sidhex)
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
            }
        }
        #endregion
    }
}