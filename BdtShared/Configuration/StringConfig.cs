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

namespace Bdt.Shared.Configuration
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Représente une source de configuration basée sur une ligne de commande
    /// </summary>
    /// -----------------------------------------------------------------------------
    public sealed class StringConfig : BaseConfig
    {

        #region " Propriétés "

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Retourne/Fixe les arguments de la ligne de commande
	    /// </summary>
	    /// <returns>les arguments de la ligne de commande</returns>
	    /// -----------------------------------------------------------------------------
	    private string[] Args { get; set; }

	    #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Création d'une source de donnée basée sur la ligne de commande
        /// </summary>
        /// <param name="args">les arguments de la ligne de commande</param>
        /// <param name="priority">la priorité de cette source (la plus basse=prioritaire)</param>
        /// -----------------------------------------------------------------------------
        public StringConfig(string[] args, int priority)
            : base(priority)
        {
            Args = args;
            Rehash();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Force le rechargement de la source de donnée
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void Rehash()
        {
            foreach (string arg in Args)
            {
                var equalIndex = arg.IndexOf(SourceItemEquals, System.StringComparison.Ordinal);
                if ((equalIndex >= 0) && equalIndex + 1 < arg.Length)
                    SetValue(arg.Substring(0, equalIndex), arg.Substring(equalIndex + 1));
            }
        }
        #endregion

    }

}


