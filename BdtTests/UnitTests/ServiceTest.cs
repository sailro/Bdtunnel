// -----------------------------------------------------------------------------
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
#endregion

namespace Bdt.Tests.UnitTests
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Tests sur les méthodes du service
    /// </summary>
    /// -----------------------------------------------------------------------------
    [TestClass]
    public class ServiceTest : BaseTest 
    {

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Test le login/logout
        /// </summary>
        /// -----------------------------------------------------------------------------
        [TestMethod]
        public void TestLoginLogout()
        {
            SharedConfig servercfg = new SharedConfig(null);
            servercfg.ServiceName = "BdtTestServer";
            servercfg.ServicePort = 9090;
            servercfg.ServiceProtocol = "Bdt.Shared.Protocol.HttpBinaryRemoting";

            BdtServer server = new TestServer(TestContext, servercfg);

            server.LoadConfiguration(new String[] { });

            Tunnel.Configuration = server.Configuration;
            Tunnel.Logger = LoggedObject.GlobalLogger;
            server.Protocol.ConfigureServer(typeof(Tunnel));

            Tunnel.DisableChecking();

            server.UnLoadConfiguration();

        }
        #endregion
    }
}
