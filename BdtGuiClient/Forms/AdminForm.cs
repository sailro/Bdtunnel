// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

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

        private void HandleResponse(IMinimalResponse response)
        {
            if (!response.Success)
            {
                MessageBox.Show(response.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void RefreshSessions()
        {
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

        private void Sessions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < Sessions.Rows.Count)
            {
                m_currentsession = (Session)Sessions.Rows[e.RowIndex].DataBoundItem;
                ConnectionsBindingSource.DataSource = m_currentsession.Connections;
            }
        }

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
                        MessageBox.Show("You can't kill your own session", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshSessions();
        }

        private void SessionsMenu_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = Sessions.SelectedRows.Count == 0;
        }

        private void ConnectionsMenu_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = Connections.SelectedRows.Count == 0;
        }

        private void Sessions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Session session = (Session)Sessions.Rows[e.RowIndex].DataBoundItem;
            if (session.Sid == m_sidhex)
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
            }
        }

    }
}