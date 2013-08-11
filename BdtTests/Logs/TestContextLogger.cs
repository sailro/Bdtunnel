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
        private readonly TestContext _context;
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
            _context = context;
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
            _context.WriteLine("[{0}] {1} {2}", severity, sender.GetType().Name, message);
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
