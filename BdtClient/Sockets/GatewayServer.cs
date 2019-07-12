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

using System.Net.Sockets;
using Bdt.Shared.Logs;
using Bdt.Shared.Service;
using Bdt.Client.Resources;

namespace Bdt.Client.Sockets
{
	public class GatewayServer : TcpServer
	{
		private readonly ITunnel _tunnel;
		private readonly string _address;
		private readonly int _remoteport;
		private readonly int _sid;

		public GatewayServer(int localport, bool shared, int remoteport, string address, ITunnel tunnel, int sid) : base(localport, shared)
		{
			Log(string.Format(Strings.FORWARDING, Ip, localport, address, remoteport), ESeverity.INFO);

			_tunnel = tunnel;
			_sid = sid;
			_address = address;
			_remoteport = remoteport;
		}

		protected override void OnNewConnection(TcpClient client)
		{
// ReSharper disable ObjectCreationAsStatement
			new Gateway(client, _tunnel, _sid, _address, _remoteport);
// ReSharper restore ObjectCreationAsStatement
		}
	}
}
