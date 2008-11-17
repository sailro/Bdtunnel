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
using Bdt.Shared.Logs;
using Bdt.Shared.Configuration;
using Bdt.Tests.Logs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bdt.Shared.Protocol;
#endregion

namespace Bdt.Tests.Runtime
{
    class TestServer : BdtServer
    {

        #region " Attributs "
        private TestContext m_context;
        private SharedConfig m_servercfg;
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Initialisation des loggers
        /// </summary>
        /// <returns>un MultiLogger lié à une source fichier et console</returns>
        /// -----------------------------------------------------------------------------
        public override BaseLogger CreateLoggers()
        {
            return new TestContextLogger(m_context); ;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Chargement des données de configuration
        /// </summary>
        /// <param name="args">Arguments de la ligne de commande</param>
        /// -----------------------------------------------------------------------------
        public override void LoadConfiguration(string[] args)
        {
            m_args = args;

            LoggedObject.GlobalLogger = CreateLoggers();
            Log(Bdt.Shared.Resources.Strings.LOADING_CONFIGURATION, ESeverity.DEBUG);
            m_protocol = GenericProtocol.GetInstance(m_servercfg);
            SetCulture(m_servercfg.ServiceCulture);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="context">le contexte du test</param>
        /// <param name="servercfg">la configuration serveur</param>
        /// -----------------------------------------------------------------------------
        public TestServer(TestContext context, SharedConfig servercfg)
        {
            m_context = context;
            m_servercfg = servercfg;
        }
        #endregion

    }
}
