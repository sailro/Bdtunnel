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
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Net;

using Bdt.Client.Runtime;
using Bdt.Client.Configuration;
using Bdt.GuiClient.Forms;
using Bdt.Shared.Logs;
using Bdt.Shared.Configuration;
using Bdt.GuiClient.Logs;
using Bdt.Shared.Protocol;
#endregion

namespace Bdt.GuiClient.Runtime
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Un client Bdt lié à un système graphique
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class BdtGuiClient : BdtClient
    {

        #region " Attributs "
        protected MainComponent m_mainComponent;
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le fichier de configuration
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override string ConfigFile
        {
            get
            {
                return string.Format("{0}Cfg.xml", typeof(BdtClient).Assembly.GetName().Name);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// La fenetre principale
        /// </summary>
        /// -----------------------------------------------------------------------------
        public MainComponent MainComponent
        {
            get
            {
                return m_mainComponent;
            }
        }
        #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Initialisation des loggers
        /// </summary>
        /// <returns>un MultiLogger lié à une source fichier et console</returns>
        /// -----------------------------------------------------------------------------
        public override BaseLogger CreateLoggers ()
        {
            StringConfig ldcConfig = new StringConfig(m_args, 0);
            XMLConfig xmlConfig = new XMLConfig(ConfigFile, 1);
            m_config = new ConfigPackage();
            m_config.AddSource(ldcConfig);
            m_config.AddSource(xmlConfig);

            MultiLogger log = new MultiLogger();
            // on utilise le référence d'un BdtGuiClient au lieu de passer directement un NotifyIcon car à ce stade
            // on ne peut pas créer de formulaire, car la Culture serait incorrecte, le fichier de configuration
            // n'étant pas déjà parsé
            m_consoleLogger = new NotifyIconLogger(CFG_CONSOLE, m_config, this, this.GetType().Assembly.GetName().Name, 1);
            m_fileLogger = new Bdt.Shared.Logs.FileLogger(CFG_FILE, m_config);
            log.AddLogger(m_consoleLogger);
            log.AddLogger(m_fileLogger);

            return log;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Saisie des informations d'authentification sur le proxy
        /// </summary>
        /// <param name="proxyProtocol">le protocol IProxyCompatible à alterer</param>
        /// <param name="retry">pour permettre les essais multiples</param>
        /// -----------------------------------------------------------------------------
        protected override void InputProxyCredentials (IProxyCompatible proxyProtocol, ref bool retry)
        {
            m_mainComponent.InputProxyCredentials(proxyProtocol, ref retry);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fixe la culture courante
        /// </summary>
        /// <param name="name">le nom de la culture</param>
        /// -----------------------------------------------------------------------------
        public override void SetCulture(String name)
        {
            base.SetCulture(name);
            if ((name != null) && (name != String.Empty))
            {
                Bdt.GuiClient.Resources.Strings.Culture = new CultureInfo(name);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        /// <param name="args">les arguments de la ligne de commande</param>
        /// -----------------------------------------------------------------------------
        [STAThread]
        static new void Main(string[] args)
        {
            BdtGuiClient guiclient = new BdtGuiClient();
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.ThreadException += new ThreadExceptionEventHandler(guiclient.HandleError);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(guiclient.HandleError);

                guiclient.Run(args);
            }
            catch (Exception e)
            {
                if (guiclient != null)
                {
                    guiclient.HandleError(e);
                }
            }

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gestion des erreurs
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void HandleError(Object sender, UnhandledExceptionEventArgs e)
        {
            HandleError((Exception) e.ExceptionObject);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gestion des erreurs
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void HandleError(Object sender, ThreadExceptionEventArgs e)
        {
            HandleError(e.Exception);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gestion des erreurs
        /// </summary>
        /// <param name="e">l'erreur à traiter</param>
        /// -----------------------------------------------------------------------------
        protected void HandleError(Exception e)
        {
                if (LoggedObject.GlobalLogger != null)
                {
                    this.Log(e.Message, ESeverity.ERROR);
                    this.Log(e.ToString(), ESeverity.DEBUG);
                }
                else
                {
                    MessageBox.Show(e.Message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Traitement principal
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected override void Run (string[] args)
        {
#pragma warning disable
            ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
#pragma warning restore

            LoadConfiguration(args);
            m_mainComponent = new MainComponent(this);
            Application.Run();
            UnLoadConfiguration();
        }
        #endregion

    }
}