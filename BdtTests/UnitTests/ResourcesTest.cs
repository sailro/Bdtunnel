/* BoutDuTunnel Copyright (c) 2007-2010 Sebastien LEBRETON

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

#region " Inclusions "
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bdt.Tests.Model;
#endregion

namespace Bdt.Tests.UnitTests
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Tests sur les fichiers de ressources localisées
    /// </summary>
    /// -----------------------------------------------------------------------------
    [TestClass]
    public class ResourcesTest : BaseTest
    {

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Test que les clefs présentes dans la traduction de référence sont également
        /// présentes dans chacune des traductions
        /// </summary>
        /// -----------------------------------------------------------------------------
        [TestMethod]
        public void TestTranslatedResources()
        {
            foreach (Project project in AllTranslatedProjects)
            {
                Dictionary<string, string> reference = ReadResources(project);
                foreach (Translation translation in AllTranslationsExceptDefault)
                {
                    Dictionary<string, string> translated = ReadResources(project, translation);

                    // default -> translated
                    foreach (string key in reference.Keys)
                    {
                        if (!translated.ContainsKey(key))
                        {
                            Assert.Fail(String.Format("Check project={0}, translation={1}, entry={2} doesn't exists", project, translation, key));
                        }
                    }

                    // translated -> default (reverse check)
                    foreach (string key in translated.Keys)
                    {
                        if (!reference.ContainsKey(key))
                        {
                            Assert.Fail(String.Format("Check project={0}, translation={1}, entry={2} doesn't exists in the default resource", project, translation, key));
                        }
                    }
                }
            }
        }
        #endregion    

    }
}
