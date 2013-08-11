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
using System.Net.Sockets;
using System.Threading;

using Bdt.Shared.Logs;
using Bdt.Shared.Service;
using Bdt.Shared.Request;
using Bdt.Shared.Response;
#endregion

namespace Bdt.Client.Sockets
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Passerelle entre le tunnel et le socket local, initiée par le serveur tcp
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class Gateway : LoggedObject
    {

        #region " Constantes "
        // La taille du buffer d'IO
	    private const int BufferSize = 32768;
        // La durée minimale entre deux tests de l'état de connexion
	    private const int StatePollingMinTime = 10;
        // La durée maximale entre deux tests de l'état de connexion
	    private const int StatePollingMaxTime = 5000;
        // Le coefficient de décélération,
	    private const double StatePollingFactor = 1.1;
        // Le test de la connexion effective
	    private const int SocketTestPollingTime = 100;
        #endregion

        #region " Attributs "
        private TcpClient _client;
        private NetworkStream _stream;
        private readonly ManualResetEvent _mre = new ManualResetEvent(false);
        private readonly ITunnel _tunnel;
        private readonly int _sid;
        private readonly string _address;
        private readonly int _port;
        private int _cid;
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="client">le socket client pour la communication locale</param>
        /// <param name="tunnel">le tunnel pour la communication distante</param>
        /// <param name="sid">le session-id</param>
        /// <param name="address">l'adresse distante de connexion</param>
        /// <param name="port">le port distant de connexion</param>
        /// -----------------------------------------------------------------------------
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

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gestion des erreurs sur une réponse du tunnel
        /// </summary>
        /// <param name="response">la réponse du tunnel</param>
        /// -----------------------------------------------------------------------------
        private void HandleError(IConnectionContextResponse response)
        {
            HandleError(response.Message, true);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gestion des erreurs
        /// </summary>
        /// <param name="ex">l'exception à gérer</param>
        /// <param name="show">affichage du message d'erreur</param>
        /// -----------------------------------------------------------------------------
        private void HandleError(Exception ex, bool show)
        {
            HandleError(ex.Message, show);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gestion des erreurs
        /// </summary>
        /// <param name="message">le message à gérer</param>
        /// <param name="show">affichage du message d'erreur</param>
        /// -----------------------------------------------------------------------------
        private void HandleError(string message, bool show)
        {
            if (show)
                Log(message, ESeverity.ERROR);

			_mre.Set();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gestion de l'état de connexion
        /// </summary>
        /// <param name="response">la réponse du tunnel</param>
        /// -----------------------------------------------------------------------------
        private void HandleState(IConnectionContextResponse response)
        {
            if (!response.Connected)
                _mre.Set();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Calcule le temps d'attente nécessaire entre deux traitements
        /// </summary>
        /// <param name="polltime">l'attente entre pollings</param>
        /// <param name="adjpolltime">ajustement (durée du dernier aller-retour</param>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        private int WaitTime (int polltime, int adjpolltime)
        {
	        return adjpolltime > polltime ? 0 : Math.Max(polltime - adjpolltime, StatePollingMinTime);
        }

	    /// -----------------------------------------------------------------------------
        /// <summary>
        /// Traitement principal du thread
        /// </summary>
        /// -----------------------------------------------------------------------------
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

			    // Si des données sont présentes sur le socket, envoi au tunnel
			    var isConnected = false;
			    var isDataAvailAble = false;
                    
			    try
			    {
				    isConnected = (!(_client.Client.Poll(SocketTestPollingTime, SelectMode.SelectRead) && _client.Client.Available == 0));
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
						    // Des données sont présentes en local, envoi
						    var transBuffer = new byte[count];
						    Array.Copy(buffer, transBuffer, count);
						    Shared.Runtime.Program.StaticXorEncoder(ref transBuffer, _cid);
						    IConnectionContextResponse writeResponse = _tunnel.Write(new WriteRequest(_sid, _cid, transBuffer));
						    if (writeResponse.Success)
							    HandleState(writeResponse);
						    else
							    HandleError(writeResponse);

						    // Si des données sont présentes, on repasse en mode 'actif'
						    polltime = StatePollingMinTime;
					    }
				    }
				    else
				    {
					    // Sinon on augmente le temps de latence
					    polltime = Convert.ToInt32(Math.Round(StatePollingFactor * polltime));
					    polltime = Math.Min(polltime, StatePollingMaxTime);
				    }


				    // Si des données sont présentes dans le tunnel, envoi au socket
				    var readResponse = _tunnel.Read(new ConnectionContextRequest(_sid, _cid));
				    if (readResponse.Success)
				    {
					    if (readResponse.Connected && readResponse.DataAvailable)
					    {
						    // Puis écriture sur le socket local
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
						    // Si des données sont présentes, on repasse en mode 'actif'
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
				    // Deconnexion
				    _mre.Set();
			    }
			    adjpolltime = Convert.ToInt32(DateTime.Now.Subtract(startmarker).TotalMilliseconds);
		    }
		    Disconnect();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Deconnexion
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Disconnect()
        {
	        if (_client == null)
				return;
	        
			_stream.Close();
	        _client.Close();
	        IConnectionContextResponse response = _tunnel.Disconnect(new ConnectionContextRequest(_sid, _cid));
	        Log(response.Message, ESeverity.INFO);
	        _stream = null;
	        _client = null;
        }

        #endregion

    }

}

