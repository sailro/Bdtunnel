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
using System.Diagnostics;
using Bdt.Shared.Protocol;
using Bdt.Client.Runtime;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Bdt.Shared.Logs;
using Bdt.GuiClient.Resources;

namespace Bdt.GuiClient.Forms
{
	public partial class MainComponent : Component
	{
		protected enum EClientState
		{
			Changing = 0,
			Started = 1,
			Stopped = 2,
		}

		private readonly BdtClient _client;
		protected EClientState ClientState = EClientState.Stopped;

		private void ConfigureItem_Click(object sender, EventArgs e)
		{
			var previous = ClientState;
			ClientState = EClientState.Changing;
			using (var setup = new SetupForm(_client.ClientConfig))
			{
				if (setup.ShowDialog() == DialogResult.OK)
				{
					ClientState = previous;
					WaitThenStopClientIfNeeded();
					try
					{
						_client.ClientConfig.SaveToFile(Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + _client.ConfigFile);
						_client.UnLoadConfiguration();
						_client.LoadConfiguration();
						StartItem_Click(sender, e);
					}
					catch (Exception ex)
					{
						_client.Log(ex.Message, ESeverity.ERROR);
						_client.Log(ex.ToString(), ESeverity.DEBUG);
					}
				}
				else
				{
					ClientState = previous;
				}
			}
		}

		private void AdminItem_Click(object sender, EventArgs e)
		{
			var previous = ClientState;
			ClientState = EClientState.Changing;

			using (var admin = new AdminForm(_client.Tunnel, _client.Sid))
				admin.ShowDialog();

			ClientState = previous;
		}

// ReSharper disable RedundantAssignment
		public void InputProxyCredentials(IProxyCompatible proxyProtocol, ref bool retry)
// ReSharper restore RedundantAssignment
		{
			using (var proxy = new ProxyForm(_client.ClientConfig))
			{
				if (proxy.ShowDialog() == DialogResult.OK)
				{
					proxyProtocol.Proxy.Credentials = new NetworkCredential(_client.ClientConfig.ProxyUserName,
						_client.ClientConfig.ProxyPassword,
						_client.ClientConfig.ProxyDomain);
					retry = true;
				}
				else
					retry = false;
			}
		}

		private void StartItem_Click(object sender, EventArgs e)
		{
			ClientState = EClientState.Changing;
			UpdateNotifyIcon(Strings.MAINFORM_CLIENT_STARTING, false);
			System.Threading.ThreadPool.QueueUserWorkItem(StartClient);
		}

		private void StopItem_Click(object sender, EventArgs e)
		{
			ClientState = EClientState.Changing;
			UpdateNotifyIcon(Strings.MAINFORM_CLIENT_STOPPING, false);
			System.Threading.ThreadPool.QueueUserWorkItem(StopClient);
		}

		private void StartClient(Object state)
		{
			try
			{
				_client.StartClient();
				ClientState = EClientState.Started;
				NotifyContextMenu.Invoke(new UpdateNotifyIconDelegate(UpdateNotifyIcon), Strings.MAINFORM_CLIENT_STARTED, true);
			}
			catch (Exception e)
			{
				_client.Log(e.Message, ESeverity.ERROR);
				_client.Log(e.ToString(), ESeverity.DEBUG);
				StopClient(null);
			}
			NotifyContextMenu_Opened(this, EventArgs.Empty);
		}

		private void StopClient(object state)
		{
			try
			{
				_client.StopClient();
				ClientState = EClientState.Stopped;
				NotifyContextMenu.Invoke(new UpdateNotifyIconDelegate(UpdateNotifyIcon), Strings.MAINFORM_CLIENT_STOPPED, false);
			}
			catch (Exception e)
			{
				_client.Log(e.Message, ESeverity.ERROR);
				_client.Log(e.ToString(), ESeverity.DEBUG);
			}
		}

		private delegate void UpdateNotifyIconDelegate(string text, bool useBalloon);

		private void UpdateNotifyIcon(string text, bool useBalloon)
		{
			if (text != null)
				NotifyIcon.Text = text;

			StartItem.Enabled = (ClientState == EClientState.Stopped);
			StopItem.Enabled = (ClientState == EClientState.Started);
			AdminItem.Enabled = (ClientState == EClientState.Started);
			ConfigureItem.Enabled = (ClientState != EClientState.Changing);
			QuitItem.Enabled = (ClientState != EClientState.Changing);
			InfoItem.Text = ClientState == EClientState.Changing
				? Strings.MAINFORM_PLEASE_WAIT
				: string.Format(Strings.MAINFORM_CLIENT_TITLE, GetType().Assembly.GetName().Version.ToString(3));
			LogsItem.Enabled = _client.ClientConfig.FileLogger.Enabled;

			if (useBalloon && text != null)
				NotifyIcon.ShowBalloonTip(0, InfoItem.Text, text, ToolTipIcon.Info);
		}

		private void WaitThenStopClientIfNeeded()
		{
			while (ClientState == EClientState.Changing)
				Application.DoEvents();

			if (ClientState == EClientState.Started)
				StopClient(null);
		}

		private void QuitItem_Click(object sender, EventArgs e)
		{
			WaitThenStopClientIfNeeded();
			Application.Exit();
		}

		private void LogsItem_Click(object sender, EventArgs e)
		{
			var info = new ProcessStartInfo(_client.ClientConfig.FileLogger.Filename);
			Process.Start(info);
		}

		private void NotifyContextMenu_Opened(object sender, EventArgs e)
		{
			UpdateNotifyIcon(null, false);
		}

		public MainComponent()
		{
			InitializeComponent();
			UpdateNotifyIcon("Hello", true);
		}

		public MainComponent(BdtClient client)
		{
			_client = client;
			InitializeComponent();
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			var handle = NotifyContextMenu.Handle;
			if (handle.ToInt32() <= 0)
				return;

			Timer.Enabled = false;
			StartItem_Click(sender, e);
		}
	}
}