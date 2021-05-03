/* BoutDuTunnel Copyright (c) 2006-2021 Sebastien Lebreton

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

using System.Xml;

namespace Bdt.Shared.Configuration
{
	public sealed class XmlConfig : BaseConfig
	{
		private string FileName { get; }

		public XmlConfig(string filename, int priority) : base(priority)
		{
			FileName = filename;
			Rehash();
		}

		private void ParseNode(string path, XmlNode node)
		{
			foreach (XmlNode subnode in node.ChildNodes)
			{
				if (subnode.Attributes != null)
				{
					foreach (XmlAttribute attr in subnode.Attributes)
					{
						if (attr.Value != string.Empty)
							SetValue(path + subnode.Name + SourceItemAttribute + attr.Name, attr.Value);
					}

					if (subnode.InnerText != string.Empty)
						SetValue(path + subnode.Name, subnode.InnerText);
				}

				if (subnode.HasChildNodes && (subnode.ChildNodes[0].NodeType == XmlNodeType.Element || subnode.ChildNodes[0].NodeType == XmlNodeType.Comment))
				{
					ParseNode(path + subnode.Name + SourcePathSeparator, subnode);
				}
			}
		}

		public override void Rehash()
		{
			var docXml = new XmlDocument();
			docXml.Load(FileName);
			ParseNode(string.Empty, docXml.DocumentElement);
		}
	}
}
