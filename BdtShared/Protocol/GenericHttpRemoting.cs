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
        private const string HACK_CLIENT_CHANNEL = "_clientChannel";
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
                FieldInfo clientChannelFieldInfo;
                clientChannelFieldInfo = typeof(HttpChannel).GetField(HACK_CLIENT_CHANNEL, BindingFlags.Instance | BindingFlags.NonPublic);
                if (clientChannelFieldInfo == null)
                {
                    // HACK: MONO
                    clientChannelFieldInfo = typeof(HttpChannel).GetField(HACK_CLIENT_CHANNEL.Substring(1), BindingFlags.Instance | BindingFlags.NonPublic);
                }
                HttpClientChannel channel = ((HttpClientChannel)clientChannelFieldInfo.GetValue(ClientChannel));
                return channel;
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
                return ((IWebProxy)proxyObjectFieldInfo.GetValue(ProxyChannel));
            }
            set
            {
                FieldInfo proxyObjectFieldInfo = typeof(HttpClientChannel).GetField(HACK_PROXY_OBJECT, BindingFlags.Instance | BindingFlags.NonPublic);
                proxyObjectFieldInfo.SetValue(ProxyChannel, value);
            }
        }
        #endregion

    }

}

