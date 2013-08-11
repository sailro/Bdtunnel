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
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Bdt.Shared.Logs;
using Bdt.Client.Resources;
#endregion

namespace Bdt.Client.Sockets
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Serveur TCP de base
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class TcpServer : LoggedObject
    {

        #region " Constantes "
	    private const int AcceptPollingTime = 50;
        #endregion

        #region " Attributs "
        private readonly TcpListener _listener;
        private readonly ManualResetEvent _mre = new ManualResetEvent(false);
	    #endregion

        #region " Proprietes "

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// L'ip d'écoute
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    protected IPAddress Ip { get; private set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le port d'écoute
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    private int Port { get; set; }

	    #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="port">port local côté client</param>
        /// <param name="shared">bind sur toutes les ip/ip locale</param>
        /// -----------------------------------------------------------------------------
        protected TcpServer(int port, bool shared)
        {
            Ip = shared ? IPAddress.Any : IPAddress.Loopback;
            Port = port;

            _listener = new TcpListener(Ip, Port);
            var thr = new Thread(ServerThread);
            thr.Start();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Callback en cas de nouvelle connexion
        /// </summary>
        /// <param name="client">le socket client</param>
        /// -----------------------------------------------------------------------------
        protected abstract void OnNewConnection(TcpClient client);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fermeture de l'écoute
        /// </summary>
        /// -----------------------------------------------------------------------------
        public void CloseServer()
        {
            _mre.Set();
            _listener.Stop();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Traitement principal du thread
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void ServerThread()
        {
            try
            {
                _listener.Start();
                while (!_mre.WaitOne(AcceptPollingTime, false))
                {
                    try
                    {
                        TcpClient client = _listener.AcceptTcpClient();
                        OnNewConnection(client);
                    }
                    catch (SocketException ex)
                    {
                        if (ex.SocketErrorCode != SocketError.Interrupted)
                        {
                            Log(ex.Message, ESeverity.ERROR);
                            Log(ex.ToString(), ESeverity.DEBUG);
                        }
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
                    Log(String.Format(Strings.TCP_SERVER_DISABLED, Port), ESeverity.WARN);
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
        #endregion

    }

}

