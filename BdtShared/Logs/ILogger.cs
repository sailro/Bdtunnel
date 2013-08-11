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

namespace Bdt.Shared.Logs
{
    #region " Enumerations "
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Severité d'une entrée de log, Filter permet de donner un niveau minimum
    /// </summary>
    /// -----------------------------------------------------------------------------
    public enum ESeverity
    {
		// ReSharper disable InconsistentNaming
        DEBUG = 1,
        INFO = 2,
        WARN = 3,
        @ERROR = 4,
		// ReSharper disable UnusedMember.Global
        FATAL = 5
		// ReSharper restore UnusedMember.Global
		// ReSharper restore InconsistentNaming
	}
    #endregion

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Interface minimale pour la génération d'un log
    /// </summary>
    /// -----------------------------------------------------------------------------
    public interface ILogger
    {

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ecriture d'une entrée de log. Ne sera pas prise en compte si le log est inactif
        /// ou si le filtre l'impose
        /// </summary>
        /// <param name="sender">l'emetteur</param>
        /// <param name="message">le message à logger</param>
        /// <param name="severity">la sévérité</param>
        /// -----------------------------------------------------------------------------
        void Log(object sender, string message, ESeverity severity);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fermeture du logger
        /// </summary>
        /// -----------------------------------------------------------------------------
        void Close();
        #endregion

    }

}

