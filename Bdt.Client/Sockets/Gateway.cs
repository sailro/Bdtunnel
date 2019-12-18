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
using System.Threading;
using Bdt.Shared.Logs;
using Bdt.Shared.Service;
using Bdt.Shared.Request;
using Bdt.Shared.Response;

namespace Bdt.Client.Sockets
{
	public class Gateway : LoggedObject
	{
		private const int BufferSize = 32768;
		private const int StatePollingMinTime = 10;
		private const int StatePollingMaxTime = 5000;
		private const double StatePollingFactor = 1.1;
		private const int SocketTestPollingTime = 100;

		private TcpClient _client;
		private NetworkStream _stream;
		private readonly ManualResetEvent _mre = new ManualResetEvent(false);
		private readonly ITunnel _tunnel;
		private readonly int _sid;
		private readonly string _address;
		private readonly int _port;
		private int _cid;

		public Gateway(TcpClient client, ITunnel tunnel, int sid, string address, int port)
		{
			_client = client;
			_tunnel = tunnel;
			_sid = sid;
			_stream = client.GetStream();
			_address = address;
			_port = port;

			var thr = new Thread(CommunicationThread) {IsBackground = true};
			thr.Start();
		}

		private void HandleError(IMinimalResponse response)
		{
			HandleError(response.Message, true);
		}

		private void HandleError(Exception ex, bool show)
		{
			HandleError(ex.Message, show);
		}

		private void HandleError(string message, bool show)
		{
			if (show)
				Log(message, ESeverity.ERROR);

			_mre.Set();
		}

		private void HandleState(IConnectionContextResponse response)
		{
			if (!response.Connected)
				_mre.Set();
		}

		private static int WaitTime(int polltime, int adjpolltime)
		{
			return adjpolltime > polltime ? 0 : Math.Max(polltime - adjpolltime, StatePollingMinTime);
		}

		private void CommunicationThread()
		{
			var buffer = new byte[BufferSize];
			var polltime = StatePollingMinTime;
			var adjpolltime = 0;

			var response = _tunnel.Connect(new ConnectRequest(_sid, _address, _port));
			Log(response.Message, ESeverity.INFO);

			if (!response.Success)
				return;

			_cid = response.Cid;

			while (!_mre.WaitOne(WaitTime(polltime, adjpolltime), false))
			{
				var startmarker = DateTime.Now;

				var isConnected = false;
				var isDataAvailAble = false;

				try
				{
					isConnected = !(_client.Client.Poll(SocketTestPollingTime, SelectMode.SelectRead) && _client.Client.Available == 0);
					isDataAvailAble = _stream.DataAvailable;
				}
				catch (Exception ex)
				{
					HandleError(ex, false);
				}

				if (isConnected)
				{
					if (isDataAvailAble)
					{
						var count = 0;
						try
						{
							count = _stream.Read(buffer, 0, BufferSize);
						}
						catch (Exception ex)
						{
							HandleError(ex, true);
						}

						if (count > 0)
						{
							var transBuffer = new byte[count];
							Array.Copy(buffer, transBuffer, count);
							Shared.Runtime.Program.StaticXorEncoder(ref transBuffer, _cid);
							IConnectionContextResponse writeResponse = _tunnel.Write(new WriteRequest(_sid, _cid, transBuffer));
							if (writeResponse.Success)
								HandleState(writeResponse);
							else
								HandleError(writeResponse);

							polltime = StatePollingMinTime;
						}
					}
					else
					{
						polltime = Convert.ToInt32(Math.Round(StatePollingFactor * polltime));
						polltime = Math.Min(polltime, StatePollingMaxTime);
					}

					var readResponse = _tunnel.Read(new ConnectionContextRequest(_sid, _cid));
					if (readResponse.Success)
					{
						if (readResponse.Connected && readResponse.DataAvailable)
						{
							var result = readResponse.Data;
							Shared.Runtime.Program.StaticXorEncoder(ref result, _cid);
							try
							{
								_stream.Write(result, 0, result.Length);
							}
							catch (Exception ex)
							{
								HandleError(ex, true);
							}

							polltime = StatePollingMinTime;
						}
						else
							HandleState(readResponse);
					}
					else
						HandleError(readResponse);
				}
				else
				{
					_mre.Set();
				}

				adjpolltime = Convert.ToInt32(DateTime.Now.Subtract(startmarker).TotalMilliseconds);
			}

			Disconnect();
		}

		private void Disconnect()
		{
			if (_client == null)
				return;

			_stream.Close();
			_client.Close();

			var response = _tunnel.Disconnect(new ConnectionContextRequest(_sid, _cid));
			Log(response.Message, ESeverity.INFO);
			_stream = null;
			_client = null;
		}
	}
}
