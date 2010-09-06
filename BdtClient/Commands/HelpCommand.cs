/* BoutDuTunnel Copyright (c) 2007-2010 Sebastien LEBRETON

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

using Bdt.Client.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Service;
#endregion

namespace Bdt.Client.Commands
{
    public class HelpCommand : Command 
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
                return "-h";
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
                return Strings.HELP_HELP;
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
                return new string[] { };
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
            foreach (Command cmd in Command.GetCommands())
            {
                logger.Log(this, String.Format("{0}: {1}", cmd.Help, cmd.Switch + " " + string.Join(" ", cmd.ParametersName)), ESeverity.INFO);
            }
        }
    }
}