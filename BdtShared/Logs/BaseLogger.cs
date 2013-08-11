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
using System.Globalization;
using System.IO;
using System.ComponentModel;
#endregion

namespace Bdt.Shared.Logs
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Classe de base pour la génération d'un log
    /// </summary>
    /// -----------------------------------------------------------------------------
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class BaseLogger : ILogger
    {

        #region " Constantes "
        public const string ConfigEnabled = "enabled";
        public const string ConfigFilter = "filter";
        public const string ConfigDateFormat = "dateformat";
        public const string ConfigStringFormat = "stringformat";
	    private const string ConfigTagStart = "{";
	    private const string ConfigTagEnd = "}";
        #endregion

        #region " Enumerations "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ces tags permettent de construire une chaine de sortie personnalisée
        /// </summary>
        /// -----------------------------------------------------------------------------
		// ReSharper disable InconsistentNaming
		private enum ETags
        {
            TIMESTAMP = 0,
            SEVERITY = 1,
            TYPE = 2,
            MESSAGE = 3
        }
		// ReSharper restore InconsistentNaming
		#endregion

        #region " Attributs "
        protected TextWriter Writer;
	    #endregion

        #region " Proprietes "

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Fixe/retourne l'état d'activation du log
	    /// </summary>
	    /// <returns>l'état d'activation du log</returns>
	    /// -----------------------------------------------------------------------------
	    public bool Enabled { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Fixe/retourne la chaine personnalisée pour la sortie des logs.
	    /// Utiliser les constantes ETags entre accolades {}
	    /// </summary>
	    /// <returns>la chaine personnalisée pour la sortie des logs</returns>
	    /// -----------------------------------------------------------------------------
	    public string StringFormat { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Retourne/Fixe le format des dates de timestamp
	    /// </summary>
	    /// <returns>le format des dates de timestamp</returns>
	    /// -----------------------------------------------------------------------------
	    public string DateFormat { get; protected set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Retourne/fixe le niveau de filtrage pour la sortie des logs
	    /// </summary>
	    /// <returns>le niveau de filtrage pour la sortie des logs</returns>
	    /// -----------------------------------------------------------------------------
	    public ESeverity Filter { get; protected set; }

	    #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur pour un log vierge
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected BaseLogger ()
        {
	        Filter = ESeverity.DEBUG;
	        DateFormat = "dd/MM/yyyy HH:mm:ss";
	        StringFormat = ConfigTagStart + ETags.TIMESTAMP + ConfigTagEnd + " " + ConfigTagStart + ETags.SEVERITY + ConfigTagEnd + " [" + ConfigTagStart + ETags.TYPE + ConfigTagEnd + "] " + ConfigTagStart + ETags.MESSAGE + ConfigTagEnd;
	        Enabled = true;
        }

	    /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur pour un log à partir des données fournies dans une configuration
        /// </summary>
        /// <param name="writer">le textwriter pour la sortie des logs</param>
        /// <param name="prefix">le prefixe dans la configuration ex: application/log</param>
        /// <param name="config">la configuration pour la lecture des parametres</param>
        /// -----------------------------------------------------------------------------
	    protected BaseLogger(TextWriter writer, string prefix, Configuration.ConfigPackage config)
        {
            Writer = writer;

            Enabled = config.ValueBool(prefix + Configuration.BaseConfig.SourceItemAttribute + ConfigEnabled, Enabled);
            Filter = ((ESeverity)Enum.Parse(typeof(ESeverity), config.Value(prefix + Configuration.BaseConfig.SourceItemAttribute + ConfigFilter, Filter.ToString())));
            DateFormat = config.Value(prefix + Configuration.BaseConfig.SourceItemAttribute + ConfigDateFormat, DateFormat);
            StringFormat = config.Value(prefix + Configuration.BaseConfig.SourceItemAttribute + ConfigStringFormat, StringFormat);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur pour un log
        /// </summary>
        /// <param name="writer">le textwriter pour la sortie des logs</param>
        /// <param name="dateFormat">le format des dates de timestamp</param>
        /// <param name="filter">le niveau de filtrage pour la sortie des logs</param>
        /// -----------------------------------------------------------------------------
        protected BaseLogger(TextWriter writer, string dateFormat, ESeverity filter)
        {
	        StringFormat = ConfigTagStart + ETags.TIMESTAMP + ConfigTagEnd + " " + ConfigTagStart + ETags.SEVERITY + ConfigTagEnd + " [" + ConfigTagStart + ETags.TYPE + ConfigTagEnd + "] " + ConfigTagStart + ETags.MESSAGE + ConfigTagEnd;
	        Enabled = true;
	        Writer = writer;
            DateFormat = dateFormat;
            Filter = filter;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ecriture d'une entrée de log. Ne sera pas prise en compte si le log est inactif
        /// ou si le filtre l'impose
        /// </summary>
        /// <param name="sender">l'emetteur</param>
        /// <param name="message">le message à logger</param>
        /// <param name="severity">la sévérité</param>
        /// -----------------------------------------------------------------------------
        public virtual void Log(object sender, string message, ESeverity severity)
        {
            if ((Enabled) && (severity >= Filter) && (Writer != null))
            {
                // Remplacement des id chaines de ETags en leur équivalent integer
                string format = StringFormat;
                foreach (ETags tag in Enum.GetValues(typeof(ETags)))
                    format = format.Replace(tag.ToString(), Convert.ToInt32(tag).ToString(CultureInfo.InvariantCulture));

				Writer.WriteLine(format, DateTime.Now.ToString(DateFormat), severity, sender.GetType().Name, message);
                Writer.Flush();
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fermeture du logger
        /// </summary>
        /// -----------------------------------------------------------------------------
        public virtual void Close()
        {
	        if (Writer == null) return;
	        // ReSharper disable EmptyGeneralCatchClause
	        try
	        {
		        Writer.Close();
	        }
	        catch
	        {
	        }
	        // ReSharper restore EmptyGeneralCatchClause
	        Writer = null;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Destruction d'un log
        /// </summary>
        /// -----------------------------------------------------------------------------
        ~BaseLogger()
        {
            Close();
        }
        #endregion

    }

}


