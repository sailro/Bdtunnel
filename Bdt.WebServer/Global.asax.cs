#region " Inclusions "

using System;
using Bdt.Shared.Logs;

#endregion

namespace Bdt.WebServer.Runtime
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			BdtWebServer server = new BdtWebServer(Server);
			Application[this.GetType().FullName] = server;
			server.Start();
		}

		protected void Session_Start(object sender, EventArgs e)
		{
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception ex = Server.GetLastError();
			BdtWebServer server = (BdtWebServer)Application[this.GetType().FullName];

			if ((ex != null) && (server != null))
			{
				if (LoggedObject.GlobalLogger != null)
				{
					server.Log(ex.Message, ESeverity.ERROR);
					server.Log(ex.ToString(), ESeverity.DEBUG);
				}
				else
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

		protected void Session_End(object sender, EventArgs e)
		{
		}

		protected void Application_End(object sender, EventArgs e)
		{
			BdtWebServer server = (BdtWebServer)Application[this.GetType().FullName];
			if (server != null)
			{
				server.Stop();
			}
		}
	}
}
