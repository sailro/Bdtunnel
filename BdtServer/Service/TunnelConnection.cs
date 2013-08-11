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

using Bdt.Server.Resources;
using Bdt.Shared.Logs;
#endregion

namespace Bdt.Server.Service
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Une connexion au sein du tunnel
    /// </summary>
    /// -----------------------------------------------------------------------------
    internal class TunnelConnection : TimeoutObject 
    {

		#region " Proprietes "

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// L'adresse distant
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public string Address { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le port distant
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public int Port { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// L'hôte distant
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public string Host { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le client TCP associé
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public TcpClient TcpClient { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le flux associé
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public NetworkStream Stream { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le nombre d'octets lus
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public int ReadCount { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le nombre d'octets écrits
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public int WriteCount { get; set; }

	    #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="timeoutdelay">valeur du timeout</param>
        /// -----------------------------------------------------------------------------
        public TunnelConnection(int timeoutdelay) : base(timeoutdelay)
        {
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Timeout de l'objet
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected override void Timeout(ILogger logger)
        {
            logger.Log(this, String.Format(Strings.CONNECTION_TIMEOUT, TcpClient.Client.RemoteEndPoint), ESeverity.INFO);
            SafeDisconnect();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fermeture de la connexion
        /// </summary>
        /// -----------------------------------------------------------------------------
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
            catch (Exception) { }
// ReSharper restore EmptyGeneralCatchClause

            try
            {
	            if (TcpClient != null)
		            TcpClient.Close();
            }
// ReSharper disable EmptyGeneralCatchClause
            catch (Exception) { }
// ReSharper restore EmptyGeneralCatchClause

            Stream = null;
            TcpClient = null;
        }
        #endregion

    }

}
