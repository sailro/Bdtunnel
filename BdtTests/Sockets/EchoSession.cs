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

namespace Bdt.Tests.Sockets
{
	public class EchoSession : LoggedObject
	{
		private const int BufferSize = 65536;
		private const int StatePollingMinTime = 10;
		private const int StatePollingMaxTime = 5000;
		private const double StatePollingFactor = 1.1;
		private const int SocketTestPollingTime = 100;

		private TcpClient _client;
		private NetworkStream _stream;
		private readonly ManualResetEvent _mre = new ManualResetEvent(false);

		public EchoSession(TcpClient client)
		{
			_client = client;
			_stream = client.GetStream();

			var thr = new Thread(CommunicationThread) {IsBackground = true};
			thr.Start();
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

		private static int WaitTime(int polltime, int adjpolltime)
		{
			return adjpolltime > polltime ? 0 : Math.Max(polltime - adjpolltime, StatePollingMinTime);
		}


		private void CommunicationThread()
		{
			var buffer = new byte[BufferSize];
			var polltime = StatePollingMinTime;
			var adjpolltime = 0;

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
							try
							{
								_stream.Write(buffer, 0, count);
								_stream.Flush();
							}
							catch (Exception ex)
							{
								HandleError(ex, true);
							}

							polltime = StatePollingMinTime;
						}
					}
					else
					{
						polltime = Convert.ToInt32(Math.Round(StatePollingFactor * polltime));
						polltime = Math.Min(polltime, StatePollingMaxTime);
					}
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
			_stream = null;
			_client = null;
		}
	}
}
