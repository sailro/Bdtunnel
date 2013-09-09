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

using Bdt.Shared.Logs;
using Bdt.Client.Resources;
#endregion

namespace Bdt.Client.Socks
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Gestionnaire Socks v4 (sans DNS)
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class Socks4Handler : GenericSocksHandler
    {

        #region " Constantes "
	    protected const int Socks4ReplyVersion = 0;
	    protected const int Socks4Ok = 90; // Requête acceptée
	    protected const int Socks4Ko = 91; // Requête refusée
	    protected const int Socks4BindCommand = 2; // commande BIND (non supportée)
	    private const int ReplySize = 8; // octets de réponse
        #endregion

        #region " Attributs "

	    #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le handler est-il adapté à la requête?
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected override bool IsHandled
        {
            get
            {
                // Préparation pessimiste de la réponse
                Reply[0] = Socks4ReplyVersion;
                Reply[1] = Socks4Ko;
                Array.Clear(Reply, 2, 6);

                if (Version != 4)
                {
                    return false;
                }

                // Teste pour basic Socks4 (pas Socks4a)
                if ((Buffer[4] != 0) || (Buffer[5] != 0) || (Buffer[6] != 0))
                {
                    if (Command != Socks4BindCommand)
                    {
                        RemotePort = 256 * Convert.ToInt32(Buffer[2]) + Convert.ToInt32(Buffer[3]);
                        Address = Buffer[4] + "." + Buffer[5] + "." + Buffer[6] + "." + Buffer[7];
                        // Préparation de la réponse
                        Reply[1] = Socks4Ok;
                        Array.Copy(Buffer, 2, Reply, 2, 6);
                        Log(Strings.SOCKS4_REQUEST_HANDLED, ESeverity.DEBUG);
                        return true;
                    }
	                
					// Socks4 BIND
	                Log(Strings.SOCKS_BIND_UNSUPPORTED, ESeverity.WARN);
                }
                return false;
            }
        }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Les données de réponse
	    /// </summary>
	    /// -----------------------------------------------------------------------------
		protected override sealed byte[] Reply { get; set; }

	    #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="buffer"></param>
        /// -----------------------------------------------------------------------------
        public Socks4Handler(byte[] buffer)
            : base(buffer)
        {
	        Reply = new byte[ReplySize];
	        Version = buffer[0];
            Command = buffer[1];
        }
        #endregion

    }
}

