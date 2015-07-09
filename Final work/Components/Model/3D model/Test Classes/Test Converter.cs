using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics;

using NUnit.Framework;

namespace DisplayModel.Test_Classes
{
    [TestFixture]
    class Test_Converter
    {
        #region Fields
        private GameObject Cube;
        private GameObject Susan;
        private GameObject Predator;

        private string CubeUrl = "Objects/Cube.obj";
        private string SusanUrl = "Objects/Susan.obj";
        private string PredatorUrl = "Objects/Predator.obj";
        #endregion

        #region Setup
        [SetUp]
        public void init()
        {
            Cube = Converter.fromOBJ(CubeUrl, "");
            Susan = Converter.fromOBJ(SusanUrl, "");
            Predator = Converter.fromOBJ(PredatorUrl, "");
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
            Assert.AreEqual(Vector3.Zero, Cube.Transform.Position);
            Assert.AreEqual(Vector3.Zero, Cube.Transform.Rotation);
            Assert.AreEqual(Vector3.One, Cube.Transform.Scale);

            Assert.AreEqual(Vector3.Zero, Susan.Transform.Position);
            Assert.AreEqual(Vector3.Zero, Susan.Transform.Rotation);
            Assert.AreEqual(Vector3.One, Susan.Transform.Scale);

            Assert.AreEqual(Vector3.Zero, Predator.Transform.Position);
            Assert.AreEqual(Vector3.Zero, Predator.Transform.Rotation);
            Assert.AreEqual(Vector3.One, Predator.Transform.Scale);
        }
        #endregion
    }
}
