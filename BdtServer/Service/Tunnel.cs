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
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

using Bdt.Server.Resources;
using Bdt.Shared.Configuration;
using Bdt.Shared.Logs;
using Bdt.Shared.Request;
using Bdt.Shared.Response;
using Bdt.Shared.Service;
using System.Net;
using Bdt.Shared.Runtime;
#endregion

namespace Bdt.Server.Service
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Le tunnel, assure les services de l'interface ITunnel
    /// </summary>
    /// -----------------------------------------------------------------------------
    public sealed class Tunnel : MarshalByRefObject, ITunnel, ILogger
    {

        #region " Constantes "
	    private const int BufferSize = 32768;
	    private const int PollingTime = 1000 * 60; // msec -> 1m

	    private const string ConfigUserTemplate = "users/{0}@";
	    private const string ConfigUserEnabled = "enabled";
	    private const string ConfigUserPassword = "password";
	    private const string ConfigUserAdmin = "admin";
	    private const string ConfigSessionTimeout = "stimeout";
	    private const string ConfigConnectionTimeout = "ctimeout";

	    private const int DefaultConnectionTimeoutDelay = 1; // heures
	    private const int DefaultSessionTimeoutDelay = 12; // heures
        #endregion

        #region " Attributs "
	    private static readonly ManualResetEvent Mre = new ManualResetEvent(false);
	    #endregion

        #region " Proprietes "

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Les sessions
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    private Dictionary<int, TunnelSession> Sessions { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// La configuration serveur (d'après le fichier xml + ligne de commande)
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public static ConfigPackage Configuration { private get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le loggeur
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public static ILogger Logger { private get; set; }

	    #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// -----------------------------------------------------------------------------
        public Tunnel()
        {
	        Sessions = new Dictionary<int, TunnelSession>();
	        var thr = new Thread(CheckerThread);
            thr.Start();
        }

	    static Tunnel()
	    {
		    Logger = null;
	    }

	    /// -----------------------------------------------------------------------------
        /// <summary>
        /// Generation d'id non encore présent dans une table de hash
        /// </summary>
        /// <param name="hash">la table à analyser</param>
        /// <returns>un entier unique</returns>
        /// -----------------------------------------------------------------------------
        internal static int GetNewId(System.Collections.IDictionary hash)
        {
            var rnd = new Random();
            var key = rnd.Next(0, int.MaxValue);
		    
			while (hash.Contains(key))
			    key += 1;
		    
			return key;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Generation d'un identifiant session unique
        /// </summary>
        /// <returns>un entier unique</returns>
        /// -----------------------------------------------------------------------------
        private int GetNewSid()
        {
            return GetNewId(Sessions);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Destructeur
        /// </summary>
        /// -----------------------------------------------------------------------------
        ~Tunnel()
        {
            DisableChecking();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Annule la vérification
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void DisableChecking()
        {
            Mre.Set();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fixe une durée de vie au tunnel
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override object InitializeLifetimeService()
        {
            return null;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Traitement principal de thread de vérification des connexions et sessions
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void CheckerThread()
        {
	        while (!Mre.WaitOne(PollingTime, false))
		        TimeoutObject.CheckTimeout(this, Sessions);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Vérification de l'authentification d'un utilisateur associé à une requête
        /// </summary>
        /// <param name="request">la requête</param>
        /// <param name="response">la réponse à préparer</param>
        /// <returns>La session si l'utilisateur est authentifié</returns>
        /// -----------------------------------------------------------------------------
// ReSharper disable InconsistentNaming
        private TunnelSession CheckSession<I, O> (ref I request, ref O response)
// ReSharper restore InconsistentNaming
            where I : ISessionContextRequest
            where O : IMinimalResponse
        {
            TunnelSession session;
            if (!Sessions.TryGetValue(request.Sid, out session))
            {
                response.Success = false;
                response.Message = Strings.SERVER_SIDE + Strings.SID_NOT_FOUND;
                return null;
            }
	        
			session.LastAccess = DateTime.Now;
	        response.Success = true;
	        response.Message = string.Empty;
	        return session;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Login d'un utilisateur
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>une réponse LoginResponse avec un sid</returns>
        /// -----------------------------------------------------------------------------
        public LoginResponse Login(LoginRequest request)
        {
            var enabled = Configuration.ValueBool(string.Format(ConfigUserTemplate, request.Username) + ConfigUserEnabled, false);
            var password = Configuration.Value(string.Format(ConfigUserTemplate, request.Username) + ConfigUserPassword, String.Empty);
            var admin = Configuration.ValueBool(string.Format(ConfigUserTemplate, request.Username) + ConfigUserAdmin, false);
            var stimeout = Configuration.ValueInt(string.Format(ConfigUserTemplate, request.Username) + ConfigSessionTimeout, int.MinValue);
            var ctimeout = Configuration.ValueInt(string.Format(ConfigUserTemplate, request.Username) + ConfigConnectionTimeout, int.MinValue);

            string message;
            var success = false;
            var sid = -1;

	        if (!enabled)
		        message = Strings.SERVER_SIDE + String.Format(Strings.ACCESS_DENIED, request.Username);
	        else
	        {
		        if (password == request.Password)
		        {
			        // Vérifications des timeouts
			        if (stimeout == int.MinValue)
			        {
				        stimeout = DefaultSessionTimeoutDelay;
				        Log(String.Format(Strings.DEFAULT_SESSION_TIMEOUT, request.Username, stimeout), ESeverity.WARN);
			        }
			        if (ctimeout == int.MinValue)
			        {
				        ctimeout = DefaultConnectionTimeoutDelay;
				        Log(String.Format(Strings.DEFAULT_CONNECTION_TIMEOUT, request.Username, ctimeout), ESeverity.WARN);
			        }

			        var session = new TunnelSession(stimeout, ctimeout) {Logon = DateTime.Now};
			        sid = GetNewSid();
			        session.Username = request.Username;
			        session.LastAccess = DateTime.Now;
			        session.Admin = admin;
			        Sessions.Add(sid, session);
			        message = Strings.SERVER_SIDE + string.Format(Strings.ACCESS_GRANTED, request.Username);
			        success = true;
		        }
		        else
			        message = Strings.SERVER_SIDE + string.Format(Strings.ACCESS_DENIED_BAD_PASSWORD, request.Username);
	        }

	        Log(message, ESeverity.INFO);
            return new LoginResponse(success, message, sid);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Logout d'un utilisateur
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>une réponse générique</returns>
        /// -----------------------------------------------------------------------------
        public MinimalResponse Logout(SessionContextRequest request)
        {
            var response = new MinimalResponse();
            var session = CheckSession(ref request, ref response);

            if (session != null)
            {
                try
                {
                    session.DisconnectAndRemoveAllConnections();
                    Sessions.Remove(request.Sid);

                    response.Success = true;
                    response.Message = Strings.SERVER_SIDE + String.Format(Strings.SESSION_LOGOUT, session.Username);
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = Strings.SERVER_SIDE + ex.Message;
                }
            }

            Log(response.Message, ESeverity.INFO);
            return response;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ping du serveur
        /// </summary>
        /// -----------------------------------------------------------------------------
        public void Ping()
        {
            // tout est déjà fait par l'infrastructure sous-jacente
        }
        
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// La version du serveur
        /// </summary>
        /// <returns>La version de l'assembly</returns>
        /// -----------------------------------------------------------------------------
        public MinimalResponse Version()
        {
            var name = GetType().Assembly.GetName();
            return new MinimalResponse(true, Strings.SERVER_SIDE + string.Format("{0} v{1}, {2}", name.Name, name.Version.ToString(3), Program.FrameworkVersion()));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Etablissement d'une nouvelle connexion
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>le cid de connexion</returns>
        /// -----------------------------------------------------------------------------
        public ConnectResponse Connect(ConnectRequest request)
        {
            var response = new ConnectResponse();
            var session = CheckSession(ref request, ref response);

            if ( session != null )
            {
                var connection = session.CreateConnection();
                try
                {
                    connection.TcpClient = new TcpClient(request.Address, request.Port);
                    connection.Stream = connection.TcpClient.GetStream();

                    var endpoint = (IPEndPoint)connection.TcpClient.Client.RemoteEndPoint;
                    connection.Address = endpoint.Address.ToString();
                    connection.Port = endpoint.Port;
                    connection.LastAccess = DateTime.Now;
                    connection.ReadCount = 0;
                    connection.WriteCount = 0;

                    var enpoint = (IPEndPoint) connection.TcpClient.Client.RemoteEndPoint;
                    connection.Address = enpoint.Address.ToString();
                    connection.Port = enpoint.Port;

                    try
                    {
                        connection.Host = Dns.GetHostEntry(endpoint.Address).HostName;
                    }
                    catch (Exception)
                    {
                        connection.Host = enpoint.Address.ToString();
                    }
                    
                    response.Connected = true;
                    response.DataAvailable = connection.Stream.DataAvailable;

                    var cid = session.AddConnection(connection);
                    response.Success = true;
                    response.Message = Strings.SERVER_SIDE + string.Format(Strings.CONNECTED, connection.TcpClient.Client.RemoteEndPoint, request.Address);
                    response.Cid = cid;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = Strings.SERVER_SIDE + string.Format(Strings.CONNECTION_REFUSED, request.Address, request.Port, ex.Message);
                    response.Cid = -1;
                }
            }

            Log(response.Message, ESeverity.INFO);
            return response;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Déconnexion
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>une reponse générique</returns>
        /// -----------------------------------------------------------------------------
        public ConnectionContextResponse Disconnect(ConnectionContextRequest request)
        {
            var response = new ConnectionContextResponse();
            var session = CheckSession(ref request, ref response);

            if ( session != null )
            {
                var connection = session.CheckConnection(ref request, ref response);
                if (connection != null)
                {
                    try
                    {
                        response.Message = Strings.SERVER_SIDE + string.Format(Strings.DISCONNECTED, connection.TcpClient.Client.RemoteEndPoint);

                        connection.SafeDisconnect();
                        response.Connected = false;
                        response.DataAvailable = false;
                        response.Success = true;
                        session.RemoveConnection(request.Cid);
                    }
                    catch (Exception ex)
                    {
                        response.Success = false;
                        response.Message = Strings.SERVER_SIDE + ex.Message;
                    }
                }
            }

            Log(response.Message, ESeverity.INFO);
            return response;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Lecture de données
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>les données lues dans response.Data</returns>
        /// -----------------------------------------------------------------------------
        public ReadResponse Read(ConnectionContextRequest request)
        {
            var response = new ReadResponse();
            var session = CheckSession(ref request, ref response);

            if ( session != null )
            {
                var connection = session.CheckConnection(ref request, ref response);
                if (connection != null)
                {
                    if (response.Connected && response.DataAvailable)
                    {
                        // Données disponibles
                        try
                        {
                            var buffer = new byte[BufferSize];
                            var count = connection.Stream.Read(buffer, 0, BufferSize);
                            if (count > 0)
                            {
                                Array.Resize(ref buffer, count);
                                response.Success = true;
                                response.Message = string.Empty;
                                Program.StaticXorEncoder(ref buffer, request.Cid);
                                response.Data = buffer;
                                connection.ReadCount += count;
                            }
                            else
                            {
                                response.Success = false;
                                response.Data = null;
                                response.Message = Strings.SERVER_SIDE + Strings.DISCONNECTION_DETECTED;
                            }
                        }
                        catch (Exception ex)
                        {
                            response.Success = false;
                            response.Data = null;
                            response.Message = Strings.SERVER_SIDE + ex.Message;
                        }
                    }
                    else
                    {
                        // Pas de données disponibles
                        response.Success = true;
                        response.Message = string.Empty;
                        response.Data = new byte[] { };
                    }
                }
            }

            return response;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ecriture de données
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>une réponse générique</returns>
        /// -----------------------------------------------------------------------------
        public ConnectionContextResponse Write(WriteRequest request)
        {
            var response = new ConnectionContextResponse();
            var session = CheckSession(ref request, ref response);

            if ( session != null )
            {
                var connection = session.CheckConnection(ref request, ref response);
                if (connection != null)
                {
                    try
                    {
                        var result = request.Data;
                        Program.StaticXorEncoder(ref result, request.Cid);
                        connection.Stream.Write(result, 0, result.Length);
                        response.Success = true;
                        response.Message = string.Empty;
                        connection.WriteCount += result.Length;
                    }
                    catch (Exception ex)
                    {
                        response.Success = false;
                        response.Message = Strings.SERVER_SIDE + ex.Message;
                    }
                }
            }

            return response;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Enumeration des session
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>les sessions dans response.Sessions</returns>
        /// -----------------------------------------------------------------------------
        public MonitorResponse Monitor(SessionContextRequest request)
        {

            var response = new MonitorResponse();
            var session = CheckSession(ref request, ref response);

            if (session != null)
            {
                try
                {
                    if (session.Admin)
                    {
                        var exportsessions = new List<Session>();

                        foreach (int sid in Sessions.Keys)
                        {
                            var cursession = Sessions[sid];
                            var export = new Session
	                        {
		                        Sid = sid.ToString("x"),
		                        Admin = cursession.Admin,
		                        Connections = cursession.GetConnectionsStruct(),
		                        LastAccess = cursession.LastAccess,
		                        Logon = cursession.Logon,
		                        Username = cursession.Username
	                        };
	                        exportsessions.Add(export);
                        }

                        response.Sessions = exportsessions.ToArray();
                        response.Success = true;
                        response.Message = string.Empty;
                    }
                    else
                    {
                        response.Success = false;
                        response.Sessions = null;
                        response.Message = Strings.SERVER_SIDE + Strings.ADMIN_REQUIRED;
                    }
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Sessions = null;
                    response.Message = Strings.SERVER_SIDE + ex.Message;
                }
            }

            return response;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Demande de suppression d'une session
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>une réponse générique</returns>
        /// -----------------------------------------------------------------------------
        public MinimalResponse KillSession(KillSessionRequest request)
        {
            var response = new MinimalResponse();
            var targetsession = CheckSession(ref request, ref response);

            if (targetsession != null)
            {
                var fake = new SessionContextRequest(request.AdminSid);
                var adminsession = CheckSession(ref fake, ref response);
                if (adminsession != null)
                {
                    if (adminsession.Admin)
                    {
                        try
                        {
                            targetsession.DisconnectAndRemoveAllConnections();
                            Sessions.Remove(request.Sid);

                            response.Success = true;
                            response.Message = Strings.SERVER_SIDE + String.Format(Strings.SESSION_KILLED, targetsession.Username, adminsession.Username);
                        }
                        catch (Exception ex)
                        {
                            response.Success = false;
                            response.Message = Strings.SERVER_SIDE + ex.Message;
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = Strings.SERVER_SIDE + Strings.ADMIN_REQUIRED;
                    }
                }
            }

            Log(response.Message, ESeverity.INFO);
            return response;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Demande de suppression d'une connexion
        /// </summary>
        /// <param name="request">la requête</param>
        /// <returns>une réponse générique</returns>
        /// -----------------------------------------------------------------------------
        public ConnectionContextResponse KillConnection(KillConnectionRequest request)
        {
            var response = new ConnectionContextResponse();
            var targetsession = CheckSession(ref request, ref response);

            if (targetsession != null)
            {
                var targetconnection = targetsession.CheckConnection(ref request, ref response);
                if (targetconnection != null)
                {
                    var fake = new SessionContextRequest(request.AdminSid);
                    var adminsession = CheckSession(ref fake, ref response);
                    if (adminsession != null)
                    {
                        if (adminsession.Admin)
                        {
                            try
                            {
                                response.Message = Strings.SERVER_SIDE + string.Format(Strings.CONNECTION_KILLED, targetconnection.TcpClient.Client.RemoteEndPoint, targetsession.Username, adminsession.Username);

                                targetconnection.SafeDisconnect();
                                response.Connected = false;
                                response.DataAvailable = false;
                                response.Success = true;
                                targetsession.RemoveConnection(request.Cid);
                            }
                            catch (Exception ex)
                            {
                                response.Success = false;
                                response.Message = Strings.SERVER_SIDE + ex.Message;
                            }
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = Strings.SERVER_SIDE + Strings.ADMIN_REQUIRED;
                        }
                    }
                }
            }

            Log(response.Message, ESeverity.INFO);
            return response;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ecriture d'une entrée de log. Ne sera pas prise en compte si le log est inactif
        /// ou si le filtre l'impose
        /// </summary>
        /// <param name="sender">l'emetteur</param>
        /// <param name="message">le message à logger</param>
        /// <param name="severity">la sévérité</param>
        /// -----------------------------------------------------------------------------
        public void Log(object sender, string message, ESeverity severity)
        {
	        if (Logger != null)
		        Logger.Log(sender, message, severity);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ecriture d'une entrée de log. Ne sera pas prise en compte si le log est inactif
        /// ou si le filtre l'impose
        /// </summary>
        /// <param name="message">le message à logger</param>
        /// <param name="severity">la sévérité</param>
        /// -----------------------------------------------------------------------------
        private void Log(string message, ESeverity severity)
        {
            Log(this, message, severity);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fermeture du logger
        /// </summary>
        /// -----------------------------------------------------------------------------
        public void Close()
        {
	        if (Logger != null)
		        Logger.Close();
        }
        #endregion

    }

}


