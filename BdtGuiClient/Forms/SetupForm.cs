/* BoutDuTunnel Copyright (c) 2007-2016 Sebastien LEBRETON

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

using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
using Bdt.Client.Configuration;
using Bdt.GuiClient.Logs;
using Bdt.GuiClient.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Protocol;

namespace Bdt.GuiClient.Forms
{
	public partial class SetupForm : Form
	{
		private readonly ClientConfig _clientConfig;

		private class ProtocolItem
		{
			private readonly string _caption;
			private readonly Type _protocol;

			public ProtocolItem(string caption, Type protocol)
			{
				_caption = caption;
				_protocol = protocol;
			}

			public override string ToString()
			{
				return _caption;
			}

			public Type Protocol
			{
				get { return _protocol; }
			}
		}

		public SetupForm(ClientConfig clientConfig)
		{
			InitializeComponent();
			_clientConfig = clientConfig;
		}

		private void SetupForm_Load(object sender, EventArgs e)
		{
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

			SocksEnabledEdit.Checked = _clientConfig.SocksEnabled;
			SocksSharedEdit.Checked = _clientConfig.SocksShared;
			SocksPortEdit.Minimum = IPEndPoint.MinPort;
			SocksPortEdit.Maximum = IPEndPoint.MaxPort;
			SocksPortEdit.Value = _clientConfig.SocksPort;

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

			var forwards = new BindingList<PortForward>();
			foreach (var forward in _clientConfig.Forwards.Values)
				forwards.Add(forward);

			BindingSource.DataSource = forwards;

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

			ProxyAuthenticationEdit_CheckedChanged(this, EventArgs.Empty);
			ProxyAutoConfigurationEdit_CheckedChanged(this, EventArgs.Empty);
		}

		private void FileSearch_Click(object sender, EventArgs e)
		{
			if (SaveFileDialog.ShowDialog() == DialogResult.OK)
				FileLogNameEdit.Text = SaveFileDialog.FileName;
		}

		private void ProxyAuthenticationEdit_CheckedChanged(object sender, EventArgs e)
		{
			ProxyUserNameEdit.Enabled = !ProxyAuthenticationEdit.Checked;
			ProxyPasswordEdit.Enabled = !ProxyAuthenticationEdit.Checked;
			ProxyDomainEdit.Enabled = !ProxyAuthenticationEdit.Checked;
		}

		private void ProxyAutoConfigurationEdit_CheckedChanged(object sender, EventArgs e)
		{
			ProxyAddressEdit.Enabled = !ProxyAutoConfigurationEdit.Checked;
			ProxyPortEdit.Enabled = !ProxyAutoConfigurationEdit.Checked;
		}

		private void PortForwardDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			if (e.ColumnIndex < 0 || e.ColumnIndex >= PortForwardDataGridView.ColumnCount)
				return;

			var col = PortForwardDataGridView.Columns[e.ColumnIndex];
			if (col.Name == PortForwardLocalPort.Name || col.Name == PortForwardRemotePort.Name)
				MessageBox.Show(string.Format(Strings.SETUPFORM_INVALID_PORT, IPEndPoint.MinPort, IPEndPoint.MaxPort),
					Strings.SETUPFORM_INVALID_INPUT, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void Apply_Click(object sender, EventArgs e)
		{
			_clientConfig.ServiceName = ServiceNameEdit.Text;
			_clientConfig.ServiceProtocol = ((ProtocolItem) ServiceProtocolEdit.SelectedItem).Protocol.FullName;
			_clientConfig.ServiceAddress = ServiceAddressEdit.Text;
			_clientConfig.ServicePort = Convert.ToInt32(ServicePortEdit.Value);
			_clientConfig.ServiceUserName = ServiceUserEdit.Text;
			_clientConfig.ServicePassword = ServicePasswordEdit.Text;

			_clientConfig.SocksEnabled = SocksEnabledEdit.Checked;
			_clientConfig.SocksShared = SocksSharedEdit.Checked;
			_clientConfig.SocksPort = Convert.ToInt32(SocksPortEdit.Value);

			_clientConfig.ProxyEnabled = ProxyEnabledEdit.Checked;
			_clientConfig.Expect100Continue = Proxy100Continue.Checked;
			_clientConfig.ProxyAutoAuthentication = ProxyAuthenticationEdit.Checked;
			_clientConfig.ProxyUserName = ProxyUserNameEdit.Text;
			_clientConfig.ProxyPassword = ProxyPasswordEdit.Text;
			_clientConfig.ProxyDomain = ProxyDomainEdit.Text;
			_clientConfig.ProxyAutoConfiguration = ProxyAutoConfigurationEdit.Checked;
			_clientConfig.ProxyAddress = ProxyAddressEdit.Text;
			_clientConfig.ProxyPort = Convert.ToInt32(ProxyPortEdit.Value);

			_clientConfig.ConsoleLogger = new NullConsoleLogger(ConsoleLogDateFormatEdit.Text, (ESeverity) ConsoleLogFilterEdit.SelectedItem)
			{
				Enabled = ConsoleLogEnabledEdit.Checked,
				StringFormat = ConsoleLogStringFormatEdit.Text
			};
			_clientConfig.FileLogger = new NullFileLogger(FileLogNameEdit.Text, FileLogAppendEdit.Checked, FileLogDateFormatEdit.Text,
				(ESeverity) FileLogFilterEdit.SelectedItem)
			{
				Enabled = FileLogEnabledEdit.Checked,
				StringFormat = FileLogStringFormatEdit.Text
			};

			_clientConfig.Forwards.Clear();
			foreach (var forward in (BindingList<PortForward>) BindingSource.DataSource)
				if (!_clientConfig.Forwards.ContainsKey(forward.LocalPort))
					_clientConfig.Forwards.Add(forward.LocalPort, forward);
		}
	}
}