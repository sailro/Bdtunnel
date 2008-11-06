// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using Bdt.Client.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Request;
using Bdt.Shared.Response;
using Bdt.Shared.Service;
#endregion

namespace Bdt.Client.Commands
{
    public class KillConnectionCommand : Command
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
                return "-kc";
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
                return Strings.KILL_CONNECTION_HELP;
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
                return new string[] { "sid", "cid" };
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
            int targetsid = 0;
            int targetcid = 0;

            if (int.TryParse(parameters[0], System.Globalization.NumberStyles.HexNumber, null, out targetsid)
                && int.TryParse(parameters[1], System.Globalization.NumberStyles.HexNumber, null, out targetcid))
            {
                ConnectionContextResponse response = tunnel.KillConnection(new KillConnectionRequest(targetsid, sid, targetcid));
                if (response.Success)
                {
                    logger.Log(this, response.Message, ESeverity.INFO);
                }
                else
                {
                    logger.Log(this, response.Message, ESeverity.ERROR);
                }
            }
            else
            {
                logger.Log(this, Strings.CHECK_PARAMETERS, ESeverity.ERROR);
            }
        }
    }
}