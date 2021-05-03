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
using Bdt.Shared.Service;

namespace Bdt.Shared.Protocol
{
	public abstract class GenericProtocol : Logs.LoggedObject
	{
		protected string Name { get; private set; }
		protected int Port { get; private set; }
		protected string Address { get; private set; }

		public abstract void ConfigureServer(Type type);
		public abstract void ConfigureClient();
		public abstract void UnConfigureClient();
		public abstract void UnConfigureServer();

		public abstract ITunnel GetTunnel();

		public static GenericProtocol GetInstance(SharedConfig config)
		{
			var protoObj = (GenericProtocol)typeof(GenericProtocol).Assembly.CreateInstance(config.ServiceProtocol);
			if (protoObj == null)
				throw new NotSupportedException(config.ServiceProtocol);

			protoObj.Name = config.ServiceName;
			protoObj.Port = config.ServicePort;
			protoObj.Address = config.ServiceAddress;
			return protoObj;
		}
	}
}
