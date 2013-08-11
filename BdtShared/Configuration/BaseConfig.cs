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
using System;
using System.Collections;
#endregion

namespace Bdt.Shared.Configuration
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Représente une source de configuration générique. Permet de servir de base pour l'élaboration
    /// d'autres sources.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class BaseConfig : IComparable
    {

        #region " Constantes "
	    protected const string SourcePathSeparator = "/";
        public const string SourceItemAttribute = "@";
	    protected const string SourceItemEquals = "=";
        #endregion

        #region " Attributs "
        private readonly SortedList _values = new SortedList(); // Les elements classés par code

	    #endregion

        #region " Propriétés "

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Retourne/Fixe la priorité de la source
	    /// </summary>
	    /// <returns>la priorité de la source</returns>
	    /// -----------------------------------------------------------------------------
	    private int Priority { get; set; }

	    /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retourne/Fixe la priorité de la source
        /// </summary>
        /// <returns>la priorité de la source</returns>
        /// -----------------------------------------------------------------------------
        public string Value(string code, string defaultValue)
	    {
		    return _values.ContainsKey(code) ? Convert.ToString(_values[code]) : defaultValue;
	    }

	    public void SetValue(string code, string value)
        {
            if (_values.ContainsKey(code))
                _values[code] = value;
            else
                _values.Add(code, value);
        }
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur d'une source avec un décrypteur de données optionnel
        /// Les valeurs entre SOURCE_SCRAMBLED_START et SOURCE_SCRAMBLED_END seront
        /// automatiquement décryptées
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected BaseConfig(int priority)
        {
            Priority = priority;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Force le rechargement de la source de donnée
        /// </summary>
        /// -----------------------------------------------------------------------------
        public abstract void Rehash();

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Concatène tous les éléments depuis cette source
        /// </summary>
        /// <returns>le format de chaque ligne est classe,priorité,code,valeur</returns>
        /// -----------------------------------------------------------------------------
        public sealed override string ToString()
        {
	        string returnValue = string.Empty;

            foreach (string key in _values.Keys)
                returnValue += "   <" + GetType().Name + "(" + Priority + ")" + "> [" + key + "] " + SourceItemEquals + " [" + Value(key, String.Empty) + "]" + "\r\n";

			return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comparateur par priorité
        /// </summary>
        /// <param name="obj">la config à comparer</param>
        /// <returns>voir IComparable.CompareTo</returns>
        /// -----------------------------------------------------------------------------
        public int CompareTo(object obj)
        {
	        if ((obj) is BaseConfig)
                return Priority - ((BaseConfig)obj).Priority;

			return 0;
        }

	    #endregion

    }
}

