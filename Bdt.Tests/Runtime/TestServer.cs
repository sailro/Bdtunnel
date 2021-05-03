/* BoutDuTunnel Copyright (c) 2006-2021 Sebastien Lebreton

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

using Bdt.Server.Runtime;
using Bdt.Shared.Logs;
using Bdt.Shared.Configuration;
using Bdt.Tests.Logs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bdt.Shared.Protocol;

namespace Bdt.Tests.Runtime
{
	internal class TestServer : BdtServer
	{
		private readonly TestContext _context;
		private readonly SharedConfig _servercfg;

		protected override BaseLogger CreateLoggers()
		{
			return new TestContextLogger(_context);
		}

		public override void LoadConfiguration(string[] args)
		{
			Args = args;
			GlobalLogger = CreateLoggers();
			Log(Shared.Resources.Strings.LOADING_CONFIGURATION, ESeverity.DEBUG);
			Protocol = GenericProtocol.GetInstance(_servercfg);
			SetCulture(_servercfg.ServiceCulture);
		}

		public TestServer(TestContext context, ConfigPackage config)
		{
			_context = context;
			_servercfg = new SharedConfig(config);
			Configuration = config;
		}
	}
}
