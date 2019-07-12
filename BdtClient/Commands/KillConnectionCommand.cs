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

using Bdt.Client.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Request;
using Bdt.Shared.Service;

namespace Bdt.Client.Commands
{
	public class KillConnectionCommand : Command
	{
		public override string Switch
		{
			get { return "-kc"; }
		}

		public override string Help
		{
			get { return Strings.KILL_CONNECTION_HELP; }
		}

		public override string[] ParametersName
		{
			get { return new[] {"sid", "cid"}; }
		}

		public override void Execute(string[] parameters, ILogger logger, ITunnel tunnel, int sid)
		{
			int targetsid;
			int targetcid;

			if (int.TryParse(parameters[0], System.Globalization.NumberStyles.HexNumber, null, out targetsid)
			    && int.TryParse(parameters[1], System.Globalization.NumberStyles.HexNumber, null, out targetcid))
			{
				var response = tunnel.KillConnection(new KillConnectionRequest(targetsid, sid, targetcid));
				logger.Log(this, response.Message, response.Success ? ESeverity.INFO : ESeverity.ERROR);
			}
			else
			{
				logger.Log(this, Strings.CHECK_PARAMETERS, ESeverity.ERROR);
			}
		}
	}
}
