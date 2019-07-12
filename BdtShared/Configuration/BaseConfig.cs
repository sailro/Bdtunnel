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
using System.Collections;

namespace Bdt.Shared.Configuration
{
	public abstract class BaseConfig : IComparable
	{
		protected const string SourcePathSeparator = "/";
		public const string SourceItemAttribute = "@";
		protected const string SourceItemEquals = "=";

		private readonly SortedList _values = new SortedList();

		private int Priority { get; set; }

		public string Value(string code, string defaultValue)
		{
			return _values.ContainsKey(code) ? Convert.ToString(_values[code]) : defaultValue;
		}

		public void SetValue(string code, string value)
		{
			if (_values.ContainsKey(code))
				_values[code] = value;
			else
				_values.Add(code, value);
		}

		protected BaseConfig(int priority)
		{
			Priority = priority;
		}

		public abstract void Rehash();

		public sealed override string ToString()
		{
			var returnValue = string.Empty;

			foreach (string key in _values.Keys)
				returnValue += "   <" + GetType().Name + "(" + Priority + ")" + "> [" + key + "] " + SourceItemEquals + " [" + Value(key, string.Empty) + "]" + "\r\n";

			return returnValue;
		}

		public int CompareTo(object obj)
		{
			var baseConfig = obj as BaseConfig;
			if (baseConfig != null)
				return Priority - baseConfig.Priority;

			return 0;
		}
	}
}
