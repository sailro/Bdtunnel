﻿// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bdt.Server.Runtime;
using Bdt.Server.Service;
using Bdt.Shared.Logs;
using Bdt.Tests.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bdt.Shared.Configuration;
using Bdt.Client.Configuration;
using Bdt.Client.Runtime;
using Bdt.Tests.Model;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using Bdt.Shared.Protocol;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using Bdt.Shared.Service;
using Bdt.Shared.Request;
using Bdt.Shared.Response;
using Bdt.Tests.Sockets;
using Bdt.Client.Sockets;
using System.Net;
#endregion

namespace Bdt.Tests.UnitTests
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Tests sur les méthodes du service
    /// </summary>
    /// -----------------------------------------------------------------------------
    [TestClass]
    public class ProtocolsTest : BaseTest 
    {
        #region " Constantes "
        const int ECHO_OFFSET = 10;
        const int GATEWAY_OFFSET = ECHO_OFFSET + 10; // Must have GATEWAY_OFFSET > ECHO_OFFSET
        #endregion

        #region " Attributs "
        static List<TcpServer> servers = new List<TcpServer>();
        #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Initialise les différents serveurs pour le test
        /// </summary>
        /// <typeparam name="T">le protocole à utiliser</typeparam>
        /// <param name="port">le port de base pour le remoting</param>
        /// <param name="server">le serveur (tunnel)</param>
        /// <param name="client">le client (tunnel)</param>
        /// <param name="echo">le serveur d'echo</param>
        /// <param name="gw">la passerelle</param>
        /// -----------------------------------------------------------------------------
        public void Initialize<T>(int port, out BdtServer server, out BdtClient client, out EchoServer echo, out GatewayServer gw) where T : GenericProtocol
        {
            Assert.AreEqual(ChannelServices.RegisteredChannels.Length, 0);
            TestConfigPackage config = new TestConfigPackage(typeof(T).FullName, port);

            server = new TestServer(TestContext, config);
            client = new TestClient(TestContext, config);

            String[] args = new String[] { };
            server.LoadConfiguration(args);
            client.LoadConfiguration(args);

            Tunnel.Configuration = server.Configuration;
            Tunnel.Logger = LoggedObject.GlobalLogger;
            server.Protocol.ConfigureServer(typeof(Tunnel));

            client.StartClient();

            Assert.AreEqual(ChannelServices.RegisteredChannels.Length, 2);

            echo = new EchoServer(port + ECHO_OFFSET, false);
            servers.Add(echo);

            gw = new GatewayServer(port + GATEWAY_OFFSET, false, port + ECHO_OFFSET, "localhost", client.Tunnel, client.Sid);
            servers.Add(gw);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Destruction des entités de test
        /// </summary>
        /// <param name="server">le serveur (tunnel)</param>
        /// <param name="client">le client (tunnel)</param>
        /// <param name="echo">le serveur d'echo</param>
        /// <param name="gw">la passerelle</param>
        /// -----------------------------------------------------------------------------
        public void Finalize(ref BdtServer server, ref BdtClient client, ref EchoServer echo, ref GatewayServer gw)
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
            servers.Remove(echo);
            echo = null;

            gw.CloseServer();
            servers.Remove(gw);
            gw = null;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Test du protocole
        /// </summary>
        /// <typeparam name="TP">Le type de protocole</typeparam>
        /// <typeparam name="TC">Le type de canal</typeparam>
        /// <param name="port">le port de base</param>
        /// -----------------------------------------------------------------------------
        public void TestProtocol<TP, TC>(int port)
            where TP : GenericProtocol
            where TC : IChannel 
        {
            BdtServer server;
            BdtClient client;
            EchoServer echo;
            GatewayServer gw;

            Initialize<TP>(port, out server, out client, out echo, out gw);
            foreach (IChannel channel in ChannelServices.RegisteredChannels)
            {
                Assert.IsInstanceOfType(channel, typeof(TC));
                Assert.IsTrue(channel.ChannelName == client.ClientConfig.ServiceName || channel.ChannelName == client.ClientConfig.ServiceName + ".Client");
            }

            ConnectResponse conr = client.Tunnel.Connect(new ConnectRequest(client.Sid, "localhost", port + GATEWAY_OFFSET));
            Assert.IsTrue(conr.Connected);
            Assert.IsTrue(conr.Success);

            int totalread = 0;

            for (int datalength = 0; datalength < 4096; datalength = (datalength == 0) ? 1 : datalength * 2)
            {
                byte[] buffer = new byte[datalength];
                byte[] outbuffer = new byte[datalength];
                int readcount = 0;

                Random rnd = new Random();
                rnd.NextBytes(buffer);

                ConnectionContextResponse wrir = client.Tunnel.Write(new WriteRequest(client.Sid, conr.Cid, buffer));
                Assert.IsTrue(wrir.Connected);
                Assert.IsTrue(wrir.Success);
                while (readcount < datalength)
                {
                    ReadResponse rear = client.Tunnel.Read(new ConnectionContextRequest(client.Sid, conr.Cid));
                    Assert.IsTrue(rear.Connected);
                    Assert.IsTrue(rear.Success);
                    if (rear.DataAvailable)
                    {
                        Array.Copy(rear.Data, 0, outbuffer, readcount, rear.Data.Length);
                        readcount += rear.Data.Length;
                    }
                }
                Assert.AreEqual(datalength, readcount);
                for (int i = 0; i < datalength; i++)
                {
                    Assert.AreEqual(buffer[i], outbuffer[i], "Offset "+i);
                }
                totalread += readcount;
            }

            // Test Monitor method
            MonitorResponse monr = client.Tunnel.Monitor(new SessionContextRequest(client.Sid));

            Assert.IsTrue(monr.Success);
            Assert.AreEqual(1, monr.Sessions.Length);
            Assert.IsTrue(monr.Sessions[0].Admin);
            Assert.AreEqual(client.Sid.ToString("x"), monr.Sessions[0].Sid);
            Assert.AreEqual(2, monr.Sessions[0].Connections.Length);

            var connections = (from c in monr.Sessions[0].Connections orderby c.Port select c).ToArray();
            string hostname = Dns.GetHostEntry(connections[0].Address).HostName;

            Assert.AreEqual(hostname, connections[0].Host);
            Assert.AreEqual("127.0.0.1", connections[0].Address);
            Assert.AreEqual(port + ECHO_OFFSET, connections[0].Port);
            Assert.AreEqual(totalread, connections[0].ReadCount);
            Assert.AreEqual(totalread, connections[0].WriteCount);

            Assert.AreEqual(hostname, connections[1].Host);
            Assert.AreEqual("127.0.0.1", connections[1].Address);
            Assert.AreEqual(port + GATEWAY_OFFSET, connections[1].Port);
            Assert.AreEqual(totalread, connections[1].ReadCount);
            Assert.AreEqual(totalread, connections[1].WriteCount);
            Assert.AreEqual(conr.Cid.ToString("x"), connections[1].Cid);

            // Test Disconnect method
            ConnectionContextResponse disr = client.Tunnel.Disconnect(new ConnectionContextRequest(client.Sid, conr.Cid));
            Assert.IsFalse(disr.Connected);
            Assert.IsTrue(disr.Success);

            TestKill(client, port);
            TestBadValues(client, port);

            Finalize(ref server, ref client, ref echo, ref gw);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Test de valeurs incorrectes
        /// </summary>
        /// <param name="client">Le client du tunnel</param>
        /// <param name="port">Le port de base</param>
        /// -----------------------------------------------------------------------------
        public void TestBadValues(BdtClient client, int port)
        {
            // Test Login method with bad values - access denied
            LoginResponse bloginr = client.Tunnel.Login(new LoginRequest("foo", "foo"));
            Assert.IsFalse(bloginr.Success);
            Assert.IsTrue(bloginr.Message.Contains(String.Format(Bdt.Server.Resources.Strings.ACCESS_DENIED, "foo")), "Deny access to non-user");

            // Test Login method with bad values - access denied (disabled)
            bloginr = client.Tunnel.Login(new LoginRequest(TestConfigPackage.USER_DISABLED_LOGIN, TestConfigPackage.USER_DISABLED_PASSWORD));
            Assert.IsFalse(bloginr.Success);
            Assert.IsTrue(bloginr.Message.Contains(String.Format(Bdt.Server.Resources.Strings.ACCESS_DENIED, TestConfigPackage.USER_DISABLED_LOGIN)), "Deny access to disabled-user");

            // Test Login method with bad values - access denied (bad password)
            bloginr = client.Tunnel.Login(new LoginRequest(TestConfigPackage.USER_LAMBDA_LOGIN, "foo"));
            Assert.IsFalse(bloginr.Success);
            Assert.IsTrue(bloginr.Message.Contains(String.Format(Bdt.Server.Resources.Strings.ACCESS_DENIED_BAD_PASSWORD, TestConfigPackage.USER_LAMBDA_LOGIN)), "Deny access - bad password");

            // Test logout method with bad values (bad sid)
            MinimalResponse blogoutr = client.Tunnel.Logout(new SessionContextRequest(-1));
            Assert.IsFalse(bloginr.Success);
            Assert.IsTrue(blogoutr.Message.Contains(Bdt.Server.Resources.Strings.SID_NOT_FOUND), "Sid Logout");

            // Test monitor method with bad values (bad sid)
            MonitorResponse bmonr = client.Tunnel.Monitor(new SessionContextRequest(-1));
            Assert.IsFalse(bmonr.Success);
            Assert.IsTrue(bmonr.Message.Contains(Bdt.Server.Resources.Strings.SID_NOT_FOUND), "Sid Monitor");
            Assert.IsNull(bmonr.Sessions);

            // Create valid session
            LoginResponse lambdaloginr = client.Tunnel.Login(new LoginRequest(TestConfigPackage.USER_LAMBDA_LOGIN, TestConfigPackage.USER_LAMBDA_PASSWORD));
            Assert.IsTrue(lambdaloginr.Success);

            // Test monitor method with bad values (admin privileges required)
            bmonr = client.Tunnel.Monitor(new SessionContextRequest(lambdaloginr.Sid));
            Assert.IsFalse(bmonr.Success);
            Assert.IsTrue(bmonr.Message.Contains(Bdt.Server.Resources.Strings.ADMIN_REQUIRED), "Deny Monitor to non-admin user");
            Assert.IsNull(bmonr.Sessions);

            // Test killsession method with bad values (admin privileges required)
            MinimalResponse killsr = client.Tunnel.KillSession(new KillSessionRequest(lambdaloginr.Sid, lambdaloginr.Sid));
            Assert.IsFalse(killsr.Success);
            Assert.IsTrue(killsr.Message.Contains(Bdt.Server.Resources.Strings.ADMIN_REQUIRED), "Deny KillSession to non-admin user");

            // Test killsession method with bad values (bad sid)
            killsr = client.Tunnel.KillSession(new KillSessionRequest(-1, -1));
            Assert.IsFalse(killsr.Success);
            Assert.IsTrue(killsr.Message.Contains(Bdt.Server.Resources.Strings.SID_NOT_FOUND), "Sid KillSession");

            // Test killconnection method with bad values (bad cid)
            ConnectionContextResponse killcr = client.Tunnel.KillConnection(new KillConnectionRequest(lambdaloginr.Sid, lambdaloginr.Sid, -1));
            Assert.IsFalse(killcr.Success);
            Assert.IsTrue(killcr.Message.Contains(Bdt.Server.Resources.Strings.CID_NOT_FOUND), "Cid KillConnection");

            // Test killconnection method with bad values (bad sid)
            killcr = client.Tunnel.KillConnection(new KillConnectionRequest(-1, -1, -1));
            Assert.IsFalse(killcr.Success);
            Assert.IsTrue(killcr.Message.Contains(Bdt.Server.Resources.Strings.SID_NOT_FOUND), "Sid KillConnection");

            // Test connect method with bad values (unknown host)
            ConnectResponse conr = client.Tunnel.Connect(new ConnectRequest(lambdaloginr.Sid, "host.unknown", port + GATEWAY_OFFSET));
            Assert.IsFalse(conr.Success);
            Assert.IsFalse(conr.Connected);

            // Test connect method with bad values (port not listening)
            conr = client.Tunnel.Connect(new ConnectRequest(lambdaloginr.Sid, "localhost", 1));
            Assert.IsFalse(conr.Success);
            Assert.IsFalse(conr.Connected);

            // Create valid connection
            conr = client.Tunnel.Connect(new ConnectRequest(lambdaloginr.Sid, "localhost", port + GATEWAY_OFFSET));
            Assert.IsTrue(conr.Success);
            Assert.IsTrue(conr.Connected);

            // Test killconnection method with bad values (admin privileges required)
            killcr = client.Tunnel.KillConnection(new KillConnectionRequest(lambdaloginr.Sid, lambdaloginr.Sid, conr.Cid));
            Assert.IsFalse(killcr.Success);
            Assert.IsTrue(killcr.Message.Contains(Bdt.Server.Resources.Strings.ADMIN_REQUIRED), "Deny KillConnection to non-admin user");

            // Dispose valid connection
            ConnectionContextResponse disr = client.Tunnel.Disconnect(new ConnectionContextRequest(lambdaloginr.Sid, conr.Cid));
            Assert.IsTrue(disr.Success);
            Assert.IsFalse(disr.Connected);

            // Dispose valid session
            MinimalResponse lambdalogoutr = client.Tunnel.Logout(new SessionContextRequest(lambdaloginr.Sid));
            Assert.IsTrue(lambdalogoutr.Success);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Test de suppression de sessions et connexions
        /// </summary>
        /// <param name="client">Le client du tunnel</param>
        /// <param name="port">Le port de base</param>
        /// -----------------------------------------------------------------------------
        public void TestKill(BdtClient client, int port)
        {
            // Create valid session
            LoginResponse lambdaloginr = client.Tunnel.Login(new LoginRequest(TestConfigPackage.USER_LAMBDA_LOGIN, TestConfigPackage.USER_LAMBDA_PASSWORD));
            Assert.IsTrue(lambdaloginr.Success);

            // Create valid connection
            ConnectResponse conr = client.Tunnel.Connect(new ConnectRequest(lambdaloginr.Sid, "localhost", port + GATEWAY_OFFSET));
            Assert.IsTrue(conr.Success);
            Assert.IsTrue(conr.Connected);

            // Kill connection
            ConnectionContextResponse killcr = client.Tunnel.KillConnection(new KillConnectionRequest(lambdaloginr.Sid, client.Sid, conr.Cid));
            Assert.IsTrue(killcr.Success);
            Assert.IsFalse(killcr.Connected);
            Assert.IsFalse(killcr.DataAvailable);

            // Monitor connection
            MonitorResponse monr = client.Tunnel.Monitor(new SessionContextRequest(client.Sid));
            Assert.IsTrue(monr.Success);
            Assert.AreEqual(0, (from s in monr.Sessions where s.Sid == lambdaloginr.Sid.ToString("x") select s).First().Connections.Length, "Connection is not killed");

            // Kill session
            MinimalResponse killsr = client.Tunnel.KillSession(new KillSessionRequest(lambdaloginr.Sid, client.Sid));
            Assert.IsTrue(killsr.Success);

            // Monitor session
            monr = client.Tunnel.Monitor(new SessionContextRequest(client.Sid));
            Assert.IsTrue(monr.Success);
            Assert.AreEqual(0, (from s in monr.Sessions where s.Sid == lambdaloginr.Sid.ToString("x") select s).Count(), "Session is not killed");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Nettoyage pour les tests ayant échoués
        /// </summary>
        /// -----------------------------------------------------------------------------
        [TestCleanup()]
        public void TestCleanup() {
            foreach (IChannel channel in ChannelServices.RegisteredChannels)
            {
                ChannelServices.UnregisterChannel(channel);
                if (channel is IChannelReceiver)
                {
                    ((IChannelReceiver)channel).StopListening(null);
                }
            }
            foreach (TcpServer server in servers)
            {
                try
                {
                    server.CloseServer();
                }
                catch (Exception) { }
            }
            servers.Clear();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Test le protocole IPC
        /// </summary>
        /// -----------------------------------------------------------------------------
        [TestMethod]
        public void TestIpcRemoting()
        {
            TestProtocol<IpcRemoting, IpcChannel>(9079);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Test le protocole TCP
        /// </summary>
        /// -----------------------------------------------------------------------------
        [TestMethod]
        public void TestTcpRemoting()
        {
            TestProtocol<TcpRemoting, TcpChannel>(9080);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Test le protocole HTTP/SOAP
        /// </summary>
        /// -----------------------------------------------------------------------------
        [TestMethod]
        public void TestHttpSoapRemoting()
        {
            TestProtocol<HttpSoapRemoting, HttpChannel>(9081);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Test le protocole HTTP/BINARY
        /// </summary>
        /// -----------------------------------------------------------------------------
        [TestMethod]
        public void TestHttpBinaryRemoting()
        {
            TestProtocol<HttpBinaryRemoting, HttpChannel>(9082);
        }
        #endregion

    }
}