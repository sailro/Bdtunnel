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
using System.Xml;
#endregion

namespace Bdt.Shared.Configuration
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Représente une source de configuration basée sur un fichier XML
    /// </summary>
    /// -----------------------------------------------------------------------------
    public sealed class XMLConfig : BaseConfig
    {

        #region " Propriétés "

		/// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Retourne/Fixe le nom du fichier XML associé à la source
	    /// </summary>
	    /// <returns>le nom du fichier XML associé à la source</returns>
	    /// -----------------------------------------------------------------------------
		private string FileName { get; set; }

	    #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Création d'une source de donnée basée sur un fichier XML
        /// </summary>
        /// <param name="filename">le nom du fichier XML à lier</param>
        /// <param name="priority">la priorité de cette source (la plus basse=prioritaire)</param>
        /// -----------------------------------------------------------------------------
        public XMLConfig(string filename, int priority)
            : base(priority)
        {
            FileName = filename;
            Rehash();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Parsing des noeuds XML
        /// </summary>
        /// <param name="path">le chemin courant</param>
        /// <param name="node">le noeud en cours</param>
        /// -----------------------------------------------------------------------------
        private void ParseNode(string path, XmlNode node)
        {
            foreach (XmlNode subnode in node.ChildNodes)
            {

                if (subnode.Attributes != null)
                {
                    foreach (XmlAttribute attr in subnode.Attributes)
                    {
                        if (attr.Value != string.Empty)
                            SetValue(path + subnode.Name + SourceItemAttribute + attr.Name, attr.Value);
                    }
                    if (subnode.InnerText != string.Empty)
                        SetValue(path + subnode.Name, subnode.InnerText);
                }

                if ((subnode.HasChildNodes) && (subnode.ChildNodes[0].NodeType == XmlNodeType.Element || subnode.ChildNodes[0].NodeType == XmlNodeType.Comment))
                {
                    // Chemin
                    ParseNode(path + subnode.Name + SourcePathSeparator, subnode);
                }
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Force le rechargement de la source de donnée
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void Rehash()
        {
            var docXML = new XmlDocument();
            docXML.Load(FileName);
            ParseNode(string.Empty, docXML.DocumentElement);
        }
        #endregion

    }

}


