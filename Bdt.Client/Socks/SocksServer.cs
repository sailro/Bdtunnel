/* BoutDuTunnel Copyright (c) 2006-2019 Sebastien Lebreton

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
using System.Net.Sockets;
using Bdt.Client.Sockets;
using Bdt.Client.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Service;

namespace Bdt.Client.Socks
{
	public class SocksServer : TcpServer
	{
		private readonly ITunnel _tunnel;
		private readonly int _sid;

		public SocksServer(int port, bool shared, ITunnel tunnel, int sid) : base(port, shared)
		{
			Log(string.Format(Strings.SOCKS_SERVER_STARTED, Ip, port), ESeverity.INFO);

			_tunnel = tunnel;
			_sid = sid;
		}

		protected override void OnNewConnection(TcpClient client)
		{
			GenericSocksHandler handler = null;
			try
			{
				handler = GenericSocksHandler.GetInstance(client);
			}
			catch (Exception ex)
			{
				Log(ex.Message, ESeverity.ERROR);
				Log(ex.ToString(), ESeverity.DEBUG);
			}

			if (handler != null)
			{
// ReSharper disable ObjectCreationAsStatement
				new Gateway(client, _tunnel, _sid, handler.Address, handler.RemotePort);
// ReSharper restore ObjectCreationAsStatement
			}
		}
	}
}
