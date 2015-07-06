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
        private Model3D Cube;
        private Model3D Susan;
        private Model3D Predator;

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
            Assert.AreEqual(1, Cube.Model3DWindow.Length);
            Assert.AreEqual(1, Susan.Model3DWindow.Length);
            Assert.AreEqual(1, Predator.Model3DWindow.Length);

            Assert.AreEqual(36, Cube.Model3DWindow[0].Vertex.Length);
            Assert.AreEqual(2904, Susan.Model3DWindow[0].Vertex.Length);
            Assert.AreEqual(2960157, Predator.Model3DWindow[0].Vertex.Length);

            Assert.AreEqual(36, Cube.Model3DWindow[0].Colour.Length);
            Assert.AreEqual(2904, Susan.Model3DWindow[0].Colour.Length);
            Assert.AreEqual(2960157, Predator.Model3DWindow[0].Colour.Length);

            Assert.AreEqual(0, Cube.Model3DWindow[0].Texture.Length);
            Assert.AreEqual(0, Susan.Model3DWindow[0].Texture.Length);
            Assert.AreEqual(0, Predator.Model3DWindow[0].Texture.Length);

            Assert.AreEqual(36, Cube.Model3DWindow[0].Normal.Length);
            Assert.AreEqual(2904, Susan.Model3DWindow[0].Normal.Length);
            Assert.AreEqual(2960157, Predator.Model3DWindow[0].Normal.Length);
        }

        [Test]
        public void TestDefaultMatrices()
        {
            Assert.AreEqual(Matrix4.Identity, Cube.Model3DWindow[0].ModelMatrix);
            Assert.AreEqual(Matrix4.Identity, Cube.Model3DWindow[0].ViewProjectionMatrix);
            Assert.AreEqual(Matrix4.Identity, Cube.Model3DWindow[0].ModelViewProjectionMatrix);

            Assert.AreEqual(Matrix4.Identity, Susan.Model3DWindow[0].ModelMatrix);
            Assert.AreEqual(Matrix4.Identity, Susan.Model3DWindow[0].ViewProjectionMatrix);
            Assert.AreEqual(Matrix4.Identity, Susan.Model3DWindow[0].ModelViewProjectionMatrix);

            Assert.AreEqual(Matrix4.Identity, Predator.Model3DWindow[0].ModelMatrix);
            Assert.AreEqual(Matrix4.Identity, Predator.Model3DWindow[0].ViewProjectionMatrix);
            Assert.AreEqual(Matrix4.Identity, Predator.Model3DWindow[0].ModelViewProjectionMatrix);
        }
        #endregion

        #region Material
        [Test]
        public void TestDefaultMaterial()
        {
            Assert.AreEqual(Color4.LightGray, Cube.Material.Colour);
            Assert.AreEqual(Color4.LightGray, Susan.Material.Colour);
            Assert.AreEqual(Color4.LightGray, Predator.Material.Colour);

            Assert.AreEqual("", Cube.Material.FilePath);
            Assert.AreEqual("", Susan.Material.FilePath);
            Assert.AreEqual("", Predator.Material.FilePath);
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
