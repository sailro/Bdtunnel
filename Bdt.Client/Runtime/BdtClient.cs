/* BoutDuTunnel Copyright (c) 2006-2021 Sebastien Lebreton

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
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using Bdt.Client.Configuration;
using Bdt.Client.Resources;
using Bdt.Client.Sockets;
using Bdt.Client.Socks;
using Bdt.Shared.Runtime;
using Bdt.Shared.Service;
using Bdt.Shared.Protocol;
using Bdt.Shared.Logs;
using Bdt.Shared.Request;
using Bdt.Shared.Response;
using Bdt.Client.Commands;

namespace Bdt.Client.Runtime
{
	public class BdtClient : Program
	{
		private readonly Dictionary<int, TcpServer> _servers = new();
		private int? _sid;

		public ClientConfig ClientConfig { get; protected set; }

		public ITunnel Tunnel { get; private set; }

		public int Sid
		{
			get
			{
				if (!_sid.HasValue)
					throw new ArgumentException();

				return _sid.Value;
			}
		}

		public static void Main(string[] args)
		{
			new BdtClient().Run(args);
		}

		private void InitializeForwards(ITunnel tunnel, int sid)
		{
			foreach (var forward in ClientConfig.Forwards.Values)
			{
				if (!forward.Enabled)
					continue;

				var remotePort = forward.RemotePort;
				var localPort = forward.LocalPort;
				var shared = forward.Shared;
				var address = forward.Address;

				if (_servers.ContainsKey(localPort))
				{
					Log(string.Format(Strings.FORWARD_CANCELED, localPort, address, remotePort), ESeverity.WARN);
				}
				else
				{
					var server = new GatewayServer(localPort, shared, remotePort, address, tunnel, sid);
					_servers.Add(localPort, server);
				}
			}
		}

		private void InitializeSocks(ITunnel tunnel, int sid)
		{
			if (!ClientConfig.SocksEnabled)
				return;

			var port = ClientConfig.SocksPort;
			if (_servers.ContainsKey(port))
			{
				Log(string.Format(Strings.SOCKS_SERVER_DISABLED, port), ESeverity.WARN);
			}
			else
			{
				var socks = new SocksServer(port, ClientConfig.SocksShared, tunnel, sid);
				_servers.Add(port, socks);
			}
		}

		private void DisposeServers()
		{
			foreach (var server in _servers.Values)
				server.CloseServer();

			_servers.Clear();
		}

		private void ConfigureProxy(GenericProtocol protocol)
		{
			ServicePointManager.Expect100Continue = ClientConfig.Expect100Continue;

			if (!(protocol is IProxyCompatible))
				return;

			var proxyProtocol = ((IProxyCompatible)protocol);
			IWebProxy proxy;

			if (ClientConfig.ProxyEnabled)
			{
				// Configuration
#pragma warning disable 618
				proxy = ClientConfig.ProxyAutoConfiguration ? GlobalProxySelection.Select : new WebProxy(ClientConfig.ProxyAddress, ClientConfig.ProxyPort);
#pragma warning restore 618

				if (proxy != null)
				{
					// Authentification
					if (ClientConfig.ProxyAutoAuthentication)
					{
						proxy.Credentials = CredentialCache.DefaultCredentials;
					}
					else
					{
						var netCreds = new NetworkCredential {UserName = ClientConfig.ProxyUserName, Password = ClientConfig.ProxyPassword, Domain = ClientConfig.ProxyDomain};
						proxy.Credentials = netCreds;
					}
				}
			}
			else
			{
#pragma warning disable 618
				proxy = GlobalProxySelection.GetEmptyWebProxy();
#pragma warning restore 618
			}

			var webProxy = proxy as WebProxy;
			if (webProxy != null && webProxy.Address == null)
			{
#pragma warning disable 618
				proxy = GlobalProxySelection.GetEmptyWebProxy();
#pragma warning restore 618
			}

			proxyProtocol.Proxy = proxy;
			Log(webProxy != null ? string.Format(Strings.USING_PROXY, webProxy.Address) : Strings.NOT_USING_PROXY, ESeverity.INFO);
		}

		private static string InputString(string msg, bool hide)
		{
			ConsoleKeyInfo cki;
			Console.Write(@"INPUT {0}", msg);
			var left = Console.CursorLeft;
			var top = Console.CursorTop;

			Console.TreatControlCAsInput = false;
			var result = new StringBuilder();

			do
			{
				cki = Console.ReadKey(true);
				switch (cki.Key)
				{
					case ConsoleKey.Backspace:
						if (result.Length > 0)
						{
							Console.SetCursorPosition(left + result.Length - 1, top);
							Console.Write(@" ");
							result.Remove(result.Length - 1, 1);
						}

						Console.SetCursorPosition(left + result.Length, top);
						break;
					default:
						if (char.IsLetterOrDigit(cki.KeyChar) && (result.Length < 32))
						{
							result.Append(cki.KeyChar);
							Console.Write(hide ? '*' : cki.KeyChar);
						}

						break;
				}
			} while (cki.Key != ConsoleKey.Enter);

			Console.WriteLine();
			return result.ToString();
		}

// ReSharper disable RedundantAssignment
		protected virtual void InputProxyCredentials(IProxyCompatible proxyProtocol, ref bool retry)
// ReSharper restore RedundantAssignment
		{
			Log(Strings.PROXY_AUTH_REQUESTED, ESeverity.INFO);
			var username = InputString(Strings.INPUT_PROXY_USERNAME, false);
			var password = InputString(Strings.INPUT_PROXY_PASSWORD, true);
			var domain = InputString(Strings.INPUT_PROXY_DOMAIN, false);

			proxyProtocol.Proxy.Credentials = new NetworkCredential(username, password, domain);
			retry = true;
		}

		public void StartClient()
		{
			Log(string.Format(Strings.CLIENT_TITLE, GetType().Assembly.GetName().Version.ToString(3)), ESeverity.INFO);
			Log(FrameworkVersion(), ESeverity.INFO);

			Protocol.ConfigureClient();
			ConfigureProxy(Protocol);

			IMinimalResponse response = null;
			bool retry;

			do
			{
				retry = false;
				try
				{
					Tunnel = Protocol.GetTunnel();
					response = Tunnel.Version();
				}
				catch (WebException ex)
				{
					if (ex.Response is HttpWebResponse webResponse && Protocol is IProxyCompatible proxyProtocol)
					{
						if (webResponse.StatusCode != HttpStatusCode.ProxyAuthenticationRequired)
							continue;

						retry = true;
						InputProxyCredentials(proxyProtocol, ref retry);
					}
					else
					{
						throw;
					}
				}
			} while (retry);

			if (response != null && Tunnel != null)
			{
				if (response.Message.IndexOf(GetType().Assembly.GetName().Version.ToString(3), StringComparison.Ordinal) < 0)
				{
					Log(Strings.VERSION_MISMATCH, ESeverity.WARN);
				}

				Log(response.Message, ESeverity.INFO);
				if (!response.Success)
					return;

				var loginResponse = Tunnel.Login(new LoginRequest(ClientConfig.ServiceUserName, ClientConfig.ServicePassword));
				if (loginResponse.Success)
				{
					_sid = loginResponse.Sid;
					Log(loginResponse.Message, ESeverity.INFO);

					if (Args != null && Args.Length != 0)
						return;

					InitializeForwards(Tunnel, loginResponse.Sid);
					InitializeSocks(Tunnel, loginResponse.Sid);
				}
				else
				{
					throw new Exception(loginResponse.Message);
				}
			}
			else
			{
				throw new Exception(Strings.CONNECTION_FAILED);
			}
		}

		public void StopClient()
		{
			DisposeServers();

			if (Tunnel != null && _sid.HasValue)
			{
				try
				{
					var response = Tunnel.Logout(new SessionContextRequest(_sid.Value));
					Log(response.Message, ESeverity.INFO);
				}
// ReSharper disable EmptyGeneralCatchClause
				catch
// ReSharper restore EmptyGeneralCatchClause
				{
					// ignore
				}
			}

			Tunnel = null;
			_sid = null;

			Protocol.UnConfigureClient();
		}

		public override void LoadConfiguration(string[] args)
		{
			base.LoadConfiguration(args);
			ClientConfig = new ClientConfig(Configuration, ConsoleLogger, FileLogger);
		}

		public override void UnLoadConfiguration()
		{
			base.UnLoadConfiguration();
			ClientConfig = null;
		}

		protected override void SetCulture(string name)
		{
			base.SetCulture(name);
			if (!string.IsNullOrEmpty(name))
				Strings.Culture = new CultureInfo(name);
		}

		protected virtual void Run(string[] args)
		{
			try
			{
#pragma warning disable 612,618
				ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
#pragma warning restore 612,618

				LoadConfiguration(args);

				StartClient();

				if (args.Length > 0 && _sid.HasValue)
				{
					if (!Command.FindAndExecute(args, this, Tunnel, _sid.Value))
						new HelpCommand().Execute(args, this, null, 0);
				}
				else
				{
					Log(Strings.CLIENT_STARTED, ESeverity.INFO);
					Console.ReadLine();
				}

				StopClient();

				UnLoadConfiguration();
			}
			catch (Exception ex)
			{
				if (GlobalLogger != null)
				{
					Log(ex.Message, ESeverity.ERROR);
					Log(ex.ToString(), ESeverity.DEBUG);
				}
				else
				{
					Console.WriteLine(ex.Message);
				}
			}
		}
	}
}
