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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using Bdt.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion

namespace Bdt.Tests.UnitTests
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Le corps d'un test unitaire
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class BaseTest
    {
        #region " Attributs "
        private TestContext testContextInstance;
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        /// -----------------------------------------------------------------------------
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// La liste de tous les projets
        /// </summary>
        /// -----------------------------------------------------------------------------
        public IEnumerable<Project> AllProjects
        {
            get
            {
                return System.Enum.GetValues(typeof(Project)).OfType<Project>(); 
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// La liste de tous les projets traduits
        /// </summary>
        /// -----------------------------------------------------------------------------
        public IEnumerable<Project> AllTranslatedProjects
        {
            get
            {
                return AllProjects.Where(p => p != Project.BdtWebServer).ToArray();
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// La liste des traductions
        /// </summary>
        /// -----------------------------------------------------------------------------
        public IEnumerable<Translation> AllTranslations
        {
            get
            {
                return System.Enum.GetValues(typeof(Translation)).OfType<Translation>();
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// La liste des traductions, sauf celle(s) par défaut
        /// </summary>
        /// -----------------------------------------------------------------------------
        public IEnumerable<Translation> AllTranslationsExceptDefault
        {
            get
            {
                return System.Enum.GetValues(typeof(Translation)).OfType<Translation>().Where(t => ((int)t)!= (int)Translation.Default);
            }
        }
        #endregion

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retrouve le répertoire du projet actif
        /// </summary>
        /// <param name="project">Le projet actif</param>
        /// <returns>le répertoire</returns>
        /// -----------------------------------------------------------------------------
        public string GetProjectDirectory(Project project)
        {
            return Path.GetFullPath(Path.Combine(Path.Combine(Path.Combine(TestContext.TestDir, ".."), ".."), project.ToString()));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retrouve le répertoire du projet actif, suivant la cible courante (debug,
        /// release) en tenant compte d'un sous-répertoire
        /// </summary>
        /// <param name="project">Le projet actif</param>
        /// <param name="directory">le sous répertoire</param>
        /// <returns>le répertoire</returns>
        /// -----------------------------------------------------------------------------
        public string GetProjectTargetDirectory(Project project, string directory)
        {
#if DEBUG
            return Path.Combine(Path.Combine(GetProjectDirectory(project), directory), "Debug");
#else
            return Path.Combine(Path.Combine(GetProjectDirectory(project), directory), "Release");
#endif
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retrouve le répertoire 'obj' du projet actif, suivant la cible courante (debug,
        /// </summary>
        /// <param name="project">Le projet actif</param>
        /// <returns>le répertoire</returns>
        /// -----------------------------------------------------------------------------
        public string GetProjectObjDirectory(Project project)
        {
            return GetProjectTargetDirectory(project, "obj");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retrouve le répertoire 'bin' du projet actif, suivant la cible courante (debug,
        /// </summary>
        /// <param name="project">Le projet actif</param>
        /// <returns>le répertoire</returns>
        /// -----------------------------------------------------------------------------
        public string GetProjectBinDirectory(Project project)
        {
            return GetProjectTargetDirectory(project, "bin");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Remplissage d'une table de hash depuis un ensemble de keypairs
        /// </summary>
        /// <typeparam name="K">type de clef</typeparam>
        /// <typeparam name="V">type de valeur</typeparam>
        /// <param name="dic">le conteneur cible</param>
        /// <param name="enumerator">le conteneur source</param>
        /// -----------------------------------------------------------------------------
        public void FillDictionary<K, V>(Dictionary<K, V> dic, IDictionaryEnumerator enumerator)
        {
            dic.Clear();
            while (enumerator.MoveNext())
            {
                dic.Add((K)enumerator.Key, (V)enumerator.Value);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Lecture des clefs de ressources
        /// </summary>
        /// <param name="project">Le nom du projet</param>
        /// <param name="translation">La traduction</param>
        /// <returns>Une table de hash des clefs/valeurs de ressources</returns>
        /// -----------------------------------------------------------------------------
        public Dictionary<string, string> ReadResources(Project project, Translation translation)
        {
            Dictionary<string, string> result = new Dictionary<String, String>();
            string resourceTemplate = Path.Combine(GetProjectObjDirectory(project), "Bdt.{0}.Resources.Strings{1}.resources");

            string suffix = (translation == Translation.Default) ? string.Empty : "." + translation.ToString().ToLower();
            string filename = String.Format(resourceTemplate, project.ToString().Replace("Bdt", ""), suffix);

            TestContext.WriteLine("Reading resource: {0}", filename);
            using (ResourceReader defReader = new ResourceReader(filename))
            {
                FillDictionary(result, defReader.GetEnumerator());
            }
            TestContext.WriteLine("Key count: {0}", result.Count);

            return result;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Lecture des clefs de ressources de la langue par défaut
        /// </summary>
        /// <param name="project">Le nom du projet</param>
        /// <returns>Une table de hash des clefs/valeurs de ressources</returns>
        /// -----------------------------------------------------------------------------
        public Dictionary<String, String> ReadResources(Project project)
        {
            return ReadResources(project, Translation.Default);
        }
        #endregion

    }
}
