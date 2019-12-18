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
using Bdt.Shared.Logs;
using Bdt.Client.Resources;

namespace Bdt.Client.Socks
{
	public class Socks5Handler : GenericSocksHandler
	{
		private const int Socks5NoAuthenticationRequired = 0;
		private const int Socks5NoAcceptableMethods = 255;

		private const int Socks5ConnectCommand = 1;
		private const int Socks5BindCommand = 2;
		private const int Socks5UdpAssociateCommand = 3;

		private const int Socks5Ipv4 = 1;
		private const int Socks5Domain = 3;
		private const int Socks5Ipv6 = 4;

		private const int Socks5Ok = 0;
		private const int Socks5Ko = 1;
		private const int Socks5ReplyVersion = 5;

		private const int ReplySize = 10;

		private readonly NetworkStream _stream;

		protected override bool IsHandled
		{
			get
			{
				Reply[0] = Socks5ReplyVersion;
				Reply[1] = Socks5Ko;
				Reply[2] = 0;
				Reply[3] = Socks5Ipv4;
				Array.Clear(Reply, 4, 6);

				if (Version != 5)
					return false;

				int numMethods = Buffer[1];
				var methodAccepted = false;

				if (numMethods <= 0 || numMethods != Buffer.Length - 2)
				{
					Log(Strings.SOCKS5_MALFORMED_METHOD_ENUM, ESeverity.WARN);
					return false;
				}

				var handshake = new byte[2];
				var i = 0;
				while (i < numMethods && !methodAccepted)
				{
					methodAccepted = (Buffer[i + 2] == Socks5NoAuthenticationRequired);
					i += 1;
				}

				handshake[0] = 5; // version

				if (!methodAccepted)
				{
					Log(Strings.SOCKS5_NO_AUTHENTICATION_SUPPORTED, ESeverity.WARN);
					handshake[1] = Socks5NoAcceptableMethods;
					_stream.Write(handshake, 0, handshake.Length);
					return false;
				}

				handshake[1] = Socks5NoAuthenticationRequired;
				_stream.Write(handshake, 0, handshake.Length);

				var request = new byte[BufferSize];
				_stream.Read(request, 0, request.Length);

				Version = request[0];
				Command = request[1];
				int addressType = request[3];

				if (Version != 5)
				{
					Log(Strings.SOCKS5_BAD_VERSION, ESeverity.WARN);
					return false;
				}

				switch (Command)
				{
					case Socks5ConnectCommand:
						switch (addressType)
						{
							case Socks5Ipv4:
								RemotePort = 256 * Convert.ToInt32(request[8]) + Convert.ToInt32(request[9]);
								Address = request[4] + "." + request[5] + "." + request[6] + "." + request[7];
								Reply[1] = Socks5Ok;
								Array.Copy(request, 4, Reply, 4, 6);
								Log(Strings.SOCKS5_REQUEST_HANDLED, ESeverity.DEBUG);
								return true;
							case Socks5Domain:
								int length = request[4];
								Address = new string(System.Text.Encoding.ASCII.GetChars(request), 5, length);
								RemotePort = 256 * Convert.ToInt32(request[length + 5]) + Convert.ToInt32(request[length + 6]);
								Reply[1] = Socks5Ok;
								Array.Clear(Reply, 4, 6);
								Log(Strings.SOCKS5_REQUEST_HANDLED, ESeverity.DEBUG);
								return true;
							case Socks5Ipv6:
								Log(Strings.SOCKS5_IPV6_UNSUPPORTED, ESeverity.WARN);
								break;
							default:
								Log(Strings.SOCKS5_ADDRESS_TYPE_UNKNOWN, ESeverity.WARN);
								break;
						}

						break;
					case Socks5BindCommand:
						Log(Strings.SOCKS_BIND_UNSUPPORTED, ESeverity.WARN);
						break;
					case Socks5UdpAssociateCommand:
						Log(Strings.SOCKS5_UDP_UNSUPPORTED, ESeverity.WARN);
						break;
					default:
						Log(Strings.SOCKS5_UNKNOWN_COMMAND, ESeverity.WARN);
						break;
				}

				return false;
			}
		}

		protected sealed override byte[] Reply { get; set; }

		public Socks5Handler(TcpClient client, byte[] buffer) : base(buffer)
		{
			Reply = new byte[ReplySize];
			Version = buffer[0];
			Command = buffer[1];
			_stream = client.GetStream();
		}
	}
}
