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

namespace Bdt.Shared.Configuration
{
	public sealed class StringConfig : BaseConfig
	{
		private string[] Args { get; set; }

		public StringConfig(string[] args, int priority) : base(priority)
		{
			Args = args;
			Rehash();
		}


		public override void Rehash()
		{
			foreach (var arg in Args)
			{
				var equalIndex = arg.IndexOf(SourceItemEquals, System.StringComparison.Ordinal);
				if (equalIndex >= 0 && equalIndex + 1 < arg.Length)
					SetValue(arg.Substring(0, equalIndex), arg.Substring(equalIndex + 1));
			}
		}
	}
}
