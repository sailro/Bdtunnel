/// -----------------------------------------------------------------------------
/// BoutDuTunnel
/// Sebastien LEBRETON
/// sebastien.lebreton[-at-]free.fr
/// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Collections.Generic;
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
        public const string WORD_FORWARD = "forward";
        public const string WORD_SOCKS = "socks";
        public const string WORD_SHARED = "shared";
        public const string WORD_ENABLED = "enabled";
        public const string WORD_PROXY = "proxy";
        public const string WORD_AUTHENTICATION = "authentification";
        public const string WORD_CONFIGURATION = "configuration";
        public const string WORD_AUTO = "auto";
        public const string WORD_DOMAIN = "domain";

        // Les constantes du fichier de configuration liées au proxy
        protected const string CFG_PROXY_ENABLED = WORD_PROXY + TAG_ATTRIBUTE + WORD_ENABLED;

        // Configuration du proxy
        protected const string CFG_PROXY_CONFIG_AUTO = WORD_PROXY + TAG_ELEMENT + WORD_CONFIGURATION + TAG_ATTRIBUTE + WORD_AUTO;
        protected const string CFG_PROXY_ADDRESS = WORD_PROXY + TAG_ELEMENT + WORD_CONFIGURATION + TAG_ATTRIBUTE + WORD_ADDRESS;
        protected const string CFG_PROXY_PORT = WORD_PROXY + TAG_ELEMENT + WORD_CONFIGURATION + TAG_ATTRIBUTE + WORD_PORT;

        // Authentification du proxy
        protected const string CFG_PROXY_AUTH_AUTO = WORD_PROXY + TAG_ELEMENT + WORD_AUTHENTICATION + TAG_ATTRIBUTE + WORD_AUTO;
        protected const string CFG_PROXY_USERNAME = WORD_PROXY + TAG_ELEMENT + WORD_AUTHENTICATION + TAG_ATTRIBUTE + WORD_USERNAME;
        protected const string CFG_PROXY_PASSWORD = WORD_PROXY + TAG_ELEMENT + WORD_AUTHENTICATION + TAG_ATTRIBUTE + WORD_PASSWORD;
        protected const string CFG_PROXY_DOMAIN = WORD_PROXY + TAG_ELEMENT + WORD_AUTHENTICATION + TAG_ATTRIBUTE + WORD_DOMAIN;

        // Les constantes du fichier de configuration liées au socks
        protected const string CFG_SOCKS_ENABLED = WORD_SOCKS + TAG_ATTRIBUTE + WORD_ENABLED;
        protected const string CFG_SOCKS_SHARED = WORD_SOCKS + TAG_ATTRIBUTE + WORD_SHARED;
        protected const string CFG_SOCKS_PORT = WORD_SOCKS + TAG_ATTRIBUTE + WORD_PORT; 
        #endregion

        #region " Attributs "
            protected bool m_proxyEnabled;
            protected bool m_proxyAutoConfiguration;
            protected string m_proxyAddress;
            protected bool m_proxyAutoAuthentication;
            protected string m_proxyUserName;
            protected string m_proxyPassword;
            protected string m_proxyDomain;
            protected int m_proxyPort;
            protected bool m_socksEnabled;
            protected bool m_socksShared;
            protected int m_socksPort;
            protected Dictionary<int, PortForward> m_forwards;
            protected FileLogger m_fileLogger;
            protected BaseLogger m_consoleLogger;
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le proxy est-il actif?
        /// </summary>
        /// -----------------------------------------------------------------------------
        public bool ProxyEnabled
        {
            get
            {
                return m_proxyEnabled;
            }
            set
            {
                m_proxyEnabled = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// L'auto-configuration du proxy est-elle active?
        /// </summary>
        /// -----------------------------------------------------------------------------
        public bool ProxyAutoConfiguration
        {
            get
            {
                return m_proxyAutoConfiguration;
            }
            set
            {
                m_proxyAutoConfiguration = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Adresse du proxy (FQDN ou IP), si l'autoconfig est désactivée
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string ProxyAddress
        {
            get
            {
                return m_proxyAddress;
            }
            set
            {
                m_proxyAddress = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// L'auto-authentification du proxy est-elle active?
        /// </summary>
        /// -----------------------------------------------------------------------------
        public bool ProxyAutoAuthentication
        {
            get
            {
                return m_proxyAutoAuthentication;
            }
            set
            {
                m_proxyAutoAuthentication = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Nom d'utilisateur du proxy
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string ProxyUserName
        {
            get
            {
                return m_proxyUserName;
            }
            set
            {
                m_proxyUserName = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Mot de passe du proxy
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string ProxyPassword
        {
            get
            {
                return m_proxyPassword;
            }
            set
            {
                m_proxyPassword = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Domaine du proxy
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string ProxyDomain
        {
            get
            {
                return m_proxyDomain;
            }
            set
            {
                m_proxyDomain = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Port du proxy
        /// </summary>
        /// -----------------------------------------------------------------------------
        public int ProxyPort
        {
            get
            {
                return m_proxyPort;
            }
            set
            {
                m_proxyPort = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le serveur Socks est-il actif?
        /// </summary>
        /// -----------------------------------------------------------------------------
        public bool SocksEnabled
        {
            get
            {
                return m_socksEnabled;
            }
            set
            {
                m_socksEnabled = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le serveur Socks est-il partagé (bind sur toutes les ips)
        /// </summary>
        /// -----------------------------------------------------------------------------
        public bool SocksShared
        {
            get
            {
                return m_socksShared;
            }
            set
            {
                m_socksShared = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Port du serveur Socks
        /// </summary>
        /// -----------------------------------------------------------------------------
        public int SocksPort
        {
            get
            {
                return m_socksPort;
            }
            set
            {
                m_socksPort = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retourne une configuration de ports forwardés indexés par le port local
        /// </summary>
        /// -----------------------------------------------------------------------------
        public Dictionary<int, PortForward> Forwards
        {
            get
            {
                return m_forwards;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retourne le log fichier
        /// </summary>
        /// -----------------------------------------------------------------------------
        public FileLogger FileLogger
        {
            get
            {
                return m_fileLogger;
            }
            set
            {
                m_fileLogger = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retourne le log console
        /// </summary>
        /// -----------------------------------------------------------------------------
        public BaseLogger ConsoleLogger
        {
            get
            {
                return m_consoleLogger;
            }
            set
            {
                m_consoleLogger = value;
            }
        }
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
            m_consoleLogger = consoleLogger;
            m_fileLogger = fileLogger;

            m_proxyEnabled = config.ValueBool(CFG_PROXY_ENABLED, false);
            m_proxyAutoConfiguration = config.ValueBool(CFG_PROXY_CONFIG_AUTO, false);
            m_proxyAddress = config.Value(CFG_PROXY_ADDRESS, string.Empty);
            m_proxyAutoAuthentication = config.ValueBool(CFG_PROXY_AUTH_AUTO, false);
            m_proxyUserName = config.Value(CFG_PROXY_USERNAME, string.Empty);
            m_proxyPassword = config.Value(CFG_PROXY_PASSWORD, string.Empty);
            m_proxyDomain = config.Value(CFG_PROXY_DOMAIN, string.Empty);
            m_proxyPort = config.ValueInt(CFG_PROXY_PORT, 0);
            m_socksEnabled = config.ValueBool(CFG_SOCKS_ENABLED, false);
            m_socksShared = config.ValueBool(CFG_SOCKS_SHARED, false);
            m_socksPort = config.ValueInt(CFG_SOCKS_PORT, 0);

            m_forwards = new Dictionary<int,PortForward>();
            for (int i = IPEndPoint.MinPort; i <= IPEndPoint.MaxPort; i++)
            {
                PortForward forward = new PortForward(config, i);
                if ((forward.RemotePort > 0) && (forward.Address != String.Empty))
                {
                    m_forwards.Add(i, forward);
                }
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
            XmlAttribute attr = doc.CreateAttribute(name);
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
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("bdtclient");
            XmlElement service = doc.CreateElement(WORD_SERVICE);
            XmlElement socks = doc.CreateElement(WORD_SOCKS);
            XmlElement proxy = doc.CreateElement(WORD_PROXY);
            XmlElement proxyAuth = doc.CreateElement(WORD_AUTHENTICATION);
            XmlElement proxyConfig = doc.CreateElement(WORD_CONFIGURATION);
            XmlElement forward = doc.CreateElement(WORD_FORWARD);
            XmlElement logs = doc.CreateElement(WORD_LOGS);
            XmlElement logsConsole = doc.CreateElement(WORD_CONSOLE);
            XmlElement logsFile = doc.CreateElement(WORD_FILE);

            doc.AppendChild(root);
            root.AppendChild(service);
            root.AppendChild(socks);
            root.AppendChild(proxy);
            root.AppendChild(forward);
            root.AppendChild(logs);
            logs.AppendChild(logsConsole);
            logs.AppendChild(logsFile);

            service.Attributes.Append(CreateAttribute(doc, WORD_NAME, ServiceName));
            service.Attributes.Append(CreateAttribute(doc, WORD_PROTOCOL, ServiceProtocol));
            service.Attributes.Append(CreateAttribute(doc, WORD_ADDRESS, ServiceAddress));
            service.Attributes.Append(CreateAttribute(doc, WORD_PORT, ServicePort));
            service.Attributes.Append(CreateAttribute(doc, WORD_USERNAME, ServiceUserName));
            service.Attributes.Append(CreateAttribute(doc, WORD_PASSWORD, ServicePassword));
            service.Attributes.Append(CreateAttribute(doc, WORD_CULTURE, ServiceCulture));

            socks.Attributes.Append(CreateAttribute(doc, WORD_ENABLED, SocksEnabled));
            socks.Attributes.Append(CreateAttribute(doc, WORD_SHARED, SocksShared));
            socks.Attributes.Append(CreateAttribute(doc, WORD_PORT, SocksPort));

            proxy.Attributes.Append(CreateAttribute(doc, WORD_ENABLED, ProxyEnabled));
            proxy.AppendChild(proxyAuth);
            proxy.AppendChild(proxyConfig);

            proxyAuth.Attributes.Append(CreateAttribute(doc, WORD_AUTO, ProxyAutoAuthentication));
            proxyAuth.Attributes.Append(CreateAttribute(doc, WORD_USERNAME, ProxyUserName));
            proxyAuth.Attributes.Append(CreateAttribute(doc, WORD_PASSWORD, ProxyPassword));
            proxyAuth.Attributes.Append(CreateAttribute(doc, WORD_DOMAIN, ProxyDomain));

            proxyConfig.Attributes.Append(CreateAttribute(doc, WORD_AUTO, ProxyAutoConfiguration));
            proxyConfig.Attributes.Append(CreateAttribute(doc, WORD_ADDRESS, ProxyAddress));
            proxyConfig.Attributes.Append(CreateAttribute(doc, WORD_PORT, ProxyPort));

            foreach (PortForward portforward in Forwards.Values)
            {
                XmlElement pf = doc.CreateElement(WORD_PORT + portforward.LocalPort.ToString());
                forward.AppendChild(pf);
                pf.Attributes.Append(CreateAttribute(doc, WORD_ENABLED, portforward.Enabled));
                pf.Attributes.Append(CreateAttribute(doc, WORD_SHARED, portforward.Shared));
                pf.Attributes.Append(CreateAttribute(doc, WORD_ADDRESS, portforward.Address));
                pf.Attributes.Append(CreateAttribute(doc, WORD_PORT, portforward.RemotePort));
            }

            logsConsole.Attributes.Append(CreateAttribute(doc, BaseLogger.CONFIG_ENABLED, m_consoleLogger.Enabled));
            logsConsole.Attributes.Append(CreateAttribute(doc, BaseLogger.CONFIG_FILTER, m_consoleLogger.Filter));
            logsConsole.Attributes.Append(CreateAttribute(doc, BaseLogger.CONFIG_STRING_FORMAT, m_consoleLogger.StringFormat));
            logsConsole.Attributes.Append(CreateAttribute(doc, BaseLogger.CONFIG_DATE_FORMAT, m_consoleLogger.DateFormat));

            logsFile.Attributes.Append(CreateAttribute(doc, BaseLogger.CONFIG_ENABLED, m_fileLogger.Enabled));
            logsFile.Attributes.Append(CreateAttribute(doc, BaseLogger.CONFIG_FILTER, m_fileLogger.Filter));
            logsFile.Attributes.Append(CreateAttribute(doc, BaseLogger.CONFIG_STRING_FORMAT, m_fileLogger.StringFormat));
            logsFile.Attributes.Append(CreateAttribute(doc, BaseLogger.CONFIG_DATE_FORMAT, m_fileLogger.DateFormat));
            logsFile.Attributes.Append(CreateAttribute(doc, FileLogger.CONFIG_FILENAME, m_fileLogger.Filename));
            logsFile.Attributes.Append(CreateAttribute(doc, FileLogger.CONFIG_APPEND, m_fileLogger.Append));

            XmlTextWriter xmltw = new XmlTextWriter(filename, new UTF8Encoding());
            xmltw.Formatting = Formatting.Indented;
            doc.WriteTo(xmltw);
            xmltw.Close();
        }
        #endregion
        
	}
}
