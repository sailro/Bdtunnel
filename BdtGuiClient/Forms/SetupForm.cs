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
#endregion

namespace Bdt.GuiClient.Forms
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Fenêtre de configuration du client
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class SetupForm : Form
    {

        #region " Attributs "
        protected ClientConfig m_clientConfig;
        #endregion

        #region " Classes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Un item pour les combobox de selection du protocole
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected class ProtocolItem
        {

            #region " Attributs "
            protected string m_caption;
            protected Type m_protocol;
            #endregion

            #region " Méthodes "
            /// -----------------------------------------------------------------------------
            /// <summary>
            /// Constructeur
            /// </summary>
            /// <param name="caption">le libellé du protocole</param>
            /// <param name="protocol">le type du protocole</param>
            /// -----------------------------------------------------------------------------
            public ProtocolItem (string caption, Type protocol)
            {
                m_caption = caption;
                m_protocol = protocol;
            }

            /// -----------------------------------------------------------------------------
            /// <summary>
            /// Representation chaine de l'item
            /// </summary>
            /// <returns>on affiche le libellé du protocole</returns>
            /// -----------------------------------------------------------------------------
            public override string ToString ()
            {
                return m_caption;
            }

            /// -----------------------------------------------------------------------------
            /// <summary>
            /// Le protocol à utiliser
            /// </summary>
            /// -----------------------------------------------------------------------------
            public Type Protocol
            {
                get
                {
                    return m_protocol;
                }
            }
            #endregion

        }
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="clientConfig">la configuration du client</param>
        /// -----------------------------------------------------------------------------
        public SetupForm (ClientConfig clientConfig)
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
        private void SetupForm_Load (object sender, EventArgs e)
        {
            // Onglet Tunnel
            ServiceNameEdit.Text = m_clientConfig.ServiceName;
            ServiceProtocolEdit.Items.Add(new ProtocolItem(Strings.SETUPFORM_HTTP_BINARY_REMOTING, typeof(HttpBinaryRemoting)));
            ServiceProtocolEdit.Items.Add(new ProtocolItem(Strings.SETUPFORM_HTTP_SOAP_REMOTING, typeof(HttpSoapRemoting)));
            ServiceProtocolEdit.Items.Add(new ProtocolItem(Strings.SETUPFORM_HTTPS_BINARY_REMOTING, typeof(HttpSslBinaryRemoting)));
            ServiceProtocolEdit.Items.Add(new ProtocolItem(Strings.SETUPFORM_HTTPS_SOAP_REMOTING, typeof(HttpSslSoapRemoting)));
            ServiceProtocolEdit.Items.Add(new ProtocolItem(Strings.SETUPFORM_TCP_REMOTING, typeof(TcpRemoting)));
            ServiceProtocolEdit.Items.Add(new ProtocolItem(Strings.SETUPFORM_IPC_REMOTING, typeof(IpcRemoting)));
            foreach (ProtocolItem item in ServiceProtocolEdit.Items)
            {
                if (item.Protocol.FullName == m_clientConfig.ServiceProtocol)
                {
                    ServiceProtocolEdit.SelectedItem = item;
                }
            }
            ServiceAddressEdit.Text = m_clientConfig.ServiceAddress;
            ServicePortEdit.Minimum = IPEndPoint.MinPort;
            ServicePortEdit.Maximum = IPEndPoint.MaxPort;
            ServicePortEdit.Value = m_clientConfig.ServicePort;
            ServiceUserEdit.Text = m_clientConfig.ServiceUserName;
            ServicePasswordEdit.Text = m_clientConfig.ServicePassword;

            // Onglet Socks
            SocksEnabledEdit.Checked = m_clientConfig.SocksEnabled;
            SocksSharedEdit.Checked = m_clientConfig.SocksShared;
            SocksPortEdit.Minimum = IPEndPoint.MinPort;
            SocksPortEdit.Maximum = IPEndPoint.MaxPort;
            SocksPortEdit.Value = m_clientConfig.SocksPort;

            // Onglet Proxy
            ProxyEnabledEdit.Checked = m_clientConfig.ProxyEnabled;
            ProxyAuthenticationEdit.Checked = m_clientConfig.ProxyAutoAuthentication;
            ProxyUserNameEdit.Text = m_clientConfig.ProxyUserName;
            ProxyPasswordEdit.Text = m_clientConfig.ProxyPassword;
            ProxyDomainEdit.Text = m_clientConfig.ProxyDomain;
            ProxyAutoConfigurationEdit.Checked = m_clientConfig.ProxyAutoConfiguration;
            ProxyAddressEdit.Text = m_clientConfig.ProxyAddress;
            ProxyPortEdit.Minimum = IPEndPoint.MinPort;
            ProxyPortEdit.Maximum = IPEndPoint.MaxPort;
            ProxyPortEdit.Value = m_clientConfig.ProxyPort;

            // Redirections
            BindingList<PortForward> forwards = new BindingList<PortForward>();
            foreach (PortForward forward in m_clientConfig.Forwards.Values)
            {
                forwards.Add(forward);
            }
            BindingSource.DataSource = forwards;

            // Onglet Logs
            ConsoleLogEnabledEdit.Checked = m_clientConfig.ConsoleLogger.Enabled;
            ConsoleLogFilterEdit.DataSource = Enum.GetValues(typeof(ESeverity));
            ConsoleLogFilterEdit.SelectedItem = m_clientConfig.ConsoleLogger.Filter;
            ConsoleLogStringFormatEdit.Text = m_clientConfig.ConsoleLogger.StringFormat;
            ConsoleLogDateFormatEdit.Text = m_clientConfig.ConsoleLogger.DateFormat;
            FileLogEnabledEdit.Checked = m_clientConfig.FileLogger.Enabled;
            FileLogFilterEdit.DataSource = Enum.GetValues(typeof(ESeverity));
            FileLogFilterEdit.SelectedItem = m_clientConfig.FileLogger.Filter;
            FileLogStringFormatEdit.Text = m_clientConfig.FileLogger.StringFormat;
            FileLogDateFormatEdit.Text = m_clientConfig.FileLogger.DateFormat;
            FileLogNameEdit.Text = m_clientConfig.FileLogger.Filename;
            FileLogAppendEdit.Checked = m_clientConfig.FileLogger.Append;

            // Maj des états
            ProxyAuthenticationEdit_CheckedChanged(this, EventArgs.Empty);
            ProxyAutoConfigurationEdit_CheckedChanged(this, EventArgs.Empty);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Recherche du fichier de log
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void FileSearch_Click (object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileLogNameEdit.Text = SaveFileDialog.FileName;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Activation/Desactivation des sous contrôles liés à l'auth du proxy
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void ProxyAuthenticationEdit_CheckedChanged (object sender, EventArgs e)
        {
            ProxyUserNameEdit.Enabled = !ProxyAuthenticationEdit.Checked;
            ProxyPasswordEdit.Enabled = !ProxyAuthenticationEdit.Checked;
            ProxyDomainEdit.Enabled = !ProxyAuthenticationEdit.Checked;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Activation/Desactivation des sous contrôles liés à la localisation du proxy
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void ProxyAutoConfigurationEdit_CheckedChanged (object sender, EventArgs e)
        {
            ProxyAddressEdit.Enabled = !ProxyAutoConfigurationEdit.Checked;
            ProxyPortEdit.Enabled = !ProxyAutoConfigurationEdit.Checked;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Saisie incorrecte dans la grille de forward
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void PortForwardDataGridView_DataError (object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.ColumnIndex < PortForwardDataGridView.ColumnCount)
            {
                DataGridViewColumn col = PortForwardDataGridView.Columns[e.ColumnIndex];
                if (col.Name == PortForwardLocalPort.Name || col.Name == PortForwardRemotePort.Name)
                {
                    MessageBox.Show(String.Format(Strings.SETUPFORM_INVALID_PORT, IPEndPoint.MinPort, IPEndPoint.MaxPort), Strings.SETUPFORM_INVALID_INPUT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Enregistrement des modifications
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void Apply_Click (object sender, EventArgs e)
        {
            // Onglet Tunnel
            m_clientConfig.ServiceName = ServiceNameEdit.Text;
            m_clientConfig.ServiceProtocol = ((ProtocolItem)ServiceProtocolEdit.SelectedItem).Protocol.FullName;
            m_clientConfig.ServiceAddress = ServiceAddressEdit.Text;
            m_clientConfig.ServicePort = Convert.ToInt32(ServicePortEdit.Value);
            m_clientConfig.ServiceUserName = ServiceUserEdit.Text;
            m_clientConfig.ServicePassword = ServicePasswordEdit.Text;

            // Onglet Socks
            m_clientConfig.SocksEnabled = SocksEnabledEdit.Checked;
            m_clientConfig.SocksShared = SocksSharedEdit.Checked;
            m_clientConfig.SocksPort = Convert.ToInt32(SocksPortEdit.Value);

            // Onglet Proxy
            m_clientConfig.ProxyEnabled = ProxyEnabledEdit.Checked;
            m_clientConfig.ProxyAutoAuthentication = ProxyAuthenticationEdit.Checked;
            m_clientConfig.ProxyUserName = ProxyUserNameEdit.Text;
            m_clientConfig.ProxyPassword = ProxyPasswordEdit.Text;
            m_clientConfig.ProxyDomain = ProxyDomainEdit.Text;
            m_clientConfig.ProxyAutoConfiguration = ProxyAutoConfigurationEdit.Checked;
            m_clientConfig.ProxyAddress = ProxyAddressEdit.Text;
            m_clientConfig.ProxyPort = Convert.ToInt32(ProxyPortEdit.Value);

            // Onglet Logs
            m_clientConfig.ConsoleLogger = new NullConsoleLogger(ConsoleLogDateFormatEdit.Text, (ESeverity)ConsoleLogFilterEdit.SelectedItem);
            m_clientConfig.ConsoleLogger.Enabled = ConsoleLogEnabledEdit.Checked;
            m_clientConfig.ConsoleLogger.StringFormat = ConsoleLogStringFormatEdit.Text;
            m_clientConfig.FileLogger = new NullFileLogger(FileLogNameEdit.Text, FileLogAppendEdit.Checked, FileLogDateFormatEdit.Text, (ESeverity)FileLogFilterEdit.SelectedItem);
            m_clientConfig.FileLogger.Enabled = FileLogEnabledEdit.Checked;
            m_clientConfig.FileLogger.StringFormat = FileLogStringFormatEdit.Text;

            // Redirections
            m_clientConfig.Forwards.Clear();
            foreach (PortForward forward in (BindingList<PortForward>) BindingSource.DataSource)
            {
                if (!m_clientConfig.Forwards.ContainsKey(forward.LocalPort))
                {
                    m_clientConfig.Forwards.Add(forward.LocalPort, forward);
                }
            }
        }
        #endregion

    }
}