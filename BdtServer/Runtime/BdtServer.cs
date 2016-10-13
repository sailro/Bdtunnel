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
using Bdt.Shared.Runtime;
using Bdt.Shared.Logs;
using Bdt.Server.Service;
using Bdt.Server.Resources;

namespace Bdt.Server.Runtime
{
	public class BdtServer : Program
	{
		public static void Main(string[] args)
		{
			new BdtServer().Run(args);
		}

		protected override void SetCulture(string name)
		{
			base.SetCulture(name);
			if (!string.IsNullOrEmpty(name))
				Strings.Culture = new CultureInfo(name);
		}

		private void Run(string[] args)
		{
			try
			{
				LoadConfiguration(args);

				Log(string.Format(Strings.SERVER_TITLE, GetType().Assembly.GetName().Version.ToString(3)), ESeverity.INFO);
				Log(FrameworkVersion(), ESeverity.INFO);

				Tunnel.Configuration = Configuration;
				Tunnel.Logger = GlobalLogger;
				Protocol.ConfigureServer(typeof(Tunnel));
				Log(Strings.SERVER_STARTED, ESeverity.INFO);
				Console.ReadLine();
				Tunnel.DisableChecking();
				Protocol.UnConfigureServer();

				UnLoadConfiguration();
			}
			catch (Exception ex)
			{
				if (GlobalLogger != null)
				{
					Log(ex.Message, ESeverity.ERROR);
					Log(ex.ToString(), ESeverity.DEBUG);
				}
				else
					Console.WriteLine(ex.Message);
			}
		}
	}
}