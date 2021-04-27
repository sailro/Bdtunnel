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
using System.Collections;
using System.Collections.Generic;
using Bdt.Server.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Request;
using Bdt.Shared.Response;

namespace Bdt.Server.Service
{
	public sealed class TunnelSession : TimeoutObject
	{
		private const int SocketTestPollingTime = 100; // msec
		private readonly int _connectiontimeoutdelay;

		private Dictionary<int, TunnelConnection> Connections { get; }
		public string Username { get; set; }
		public bool Admin { get; set; }
		public DateTime Logon { get; set; }

		public TunnelSession(int timeoutdelay, int connectiontimeoutdelay) : base(timeoutdelay)
		{
			Connections = new Dictionary<int, TunnelConnection>();
			_connectiontimeoutdelay = connectiontimeoutdelay;
		}

		protected override void Timeout(ILogger logger)
		{
			logger.Log(this, string.Format(Strings.SESSION_TIMEOUT, Username), ESeverity.INFO);
			DisconnectAndRemoveAllConnections();
		}

		protected override bool CheckTimeout(ILogger logger)
		{
			CheckTimeout(logger, Connections);
			return base.CheckTimeout(logger);
		}

// ReSharper disable InconsistentNaming
		internal TunnelConnection CheckConnection<I, O>(ref I request, ref O response)
// ReSharper restore InconsistentNaming
			where I : IConnectionContextRequest
			where O : IConnectionContextResponse
		{
			if (!Connections.TryGetValue(request.Cid, out var connection))
			{
				response.Success = false;
				response.Message = Strings.SERVER_SIDE + Strings.CID_NOT_FOUND;
				return null;
			}

			connection.LastAccess = DateTime.Now;
			try
			{
				response.Connected = !(connection.TcpClient.Client.Poll(SocketTestPollingTime, System.Net.Sockets.SelectMode.SelectRead) && connection.TcpClient.Client.Available == 0);
				response.DataAvailable = connection.TcpClient.Client.Available > 0;
			}
			catch (Exception)
			{
				response.Connected = false;
				response.DataAvailable = false;
			}

			response.Success = true;
			response.Message = string.Empty;
			return connection;
		}

		private int GetNewCid()
		{
			return Tunnel.GetNewId(Connections);
		}

		internal int AddConnection(TunnelConnection connection)
		{
			var cid = GetNewCid();
			Connections.Add(cid, connection);
			return cid;
		}

		internal TunnelConnection CreateConnection()
		{
			return new(_connectiontimeoutdelay);
		}

		public void RemoveConnection(int cid)
		{
			Connections.Remove(cid);
		}

		public void DisconnectAndRemoveAllConnections()
		{
			foreach (int cid in new ArrayList(Connections.Keys))
			{
				var connection = Connections[cid];
				connection.SafeDisconnect();
				RemoveConnection(cid);
			}
		}

		public Connection[] GetConnectionsStruct()
		{
			var result = new List<Connection>();

			foreach (var cid in Connections.Keys)
			{
				var connection = Connections[cid];
				var export = new Connection
				{
					Cid = cid.ToString("x"),
					Address = connection.Address,
					Host = connection.Host,
					Port = connection.Port,
					ReadCount = connection.ReadCount,
					WriteCount = connection.WriteCount,
					LastAccess = connection.LastAccess
				};
				result.Add(export);
			}

			return result.ToArray();
		}
	}
}
