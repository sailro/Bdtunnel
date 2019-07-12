/* BoutDuTunnel Copyright (c) 2006-2019 Sebastien Lebreton

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

using System;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Channels.Http;
using Bdt.Shared.Logs;

namespace Bdt.Shared.Protocol
{
	public abstract class GenericHttpRemoting : GenericRemoting<HttpChannel>, IProxyCompatible
	{
		private readonly string[] _hackClientChannel = {"_clientChannel", "clientChannel", "client"};
		private const string HackProxyObject = "_proxyObject";

		protected override string ServerURL
		{
			get
			{
				var scheme = IsSecured ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;
				return string.Format("{0}://{1}:{2}/{3}", scheme, Address, Port, Name);
			}
		}

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

				return (IWebProxy)proxyObjectFieldInfo.GetValue(ProxyChannel);
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
	}
}
