#region Legal
/*
 * Copyright (c) 2015 The University of Pretoria.
 *
 * The following was designed for the Centre of GeoInformation
 * Science (CGIS), University of Pretoria. All code is property
 * of the University of Pretoria and is available under the 
 * Creative Commons Attribution-ShareAlike (CC BY-SA) see:
 * "https://creativecommons.org/licenses/"
 *
 * Author: Duran Cole
 * Email: u13329414@tuks.co.za
 * Author: Michael Nunes
 * Email: u12104592@tuks.co.za
 * Author: Molefe Molefe
 * Email: u12260429@tuks.co.za
 * Author: Tebogo Christopher Seshibe
 * Email: u13181442@tuks.co.za
 * Author: Timothy Snayers
 * Email: u13397134@tuks.co.za
 */
#endregion

using NUnit.Framework;
using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace DisplayModel.Test_Classes
{
    [TestFixture]
    class Test_DisplayModel
    {
        #region Fields
        private DisplayModel Display;
        private string[] args = { @"C:\Users\Duran\Desktop\NTT tests\Objects\Susan.obj", @"C:\Users\Duran\Desktop\NTT tests\Objects\TestImages\" };
        #endregion

        #region Setup
        [SetUp]
        public void init()
        {
            Display = new DisplayModel();
            Display.Run(args);
        }
        #endregion

        #region Screenshots
        [Test]
        public void TestScreenshotSuccess()
        {
            for (int i = 1; i < 12; ++i)
                Assert.True(File.Exists(@"C:\Users\Duran\Desktop\NTT tests\Objects\TestImages\view" + i + ".jpg"));
        }
        #endregion
    }
}
