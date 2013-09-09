/* BoutDuTunnel Copyright (c)  2007-2013 Sebastien LEBRETON

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
#endregion

namespace Bdt.Tests.Sockets
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Session de test
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class EchoSession : LoggedObject
    {

        #region " Constantes "
        // La taille du buffer d'IO
	    private const int BufferSize = 65536;
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
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="client">le socket client pour la communication locale</param>
        /// -----------------------------------------------------------------------------
        public EchoSession(TcpClient client)
        {
            _client = client;
            _stream = client.GetStream();

            var thr = new Thread(CommunicationThread) {IsBackground = true};
	        thr.Start();
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
        /// Calcule le temps d'attente nécessaire entre deux traitements
        /// </summary>
        /// <param name="polltime">l'attente entre pollings</param>
        /// <param name="adjpolltime">ajustement (durée du dernier aller-retour</param>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        private static int WaitTime(int polltime, int adjpolltime)
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

            while (!_mre.WaitOne(WaitTime(polltime, adjpolltime), false))
            {
                var startmarker = DateTime.Now;

                // Si des données sont présentes sur le socket, renvoi
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
                            try
                            {
                                _stream.Write(buffer, 0, count);
                                _stream.Flush();
                            }
                            catch (Exception ex)
                            {
                                HandleError(ex, true);
                            }
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
            if (_client != null)
            {
                _stream.Close();
                _client.Close();
                _stream = null;
                _client = null;
            }
        }
        #endregion

    }

}

