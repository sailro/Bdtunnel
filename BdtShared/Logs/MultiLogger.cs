/* BoutDuTunnel Copyright (c)  2007-2013 Sebastien LEBRETON

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
using System.Collections.Generic;
#endregion

namespace Bdt.Shared.Logs
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Classe permettant la propagation de logs sur de multiples flux
    /// </summary>
    /// -----------------------------------------------------------------------------
    public sealed class MultiLogger : BaseLogger
    {

        #region " Attributs "
        private readonly List<ILogger> _loggers = new List<ILogger>();
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur pour un flux multiple
        /// </summary>
        /// -----------------------------------------------------------------------------
        public MultiLogger()
            : base(null, "dd/MM/yyyy HH:mm:ss", ESeverity.DEBUG)
        {
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ajoute un logger à ce flux multiple
        /// </summary>
        /// <param name="logger"></param>
        /// -----------------------------------------------------------------------------
        public void AddLogger(ILogger logger)
        {
            _loggers.Add(logger);
        }

	    /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ecriture d'une entrée de log. Ne sera pas prise en compte si le log est inactif
        /// ou si le filtre l'impose. L'écriture sera propagée à tous les flux associés
        /// </summary>
        /// <param name="sender">l'emetteur</param>
        /// <param name="message">le message à logger</param>
        /// <param name="severity">la sévérité</param>
        /// -----------------------------------------------------------------------------
        public override void Log(object sender, string message, ESeverity severity)
        {
            if (Enabled)
            {
	            foreach (var logger in _loggers)
		            logger.Log(sender, message, severity);
            }
        }
        #endregion

    }

}
