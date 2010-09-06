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
using System.Collections.Generic;
using System.Text;
using System.Net;

using Bdt.Client.Resources;
using Bdt.Shared.Configuration;
#endregion

namespace Bdt.Client.Configuration
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Configuration de port forwarding pour un port local donné
    /// </summary>
    /// -----------------------------------------------------------------------------
    [Serializable]
    public class PortForward 
    {

        #region " Constantes "
        // Les constantes du fichier de configuration liées aux forwards
        protected const string CFG_FORWARD_TEMPLATE = ClientConfig.WORD_FORWARD + ClientConfig.TAG_ELEMENT + ClientConfig.WORD_PORT + "{0}" + ClientConfig.TAG_ATTRIBUTE;
        #endregion

        #region " Attributs "
        protected int m_localPort;
        protected bool m_enabled;
        protected bool m_shared;
        protected int m_remotePort;
        protected string m_address;
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le forward est-il actif?
        /// </summary>
        /// -----------------------------------------------------------------------------
        public bool Enabled
        {
            get
            {
                return m_enabled;
            }
            set
            {
                m_enabled = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le forward est-il partagé? (bind sur toutes les ips)
        /// </summary>
        /// -----------------------------------------------------------------------------
        public bool Shared
        {
            get
            {
                return m_shared;
            }
            set
            {
                m_shared = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Port local
        /// </summary>
        /// -----------------------------------------------------------------------------
        public int LocalPort
        {
            get
            {
                return m_localPort;
            }
            set
            {
                if (value < IPEndPoint.MinPort || value > IPEndPoint.MaxPort) throw new ArgumentException(String.Format(Strings.INVALID_PORT, IPEndPoint.MinPort, IPEndPoint.MaxPort));
                m_localPort = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Port destination
        /// </summary>
        /// -----------------------------------------------------------------------------
        public int RemotePort
        {
            get
            {
                return m_remotePort;
            }
            set
            {
                if (value < IPEndPoint.MinPort || value > IPEndPoint.MaxPort) throw new ArgumentException(String.Format(Strings.INVALID_PORT, IPEndPoint.MinPort, IPEndPoint.MaxPort));
                m_remotePort = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Adresse destination (FQDN ou IP)
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string Address
        {
            get
            {
                return m_address;
            }
            set
            {
                m_address = value;
            }
        }
        #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// -----------------------------------------------------------------------------
        public PortForward()
        { 
            m_localPort = 0;
            m_enabled = false;
            m_shared = false;
            m_remotePort = 0;
            m_address = "localhost";
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="config">la configuration</param>
        /// <param name="localPort">le port local</param>
        /// -----------------------------------------------------------------------------
        public PortForward (ConfigPackage config, int localPort)
        {
            m_localPort = localPort;
            string prefix = string.Format(CFG_FORWARD_TEMPLATE, localPort);

            if (config != null)
            {
                m_enabled = config.ValueBool(prefix + ClientConfig.WORD_ENABLED, false);
                m_shared = config.ValueBool(prefix + ClientConfig.WORD_SHARED, false);
                m_remotePort = config.ValueInt(prefix + ClientConfig.WORD_PORT, 0);
                m_address = config.Value(prefix + ClientConfig.WORD_ADDRESS, string.Empty);
            }

        }
        #endregion
    }
}
