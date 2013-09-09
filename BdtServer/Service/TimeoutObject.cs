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
using System.Collections;
using System.Collections.Generic;

using Bdt.Shared.Logs;
#endregion

namespace Bdt.Server.Service
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Un objet soumis au timeout
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class TimeoutObject
    {

        #region " Proprietes "

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Delai de Timeout
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    private int TimeoutDelay { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// La date de dernière opération I/O
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public DateTime LastAccess { get; set; }

	    #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="timeoutdelay">valeur de timeout</param>
        /// -----------------------------------------------------------------------------
        protected TimeoutObject(int timeoutdelay)
        {
            TimeoutDelay = timeoutdelay;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Timeout de l'objet
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected abstract void Timeout(ILogger logger);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Vérification de timeout de l'objet
        /// </summary>
        /// <returns>true en cas de timeout</returns>
        /// -----------------------------------------------------------------------------
        protected virtual bool CheckTimeout(ILogger logger)
        {
	        if (TimeoutDelay > 0)
		        return DateTime.Now.Subtract(LastAccess).TotalHours > TimeoutDelay;
	        return false;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Vérification de timeout dans une collection
        /// </summary>
        /// <typeparam name="T">un type TimeoutObject</typeparam>
        /// <param name="logger">un logger</param>
        /// <param name="collection">les elements à vérifier</param>
        /// -----------------------------------------------------------------------------
        public static void CheckTimeout<T>(ILogger logger, Dictionary<int, T> collection) where T : TimeoutObject
        {
            foreach (int key in new ArrayList(collection.Keys))
            {
                var item = collection[key];
	            if (!item.CheckTimeout(logger))
					continue;
	            
				item.Timeout(logger);
	            collection.Remove(key);
            }
        
        }
        #endregion

    }

}
