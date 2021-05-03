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
using System.Text;
using Bdt.Shared.Logs;
using Bdt.Client.Resources;

namespace Bdt.Client.Socks
{
	public class Socks4AHandler : Socks4Handler
	{
		protected override bool IsHandled
		{
			get
			{
				Reply[0] = Socks4ReplyVersion;
				Reply[1] = Socks4Ko;
				Array.Clear(Reply, 2, 6);

				if (Version != 4)
					return false;

				if (Buffer[4] != 0 || Buffer[5] != 0 || Buffer[6] != 0)
					return false;

				if (Command != Socks4BindCommand)
				{
					RemotePort = 256 * Convert.ToInt32(Buffer[2]) + Convert.ToInt32(Buffer[3]);
					var position = -1;
					for (var i = 8; i <= Buffer.Length - 1; i++)
					{
						if (Buffer[i] != 0)
							continue;

						position = i;
						break;
					}

					Address = position >= 0 ? new string(Encoding.ASCII.GetChars(Buffer), position + 1, Buffer.Length - position - 2) : string.Empty;

					Reply[1] = Socks4Ok;
					Array.Copy(Buffer, 2, Reply, 2, 2);
					Array.Clear(Reply, 4, 3);
					Reply[7] = Buffer[7];
					Log(Strings.SOCKS4A_REQUEST_HANDLED, ESeverity.DEBUG);
					return true;
				}

				// Socks4 BIND
				Log(Strings.SOCKS_BIND_UNSUPPORTED, ESeverity.WARN);
				return false;
			}
		}

		public Socks4AHandler(byte[] buffer) : base(buffer)
		{
		}
	}
}
