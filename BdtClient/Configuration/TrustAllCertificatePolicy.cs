// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
#endregion

namespace Bdt.Client.Configuration
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Permet d'établir une relation de confiance systèmatique
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class TrustAllCertificatePolicy : ICertificatePolicy
    {

        #region " Methodes "
        public TrustAllCertificatePolicy()
        { }

        public bool CheckValidationResult(ServicePoint sp,
         X509Certificate cert, WebRequest req, int problem)
        {
            return true;
        }
        #endregion

    }

}
