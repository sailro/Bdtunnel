/// -----------------------------------------------------------------------------
/// BoutDuTunnel
/// Sebastien LEBRETON
/// sebastien.lebreton[-at-]free.fr
/// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Collections.Generic;
using System.Text;

using Bdt.Shared.Configuration;
#endregion

namespace Bdt.Shared.Configuration
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Un frontal de proprietes mappées sur une source de configuration
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class SharedConfig
	{

        #region " Constantes "
        public const string TAG_ATTRIBUTE = "@";
        public const string TAG_ELEMENT = "/";
        public const string WORD_SERVICE = "service";
        public const string WORD_PROTOCOL = "protocol";
        public const string WORD_NAME = "name";
        public const string WORD_PORT = "port";
        public const string WORD_ADDRESS = "address";
        public const string WORD_USERNAME = "username";
        public const string WORD_PASSWORD = "password";
        public const string WORD_LOGS = "logs";
        public const string WORD_CONSOLE = "console";
        public const string WORD_FILE = "file";
        public const string WORD_CULTURE = "culture";

        protected const string CFG_SERVICE_USERNAME = WORD_SERVICE + TAG_ATTRIBUTE + WORD_USERNAME;
        protected const string CFG_SERVICE_PASSWORD = WORD_SERVICE + TAG_ATTRIBUTE + WORD_PASSWORD;
        protected const string CFG_SERVICE_PROTOCOL = WORD_SERVICE + TAG_ATTRIBUTE + WORD_PROTOCOL;
        protected const string CFG_SERVICE_NAME = WORD_SERVICE + TAG_ATTRIBUTE + WORD_NAME;
        protected const string CFG_SERVICE_PORT = WORD_SERVICE + TAG_ATTRIBUTE + WORD_PORT;
        protected const string CFG_SERVICE_ADDRESS = WORD_SERVICE + TAG_ATTRIBUTE + WORD_ADDRESS;
        protected const string CFG_SERVICE_CULTURE = WORD_SERVICE + TAG_ATTRIBUTE + WORD_CULTURE;
        #endregion

        #region  " Attributs "
        protected string m_serviceUserName;
        protected string m_servicePassword;
        protected string m_serviceProtocol;
        protected string m_serviceName;
        protected int m_servicePort;
        protected string m_serviceAddress;
        protected string m_serviceCulture;
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// L'utilisateur du tunnel
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string ServiceUserName
        {
            get
            {
                return m_serviceUserName;
            }
            set {
                m_serviceUserName = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le mot de passe du tunnel
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string ServicePassword
        {
            get
            {
                return m_servicePassword;
            }
            set
            {
                m_servicePassword = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le protocole à utiliser
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string ServiceProtocol
        {
            get
            {
                return m_serviceProtocol;
            }
            set
            {
                m_serviceProtocol = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le nom du tunnel
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string ServiceName
        {
            get
            {
                return m_serviceName;
            }
            set
            {
                m_serviceName = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le port du serveur
        /// </summary>
        /// -----------------------------------------------------------------------------
        public int ServicePort
        {
            get
            {
                return m_servicePort;
            }
            set
            {
                m_servicePort = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// L'adresse du serveur
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string ServiceAddress
        {
            get
            {
                return m_serviceAddress;
            }
            set
            {
                m_serviceAddress = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// La culture
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string ServiceCulture
        {
            get
            {
                return m_serviceCulture;
            }
            set
            {
                m_serviceCulture = value;
            }
        }
        #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="config">la configuration</param>
        /// -----------------------------------------------------------------------------
        public SharedConfig (ConfigPackage config)
        {
            m_serviceUserName = config.Value(CFG_SERVICE_USERNAME, string.Empty);
            m_servicePassword = config.Value(CFG_SERVICE_PASSWORD, string.Empty);
            m_serviceProtocol = config.Value(CFG_SERVICE_PROTOCOL, string.Empty);
            m_serviceName = config.Value(CFG_SERVICE_NAME, string.Empty);
            m_servicePort = config.ValueInt(CFG_SERVICE_PORT, 0);
            m_serviceAddress = config.Value(CFG_SERVICE_ADDRESS, string.Empty);
            m_serviceCulture = config.Value(CFG_SERVICE_CULTURE, string.Empty);
        }
        #endregion

	}
}
