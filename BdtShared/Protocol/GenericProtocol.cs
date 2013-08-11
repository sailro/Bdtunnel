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
using Bdt.Shared.Configuration;
using Bdt.Shared.Service;
#endregion

namespace Bdt.Shared.Protocol
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Classe générique pour un protocole de communication
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class GenericProtocol : Logs.LoggedObject
    {

        #region " Proprietes "

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le nom du serveur
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    protected string Name { get; private set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// Le port du serveur
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    protected int Port { get; private set; }

	    /// -----------------------------------------------------------------------------
	    /// <summary>
	    /// L'adresse du serveur
	    /// </summary>
	    /// -----------------------------------------------------------------------------
	    protected string Address { get; private set; }

	    #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Configuration côté serveur
        /// </summary>
        /// <param name="type">le type d'objet à rendre distant</param>
        /// -----------------------------------------------------------------------------
        public abstract void ConfigureServer(Type type);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Configuration côté client
        /// </summary>
        /// -----------------------------------------------------------------------------
        public abstract void ConfigureClient();

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Dé-configuration côté client
        /// </summary>
        /// -----------------------------------------------------------------------------
        public abstract void UnConfigureClient();

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Déconfiguration côté serveur
        /// </summary>
        /// -----------------------------------------------------------------------------
        public abstract void UnConfigureServer();

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retourne une instance de tunnel
        /// </summary>
        /// <returns>une instance de tunnel</returns>
        /// -----------------------------------------------------------------------------
        public abstract ITunnel GetTunnel();

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtient un protocole adapté d'après la configuration
        /// </summary>
        /// <param name="config">la configuration contenant le type du protocole</param>
        /// <returns>une instance adaptée</returns>
        /// -----------------------------------------------------------------------------
        public static GenericProtocol GetInstance(SharedConfig config)
        {
            var protoObj = ((GenericProtocol)typeof(GenericProtocol).Assembly.CreateInstance(config.ServiceProtocol));
            if (protoObj == null)
                throw new NotSupportedException(config.ServiceProtocol);

			protoObj.Name = config.ServiceName;
            protoObj.Port = config.ServicePort;
            protoObj.Address = config.ServiceAddress;
            return protoObj;
        }
        #endregion

    }

}

