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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Bdt.Client.Configuration;
#endregion

namespace Bdt.GuiClient.Forms
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Fenêtre d'authentification sur le proxy HTTP
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class ProxyForm : Form
    {

        #region " Attributs "
        protected ClientConfig m_clientConfig;
        #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="clientConfig">la configuration du client</param>
        /// -----------------------------------------------------------------------------
        public ProxyForm (ClientConfig clientConfig)
        {
            InitializeComponent();
            m_clientConfig = clientConfig;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Chargement de la page
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void ProxyForm_Load (object sender, EventArgs e)
        {
            UserNameEdit.Text = m_clientConfig.ProxyUserName;
            PasswordEdit.Text = m_clientConfig.ProxyPassword;
            DomainEdit.Text = m_clientConfig.ProxyDomain;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// On applique les nouvelles informations d'authentification
        /// </summary>
        /// <param name="sender">l'appelant</param>
        /// <param name="e">les parametres</param>
        /// -----------------------------------------------------------------------------
        private void Apply_Click (object sender, EventArgs e)
        {
            m_clientConfig.ProxyUserName = UserNameEdit.Text;
            m_clientConfig.ProxyPassword = PasswordEdit.Text;
            m_clientConfig.ProxyDomain = DomainEdit.Text;
        }
        #endregion

    }
}