/* BoutDuTunnel Copyright (c) 2007-2016 Sebastien LEBRETON

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
using System.Globalization;

namespace Bdt.Shared.Configuration
{
	public class ConfigPackage
	{
		private readonly List<BaseConfig> _sources = new List<BaseConfig>();

		public virtual string Value(string code, string defaultValue)
		{
			foreach (var source in _sources)
			{
				var result = source.Value(code, null);
				if (result == null)
					continue;

				return result;
			}
			return defaultValue;
		}

		public int ValueInt(string code, int defaultValue)
		{
			try
			{
				return int.Parse(Value(code, defaultValue.ToString(CultureInfo.InvariantCulture)));
			}
			catch (Exception)
			{
				return defaultValue;
			}
		}

		public bool ValueBool(string code, bool defaultValue)
		{
			try
			{
				return bool.Parse(Value(code, defaultValue.ToString()));
			}
			catch (Exception)
			{
				return defaultValue;
			}
		}

		public void AddSource(BaseConfig source)
		{
			_sources.Add(source);
			_sources.Sort();
		}

		public override string ToString()
		{
			string returnValue = string.Empty;
			foreach (var source in _sources)
				returnValue += source.ToString();

			return returnValue;
		}
	}
}