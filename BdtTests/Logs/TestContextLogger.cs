// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;

using Bdt.Shared.Configuration;
using Bdt.Shared.Logs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion

namespace Bdt.Tests.Logs
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Génération des logs sur le flux de test
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class TestContextLogger : BaseLogger
    {
        #region " Attributs "
        private TestContext m_context;
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur 
        /// </summary>
        /// <param name="context">Le context d'un test</param>
        /// -----------------------------------------------------------------------------
        public TestContextLogger(TestContext context)
        {
            m_context = context;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ecriture d'une entrée de log. Ne sera actif que pour une sévérité à ERREUR
        /// </summary>
        /// <param name="sender">l'emetteur</param>
        /// <param name="message">le message à logger</param>
        /// <param name="severity">la sévérité</param>
        /// -----------------------------------------------------------------------------
        public override void Log(object sender, string message, ESeverity severity)
        {
            m_context.WriteLine("[{0}] {1} {2}", severity, sender.GetType().Name, message);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fermeture du logger
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void Close()
        {
            // on ne fait rien
        }

        #endregion

    }

}
