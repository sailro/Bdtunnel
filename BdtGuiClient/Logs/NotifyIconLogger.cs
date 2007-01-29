/// -----------------------------------------------------------------------------
/// BoutDuTunnel
/// Sebastien LEBRETON
/// sebastien.lebreton[-at-]free.fr
/// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Bdt.Shared.Logs;
using Bdt.Shared.Configuration;
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
        protected NotifyIcon m_notifyIcon = null;
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
        /// <param name="notifyIcon">l'icone de notification cible</param>
        /// <param name="tipTitle">le titre à utiliser</param>
        /// <param name="timeout">le timeout d'affichage</param>
        /// -----------------------------------------------------------------------------
        public NotifyIconLogger (string prefix, ConfigPackage config, NotifyIcon notifyIcon, string tipTitle, int timeout)
            : base(null, prefix, config)
        {
            m_notifyIcon = notifyIcon;
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
        public override void Log (object sender, string message, ESeverity severity)
        {
            if ((severity == ESeverity.ERROR) && (m_notifyIcon != null))
            {
                m_notifyIcon.ShowBalloonTip(m_timeout, m_tipTitle, message, ToolTipIcon.Error);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fermeture du logger
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void Close ()
        {
            m_notifyIcon = null;
        }
        #endregion

    }
}
