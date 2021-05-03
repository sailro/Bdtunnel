/* BoutDuTunnel Copyright (c) 2006-2021 Sebastien Lebreton

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
using System.Globalization;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using Bdt.Shared.Resources;
using Bdt.Shared.Service;
using Bdt.Shared.Logs;
using System.Collections;

namespace Bdt.Shared.Protocol
{
	public abstract class GenericRemoting<T> : GenericProtocol where T : IChannel
	{
		private const string CfgName = "name";
		private const string CfgPortName = "portName";
		private const string CfgPort = "port";

		protected T ClientChannelField;
		protected T ServerChannelField;

		protected abstract T ServerChannel { get; }

		protected abstract T ClientChannel { get; }

		protected abstract string ServerURL { get; }

		protected virtual bool IsSecured
		{
			get { return false; }
		}

		public override void ConfigureClient()
		{
			Log(string.Format(Strings.CONFIGURING_CLIENT, GetType().Name, ServerURL), ESeverity.DEBUG);
			ChannelServices.RegisterChannel(ClientChannel, IsSecured);
		}

		public override void UnConfigureClient()
		{
			Log(string.Format(Strings.UNCONFIGURING_CLIENT, GetType().Name), ESeverity.DEBUG);
			ChannelServices.UnregisterChannel(ClientChannel);
		}

		public override void ConfigureServer(Type type)
		{
			Log(string.Format(Strings.CONFIGURING_SERVER, GetType().Name, Port), ESeverity.INFO);
			ChannelServices.RegisterChannel(ServerChannel, IsSecured);
			var wks = new WellKnownServiceTypeEntry(type, Name, WellKnownObjectMode.Singleton);
			RemotingConfiguration.RegisterWellKnownServiceType(wks);
		}

		public override void UnConfigureServer()
		{
			Log(string.Format(Strings.UNCONFIGURING_SERVER, GetType().Name, Port), ESeverity.INFO);
			ChannelServices.UnregisterChannel(ServerChannel);
			if (ServerChannel is IChannelReceiver channel)
				channel.StopListening(null);
		}

		public override ITunnel GetTunnel()
		{
			return (ITunnel)Activator.GetObject(typeof(ITunnel), ServerURL);
		}

		protected Hashtable CreateClientChannelProperties()
		{
			var properties = new Hashtable {{CfgName, $"{Name}.Client"}};
			properties.Add(CfgPortName, properties[CfgName]);
			return properties;
		}

		protected Hashtable CreateServerChannelProperties()
		{
			var properties = new Hashtable {{CfgName, Name}, {CfgPort, Port.ToString(CultureInfo.InvariantCulture)}};
			properties.Add(CfgPortName, properties[CfgName]);
			return properties;
		}
	}
}
