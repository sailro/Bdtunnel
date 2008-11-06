// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Collections.Generic;

using Bdt.Shared.Logs;
using Bdt.Shared.Service;
#endregion

namespace Bdt.Client.Commands
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Une commande sur la ligne de commande
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class Command
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Récuperation de l'ensemble des commandes disponibles
        /// </summary>
        /// <returns>un tableau de commandes</returns>
        /// -----------------------------------------------------------------------------
        public static Command[] GetCommands()
        {
            List<Command> result = new List<Command>();
            result.Add(new HelpCommand());
            result.Add(new KillConnectionCommand());
            result.Add(new KillSessionCommand());
            result.Add(new MonitorCommand());
            return result.ToArray();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Les switchs disponibles pour l'appel de la commande
        /// </summary>
        /// -----------------------------------------------------------------------------
        public abstract string Switch
        {
            get;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// L'aide pour la commande
        /// </summary>
        /// -----------------------------------------------------------------------------
        public abstract string Help
        {
            get;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Les noms des paramètres de la commande
        /// </summary>
        /// -----------------------------------------------------------------------------
        public abstract string[] ParametersName
        {
            get;
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
        public abstract void Execute(string[] parameters, ILogger logger, ITunnel tunnel, int sid);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Trouve puis execute une commande
        /// </summary>
        /// <param name="args">l'ensemble des arguments</param>
        /// <param name="logger">le logger</param>
        /// <param name="tunnel">le tunnel</param>
        /// <param name="sid">le jeton de session</param>
        /// <returns>true si la commande a été trouvée</returns>
        /// -----------------------------------------------------------------------------
        public static bool FindAndExecute(string[] args, ILogger logger, ITunnel tunnel, int sid)
        {
            if (args.Length > 0)
            {
                string sw = args[0];
                string[] parameters = new string[args.Length - 1];
                Array.ConstrainedCopy(args, 1, parameters, 0, parameters.Length);

                foreach (Command cmd in Command.GetCommands())
                {
                    if ((sw == cmd.Switch) && (parameters.Length == cmd.ParametersName.Length))
                    {
                        cmd.Execute(parameters, logger, tunnel, sid);
                        return true;
                    }
                }
            }

            return false;
        }

    }
}

