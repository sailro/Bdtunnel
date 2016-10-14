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

namespace Bdt.Shared.Configuration
{
	public class SharedConfig
	{
		public const string TagAttribute = "@";
		public const string TagElement = "/";
		protected const string WordService = "service";
		protected const string WordProtocol = "protocol";
		protected const string WordName = "name";
		public const string WordPort = "port";
		public const string WordAddress = "address";
		protected const string WordUsername = "username";
		protected const string WordPassword = "password";
		public const string WordLogs = "logs";
		public const string WordConsole = "console";
		public const string WordFile = "file";
		protected const string WordCulture = "culture";

		private const string CfgServiceUsername = WordService + TagAttribute + WordUsername;
		private const string CfgServicePassword = WordService + TagAttribute + WordPassword;
		private const string CfgServiceProtocol = WordService + TagAttribute + WordProtocol;
		private const string CfgServiceName = WordService + TagAttribute + WordName;
		private const string CfgServicePort = WordService + TagAttribute + WordPort;
		private const string CfgServiceAddress = WordService + TagAttribute + WordAddress;
		private const string CfgServiceCulture = WordService + TagAttribute + WordCulture;

		public string ServiceUserName { get; set; }
		public string ServicePassword { get; set; }
		public string ServiceProtocol { get; set; }
		public string ServiceName { get; set; }
		public int ServicePort { get; set; }
		public string ServiceAddress { get; set; }
		public string ServiceCulture { get; private set; }

		public SharedConfig(ConfigPackage config)
		{
			if (config == null)
				return;

			ServiceUserName = config.Value(CfgServiceUsername, string.Empty);
			ServicePassword = config.Value(CfgServicePassword, string.Empty);
			ServiceProtocol = config.Value(CfgServiceProtocol, string.Empty);
			ServiceName = config.Value(CfgServiceName, string.Empty);
			ServicePort = config.ValueInt(CfgServicePort, 0);
			ServiceAddress = config.Value(CfgServiceAddress, string.Empty);
			ServiceCulture = config.Value(CfgServiceCulture, string.Empty);
		}
	}
}