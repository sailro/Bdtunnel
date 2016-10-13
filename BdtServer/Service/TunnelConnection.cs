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

using System;
using System.Net.Sockets;
using Bdt.Server.Resources;
using Bdt.Shared.Logs;

namespace Bdt.Server.Service
{
	internal class TunnelConnection : TimeoutObject
	{
		public string Address { get; set; }
		public int Port { get; set; }
		public string Host { get; set; }
		public TcpClient TcpClient { get; set; }
		public NetworkStream Stream { get; set; }
		public int ReadCount { get; set; }
		public int WriteCount { get; set; }

		public TunnelConnection(int timeoutdelay) : base(timeoutdelay)
		{
		}

		protected override void Timeout(ILogger logger)
		{
			logger.Log(this, String.Format(Strings.CONNECTION_TIMEOUT, TcpClient.Client.RemoteEndPoint), ESeverity.INFO);
			SafeDisconnect();
		}

		public void SafeDisconnect()
		{
			try
			{
				if (Stream != null)
				{
					Stream.Flush();
					Stream.Close();
				}
			}
// ReSharper disable EmptyGeneralCatchClause
			catch (Exception)
			{
			}
// ReSharper restore EmptyGeneralCatchClause

			try
			{
				if (TcpClient != null)
					TcpClient.Close();
			}
// ReSharper disable EmptyGeneralCatchClause
			catch (Exception)
			{
			}
// ReSharper restore EmptyGeneralCatchClause

			Stream = null;
			TcpClient = null;
		}
	}
}