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
	public abstract class GenericSocksHandler : LoggedObject
	{
		protected const int BufferSize = 32768;

		protected abstract bool IsHandled { get; }

		protected abstract byte[] Reply { get; set; }

		protected int Version { get; set; }
		protected int Command { get; set; }
		public int RemotePort { get; protected set; }
		public string Address { get; protected set; }
		protected byte[] Buffer { get; private set; }

		protected GenericSocksHandler(byte[] buffer)
		{
			Buffer = buffer;
		}

		public static GenericSocksHandler GetInstance(TcpClient client)
		{
			var buffer = new byte[BufferSize];

			var stream = client.GetStream();
			var size = stream.Read(buffer, 0, BufferSize);
			Array.Resize(ref buffer, size);

			if (size < 3)
				throw new ArgumentException(Strings.INVALID_SOCKS_HANDSHAKE);

			GenericSocksHandler result = new Socks4Handler(buffer);
			if (!result.IsHandled)
			{
				result = new Socks4AHandler(buffer);
				if (!result.IsHandled)
				{
					result = new Socks5Handler(client, buffer);
					if (!result.IsHandled)
						throw (new ArgumentException(Strings.NO_VALID_SOCKS_HANDLER));
				}
			}

			var reply = result.Reply;
			client.GetStream().Write(reply, 0, reply.Length);

			return result;
		}
	}
}
