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
using System.Text;
using System.Net;
using System.Xml;

using Bdt.Shared.Configuration;
using Bdt.Shared.Logs;
#endregion

namespace Bdt.Client.Configuration
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Configuration côté client
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class ClientConfig : SharedConfig
    {

        #region " Constantes "

        // Les constantes du fichier de configuration liées au socks
        public const string WordForward = "forward";
	    private const string WordSocks = "socks";
        public const string WordShared = "shared";
        public const string WordEnabled = "enabled";
	    private const string WordProxy = "proxy";
	    private const string WordAuthentication = "authentification";
	    private const string WordConfiguration = "configuration";
	    private const string WordAuto = "auto";
	    private const string WordDomain = "domain";
		private const string WordExpect100 = "expect100";

        // Les constantes du fichier de configuration liées au proxy
	    private const string CfgProxyEnabled = WordProxy + TagAttribute + WordEnabled;
		private const string CfgProxyExpect100 = WordProxy + TagAttribute + WordExpect100;

        // Configuration du proxy
		private const string CfgProxyConfigAuto = WordProxy + TagElement + WordConfiguration + TagAttribute + WordAuto;
		private const string CfgProxyAddress = WordProxy + TagElement + WordConfiguration + TagAttribute + WordAddress;
		private const string CfgProxyPort = WordProxy + TagElement + WordConfiguration + TagAttribute + WordPort;

        // Authentification du proxy
		private const string CfgProxyAuthAuto = WordProxy + TagElement + WordAuthentication + TagAttribute + WordAuto;
		private const string CfgProxyUsername = WordProxy + TagElement + WordAuthentication + TagAttribute + WordUsername;
		private const string CfgProxyPassword = WordProxy + TagElement + WordAuthentication + TagAttribute + WordPassword;
		private const string CfgProxyDomain = WordProxy + TagElement + WordAuthentication + TagAttribute + WordDomain;

        // Les constantes du fichier de configuration liées au socks
		private const string CfgSocksEnabled = WordSocks + TagAttribute + WordEnabled;
		private const string CfgSocksShared = WordSocks + TagAttribute + WordShared;
		private const string CfgSocksPort = WordSocks + TagAttribute + WordPort; 
        #endregion

		#region " Proprietes "

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Expect 100-Continue behavior 
		/// </summary>
		/// -----------------------------------------------------------------------------
		public bool Expect100Continue { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le proxy est-il actif?
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public bool ProxyEnabled { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// L'auto-configuration du proxy est-elle active?
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public bool ProxyAutoConfiguration { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Adresse du proxy (FQDN ou IP), si l'autoconfig est désactivée
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public string ProxyAddress { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// L'auto-authentification du proxy est-elle active?
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public bool ProxyAutoAuthentication { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Nom d'utilisateur du proxy
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public string ProxyUserName { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Mot de passe du proxy
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public string ProxyPassword { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Domaine du proxy
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public string ProxyDomain { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Port du proxy
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public int ProxyPort { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le serveur Socks est-il actif?
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public bool SocksEnabled { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le serveur Socks est-il partagé (bind sur toutes les ips)
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public bool SocksShared { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Port du serveur Socks
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public int SocksPort { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Retourne une configuration de ports forwardés indexés par le port local
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public Dictionary<int, PortForward> Forwards { get; private set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Retourne le log fichier
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public FileLogger FileLogger { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Retourne le log console
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public BaseLogger ConsoleLogger { get; set; }

	    #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="config">la configuration</param>
        /// -----------------------------------------------------------------------------
        public ClientConfig (ConfigPackage config, BaseLogger consoleLogger, FileLogger fileLogger)
            : base(config)
        {
            ConsoleLogger = consoleLogger;
            FileLogger = fileLogger;

            if (config != null)
            {
                ProxyEnabled = config.ValueBool(CfgProxyEnabled, false);
				Expect100Continue = config.ValueBool(CfgProxyExpect100, true);
                ProxyAutoConfiguration = config.ValueBool(CfgProxyConfigAuto, false);
                ProxyAddress = config.Value(CfgProxyAddress, string.Empty);
                ProxyAutoAuthentication = config.ValueBool(CfgProxyAuthAuto, false);
                ProxyUserName = config.Value(CfgProxyUsername, string.Empty);
                ProxyPassword = config.Value(CfgProxyPassword, string.Empty);
                ProxyDomain = config.Value(CfgProxyDomain, string.Empty);
                ProxyPort = config.ValueInt(CfgProxyPort, 0);
                SocksEnabled = config.ValueBool(CfgSocksEnabled, false);
                SocksShared = config.ValueBool(CfgSocksShared, false);
                SocksPort = config.ValueInt(CfgSocksPort, 0);
            }

            Forwards = new Dictionary<int,PortForward>();
            for (var i = IPEndPoint.MinPort; i <= IPEndPoint.MaxPort; i++)
            {
                var forward = new PortForward(config, i);
                if ((forward.RemotePort > 0) && (forward.Address != String.Empty))
                    Forwards.Add(i, forward);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Construit un attribut et fixe sa valeur
        /// </summary>
        /// <param name="doc">le document XML</param>
        /// <param name="name">le nom de l'attribut</param>
        /// <param name="value">la valeur de l'attribut</param>
        /// <returns>l'attribut crée</returns>
        /// -----------------------------------------------------------------------------
        private XmlAttribute CreateAttribute (XmlDocument doc, string name, object value)
        {
            var attr = doc.CreateAttribute(name);
            if (value is bool) {
                attr.Value = value.ToString().ToLower();
            } else {
                attr.Value = value.ToString();
            }
            return attr;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Sauvegarde la configuration dans un fichier au format XML
        /// </summary>
        /// <param name="filename">le nom du fichier destination</param>
        /// -----------------------------------------------------------------------------
        public void SaveToFile (string filename)
        {
            var doc = new XmlDocument();

            var root = doc.CreateElement("bdtclient");
            var service = doc.CreateElement(WordService);
            var socks = doc.CreateElement(WordSocks);
            var proxy = doc.CreateElement(WordProxy);
            var proxyAuth = doc.CreateElement(WordAuthentication);
            var proxyConfig = doc.CreateElement(WordConfiguration);
            var forward = doc.CreateElement(WordForward);
            var logs = doc.CreateElement(WordLogs);
            var logsConsole = doc.CreateElement(WordConsole);
            var logsFile = doc.CreateElement(WordFile);

            doc.AppendChild(root);
            root.AppendChild(service);
            root.AppendChild(socks);
            root.AppendChild(proxy);
            root.AppendChild(forward);
            root.AppendChild(logs);
            logs.AppendChild(logsConsole);
            logs.AppendChild(logsFile);

            service.Attributes.Append(CreateAttribute(doc, WordName, ServiceName));
            service.Attributes.Append(CreateAttribute(doc, WordProtocol, ServiceProtocol));
            service.Attributes.Append(CreateAttribute(doc, WordAddress, ServiceAddress));
            service.Attributes.Append(CreateAttribute(doc, WordPort, ServicePort));
            service.Attributes.Append(CreateAttribute(doc, WordUsername, ServiceUserName));
            service.Attributes.Append(CreateAttribute(doc, WordPassword, ServicePassword));
            service.Attributes.Append(CreateAttribute(doc, WordCulture, ServiceCulture));

            socks.Attributes.Append(CreateAttribute(doc, WordEnabled, SocksEnabled));
            socks.Attributes.Append(CreateAttribute(doc, WordShared, SocksShared));
            socks.Attributes.Append(CreateAttribute(doc, WordPort, SocksPort));

            proxy.Attributes.Append(CreateAttribute(doc, WordEnabled, ProxyEnabled));
			proxy.Attributes.Append(CreateAttribute(doc, WordExpect100, Expect100Continue));
			proxy.AppendChild(proxyAuth);
            proxy.AppendChild(proxyConfig);

            proxyAuth.Attributes.Append(CreateAttribute(doc, WordAuto, ProxyAutoAuthentication));
            proxyAuth.Attributes.Append(CreateAttribute(doc, WordUsername, ProxyUserName));
            proxyAuth.Attributes.Append(CreateAttribute(doc, WordPassword, ProxyPassword));
            proxyAuth.Attributes.Append(CreateAttribute(doc, WordDomain, ProxyDomain));

            proxyConfig.Attributes.Append(CreateAttribute(doc, WordAuto, ProxyAutoConfiguration));
            proxyConfig.Attributes.Append(CreateAttribute(doc, WordAddress, ProxyAddress));
            proxyConfig.Attributes.Append(CreateAttribute(doc, WordPort, ProxyPort));

            foreach (var portforward in Forwards.Values)
            {
                var pf = doc.CreateElement(WordPort + portforward.LocalPort.ToString(CultureInfo.InvariantCulture));
                forward.AppendChild(pf);
                pf.Attributes.Append(CreateAttribute(doc, WordEnabled, portforward.Enabled));
                pf.Attributes.Append(CreateAttribute(doc, WordShared, portforward.Shared));
                pf.Attributes.Append(CreateAttribute(doc, WordAddress, portforward.Address));
                pf.Attributes.Append(CreateAttribute(doc, WordPort, portforward.RemotePort));
            }

            logsConsole.Attributes.Append(CreateAttribute(doc, BaseLogger.ConfigEnabled, ConsoleLogger.Enabled));
            logsConsole.Attributes.Append(CreateAttribute(doc, BaseLogger.ConfigFilter, ConsoleLogger.Filter));
            logsConsole.Attributes.Append(CreateAttribute(doc, BaseLogger.ConfigStringFormat, ConsoleLogger.StringFormat));
            logsConsole.Attributes.Append(CreateAttribute(doc, BaseLogger.ConfigDateFormat, ConsoleLogger.DateFormat));

            logsFile.Attributes.Append(CreateAttribute(doc, BaseLogger.ConfigEnabled, FileLogger.Enabled));
            logsFile.Attributes.Append(CreateAttribute(doc, BaseLogger.ConfigFilter, FileLogger.Filter));
            logsFile.Attributes.Append(CreateAttribute(doc, BaseLogger.ConfigStringFormat, FileLogger.StringFormat));
            logsFile.Attributes.Append(CreateAttribute(doc, BaseLogger.ConfigDateFormat, FileLogger.DateFormat));
            logsFile.Attributes.Append(CreateAttribute(doc, FileLogger.ConfigFilename, FileLogger.Filename));
            logsFile.Attributes.Append(CreateAttribute(doc, FileLogger.ConfigAppend, FileLogger.Append));

            var xmltw = new XmlTextWriter(filename, new UTF8Encoding()) {Formatting = Formatting.Indented};
	        doc.WriteTo(xmltw);
            xmltw.Close();
        }
        #endregion
        
	}
}
