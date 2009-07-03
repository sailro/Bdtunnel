// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Collections.Generic;
using System.Text;
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
        protected BdtGuiClient m_guiclient = null;
        protected string m_tipTitle = null;
        protected int m_timeout = 0;
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
            m_guiclient = guiclient;
            m_tipTitle = tipTitle;
            m_timeout = timeout;
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
            if ((severity == ESeverity.ERROR) && (m_guiclient != null) && (m_guiclient.MainComponent != null) && (m_guiclient.MainComponent.NotifyIcon != null))
            {
                m_guiclient.MainComponent.NotifyIcon.ShowBalloonTip(m_timeout, m_tipTitle, message, ToolTipIcon.Error);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fermeture du logger
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void Close()
        {
            m_guiclient = null;
        }
        #endregion

    }
}
