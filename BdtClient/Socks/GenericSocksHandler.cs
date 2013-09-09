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

using Bdt.Shared.Logs;
using Bdt.Client.Resources;
#endregion

namespace Bdt.Client.Socks
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Gestionnaire générique Socks
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class GenericSocksHandler : LoggedObject
    {

        #region " Constantes "
        // La taille du buffer d'IO
	    protected const int BufferSize = 32768;
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le handler est-il adapté à la requête?
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected abstract bool IsHandled
        {
            get;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Les données de réponse
        /// </summary>
        /// -----------------------------------------------------------------------------
		protected abstract byte[] Reply
		{
			get; set;
		}

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// La version de la requête socks
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    protected int Version { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// La commande de la requête socks
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    protected int Command { get; set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le port distant
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public int RemotePort { get; protected set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// L'adresse distante
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    public string Address { get; protected set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le buffer de la requête
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    protected byte[] Buffer { get; private set; }

	    #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="buffer"></param>
        /// -----------------------------------------------------------------------------
        protected GenericSocksHandler(byte[] buffer)
        {
            Buffer = buffer;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retourne un gestionnaire adapté à la requête
        /// </summary>
        /// <param name="client">le client TCP</param>
        /// <returns>un gestionnaire adapté</returns>
        /// -----------------------------------------------------------------------------
        public static GenericSocksHandler GetInstance(TcpClient client)
        {
            var buffer = new byte[BufferSize];

            var stream = client.GetStream();
            var size = stream.Read(buffer, 0, BufferSize);
            Array.Resize(ref buffer, size);

            if (size < 3)
                throw (new ArgumentException(Strings.INVALID_SOCKS_HANDSHAKE));

	        GenericSocksHandler result = new Socks4Handler(buffer);
            if (!result.IsHandled)
            {
                result = new Socks4AHandler(buffer);
                if (!result.IsHandled)
                {
                    result = new Socks5Handler(client, buffer);
                    if (!result.IsHandled)
                        throw (new ArgumentException(Strings.NO_VALID_SOCKS_HANDLER));
                }
            }

            var reply = result.Reply;
            client.GetStream().Write(reply, 0, reply.Length);

            return result;
        }
        #endregion

    }

}


