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
using System.Globalization;
using System.IO;
using System.ComponentModel;

namespace Bdt.Shared.Logs
{
	[Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
	public abstract class BaseLogger : ILogger
	{
		public const string ConfigEnabled = "enabled";
		public const string ConfigFilter = "filter";
		public const string ConfigDateFormat = "dateformat";
		public const string ConfigStringFormat = "stringformat";
		private const string ConfigTagStart = "{";
		private const string ConfigTagEnd = "}";

		// ReSharper disable InconsistentNaming
		private enum ETags
		{
			TIMESTAMP = 0,
			SEVERITY = 1,
			TYPE = 2,
			MESSAGE = 3
		}

		// ReSharper restore InconsistentNaming

		protected TextWriter Writer;

		public bool Enabled { get; set; }
		public string StringFormat { get; set; }
		public string DateFormat { get; protected set; }
		public ESeverity Filter { get; protected set; }

		protected BaseLogger()
		{
			Filter = ESeverity.DEBUG;
			DateFormat = "dd/MM/yyyy HH:mm:ss";
			StringFormat = ConfigTagStart + ETags.TIMESTAMP + ConfigTagEnd + " " + ConfigTagStart + ETags.SEVERITY + ConfigTagEnd + " [" + ConfigTagStart + ETags.TYPE +
			               ConfigTagEnd + "] " + ConfigTagStart + ETags.MESSAGE + ConfigTagEnd;
			Enabled = true;
		}

		protected BaseLogger(TextWriter writer, string prefix, Configuration.ConfigPackage config)
		{
			Writer = writer;

			Enabled = config.ValueBool(prefix + Configuration.BaseConfig.SourceItemAttribute + ConfigEnabled, Enabled);
			Filter = ((ESeverity) Enum.Parse(typeof(ESeverity), config.Value(prefix + Configuration.BaseConfig.SourceItemAttribute + ConfigFilter, Filter.ToString())));
			DateFormat = config.Value(prefix + Configuration.BaseConfig.SourceItemAttribute + ConfigDateFormat, DateFormat);
			StringFormat = config.Value(prefix + Configuration.BaseConfig.SourceItemAttribute + ConfigStringFormat, StringFormat);
		}

		protected BaseLogger(TextWriter writer, string dateFormat, ESeverity filter)
		{
			StringFormat = ConfigTagStart + ETags.TIMESTAMP + ConfigTagEnd + " " + ConfigTagStart + ETags.SEVERITY + ConfigTagEnd + " [" + ConfigTagStart + ETags.TYPE +
			               ConfigTagEnd + "] " + ConfigTagStart + ETags.MESSAGE + ConfigTagEnd;
			Enabled = true;
			Writer = writer;
			DateFormat = dateFormat;
			Filter = filter;
		}

		public virtual void Log(object sender, string message, ESeverity severity)
		{
			if (!Enabled || (severity < Filter) || (Writer == null))
				return;

			var format = StringFormat;
			foreach (ETags tag in Enum.GetValues(typeof(ETags)))
				format = format.Replace(tag.ToString(), Convert.ToInt32(tag).ToString(CultureInfo.InvariantCulture));

			Writer.WriteLine(format, DateTime.Now.ToString(DateFormat), severity, sender.GetType().Name, message);
			Writer.Flush();
		}

		public virtual void Close()
		{
			if (Writer == null)
				return;

			// ReSharper disable EmptyGeneralCatchClause
			try
			{
				Writer.Close();
			}
			catch
			{
			}
			// ReSharper restore EmptyGeneralCatchClause
			Writer = null;
		}

		~BaseLogger()
		{
			Close();
		}
	}
}