/* BoutDuTunnel Copyright (c) 2007-2016 Sebastien LEBRETON

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

namespace Bdt.GuiClient.Runtime
{
	public class BdtGuiClient : BdtClient
	{
		public override string ConfigFile
		{
			get { return string.Format("{0}Cfg.xml", typeof(BdtClient).Assembly.GetName().Name); }
		}

		public MainComponent MainComponent { get; private set; }

		protected override BaseLogger CreateLoggers()
		{
			var ldcConfig = new StringConfig(Args, 0);
			var xmlConfig = new XMLConfig(ConfigFile, 1);
			Configuration = new ConfigPackage();
			Configuration.AddSource(ldcConfig);
			Configuration.AddSource(xmlConfig);

			var log = new MultiLogger();
			ConsoleLogger = new NotifyIconLogger(CfgConsole, Configuration, this, GetType().Assembly.GetName().Name, 1);
			FileLogger = new FileLogger(CfgFile, Configuration);
			log.AddLogger(ConsoleLogger);
			log.AddLogger(FileLogger);

			return log;
		}

		protected override void InputProxyCredentials(IProxyCompatible proxyProtocol, ref bool retry)
		{
			MainComponent.InputProxyCredentials(proxyProtocol, ref retry);
		}

		protected override void SetCulture(string name)
		{
			base.SetCulture(name);
			if (!string.IsNullOrEmpty(name))
				Resources.Strings.Culture = new CultureInfo(name);
		}

		[STAThread]
		private new static void Main(string[] args)
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

		private void HandleError(object sender, UnhandledExceptionEventArgs e)
		{
			HandleError((Exception) e.ExceptionObject);
		}

		private void HandleError(object sender, ThreadExceptionEventArgs e)
		{
			HandleError(e.Exception);
		}

		private void HandleError(Exception e)
		{
			if (GlobalLogger != null)
			{
				Log(e.Message, ESeverity.ERROR);
				Log(e.ToString(), ESeverity.DEBUG);
			}
			else
				MessageBox.Show(e.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		protected override void Run(string[] args)
		{
#pragma warning disable 612,618
			ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
#pragma warning restore 612,618

			LoadConfiguration(args);
			MainComponent = new MainComponent(this);
			Application.Run();
			UnLoadConfiguration();
		}
	}
}