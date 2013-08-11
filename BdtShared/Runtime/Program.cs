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
using Bdt.Shared.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Configuration;
using Bdt.Shared.Protocol;
#endregion

namespace Bdt.Shared.Runtime
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Une ébauche de programme
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class Program : LoggedObject
    {

        #region " Constantes "
	    private const string CfgLog = SharedConfig.WordLogs + SharedConfig.TagElement;
        protected const string CfgConsole = CfgLog + SharedConfig.WordConsole;
        protected const string CfgFile = CfgLog + SharedConfig.WordFile;
        #endregion

        #region " Attributs "

	    protected BaseLogger ConsoleLogger;
        protected FileLogger FileLogger;
        protected string[] Args;
        #endregion

        #region " Proprietes "

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le protocole de communication
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public GenericProtocol Protocol { get; protected set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le protocole de communication
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public ConfigPackage Configuration { get; protected set; }

	    /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le fichier de configuration
        /// </summary>
        /// -----------------------------------------------------------------------------
        public virtual string ConfigFile
        {
            get
            {
                return string.Format("{0}Cfg.xml", this.GetType().Assembly.GetName().Name);
            }
        }
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Chargement des données de configuration
        /// </summary>
        /// -----------------------------------------------------------------------------
        public void LoadConfiguration()
        {
            LoadConfiguration(Args);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Initialisation des loggers
        /// </summary>
        /// <returns>un MultiLogger lié à une source fichier et console</returns>
        /// -----------------------------------------------------------------------------
        protected virtual BaseLogger CreateLoggers ()
        {
            var ldcConfig = new StringConfig(Args, 0);
            var xmlConfig = new XMLConfig(ConfigFile, 1);
            Configuration = new ConfigPackage();
            Configuration.AddSource(ldcConfig);
            Configuration.AddSource(xmlConfig);

            var log = new MultiLogger();
            ConsoleLogger = new ConsoleLogger(CfgConsole, Configuration);
            FileLogger = new FileLogger(CfgFile, Configuration);
            log.AddLogger(ConsoleLogger);
            log.AddLogger(FileLogger);

            return log;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Chargement des données de configuration
        /// </summary>
        /// <param name="args">Arguments de la ligne de commande</param>
        /// -----------------------------------------------------------------------------
        public virtual void LoadConfiguration(string[] args)
        {
            Args = args;

            GlobalLogger = CreateLoggers();
            Log(Strings.LOADING_CONFIGURATION, ESeverity.DEBUG);
            var cfg = new SharedConfig(Configuration);
            Protocol = GenericProtocol.GetInstance(cfg);
            SetCulture(cfg.ServiceCulture);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fixe la culture courante
        /// </summary>
        /// <param name="name">le nom de la culture</param>
        /// -----------------------------------------------------------------------------
        protected virtual void SetCulture(String name)
        {
	        if (!string.IsNullOrEmpty(name))
		        Strings.Culture = new CultureInfo(name);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Déchargement des données de configuration
        /// </summary>
        /// -----------------------------------------------------------------------------
        public virtual void UnLoadConfiguration()
        {
            Log(Strings.UNLOADING_CONFIGURATION, ESeverity.DEBUG);

            if (ConsoleLogger != null)
            {
                ConsoleLogger.Close();
                ConsoleLogger = null;
            }

            if (FileLogger != null)
            {
                FileLogger.Close();
                FileLogger = null;
            }

            GlobalLogger = null;
            Configuration = null;
            Protocol = null;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Affiche le nom et la version du framework utilisé
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static string FrameworkVersion()
        {
            var plateform = (Type.GetType("Mono.Runtime", false) == null) ? ".NET" : "Mono";
            return string.Format(Strings.POWERED_BY, plateform, Environment.Version);
        }

        /*
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Encodeur simple par Xor pour un tableau d'octets
        /// </summary>
        /// <param name="bytes">Le tableau à encoder/décoder (xor réversible)</param>
        /// <param name="seed">La racine d'initialisation du générateur aléatoire</param>
        /// -----------------------------------------------------------------------------
        public static void RandomXorEncoder (ref byte[] bytes, int seed)
        {
            Random rnd = new Random(seed);
            if (bytes!=null)
            {
                for (int i = 0; i <= bytes.Length - 1; i++)
                {
                    bytes[i] = (byte) (bytes[i] ^ Convert.ToByte(Math.Abs(rnd.Next() % 256)));
                }
            }
        }
        */

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Encodeur simple par Xor pour un tableau d'octets
        /// </summary>
        /// <param name="bytes">Le tableau à encoder/décoder (xor réversible)</param>
        /// <param name="key">La clef de codage</param>
        /// -----------------------------------------------------------------------------
        public static void StaticXorEncoder(ref byte[] bytes, int key)
        {
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = (byte)(bytes[i] ^ Convert.ToByte(key % 256));
                }
            }
        }
        #endregion

    }

}

