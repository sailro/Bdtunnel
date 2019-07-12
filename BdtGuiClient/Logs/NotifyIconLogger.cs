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

using System.Windows.Forms;
using Bdt.Shared.Logs;
using Bdt.Shared.Configuration;
using Bdt.GuiClient.Runtime;

namespace Bdt.GuiClient.Logs
{
	internal class NotifyIconLogger : BaseLogger
	{
		private BdtGuiClient _guiclient;
		private readonly string _tipTitle;
		private readonly int _timeout;

		public NotifyIconLogger(string prefix, ConfigPackage config, BdtGuiClient guiclient, string tipTitle, int timeout)
			: base(null, prefix, config)
		{
			_guiclient = guiclient;
			_tipTitle = tipTitle;
			_timeout = timeout;
		}

		public override void Log(object sender, string message, ESeverity severity)
		{
			if (severity == ESeverity.ERROR && (_guiclient != null) && (_guiclient.MainComponent != null) &&
			    _guiclient.MainComponent.NotifyIcon != null)
				_guiclient.MainComponent.NotifyIcon.ShowBalloonTip(_timeout, _tipTitle, message, ToolTipIcon.Error);
		}

		public override void Close()
		{
			_guiclient = null;
		}
	}
}
