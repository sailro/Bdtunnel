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
using System.Windows.Forms;

using Bdt.Client.Configuration;
#endregion

namespace Bdt.GuiClient.Forms
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Fenêtre d'authentification sur le proxy HTTP
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class ProxyForm : Form
    {

        #region " Attributs "
        protected ClientConfig m_clientConfig;
        #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="clientConfig">la configuration du client</param>
        /// -----------------------------------------------------------------------------
        public ProxyForm (ClientConfig clientConfig)
        {
            InitializeComponent();
            m_clientConfig = clientConfig;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Chargement de la page
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void ProxyForm_Load (object sender, EventArgs e)
        {
            UserNameEdit.Text = m_clientConfig.ProxyUserName;
            PasswordEdit.Text = m_clientConfig.ProxyPassword;
            DomainEdit.Text = m_clientConfig.ProxyDomain;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// On applique les nouvelles informations d'authentification
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void Apply_Click (object sender, EventArgs e)
        {
            m_clientConfig.ProxyUserName = UserNameEdit.Text;
            m_clientConfig.ProxyPassword = PasswordEdit.Text;
            m_clientConfig.ProxyDomain = DomainEdit.Text;
        }
        #endregion

    }
}