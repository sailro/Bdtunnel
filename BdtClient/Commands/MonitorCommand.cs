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
using System.Reflection;

using Bdt.Client.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Request;
using Bdt.Shared.Response;
using Bdt.Shared.Service;
#endregion

namespace Bdt.Client.Commands
{
    public class MonitorCommand : Command 
    {
        private class PropertyInfoComparer : IComparer<PropertyInfo>
        {
            int IComparer<PropertyInfo>.Compare(PropertyInfo a, PropertyInfo b)
            {
                return a.Name.CompareTo(b.Name);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Les switchs disponibles pour l'appel de la commande
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override string Switch
        {
            get
            {
                return "-m";
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
                return Strings.MONITOR_HELP;
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
        /// Affiche par reflexion le contenu d'un objet
        /// </summary>
        /// <param name="logger">le logger</param>
        /// <param name="indent">le niveau d'indentation</param>
        /// <param name="obj">l'objet</param>
        /// -----------------------------------------------------------------------------
        public void LogObject(ILogger logger, int indent, object obj)
        {
            string indentstr = "";
            while (indentstr.Length < indent) indentstr += " ";

            if (obj is Array)
            {
                Array objarray = obj as Array;
                logger.Log(this, indentstr + "{", ESeverity.INFO);
                int index = 0;
                foreach (object item in objarray)
                {
                    if (index > 0) logger.Log(this, indentstr + ",", ESeverity.INFO);
                    LogObject(logger, indent + 2, item);
                    index++;
                }
                logger.Log(this, indentstr + "}", ESeverity.INFO);
            }
            else
            {
                PropertyInfo[] properties = obj.GetType().GetProperties();
                Array.Sort<PropertyInfo>(properties, new PropertyInfoComparer()); 
                
                foreach (PropertyInfo prop in properties)
                {
                    object value = prop.GetValue(obj, null);
                    if (value is Array)
                    {
                        logger.Log(this, indentstr + string.Format("{0}=", prop.Name), ESeverity.INFO);
                        LogObject(logger, indent + 2, value);
                    }
                    else
                    {
                        logger.Log(this, indentstr + string.Format("{0}={1}", prop.Name, value), ESeverity.INFO);
                    }
                }
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
            MonitorResponse response = tunnel.Monitor(new SessionContextRequest(sid));
            if (response.Success)
            {
                LogObject(logger, 0, response.Sessions);
                logger.Log(this, string.Format(Strings.CURRENT_SID, sid.ToString("x")), ESeverity.INFO);
            }
            else
            {
                logger.Log(this, response.Message, ESeverity.ERROR);
            }
        }
    }
}