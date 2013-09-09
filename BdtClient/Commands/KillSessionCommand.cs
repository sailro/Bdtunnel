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
using Bdt.Client.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Request;
using Bdt.Shared.Service;
#endregion

namespace Bdt.Client.Commands
{
    public class KillSessionCommand : Command 
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Les switchs disponibles pour l'appel de la commande
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override string Switch
        {
            get
            {
                return "-ks";
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// L'aide pour la commande
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override string Help
        {
            get
            {
                return Strings.KILL_SESSION_HELP;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Les noms des paramètres de la commande
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override string[] ParametersName
        {
            get
            {
                return new[] { "sid" };
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Lance la commande
        /// </summary>
        /// <param name="parameters">les parametres</param>
        /// <param name="logger">le logger</param>
        /// <param name="tunnel">le tunnel</param>
        /// <param name="sid">le jeton de session</param>
        /// -----------------------------------------------------------------------------
        public override void Execute(string[] parameters, ILogger logger, ITunnel tunnel, int sid)
        {
            int targetsid;

            if (int.TryParse(parameters[0], System.Globalization.NumberStyles.HexNumber, null, out targetsid))
            {
	            var response = tunnel.KillSession(new KillSessionRequest(targetsid, sid));
	            logger.Log(this, response.Message, response.Success ? ESeverity.INFO : ESeverity.ERROR);
            }
            else
            {
                logger.Log(this, Strings.CHECK_PARAMETERS, ESeverity.ERROR);
            }
        }
    }
}