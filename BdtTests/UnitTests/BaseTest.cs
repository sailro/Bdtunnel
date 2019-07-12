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
		private TestContext _testContextInstance;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ConvertToAutoProperty
		public TestContext TestContext
// ReSharper restore ConvertToAutoProperty
// ReSharper restore MemberCanBeProtected.Global
		{
			get { return _testContextInstance; }
// ReSharper disable UnusedMember.Global
			set
// ReSharper restore UnusedMember.Global
			{
				_testContextInstance = value;
			}
		}

		private static IEnumerable<Project> AllProjects
		{
			get { return Enum.GetValues(typeof(Project)).OfType<Project>(); }
		}

		protected IEnumerable<Project> AllTranslatedProjects
		{
			get { return AllProjects.Where(p => p != Project.BdtWebServer).ToArray(); }
		}

		protected IEnumerable<Translation> AllTranslationsExceptDefault
		{
			get { return Enum.GetValues(typeof(Translation)).OfType<Translation>().Where(t => ((int)t) != (int)Translation.Default); }
		}

		private string GetProjectDirectory(Project project)
		{
			return Path.GetFullPath(Path.Combine(Path.Combine(Path.Combine(TestContext.TestDir, ".."), ".."), project.ToString()));
		}

		private string GetProjectTargetDirectory(Project project, string directory)
		{
#if DEBUG
			return Path.Combine(Path.Combine(GetProjectDirectory(project), directory), "Debug");
#else
			return Path.Combine(Path.Combine(GetProjectDirectory(project), directory), "Release");
#endif
		}

		private string GetProjectObjDirectory(Project project)
		{
			return GetProjectTargetDirectory(project, "obj");
		}

// ReSharper disable InconsistentNaming
		private static void FillDictionary<K, V>(IDictionary<K, V> dic, IDictionaryEnumerator enumerator)
// ReSharper restore InconsistentNaming
		{
			dic.Clear();
			while (enumerator.MoveNext())
			{
				dic.Add((K)enumerator.Key, (V)enumerator.Value);
			}
		}

		protected Dictionary<string, string> ReadResources(Project project, Translation translation = Translation.Default)
		{
			var result = new Dictionary<string, string>();
			var resourceTemplate = Path.Combine(GetProjectObjDirectory(project), "Bdt.{0}.Resources.Strings{1}.resources");

			var suffix = translation == Translation.Default ? string.Empty : "." + translation.ToString().ToLower();
			var filename = string.Format(resourceTemplate, project.ToString().Replace("Bdt", ""), suffix);

			TestContext.WriteLine("Reading resource: {0}", filename);
			using (var defReader = new ResourceReader(filename))
				FillDictionary(result, defReader.GetEnumerator());

			TestContext.WriteLine("Key count: {0}", result.Count);

			return result;
		}
	}
}
