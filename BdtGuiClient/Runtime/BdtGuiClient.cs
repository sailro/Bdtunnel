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
	    public MainComponent MainComponent { get; private set; }

	    #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Initialisation des loggers
        /// </summary>
        /// <returns>un MultiLogger lié à une source fichier et console</returns>
        /// -----------------------------------------------------------------------------
        protected override BaseLogger CreateLoggers ()
        {
            var ldcConfig = new StringConfig(Args, 0);
            var xmlConfig = new XMLConfig(ConfigFile, 1);
            Configuration = new ConfigPackage();
            Configuration.AddSource(ldcConfig);
            Configuration.AddSource(xmlConfig);

            var log = new MultiLogger();
            // on utilise le référence d'un BdtGuiClient au lieu de passer directement un NotifyIcon car à ce stade
            // on ne peut pas créer de formulaire, car la Culture serait incorrecte, le fichier de configuration
            // n'étant pas déjà parsé
            ConsoleLogger = new NotifyIconLogger(CfgConsole, Configuration, this, GetType().Assembly.GetName().Name, 1);
            FileLogger = new FileLogger(CfgFile, Configuration);
            log.AddLogger(ConsoleLogger);
            log.AddLogger(FileLogger);

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
            MainComponent.InputProxyCredentials(proxyProtocol, ref retry);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fixe la culture courante
        /// </summary>
        /// <param name="name">le nom de la culture</param>
        /// -----------------------------------------------------------------------------
        protected override void SetCulture(String name)
        {
            base.SetCulture(name);
	        if (!string.IsNullOrEmpty(name))
		        Resources.Strings.Culture = new CultureInfo(name);
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
            var guiclient = new BdtGuiClient();
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.ThreadException += guiclient.HandleError;
                AppDomain.CurrentDomain.UnhandledException += guiclient.HandleError;

                guiclient.Run(args);
            }
            catch (Exception e)
            {
	            guiclient.HandleError(e);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gestion des erreurs
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void HandleError(Object sender, UnhandledExceptionEventArgs e)
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
        private void HandleError(Object sender, ThreadExceptionEventArgs e)
        {
            HandleError(e.Exception);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gestion des erreurs
        /// </summary>
        /// <param name="e">l'erreur à traiter</param>
        /// -----------------------------------------------------------------------------
        private void HandleError(Exception e)
        {
	        if (GlobalLogger != null)
	        {
		        Log(e.Message, ESeverity.ERROR);
		        Log(e.ToString(), ESeverity.DEBUG);
	        }
	        else
		        MessageBox.Show(e.Message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Traitement principal
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected override void Run (string[] args)
        {
#pragma warning disable 612,618
            ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
#pragma warning restore 612,618

            LoadConfiguration(args);
            MainComponent = new MainComponent(this);
            Application.Run();
            UnLoadConfiguration();
        }
        #endregion

    }
}