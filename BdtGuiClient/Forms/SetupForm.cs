/* BoutDuTunnel Copyright (c)  2007-2013 Sebastien LEBRETON

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
using System.ComponentModel;
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
	    private readonly ClientConfig _clientConfig;
        #endregion

        #region " Classes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Un item pour les combobox de selection du protocole
        /// </summary>
        /// -----------------------------------------------------------------------------
        private class ProtocolItem
        {

            #region " Attributs "
	        private readonly string _caption;
	        private readonly Type _protocol;
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
                _caption = caption;
                _protocol = protocol;
            }

            /// -----------------------------------------------------------------------------
            /// <summary>
            /// Representation chaine de l'item
            /// </summary>
            /// <returns>on affiche le libellé du protocole</returns>
            /// -----------------------------------------------------------------------------
            public override string ToString ()
            {
                return _caption;
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
                    return _protocol;
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
            _clientConfig = clientConfig;
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
            ServiceNameEdit.Text = _clientConfig.ServiceName;
            ServiceProtocolEdit.Items.Add(new ProtocolItem(Strings.SETUPFORM_HTTP_BINARY_REMOTING, typeof(HttpBinaryRemoting)));
            ServiceProtocolEdit.Items.Add(new ProtocolItem(Strings.SETUPFORM_HTTP_SOAP_REMOTING, typeof(HttpSoapRemoting)));
            ServiceProtocolEdit.Items.Add(new ProtocolItem(Strings.SETUPFORM_HTTPS_BINARY_REMOTING, typeof(HttpSslBinaryRemoting)));
            ServiceProtocolEdit.Items.Add(new ProtocolItem(Strings.SETUPFORM_HTTPS_SOAP_REMOTING, typeof(HttpSslSoapRemoting)));
            ServiceProtocolEdit.Items.Add(new ProtocolItem(Strings.SETUPFORM_TCP_REMOTING, typeof(TcpRemoting)));
            ServiceProtocolEdit.Items.Add(new ProtocolItem(Strings.SETUPFORM_IPC_REMOTING, typeof(IpcRemoting)));

			foreach (ProtocolItem item in ServiceProtocolEdit.Items)
		        if (item.Protocol.FullName == _clientConfig.ServiceProtocol)
			        ServiceProtocolEdit.SelectedItem = item;
	        
			ServiceAddressEdit.Text = _clientConfig.ServiceAddress;
            ServicePortEdit.Minimum = IPEndPoint.MinPort;
            ServicePortEdit.Maximum = IPEndPoint.MaxPort;
            ServicePortEdit.Value = _clientConfig.ServicePort;
            ServiceUserEdit.Text = _clientConfig.ServiceUserName;
            ServicePasswordEdit.Text = _clientConfig.ServicePassword;

            // Onglet Socks
            SocksEnabledEdit.Checked = _clientConfig.SocksEnabled;
            SocksSharedEdit.Checked = _clientConfig.SocksShared;
            SocksPortEdit.Minimum = IPEndPoint.MinPort;
            SocksPortEdit.Maximum = IPEndPoint.MaxPort;
            SocksPortEdit.Value = _clientConfig.SocksPort;

            // Onglet Proxy
            ProxyEnabledEdit.Checked = _clientConfig.ProxyEnabled;
			Proxy100Continue.Checked = _clientConfig.Expect100Continue;
			ProxyAuthenticationEdit.Checked = _clientConfig.ProxyAutoAuthentication;
            ProxyUserNameEdit.Text = _clientConfig.ProxyUserName;
            ProxyPasswordEdit.Text = _clientConfig.ProxyPassword;
            ProxyDomainEdit.Text = _clientConfig.ProxyDomain;
            ProxyAutoConfigurationEdit.Checked = _clientConfig.ProxyAutoConfiguration;
            ProxyAddressEdit.Text = _clientConfig.ProxyAddress;
            ProxyPortEdit.Minimum = IPEndPoint.MinPort;
            ProxyPortEdit.Maximum = IPEndPoint.MaxPort;
            ProxyPortEdit.Value = _clientConfig.ProxyPort;

            // Redirections
            var forwards = new BindingList<PortForward>();
	        foreach (var forward in _clientConfig.Forwards.Values)
		        forwards.Add(forward);
	        
			BindingSource.DataSource = forwards;

            // Onglet Logs
            ConsoleLogEnabledEdit.Checked = _clientConfig.ConsoleLogger.Enabled;
            ConsoleLogFilterEdit.DataSource = Enum.GetValues(typeof(ESeverity));
            ConsoleLogFilterEdit.SelectedItem = _clientConfig.ConsoleLogger.Filter;
            ConsoleLogStringFormatEdit.Text = _clientConfig.ConsoleLogger.StringFormat;
            ConsoleLogDateFormatEdit.Text = _clientConfig.ConsoleLogger.DateFormat;
            FileLogEnabledEdit.Checked = _clientConfig.FileLogger.Enabled;
            FileLogFilterEdit.DataSource = Enum.GetValues(typeof(ESeverity));
            FileLogFilterEdit.SelectedItem = _clientConfig.FileLogger.Filter;
            FileLogStringFormatEdit.Text = _clientConfig.FileLogger.StringFormat;
            FileLogDateFormatEdit.Text = _clientConfig.FileLogger.DateFormat;
            FileLogNameEdit.Text = _clientConfig.FileLogger.Filename;
            FileLogAppendEdit.Checked = _clientConfig.FileLogger.Append;

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
		        FileLogNameEdit.Text = SaveFileDialog.FileName;
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
	        if (e.ColumnIndex < 0 || e.ColumnIndex >= PortForwardDataGridView.ColumnCount)
				return;
	        
			var col = PortForwardDataGridView.Columns[e.ColumnIndex];
	        if (col.Name == PortForwardLocalPort.Name || col.Name == PortForwardRemotePort.Name)
		        MessageBox.Show(String.Format(Strings.SETUPFORM_INVALID_PORT, IPEndPoint.MinPort, IPEndPoint.MaxPort),
		                        Strings.SETUPFORM_INVALID_INPUT, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            _clientConfig.ServiceName = ServiceNameEdit.Text;
            _clientConfig.ServiceProtocol = ((ProtocolItem)ServiceProtocolEdit.SelectedItem).Protocol.FullName;
            _clientConfig.ServiceAddress = ServiceAddressEdit.Text;
            _clientConfig.ServicePort = Convert.ToInt32(ServicePortEdit.Value);
            _clientConfig.ServiceUserName = ServiceUserEdit.Text;
            _clientConfig.ServicePassword = ServicePasswordEdit.Text;

            // Onglet Socks
            _clientConfig.SocksEnabled = SocksEnabledEdit.Checked;
            _clientConfig.SocksShared = SocksSharedEdit.Checked;
            _clientConfig.SocksPort = Convert.ToInt32(SocksPortEdit.Value);

            // Onglet Proxy
            _clientConfig.ProxyEnabled = ProxyEnabledEdit.Checked;
	        _clientConfig.Expect100Continue = Proxy100Continue.Checked;
            _clientConfig.ProxyAutoAuthentication = ProxyAuthenticationEdit.Checked;
            _clientConfig.ProxyUserName = ProxyUserNameEdit.Text;
            _clientConfig.ProxyPassword = ProxyPasswordEdit.Text;
            _clientConfig.ProxyDomain = ProxyDomainEdit.Text;
            _clientConfig.ProxyAutoConfiguration = ProxyAutoConfigurationEdit.Checked;
            _clientConfig.ProxyAddress = ProxyAddressEdit.Text;
            _clientConfig.ProxyPort = Convert.ToInt32(ProxyPortEdit.Value);

            // Onglet Logs
            _clientConfig.ConsoleLogger = new NullConsoleLogger(ConsoleLogDateFormatEdit.Text, (ESeverity)ConsoleLogFilterEdit.SelectedItem)
	        {
		        Enabled = ConsoleLogEnabledEdit.Checked,
		        StringFormat = ConsoleLogStringFormatEdit.Text
	        };
	        _clientConfig.FileLogger = new NullFileLogger(FileLogNameEdit.Text, FileLogAppendEdit.Checked, FileLogDateFormatEdit.Text, (ESeverity)FileLogFilterEdit.SelectedItem)
		    {
			    Enabled = FileLogEnabledEdit.Checked,
			    StringFormat = FileLogStringFormatEdit.Text
		    };

	        // Redirections
            _clientConfig.Forwards.Clear();
	        foreach (var forward in (BindingList<PortForward>) BindingSource.DataSource)
		        if (!_clientConfig.Forwards.ContainsKey(forward.LocalPort))
			        _clientConfig.Forwards.Add(forward.LocalPort, forward);
        }
        #endregion

    }
}