// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

using Bdt.Shared.Resources;
using Bdt.Shared.Logs;
using System.Collections;
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
        private readonly string[] HACK_CLIENT_CHANNEL = new string[] {"_clientChannel", "clientChannel", "client" };
        private const string HACK_PROXY_OBJECT = "_proxyObject";
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
                foreach (string fieldname in HACK_CLIENT_CHANNEL)
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
                else
                {
                    HttpClientChannel channel = ((HttpClientChannel)clientChannelFieldInfo.GetValue(ClientChannel));
                    return channel;
                }
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le proxy utilisé par le canal de communication côté client
        /// </summary>
        /// -----------------------------------------------------------------------------
        public System.Net.IWebProxy Proxy
        {
            get
            {
                FieldInfo proxyObjectFieldInfo = typeof(HttpClientChannel).GetField(HACK_PROXY_OBJECT, BindingFlags.Instance | BindingFlags.NonPublic);
                if (proxyObjectFieldInfo == null)
                {
                    // Thanks MONO 2.0 ...
                    Log("Failed to get Proxy. If you are using Mono, try a version before v2.x", ESeverity.WARN);
                    return null;
                }
                else
                {
                    return ((IWebProxy)proxyObjectFieldInfo.GetValue(ProxyChannel));
                }
            }
            set
            {
                FieldInfo proxyObjectFieldInfo = typeof(HttpClientChannel).GetField(HACK_PROXY_OBJECT, BindingFlags.Instance | BindingFlags.NonPublic);
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

