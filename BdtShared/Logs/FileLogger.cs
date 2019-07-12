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

using System.IO;
using Bdt.Shared.Configuration;

namespace Bdt.Shared.Logs
{
	public class FileLogger : BaseLogger
	{
		public const string ConfigAppend = "append";
		public const string ConfigFilename = "filename";

		public string Filename { get; protected set; }
		public bool Append { get; protected set; }

		protected FileLogger()
		{
		}

		public FileLogger(string prefix, ConfigPackage config) : base(null, prefix, config)
		{
			Filename = config.Value(prefix + BaseConfig.SourceItemAttribute + ConfigFilename, Filename);
			Append = config.ValueBool(prefix + BaseConfig.SourceItemAttribute + ConfigAppend, Append);

			if (Enabled)
				Writer = new StreamWriter(Filename, Append, System.Text.Encoding.Default);
		}
	}
}
