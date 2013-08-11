/* BoutDuTunnel Copyright (c) 2007-2010 Sebastien LEBRETON

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
        private readonly TestContext _context;
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Initialisation des loggers
        /// </summary>
        /// <returns>un MultiLogger lié à une source fichier et console</returns>
        /// -----------------------------------------------------------------------------
        protected override BaseLogger CreateLoggers()
        {
            return new TestContextLogger(_context);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Chargement des données de configuration
        /// </summary>
        /// <param name="args">Arguments de la ligne de commande</param>
        /// -----------------------------------------------------------------------------
        public override void LoadConfiguration(string[] args)
        {
            Args = args;

            GlobalLogger = CreateLoggers();
            Log(Strings.LOADING_CONFIGURATION, ESeverity.DEBUG);
			Protocol = GenericProtocol.GetInstance(ClientConfig);
			SetCulture(ClientConfig.ServiceCulture);
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
            _context = context;
			ClientConfig = new ClientConfig(config, null, null);
            Configuration = config;
        }
        #endregion

    }
}
