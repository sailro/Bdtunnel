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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using Bdt.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bdt.Tests.UnitTests
{
	public class BaseTest
	{
		public TestContext TestContext
		{
			get;
			set;
		}

		private static IEnumerable<Project> AllProjects
		{
			get { return Enum.GetValues(typeof(Project)).OfType<Project>(); }
		}

		protected IEnumerable<Project> AllTranslatedProjects
		{
			get { return AllProjects.Where(p => p != Project.WebServer).ToArray(); }
		}

		protected IEnumerable<Translation> AllTranslationsExceptDefault
		{
			get { return Enum.GetValues(typeof(Translation)).OfType<Translation>().Where(t => ((int)t) != (int)Translation.Default); }
		}

		private string GetProjectObjDirectory(Project project)
		{
			var assembly = GetType().Assembly;
			var basePath = Path.GetDirectoryName(assembly.Location);
			Assert.IsNotNull(basePath);

			var objPath = basePath.Replace("\\bin\\", "\\obj\\");
			var projectObjPath = objPath.Replace(assembly.GetName().Name, $"Bdt.{project}");
			return projectObjPath;
		}

// ReSharper disable InconsistentNaming
		private static void FillDictionary<K, V>(IDictionary<K, V> dic, IDictionaryEnumerator enumerator)
// ReSharper restore InconsistentNaming
		{
			dic.Clear();
			while (enumerator.MoveNext())
			{
				var key = (K)enumerator.Key;
				if (key != null)
					dic.Add(key, (V)enumerator.Value);
			}
		}

		protected Dictionary<string, string> ReadResources(Project project, Translation translation = Translation.Default)
		{
			var result = new Dictionary<string, string>();
			var resourceTemplate = Path.Combine(GetProjectObjDirectory(project), "Bdt.{0}.Resources.Strings{1}.resources");

			var suffix = translation == Translation.Default ? string.Empty : "." + translation.ToString().ToLower();
			var filename = string.Format(resourceTemplate, project.ToString(), suffix);

			TestContext.WriteLine("Reading resource: {0}", filename);
			using (var defReader = new ResourceReader(filename))
				FillDictionary(result, defReader.GetEnumerator());

			TestContext.WriteLine("Key count: {0}", result.Count);

			return result;
		}
	}
}
