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
using System.Collections.Generic;
using System.Globalization;

#endregion

namespace Bdt.Shared.Configuration
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Regroupe un ensemble de sources de configurations priorisées. Permet de rechercher des
    /// valeurs suivant un code.
    /// Les sources peuvent être différentes: base de donnée, fichier de configuration,
    /// ligne de commande, etc
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class ConfigPackage
    {
        #region " Attributs "
        //Les sources sont triées par priorité grâce au compareTo de SourceConfiguration (IComparable)
        private readonly List<BaseConfig> _sources = new List<BaseConfig>();
        #endregion

        #region " Propriétés "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retourne la valeur d'un élément suivant son code
        /// </summary>
        /// <param name="code">le code de l'élément</param>
        /// <param name="defaultValue">la valeur par défaut si l'élément est introuvable</param>
        /// <returns>La valeur de l'élément s'il existe ou defaultValue sinon</returns>
        /// -----------------------------------------------------------------------------
        public virtual string Value(string code, string defaultValue)
        {
            foreach (var source in _sources)
            {
                var result = source.Value(code, null);
	            if (result == null) 
					continue;

	            return result;
            }
            return defaultValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fixe/Retourne la valeur entière d'un élément suivant son code
        /// </summary>
        /// <param name="code">le code de l'élément</param>
        /// <param name="defaultValue">la valeur par défaut si l'élément est introuvable</param>
        /// <returns>La valeur de l'élément s'il existe et s'il représente un entier ou defaultValue sinon</returns>
        /// -----------------------------------------------------------------------------
        public int ValueInt(string code, int defaultValue)
        {
            try
            {
                return int.Parse(Value(code, defaultValue.ToString(CultureInfo.InvariantCulture)));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fixe/Retourne la valeur booléenne d'un élément suivant son code
        /// </summary>
        /// <param name="code">le code de l'élément</param>
        /// <param name="defaultValue">la valeur par défaut si l'élément est introuvable</param>
        /// <returns>La valeur de l'élément s'il existe et s'il représente un booléen (true/false) ou defaultValue sinon</returns>
        /// -----------------------------------------------------------------------------
        public bool ValueBool(string code, bool defaultValue)
        {
            try
            {
                return bool.Parse(Value(code, defaultValue.ToString()));
			}
            catch (Exception)
            {
                return defaultValue;
            }
        }
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ajoute une source à ce contexte de configuration
        /// </summary>
        /// <param name="source">la source à ajouter, qui sera classée par SourceConfiguration.Priority()</param>
        /// -----------------------------------------------------------------------------
        public void AddSource(BaseConfig source)
        {
            _sources.Add(source);
            _sources.Sort();
        }

	    /// -----------------------------------------------------------------------------
        /// <summary>
        /// Concatène tous les éléments depuis toutes les sources
        /// </summary>
        /// <returns>le format de chaque ligne est classe,priorité,code,valeur</returns>
        /// -----------------------------------------------------------------------------
        public override string ToString()
        {
	        string returnValue = string.Empty;
            foreach (var source in _sources)
                returnValue += source.ToString();

			return returnValue;
        }
        #endregion

    }

}

