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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Bdt.Shared.Logs;
using Bdt.Client.Resources;

namespace Bdt.Client.Sockets
{
	public abstract class TcpServer : LoggedObject
	{
		private const int AcceptPollingTime = 50;

		private readonly TcpListener _listener;
		private readonly ManualResetEvent _mre = new ManualResetEvent(false);

		protected IPAddress Ip { get; private set; }
		private int Port { get; set; }

		protected TcpServer(int port, bool shared)
		{
			Ip = shared ? IPAddress.Any : IPAddress.Loopback;
			Port = port;

			_listener = new TcpListener(Ip, Port);
			var thr = new Thread(ServerThread);
			thr.Start();
		}

		protected abstract void OnNewConnection(TcpClient client);

		public void CloseServer()
		{
			_mre.Set();
			_listener.Stop();
		}

		private void ServerThread()
		{
			try
			{
				_listener.Start();
				while (!_mre.WaitOne(AcceptPollingTime, false))
				{
					try
					{
						var client = _listener.AcceptTcpClient();
						OnNewConnection(client);
					}
					catch (SocketException ex)
					{
						if (ex.SocketErrorCode == SocketError.Interrupted)
							continue;

						Log(ex.Message, ESeverity.ERROR);
						Log(ex.ToString(), ESeverity.DEBUG);
					}
					catch (Exception ex)
					{
						Log(ex.Message, ESeverity.ERROR);
						Log(ex.ToString(), ESeverity.DEBUG);
					}
				}
			}
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
				{
					Log(string.Format(Strings.TCP_SERVER_DISABLED, Port), ESeverity.WARN);
				}
				else
				{
					Log(ex.Message, ESeverity.ERROR);
				}

				Log(ex.ToString(), ESeverity.DEBUG);
			}
			catch (Exception ex)
			{
				Log(ex.Message, ESeverity.ERROR);
				Log(ex.ToString(), ESeverity.DEBUG);
			}
		}
	}
}
