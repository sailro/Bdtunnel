// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using Bdt.Client.Runtime;
using Bdt.Shared.Configuration;
using Bdt.Shared.Logs;
using Bdt.Shared.Protocol;
using Bdt.Tests.Logs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bdt.Client.Configuration;
using Bdt.Shared.Resources;
#endregion

namespace Bdt.Tests.Runtime
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Un client de test
    /// </summary>
    /// -----------------------------------------------------------------------------
    class TestClient : BdtClient
    {

        #region " Attributs "
        private TestContext m_context;
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
            Log(Strings.LOADING_CONFIGURATION, ESeverity.DEBUG);
            m_protocol = GenericProtocol.GetInstance(m_clientConfig);
            SetCulture(m_clientConfig.ServiceCulture);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="context">le contexte du test</param>
        /// <param name="config">la configuration client</param>
        /// -----------------------------------------------------------------------------
        public TestClient(TestContext context, ConfigPackage config)
        {
            m_context = context;
            m_clientConfig = new ClientConfig(config, null, null);
            m_config = config;
        }
        #endregion

    }
}
