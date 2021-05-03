﻿/* BoutDuTunnel Copyright (c) 2006-2021 Sebastien Lebreton

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
using Bdt.Shared.Logs;
using Bdt.Shared.Service;

namespace Bdt.Client.Commands
{
	public abstract class Command
	{
		protected static IEnumerable<Command> GetCommands()
		{
			var result = new List<Command> {new HelpCommand(), new KillConnectionCommand(), new KillSessionCommand(), new MonitorCommand()};
			return result.ToArray();
		}

		public abstract string Switch { get; }

		public abstract string Help { get; }

		public abstract string[] ParametersName { get; }

		public abstract void Execute(string[] parameters, ILogger logger, ITunnel tunnel, int sid);

		public static bool FindAndExecute(string[] args, ILogger logger, ITunnel tunnel, int sid)
		{
			if (args.Length <= 0)
				return false;

			var sw = args[0];
			var parameters = new string[args.Length - 1];
			Array.ConstrainedCopy(args, 1, parameters, 0, parameters.Length);

			foreach (var cmd in GetCommands())
			{
				if ((sw != cmd.Switch) || (parameters.Length != cmd.ParametersName.Length))
					continue;

				cmd.Execute(parameters, logger, tunnel, sid);
				return true;
			}

			return false;
		}
	}
}
