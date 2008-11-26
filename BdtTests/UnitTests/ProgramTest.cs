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
using Bdt.Shared.Runtime;
#endregion

namespace Bdt.Tests.UnitTests
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Tests sur les fonctions simples
    /// </summary>
    /// -----------------------------------------------------------------------------
    [TestClass]
    public class ProgramTest : BaseTest
    {

        #region " Méthodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Test le codage des informations
        /// </summary>
        /// -----------------------------------------------------------------------------
        [TestMethod]
        public void TestStaticXorEncoder()
        {
            for (int key = 0; key < byte.MaxValue; key++)
            {
                for (int datalength = 0; datalength < 1024; datalength = (datalength == 0) ? 1 : datalength * 2)
                {
                    byte[] buffer = new byte[datalength];
                    byte[] outbuffer = new byte[datalength];

                    Random rnd = new Random();
                    rnd.NextBytes(buffer);
                    Array.Copy(buffer, outbuffer, datalength);

                    Program.StaticXorEncoder(ref buffer, key);
                    Program.StaticXorEncoder(ref buffer, key);

                    for (int i = 0; i < datalength; i++)
                    {
                        Assert.AreEqual(buffer[i], outbuffer[i], String.Format("Offset {0}, key={1}", i, key));
                    }
                }
            }
        }
        #endregion

    }
}
