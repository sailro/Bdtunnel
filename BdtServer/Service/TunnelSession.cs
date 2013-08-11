/* BoutDuTunnel Copyright (c) 2007-2010 Sebastien LEBRETON

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

#region " Inclusions "
using System;
using System.Collections;
using System.Collections.Generic;
using Bdt.Server.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Request;
using Bdt.Shared.Response;
#endregion

namespace Bdt.Server.Service
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Une session utilisateur au sein du tunnel
    /// </summary>
    /// -----------------------------------------------------------------------------
    public sealed class TunnelSession : TimeoutObject 
    {
        #region " Constantes "
        // Le test de la connexion effective
	    private const int SocketTestPollingTime = 100; // msec
        #endregion

        #region " Attributs "
	    private readonly int _connectiontimeoutdelay;
	    #endregion

        #region " Proprietes "

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Les connexions
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    private Dictionary<int, TunnelConnection> Connections { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le nom associé
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public string Username { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Utilisateur en mode admin
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public bool Admin { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// La date de login
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public DateTime Logon { get; set; }

	    #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="timeoutdelay">valeur du timeout de session</param>
        /// <param name="connectiontimeoutdelay">valeur du timeout de connexion</param>
        /// -----------------------------------------------------------------------------
        public TunnelSession(int timeoutdelay, int connectiontimeoutdelay)
            : base(timeoutdelay)
        {
	        Connections = new Dictionary<int, TunnelConnection>();
	        _connectiontimeoutdelay = connectiontimeoutdelay;
        }

	    /// -----------------------------------------------------------------------------
        /// <summary>
        /// Timeout de l'objet
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected override void Timeout(ILogger logger)
        {
            logger.Log(this, String.Format(Strings.SESSION_TIMEOUT, Username), ESeverity.INFO);
            DisconnectAndRemoveAllConnections();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Vérification de timeout de l'objet
        /// </summary>
        /// <returns>true en cas de timeout</returns>
        /// -----------------------------------------------------------------------------
        protected override bool CheckTimeout(ILogger logger)
        {
            CheckTimeout(logger, Connections);
            return base.CheckTimeout(logger);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Vérification de la connexion associée à une requête
        /// </summary>
        /// <param name="request">la requête</param>
        /// <param name="response">la réponse à préparer</param>
        /// <returns>La connexion si la connexion est valide</returns>
        /// -----------------------------------------------------------------------------
// ReSharper disable InconsistentNaming
        internal TunnelConnection CheckConnection<I, O>(ref I request, ref O response)
// ReSharper restore InconsistentNaming
            where I : IConnectionContextRequest
            where O : IConnectionContextResponse
        {
            TunnelConnection connection;
            if (!Connections.TryGetValue(request.Cid, out connection))
            {
                response.Success = false;
                response.Message = Strings.SERVER_SIDE + Strings.CID_NOT_FOUND;
                return null;
            }
	        
			connection.LastAccess = DateTime.Now;
	        try
	        {
		        response.Connected = (!(connection.TcpClient.Client.Poll(SocketTestPollingTime, System.Net.Sockets.SelectMode.SelectRead) && connection.TcpClient.Client.Available == 0));
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

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Generation d'un identifiant de connexion unique
        /// </summary>
        /// <returns>un entier unique</returns>
        /// -----------------------------------------------------------------------------
        private int GetNewCid()
        {
            return Tunnel.GetNewId(Connections);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ajoute une nouvelle connexion à la table des connexions
        /// </summary>
        /// <param name="connection">la connexion à ajouter</param>
        /// <returns>le jeton de connexion</returns>
        /// -----------------------------------------------------------------------------
        internal int AddConnection(TunnelConnection connection)
        {
            var cid = GetNewCid();
            Connections.Add(cid, connection);
            return cid;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Création d'une nouvelle connexion
        /// </summary>
        /// <returns>la connexion</returns>
        /// -----------------------------------------------------------------------------
        internal TunnelConnection CreateConnection()
        {
            return new TunnelConnection(_connectiontimeoutdelay);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Supprime une connexion de table des connexions
        /// </summary>
        /// <param name="cid">le jeton de connexion à supprimer</param>
        /// -----------------------------------------------------------------------------
        public void RemoveConnection(int cid)
        {
            Connections.Remove(cid);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Deconnexion de toutes les connexions sous cette session
        /// </summary>
        /// -----------------------------------------------------------------------------
        public void DisconnectAndRemoveAllConnections()
        {
            foreach (int cid in new ArrayList(Connections.Keys))
            {
                var connection = Connections[cid];
                connection.SafeDisconnect();
                RemoveConnection(cid);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retourne toutes les connexions sous forme "structure" pour l'export par ex
        /// </summary>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
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
        #endregion
    }

}
