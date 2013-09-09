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
using System;
using System.ComponentModel;
#endregion

namespace Bdt.Shared.Logs
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Classe de base pour un objet utilisant un flux de log
    /// </summary>
    /// -----------------------------------------------------------------------------
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class LoggedObject : ILogger
    {

        #region " Propriétés "

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Fixe/retourne le loggueur assocé à cet objet
	    /// </summary>
	    /// <returns>le loggueur assocé à cet objet</returns>
	    /// -----------------------------------------------------------------------------
	    private BaseLogger Logger { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Fixe/retourne le loggueur assocé à tous les objets dérivés.
	    /// </summary>
	    /// <returns>le loggueur assocé à tous les objets dérivés</returns>
	    /// -----------------------------------------------------------------------------
	    public static BaseLogger GlobalLogger { get; protected set; }

	    #endregion

        #region " Méthodes "
		protected LoggedObject()
		{
			Logger = null;
		}

		static LoggedObject()
		{
			GlobalLogger = null;
		}
		
		/// -----------------------------------------------------------------------------
        /// <summary>
        /// Ecriture d'une entrée de log. Ne sera pas prise en compte si le log est inactif
        /// ou si le filtre l'impose
        /// </summary>
        /// <param name="message">le message à logger</param>
        /// <param name="severity">la sévérité</param>
        /// -----------------------------------------------------------------------------
        public void Log(string message, ESeverity severity)
        {
            Log(this, message, severity);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ecriture d'une entrée de log. Ne sera pas prise en compte si le log est inactif
        /// ou si le filtre l'impose
        /// </summary>
        /// <param name="message">le message à logger</param>
        /// <param name="severity">la sévérité</param>
        /// -----------------------------------------------------------------------------
        public virtual void Log(object sender, string message, ESeverity severity)
        {
            if (Logger != null)
            {
                Logger.Log(sender, message, severity);
            }
            if (GlobalLogger != null)
            {
                GlobalLogger.Log(sender, message, severity);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fermeture du logger
        /// </summary>
        /// -----------------------------------------------------------------------------
        public void Close()
        {
            if (Logger != null)
            {
                Logger.Close();
            }
        }
        #endregion

    }

}

