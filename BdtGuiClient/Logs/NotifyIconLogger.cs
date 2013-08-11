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
using System.Windows.Forms;

using Bdt.Shared.Logs;
using Bdt.Shared.Configuration;
using Bdt.GuiClient.Runtime;
#endregion

namespace Bdt.GuiClient.Logs
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Un logger lié à un NotifyIcon pour la sortie des erreurs uniquement
    /// </summary>
    /// -----------------------------------------------------------------------------
    class NotifyIconLogger : BaseLogger
    {

        #region " Attributs "
	    private BdtGuiClient _guiclient;
	    private readonly string _tipTitle;
	    private readonly int _timeout;
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="prefix">le prefixe dans la configuration ex: application/log</param>
        /// <param name="config">la configuration pour la lecture des parametres</param>
        /// <param name="guiclient">le client bdt</param>
        /// <param name="tipTitle">le titre à utiliser</param>
        /// <param name="timeout">le timeout d'affichage</param>
        /// -----------------------------------------------------------------------------
        public NotifyIconLogger(string prefix, ConfigPackage config, BdtGuiClient guiclient, string tipTitle, int timeout)
            : base(null, prefix, config)
        {
            // on utilise le référence d'un BdtGuiClient au lieu de passer directement un NotifyIcon car à ce stade
            // on ne peut pas créer de formulaire, car la Culture serait incorrecte, le fichier de configuration
            // n'étant pas déjà parsé
            _guiclient = guiclient;
            _tipTitle = tipTitle;
            _timeout = timeout;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Ecriture d'une entrée de log. Ne sera actif que pour une sévérité à ERREUR
        /// </summary>
        /// <param name="sender">l'emetteur</param>
        /// <param name="message">le message à logger</param>
        /// <param name="severity">la sévérité</param>
        /// -----------------------------------------------------------------------------
        public override void Log(object sender, string message, ESeverity severity)
        {
	        if ((severity == ESeverity.ERROR) && (_guiclient != null) && (_guiclient.MainComponent != null) &&
	            (_guiclient.MainComponent.NotifyIcon != null))
		        _guiclient.MainComponent.NotifyIcon.ShowBalloonTip(_timeout, _tipTitle, message, ToolTipIcon.Error);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fermeture du logger
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void Close()
        {
            _guiclient = null;
        }
        #endregion

    }
}
