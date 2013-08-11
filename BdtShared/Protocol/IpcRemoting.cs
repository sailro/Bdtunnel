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
using System.Runtime.Remoting.Channels.Ipc;
#endregion

namespace Bdt.Shared.Protocol
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Protocole de communication basé sur le remoting .NET et sur le protocole IPC
    /// Exclusivement pour une communication sur la même machine (client/serveur)
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class IpcRemoting : GenericRemoting<IpcChannel>
    {

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le canal de communication côté client
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected override IpcChannel ClientChannel
        {
            get
            {
                if (ClientChannelField == null)
                {
                    ClientChannelField = new IpcChannel(CreateClientChannelProperties(), null, null);
                }
                return ClientChannelField;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le canal de communication côté serveur
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected override IpcChannel ServerChannel
        {
            get
            {
                if (ServerChannelField == null)
                {
                    ServerChannelField = new IpcChannel(CreateServerChannelProperties(), null, null);
                }
                return ServerChannelField;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// L'URL nécessaire pour se connecter au serveur
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected override string ServerURL
        {
            get
            {
                //return string.Format("ipc://{0}:{1}/{2}", Address, Port, Name);
                return string.Format("ipc://{0}/{0}", Name);
            }
        }

        #endregion

    }

}

