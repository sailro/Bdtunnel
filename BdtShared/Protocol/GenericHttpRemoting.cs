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
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Channels.Http;
using Bdt.Shared.Logs;
#endregion

namespace Bdt.Shared.Protocol
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Protocole de communication basé sur le remoting .NET et sur le protocole HTTP
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class GenericHttpRemoting : GenericRemoting<HttpChannel>, IProxyCompatible
    {
        #region " Constantes "
        private readonly string[] _hackClientChannel = new [] {"_clientChannel", "clientChannel", "client" };
        private const string HackProxyObject = "_proxyObject";
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// L'URL nécessaire pour se connecter au serveur
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected override string ServerURL
        {
            get
            {
                String scheme = (IsSecured) ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;
                return string.Format("{0}://{1}:{2}/{3}", scheme, Address, Port, Name);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Récuperation du channel pour fixer le proxy
        /// </summary>
        /// -----------------------------------------------------------------------------
        private HttpClientChannel ProxyChannel
        {
            get
            {
                FieldInfo clientChannelFieldInfo = null;
                foreach (var fieldname in _hackClientChannel)
                {
                    clientChannelFieldInfo = typeof(HttpChannel).GetField(fieldname, BindingFlags.Instance | BindingFlags.NonPublic);
                    if (clientChannelFieldInfo != null)
                    {
                        break;
                    }
                }
                if (clientChannelFieldInfo == null)
                {
                    // Thanks MONO 2.0 ...
                    Log("Failed to get ProxyChannel. If you are using Mono, try a version before v2.x", ESeverity.WARN);
                    return null;
                }
	            
				var channel = ((HttpClientChannel)clientChannelFieldInfo.GetValue(ClientChannel));
	            return channel;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le proxy utilisé par le canal de communication côté client
        /// </summary>
        /// -----------------------------------------------------------------------------
        public IWebProxy Proxy
        {
            get
            {
                var proxyObjectFieldInfo = typeof(HttpClientChannel).GetField(HackProxyObject, BindingFlags.Instance | BindingFlags.NonPublic);
                if (proxyObjectFieldInfo == null)
                {
                    // Thanks MONO 2.0 ...
                    Log("Failed to get Proxy. If you are using Mono, try a version before v2.x", ESeverity.WARN);
                    return null;
                }
	            
				return ((IWebProxy)proxyObjectFieldInfo.GetValue(ProxyChannel));
            }
            set
            {
                var proxyObjectFieldInfo = typeof(HttpClientChannel).GetField(HackProxyObject, BindingFlags.Instance | BindingFlags.NonPublic);
                if (proxyObjectFieldInfo == null)
                {
                    // Thanks MONO 2.0 ...
                    Log("Failed to set Proxy. If you are using Mono, try a version before v2.x", ESeverity.WARN);
                }
                else
                {
                    proxyObjectFieldInfo.SetValue(ProxyChannel, value);
                }
            }
        }
        #endregion

    }

}

