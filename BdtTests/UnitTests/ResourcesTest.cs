// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

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
