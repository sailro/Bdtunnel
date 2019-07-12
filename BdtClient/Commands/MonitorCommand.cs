/* BoutDuTunnel Copyright (c) 2006-2019 Sebastien Lebreton

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

using System;
using System.Collections.Generic;
using System.Reflection;
using Bdt.Client.Resources;
using Bdt.Shared.Logs;
using Bdt.Shared.Request;
using Bdt.Shared.Service;

namespace Bdt.Client.Commands
{
	public class MonitorCommand : Command
	{
		private class PropertyInfoComparer : IComparer<PropertyInfo>
		{
			int IComparer<PropertyInfo>.Compare(PropertyInfo a, PropertyInfo b)
			{
				return string.Compare(a.Name, b.Name, StringComparison.Ordinal);
			}
		}

		public override string Switch
		{
			get { return "-m"; }
		}

		public override string Help
		{
			get { return Strings.MONITOR_HELP; }
		}

		public override string[] ParametersName
		{
			get { return new string[] { }; }
		}

		public void LogObject(ILogger logger, int indent, object obj)
		{
			var indentstr = "";
			while (indentstr.Length < indent) indentstr += " ";

			var array = obj as Array;
			if (array != null)
			{
				var objarray = array;
				logger.Log(this, indentstr + "{", ESeverity.INFO);
				var index = 0;
				foreach (var item in objarray)
				{
					if (index > 0) logger.Log(this, indentstr + ",", ESeverity.INFO);
					LogObject(logger, indent + 2, item);
					index++;
				}

				logger.Log(this, indentstr + "}", ESeverity.INFO);
			}
			else
			{
				var properties = obj.GetType().GetProperties();
				Array.Sort(properties, new PropertyInfoComparer());

				foreach (var prop in properties)
				{
					var value = prop.GetValue(obj, null);
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

		public override void Execute(string[] parameters, ILogger logger, ITunnel tunnel, int sid)
		{
			var response = tunnel.Monitor(new SessionContextRequest(sid));
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
