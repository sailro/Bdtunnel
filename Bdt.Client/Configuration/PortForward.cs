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

using System;
using Bdt.Shared.Configuration;

namespace Bdt.Client.Configuration
{
	[Serializable]
	public class PortForward
	{
		private const string CfgForwardTemplate = ClientConfig.WordForward + SharedConfig.TagElement + SharedConfig.WordPort + "{0}" + SharedConfig.TagAttribute;

		public bool Enabled { get; private set; }
		public bool Shared { get; private set; }
		public int LocalPort { get; private set; }
		public int RemotePort { get; private set; }
		public string Address { get; private set; }

		public PortForward(ConfigPackage config, int localPort)
		{
			LocalPort = localPort;
			var prefix = string.Format(CfgForwardTemplate, localPort);

			if (config == null)
				return;

			Enabled = config.ValueBool(prefix + ClientConfig.WordEnabled, false);
			Shared = config.ValueBool(prefix + ClientConfig.WordShared, false);
			RemotePort = config.ValueInt(prefix + SharedConfig.WordPort, 0);
			Address = config.Value(prefix + SharedConfig.WordAddress, string.Empty);
		}
	}
}
