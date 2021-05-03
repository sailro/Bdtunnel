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
using System.Collections.Generic;
using System.Linq;
using Bdt.Server.Runtime;
using Bdt.Server.Service;
using Bdt.Shared.Logs;
using Bdt.Tests.Configuration;
using Bdt.Tests.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bdt.Client.Runtime;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using Bdt.Shared.Protocol;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using Bdt.Shared.Request;
using Bdt.Tests.Sockets;
using Bdt.Client.Sockets;
using System.Net;

namespace Bdt.Tests.UnitTests
{
	[TestClass]
	public class ProtocolsTest : BaseTest
	{
		private const int EchoOffset = 10;
		private const int GatewayOffset = EchoOffset + 10; // Must have GATEWAY_OFFSET > ECHO_OFFSET

		private static readonly List<TcpServer> Servers = new();

		private void Initialize<T>(int port, out BdtServer server, out BdtClient client, out EchoServer echo, out GatewayServer gw) where T : GenericProtocol
		{
			Assert.AreEqual(ChannelServices.RegisteredChannels.Length, 0);
			var config = new TestConfigPackage(typeof(T).FullName, port);

			server = new TestServer(TestContext, config);
			client = new TestClient(TestContext, config);

			var args = new string[] { };
			server.LoadConfiguration(args);
			client.LoadConfiguration(args);

			Tunnel.Configuration = server.Configuration;
			Tunnel.Logger = LoggedObject.GlobalLogger;
			server.Protocol.ConfigureServer(typeof(Tunnel));

			client.StartClient();

			Assert.AreEqual(ChannelServices.RegisteredChannels.Length, 2);

			echo = new EchoServer(port + EchoOffset, false);
			Servers.Add(echo);

			gw = new GatewayServer(port + GatewayOffset, false, port + EchoOffset, "localhost", client.Tunnel, client.Sid);
			Servers.Add(gw);
		}

		private static void Finalize(ref BdtServer server, ref BdtClient client, ref EchoServer echo, ref GatewayServer gw)
		{
			Assert.AreEqual(ChannelServices.RegisteredChannels.Length, 2);
			client.StopClient();

			Tunnel.DisableChecking();
			server.Protocol.UnConfigureServer();

			server.UnLoadConfiguration();
			client.UnLoadConfiguration();

			server = null;
			client = null;
			Assert.AreEqual(ChannelServices.RegisteredChannels.Length, 0);

			echo.CloseServer();
			Servers.Remove(echo);
			echo = null;

			gw.CloseServer();
			Servers.Remove(gw);
			gw = null;
		}

		private void TestProtocol<TP, TC>(int port) where TP : GenericProtocol where TC : IChannel
		{
			Initialize<TP>(port, out var server, out var client, out var echo, out var gw);
			foreach (var channel in ChannelServices.RegisteredChannels)
			{
				Assert.IsInstanceOfType(channel, typeof(TC));
				Assert.IsTrue(channel.ChannelName == client.ClientConfig.ServiceName || channel.ChannelName == client.ClientConfig.ServiceName + ".Client");
			}

			var conr = client.Tunnel.Connect(new ConnectRequest(client.Sid, "localhost", port + GatewayOffset));
			Assert.IsTrue(conr.Connected);
			Assert.IsTrue(conr.Success);

			var totalread = 0;

			for (var datalength = 0; datalength < 4096; datalength = (datalength == 0) ? 1 : datalength * 2)
			{
				var buffer = new byte[datalength];
				var outbuffer = new byte[datalength];
				var readcount = 0;

				var rnd = new Random();
				rnd.NextBytes(buffer);

				var wrir = client.Tunnel.Write(new WriteRequest(client.Sid, conr.Cid, buffer));
				Assert.IsTrue(wrir.Connected);
				Assert.IsTrue(wrir.Success);
				while (readcount < datalength)
				{
					var rear = client.Tunnel.Read(new ConnectionContextRequest(client.Sid, conr.Cid));
					Assert.IsTrue(rear.Connected);
					Assert.IsTrue(rear.Success);

					if (!rear.DataAvailable)
						continue;

					Array.Copy(rear.Data, 0, outbuffer, readcount, rear.Data.Length);
					readcount += rear.Data.Length;
				}

				Assert.AreEqual(datalength, readcount);

				for (var i = 0; i < datalength; i++)
					Assert.AreEqual(buffer[i], outbuffer[i], "Offset " + i);

				totalread += readcount;
			}

			// Test Monitor method
			var monr = client.Tunnel.Monitor(new SessionContextRequest(client.Sid));

			Assert.IsTrue(monr.Success);
			Assert.AreEqual(1, monr.Sessions.Length);
			Assert.IsTrue(monr.Sessions[0].Admin);
			Assert.AreEqual(client.Sid.ToString("x"), monr.Sessions[0].Sid);
			Assert.AreEqual(2, monr.Sessions[0].Connections.Length);

			var connections = (from c in monr.Sessions[0].Connections orderby c.Port select c).ToArray();
			var hostname = Dns.GetHostEntry(connections[0].Address).HostName;

			Assert.AreEqual(hostname, connections[0].Host);
			Assert.AreEqual("127.0.0.1", connections[0].Address);
			Assert.AreEqual(port + EchoOffset, connections[0].Port);
			Assert.AreEqual(totalread, connections[0].ReadCount);
			Assert.AreEqual(totalread, connections[0].WriteCount);

			Assert.AreEqual(hostname, connections[1].Host);
			Assert.AreEqual("127.0.0.1", connections[1].Address);
			Assert.AreEqual(port + GatewayOffset, connections[1].Port);
			Assert.AreEqual(totalread, connections[1].ReadCount);
			Assert.AreEqual(totalread, connections[1].WriteCount);
			Assert.AreEqual(conr.Cid.ToString("x"), connections[1].Cid);

			// Test Disconnect method
			var disr = client.Tunnel.Disconnect(new ConnectionContextRequest(client.Sid, conr.Cid));
			Assert.IsFalse(disr.Connected);
			Assert.IsTrue(disr.Success);

			TestKill(client, port);
			TestBadValues(client, port);

			Finalize(ref server, ref client, ref echo, ref gw);
		}

		private static void TestBadValues(BdtClient client, int port)
		{
			// Test Login method with bad values - access denied
			var bloginr = client.Tunnel.Login(new LoginRequest("foo", "foo"));
			Assert.IsFalse(bloginr.Success);
			Assert.IsTrue(bloginr.Message.Contains(string.Format(Server.Resources.Strings.ACCESS_DENIED, "foo")), "Deny access to non-user");

			// Test Login method with bad values - access denied (disabled)
			bloginr = client.Tunnel.Login(new LoginRequest(TestConfigPackage.UserDisabledLogin, TestConfigPackage.UserDisabledPassword));
			Assert.IsFalse(bloginr.Success);
			Assert.IsTrue(bloginr.Message.Contains(string.Format(Server.Resources.Strings.ACCESS_DENIED, TestConfigPackage.UserDisabledLogin)),
				"Deny access to disabled-user");

			// Test Login method with bad values - access denied (bad password)
			bloginr = client.Tunnel.Login(new LoginRequest(TestConfigPackage.UserLambdaLogin, "foo"));
			Assert.IsFalse(bloginr.Success);
			Assert.IsTrue(bloginr.Message.Contains(string.Format(Server.Resources.Strings.ACCESS_DENIED_BAD_PASSWORD, TestConfigPackage.UserLambdaLogin)),
				"Deny access - bad password");

			// Test logout method with bad values (bad sid)
			var blogoutr = client.Tunnel.Logout(new SessionContextRequest(-1));
			Assert.IsFalse(bloginr.Success);
			Assert.IsTrue(blogoutr.Message.Contains(Server.Resources.Strings.SID_NOT_FOUND), "Sid Logout");

			// Test monitor method with bad values (bad sid)
			var bmonr = client.Tunnel.Monitor(new SessionContextRequest(-1));
			Assert.IsFalse(bmonr.Success);
			Assert.IsTrue(bmonr.Message.Contains(Server.Resources.Strings.SID_NOT_FOUND), "Sid Monitor");
			Assert.IsNull(bmonr.Sessions);

			// Create valid session
			var lambdaloginr = client.Tunnel.Login(new LoginRequest(TestConfigPackage.UserLambdaLogin, TestConfigPackage.UserLambdaPassword));
			Assert.IsTrue(lambdaloginr.Success);

			// Test monitor method with bad values (admin privileges required)
			bmonr = client.Tunnel.Monitor(new SessionContextRequest(lambdaloginr.Sid));
			Assert.IsFalse(bmonr.Success);
			Assert.IsTrue(bmonr.Message.Contains(Server.Resources.Strings.ADMIN_REQUIRED), "Deny Monitor to non-admin user");
			Assert.IsNull(bmonr.Sessions);

			// Test killsession method with bad values (admin privileges required)
			var killsr = client.Tunnel.KillSession(new KillSessionRequest(lambdaloginr.Sid, lambdaloginr.Sid));
			Assert.IsFalse(killsr.Success);
			Assert.IsTrue(killsr.Message.Contains(Server.Resources.Strings.ADMIN_REQUIRED), "Deny KillSession to non-admin user");

			// Test killsession method with bad values (bad sid)
			killsr = client.Tunnel.KillSession(new KillSessionRequest(-1, -1));
			Assert.IsFalse(killsr.Success);
			Assert.IsTrue(killsr.Message.Contains(Server.Resources.Strings.SID_NOT_FOUND), "Sid KillSession");

			// Test killconnection method with bad values (bad cid)
			var killcr = client.Tunnel.KillConnection(new KillConnectionRequest(lambdaloginr.Sid, lambdaloginr.Sid, -1));
			Assert.IsFalse(killcr.Success);
			Assert.IsTrue(killcr.Message.Contains(Server.Resources.Strings.CID_NOT_FOUND), "Cid KillConnection");

			// Test killconnection method with bad values (bad sid)
			killcr = client.Tunnel.KillConnection(new KillConnectionRequest(-1, -1, -1));
			Assert.IsFalse(killcr.Success);
			Assert.IsTrue(killcr.Message.Contains(Server.Resources.Strings.SID_NOT_FOUND), "Sid KillConnection");

			// Test connect method with bad values (unknown host)
			var conr = client.Tunnel.Connect(new ConnectRequest(lambdaloginr.Sid, "host.unknown", port + GatewayOffset));
			Assert.IsFalse(conr.Success);
			Assert.IsFalse(conr.Connected);

			// Test connect method with bad values (port not listening)
			conr = client.Tunnel.Connect(new ConnectRequest(lambdaloginr.Sid, "localhost", 1));
			Assert.IsFalse(conr.Success);
			Assert.IsFalse(conr.Connected);

			// Create valid connection
			conr = client.Tunnel.Connect(new ConnectRequest(lambdaloginr.Sid, "localhost", port + GatewayOffset));
			Assert.IsTrue(conr.Success);
			Assert.IsTrue(conr.Connected);

			// Test killconnection method with bad values (admin privileges required)
			killcr = client.Tunnel.KillConnection(new KillConnectionRequest(lambdaloginr.Sid, lambdaloginr.Sid, conr.Cid));
			Assert.IsFalse(killcr.Success);
			Assert.IsTrue(killcr.Message.Contains(Server.Resources.Strings.ADMIN_REQUIRED), "Deny KillConnection to non-admin user");

			// Dispose valid connection
			var disr = client.Tunnel.Disconnect(new ConnectionContextRequest(lambdaloginr.Sid, conr.Cid));
			Assert.IsTrue(disr.Success);
			Assert.IsFalse(disr.Connected);

			// Dispose valid session
			var lambdalogoutr = client.Tunnel.Logout(new SessionContextRequest(lambdaloginr.Sid));
			Assert.IsTrue(lambdalogoutr.Success);
		}

		private static void TestKill(BdtClient client, int port)
		{
			// Create valid session
			var lambdaloginr = client.Tunnel.Login(new LoginRequest(TestConfigPackage.UserLambdaLogin, TestConfigPackage.UserLambdaPassword));
			Assert.IsTrue(lambdaloginr.Success);

			// Create valid connection
			var conr = client.Tunnel.Connect(new ConnectRequest(lambdaloginr.Sid, "localhost", port + GatewayOffset));
			Assert.IsTrue(conr.Success);
			Assert.IsTrue(conr.Connected);

			// Kill connection
			var killcr = client.Tunnel.KillConnection(new KillConnectionRequest(lambdaloginr.Sid, client.Sid, conr.Cid));
			Assert.IsTrue(killcr.Success);
			Assert.IsFalse(killcr.Connected);
			Assert.IsFalse(killcr.DataAvailable);

			// Monitor connection
			var monr = client.Tunnel.Monitor(new SessionContextRequest(client.Sid));
			Assert.IsTrue(monr.Success);
			Assert.AreEqual(0, (from s in monr.Sessions where s.Sid == lambdaloginr.Sid.ToString("x") select s).First().Connections.Length, "Connection is not killed");

			// Kill session
			var killsr = client.Tunnel.KillSession(new KillSessionRequest(lambdaloginr.Sid, client.Sid));
			Assert.IsTrue(killsr.Success);

			// Monitor session
			monr = client.Tunnel.Monitor(new SessionContextRequest(client.Sid));
			Assert.IsTrue(monr.Success);
			Assert.AreEqual(0, (from s in monr.Sessions where s.Sid == lambdaloginr.Sid.ToString("x") select s).Count(), "Session is not killed");
		}

		[TestCleanup]
		public void TestCleanup()
		{
			foreach (var channel in ChannelServices.RegisteredChannels)
			{
				ChannelServices.UnregisterChannel(channel);
				if (channel is IChannelReceiver receiver)
					receiver.StopListening(null);
			}

			foreach (var server in Servers)
			{
				try
				{
					server.CloseServer();
				}
// ReSharper disable EmptyGeneralCatchClause
				catch (Exception)
				{
				}
// ReSharper restore EmptyGeneralCatchClause
			}

			Servers.Clear();
		}

		[TestMethod]
		public void TestIpcRemoting()
		{
			TestProtocol<IpcRemoting, IpcChannel>(9079);
		}

		[TestMethod]
		public void TestTcpRemoting()
		{
			TestProtocol<TcpRemoting, TcpChannel>(9080);
		}

		[TestMethod]
		public void TestHttpSoapRemoting()
		{
			TestProtocol<HttpSoapRemoting, HttpChannel>(9081);
		}

		[TestMethod]
		public void TestHttpBinaryRemoting()
		{
			TestProtocol<HttpBinaryRemoting, HttpChannel>(9082);
		}
	}
}
