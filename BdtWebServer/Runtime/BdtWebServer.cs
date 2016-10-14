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
using System.IO;
using System.Web;
using Bdt.Server.Service;
using Bdt.Server.Runtime;
using Bdt.Shared.Configuration;
using Bdt.Shared.Logs;
using Bdt.Shared.Runtime;

namespace Bdt.WebServer.Runtime
{
	public class BdtWebServer : Program
	{
		private readonly HttpServerUtility _server;

		public override string ConfigFile
		{
			get { return _server.MapPath(string.Format("App_Data" + Path.DirectorySeparatorChar + "{0}Cfg.xml", typeof(BdtServer).Assembly.GetName().Name)); }
		}

		public BdtWebServer(HttpServerUtility server)
		{
			_server = server;
		}

		protected override void SetCulture(string name)
		{
			base.SetCulture(name);
			if (!string.IsNullOrEmpty(name))
				Server.Resources.Strings.Culture = new CultureInfo(name);
		}

		public override void LoadConfiguration(string[] args)
		{
			Args = args;

			GlobalLogger = CreateLoggers();
			Log(Shared.Resources.Strings.LOADING_CONFIGURATION, ESeverity.DEBUG);
			var cfg = new SharedConfig(Configuration);
			// unneeded in IIs web hosting model, see web.config
			// m_protocol = GenericProtocol.GetInstance(cfg);
			SetCulture(cfg.ServiceCulture);
		}

		public void Start()
		{
			LoadConfiguration(new String[] {});

			Log(string.Format(Server.Resources.Strings.SERVER_TITLE, GetType().Assembly.GetName().Version.ToString(3)), ESeverity.INFO);
			Log(FrameworkVersion(), ESeverity.INFO);

			Tunnel.Configuration = Configuration;
			Tunnel.Logger = GlobalLogger;

			// unneeded in IIs web hosting model, see web.config
			// server.Protocol.ConfigureServer(typeof(Tunnel));
			Log(Server.Resources.Strings.SERVER_STARTED, ESeverity.INFO);
		}

		protected override BaseLogger CreateLoggers()
		{
			var xmlConfig = new XMLConfig(ConfigFile, 1);
			Configuration = new ConfigPackage();
			Configuration.AddSource(xmlConfig);

			// Map the path to the current Web Application
			const string key = CfgFile + Shared.Configuration.BaseConfig.SourceItemAttribute + FileLogger.ConfigFilename;
			var filename = xmlConfig.Value(key, null);
			if ((filename != null) && (!Path.IsPathRooted(filename)))
				xmlConfig.SetValue(key, _server.MapPath("App_Data" + Path.DirectorySeparatorChar + filename));

			var log = new MultiLogger();
			ConsoleLogger = new ConsoleLogger(CfgConsole, Configuration);
			FileLogger = new FileLogger(CfgFile, Configuration);
			log.AddLogger(ConsoleLogger);
			log.AddLogger(FileLogger);

			return log;
		}

		public void Stop()
		{
			Tunnel.DisableChecking();
			Protocol.UnConfigureServer();
			UnLoadConfiguration();
		}
	}
}