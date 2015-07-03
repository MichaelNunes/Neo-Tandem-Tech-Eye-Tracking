using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using OpenTK;
using OpenTK.Graphics;
using DisplayModel;

using NUnit.Framework;

namespace DisplayModel.Test_Classes
{
    [TestFixture]
    class Test_Converter
    {
        #region Fields
        Model3D Cube;
        Model3D Susan;
        Model3D Predator;

        private string CubeUrl = "Objects/Cube.obj";
        private string SusanUrl = "Objects/Susan.obj";
        private string PredatorUrl = "Objects/Predator.obj";
        #endregion

        #region Setup
        [SetUp]
        public void init()
        {
            Cube = Converter.fromOBJ(CubeUrl, null);
            Susan = Converter.fromOBJ(SusanUrl, null);
            Predator = Converter.fromOBJ(PredatorUrl, null);
        }
        #endregion

        #region BufferData Testing
        [Test]
        public void BufferCounts()
        {
            //Vertices
            Assert.AreEqual(1, Cube.Model3DWindow.Length);
            Assert.AreEqual(36, Cube.Model3DWindow[0].Vertex.Length);

            Assert.AreEqual(1, Susan.Model3DWindow.Length);
            Assert.AreEqual(2904, Susan.Model3DWindow[0].Vertex.Length);

            Assert.AreEqual(1, Predator.Model3DWindow.Length);
            Assert.AreEqual(2960157, Predator.Model3DWindow[0].Vertex.Length);

            //Colours
            Assert.AreEqual(1, Cube.Model3DWindow.Length);
            Assert.AreEqual(36, Cube.Model3DWindow[0].Colour.Length);

            Assert.AreEqual(1, Susan.Model3DWindow.Length);
            Assert.AreEqual(2904, Susan.Model3DWindow[0].Colour.Length);

            Assert.AreEqual(1, Predator.Model3DWindow.Length);
            Assert.AreEqual(2960157, Predator.Model3DWindow[0].Colour.Length);

            //Texture Coordinates
            Assert.AreEqual(1, Cube.Model3DWindow.Length);
            Assert.AreEqual(0, Cube.Model3DWindow[0].Texture.Length);

            Assert.AreEqual(1, Susan.Model3DWindow.Length);
            Assert.AreEqual(0, Susan.Model3DWindow[0].Texture.Length);

            Assert.AreEqual(1, Predator.Model3DWindow.Length);
            Assert.AreEqual(0, Predator.Model3DWindow[0].Texture.Length);

            //Normals
            Assert.AreEqual(1, Cube.Model3DWindow.Length);
            Assert.AreEqual(36, Cube.Model3DWindow[0].Normal.Length);

            Assert.AreEqual(1, Susan.Model3DWindow.Length);
            Assert.AreEqual(2904, Susan.Model3DWindow[0].Normal.Length);

            Assert.AreEqual(1, Predator.Model3DWindow.Length);
            Assert.AreEqual(2960157, Predator.Model3DWindow[0].Normal.Length);
        }
        #endregion

        #region Material Testing

        [Test]
        public void DefaultMaterialsUsed()
        {
            Assert.AreEqual(Color4.LightGray, Cube.Material.Colour);
            Assert.AreEqual(string.Empty, Cube.Material.FilePath);
            Assert.AreEqual(Color4.LightGray, Susan.Material.Colour);
            Assert.AreEqual(string.Empty, Susan.Material.FilePath);
            Assert.AreEqual(Color4.LightGray, Predator.Material.Colour);
            Assert.AreEqual(string.Empty, Predator.Material.FilePath);
        }
        #endregion

        #region Transform Testing
        [Test]
        public void DefaultTransformationsUsed()
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
