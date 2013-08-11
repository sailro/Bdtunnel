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
using System.Net.Sockets;

using Bdt.Shared.Logs;
using Bdt.Client.Sockets;
#endregion

namespace Bdt.Tests.Sockets
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Serveur de test
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class EchoServer : TcpServer
    {

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="localport">port local côté client</param>
        /// <param name="shared">bind sur toutes les ip/ip locale</param>
        /// -----------------------------------------------------------------------------
        public EchoServer(int localport, bool shared)
            : base(localport, shared)
        {
            Log(string.Format("Echo server listenning {0}:{1}", Ip, localport), ESeverity.INFO);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Callback en cas de nouvelle connexion
        /// </summary>
        /// <param name="client">le socket client</param>
        /// -----------------------------------------------------------------------------
        protected override void OnNewConnection(TcpClient client)
        {
// ReSharper disable ObjectCreationAsStatement
            new EchoSession(client);
// ReSharper restore ObjectCreationAsStatement
        }
        #endregion

    }

}

