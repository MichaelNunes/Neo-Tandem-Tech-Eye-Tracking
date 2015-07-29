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

using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;

namespace DisplayModel.Test_Classes
{
    [TestFixture]
    class Test_Converter
    {
        #region Fields
        private GameObject Cube;
        private GameObject Susan;
        private GameObject Predator;

        //Duran Tests
        private string CubeUrl = @"C:\Users\Duran\Desktop\NTT tests\Objects\Cube.obj";
        private string CubeImageUrl = @"C:\Users\Duran\Desktop\NTT tests\Objects\Cube.png";
        private string SusanUrl = @"C:\Users\Duran\Desktop\NTT tests\Objects\Susan.obj";

        //General Tests
        //private string CubeUrl = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\Objects\Cube.obj";
        //private string CubeImageUrl = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\Objects\Cube.jpg";
        //private string SusanUrl = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\Objects\Susan.obj";
        //private string PredatorUrl = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\Objects\Predator.obj";
        #endregion

        #region Setup
        [SetUp]
        public void init()
        {
            Cube = Converter.fromOBJ(CubeUrl, CubeImageUrl);
            Cube.Material.Setup();
            Susan = Converter.fromOBJ(SusanUrl, string.Empty);
            //Predator = Converter.fromOBJ(PredatorUrl, string.Empty);
        }
        #endregion

        #region BufferData
        [Test]
        public void TestDefaultMatrices()
        {
            // VERTEX ARRAY
            Assert.AreEqual(36, Cube.BufferData.Vertex.Length);
            Assert.AreEqual(2904, Susan.BufferData.Vertex.Length);
            //Assert.AreEqual(2960157, Predator.BufferData.Vertex.Length);

            // NORMAL ARRAY
            Assert.AreEqual(36, Cube.BufferData.Normal.Length);
            Assert.AreEqual(2904, Susan.BufferData.Normal.Length);
            //Assert.AreEqual(2960157, Predator.BufferData.Normal.Length);

            // COLOUR ARRAY
            Assert.AreEqual(36, Cube.BufferData.Colour.Length);
            Assert.AreEqual(2904, Susan.BufferData.Colour.Length);
            //Assert.AreEqual(2960157, Predator.BufferData.Colour.Length);

            // TEXTURE ARRAY
            Assert.AreEqual(36, Cube.BufferData.Texture.Length);
            Assert.AreEqual(0, Susan.BufferData.Texture.Length);
            //Assert.AreEqual(0, Predator.BufferData.Texture.Length);
        }
        #endregion

        #region Material
        [Test]
        public void TestDefaultMaterial()
        {
            // COLOUR
            Assert.AreEqual(Color4.LightGray, Cube.Material.Colour);
            Assert.AreEqual(Color4.LightGray, Susan.Material.Colour);
            //Assert.AreEqual(Color4.LightGray, Predator.Material.Colour);

            // TEXTURE ID
            Assert.AreNotEqual(-1, Cube.Material.TextureId);
            Assert.AreEqual(-1, Susan.Material.TextureId);
            //Assert.AreEqual(-1, Predator.Material.TextureId);
        }
        #endregion

        #region Transform
        [Test]
        public void TestDefaultTransform()
        {
            // POSITION
            Assert.AreEqual(Vector3.Zero, Cube.Transform.Position);
            Assert.AreEqual(Vector3.Zero, Susan.Transform.Position);
            //Assert.AreEqual(Vector3.Zero, Predator.Transform.Position);

            // ROTATION
            Assert.AreEqual(Vector3.Zero, Cube.Transform.Rotation);
            Assert.AreEqual(Vector3.Zero, Susan.Transform.Rotation);
            //Assert.AreEqual(Vector3.Zero, Predator.Transform.Rotation);

            // SCALE
            Assert.AreEqual(Vector3.One, Cube.Transform.Scale);
            Assert.AreEqual(Vector3.One, Susan.Transform.Scale);
            //Assert.AreEqual(Vector3.One, Predator.Transform.Scale);
        }
        #endregion
    }
}
