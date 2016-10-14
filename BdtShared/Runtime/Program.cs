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
using Bdt.Shared.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Configuration;
using Bdt.Shared.Protocol;

namespace Bdt.Shared.Runtime
{
	public abstract class Program : LoggedObject
	{
		private const string CfgLog = SharedConfig.WordLogs + SharedConfig.TagElement;
		protected const string CfgConsole = CfgLog + SharedConfig.WordConsole;
		protected const string CfgFile = CfgLog + SharedConfig.WordFile;

		protected BaseLogger ConsoleLogger;
		protected FileLogger FileLogger;
		protected string[] Args;

		public GenericProtocol Protocol { get; protected set; }
		public ConfigPackage Configuration { get; protected set; }

		public virtual string ConfigFile
		{
			get { return string.Format("{0}Cfg.xml", GetType().Assembly.GetName().Name); }
		}

		public void LoadConfiguration()
		{
			LoadConfiguration(Args);
		}

		protected virtual BaseLogger CreateLoggers()
		{
			var ldcConfig = new StringConfig(Args, 0);
			var xmlConfig = new XmlConfig(ConfigFile, 1);
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

		public virtual void LoadConfiguration(string[] args)
		{
			Args = args;

			GlobalLogger = CreateLoggers();
			Log(Strings.LOADING_CONFIGURATION, ESeverity.DEBUG);
			var cfg = new SharedConfig(Configuration);
			Protocol = GenericProtocol.GetInstance(cfg);
			SetCulture(cfg.ServiceCulture);
		}

		protected virtual void SetCulture(string name)
		{
			if (!string.IsNullOrEmpty(name))
				Strings.Culture = new CultureInfo(name);
		}

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

		public static string FrameworkVersion()
		{
			var plateform = Type.GetType("Mono.Runtime", false) == null ? ".NET" : "Mono";
			return string.Format(Strings.POWERED_BY, plateform, Environment.Version);
		}

		public static void StaticXorEncoder(ref byte[] bytes, int key)
		{
			if (bytes == null)
				return;

			for (var i = 0; i < bytes.Length; i++)
				bytes[i] = (byte) (bytes[i] ^ Convert.ToByte(key%256));
		}
	}
}