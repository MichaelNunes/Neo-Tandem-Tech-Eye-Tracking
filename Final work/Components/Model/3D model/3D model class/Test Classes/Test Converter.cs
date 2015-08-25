using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics;

using NUnit.Framework;
using System.IO;

namespace DisplayModel.Test_Classes
{
    [TestFixture]
    class Test_Converter
    {
        #region Fields
        //private GameObject Cube;
        //private GameObject Susan;
        //private GameObject Predator;

        DisplayModel Cube;
        DisplayModel Susan;
        DisplayModel Predator;

        private string CubeUrl = @"C:\Users\COS301\Documents\Objects\Cube.obj";
        private string SusanUrl = @"C:\Users\COS301\Documents\Objects\Susan.obj";
        private string PredatorUrl = @"C:\Users\COS301\Documents\Objects\Predator.obj";
        #endregion

        #region Setup
        [SetUp]
        public void init()
        {
            Cube = new DisplayModel();
            Susan = new DisplayModel();
            Predator = new DisplayModel();

            //Cube = Converter.fromOBJ(CubeUrl, "");
            //Susan = Converter.fromOBJ(SusanUrl, "");
            //Predator = Converter.fromOBJ(PredatorUrl, "");
        }
        #endregion

        #region Screenshots
        [Test]
        public void CreateScreenshots()
        {
            Cube.Run(CubeUrl);
            Assert.AreEqual(File.Exists(@"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\TestImages\Test1.jpg"), true);
        }
        #endregion

        #region BufferData
        [Test]
        public void TestBufferCounts()
        {
            
        }

        [Test]
        public void TestDefaultMatrices()
        {

        }
        #endregion

        #region Material
        [Test]
        public void TestDefaultMaterial()
        {

        }
        #endregion

        #region Transform
        [Test]
        public void TestDefaultTransform()
        {
            /*Assert.AreEqual(Vector3.Zero, Cube.Transform.Position);
            Assert.AreEqual(Vector3.Zero, Cube.Transform.Rotation);
            Assert.AreEqual(Vector3.One, Cube.Transform.Scale);

            Assert.AreEqual(Vector3.Zero, Susan.Transform.Position);
            Assert.AreEqual(Vector3.Zero, Susan.Transform.Rotation);
            Assert.AreEqual(Vector3.One, Susan.Transform.Scale);

            Assert.AreEqual(Vector3.Zero, Predator.Transform.Position);
            Assert.AreEqual(Vector3.Zero, Predator.Transform.Rotation);
            Assert.AreEqual(Vector3.One, Predator.Transform.Scale);*/
        }
        #endregion
    }
}
