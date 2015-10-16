using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Results_Class
{
    [TestFixture]
    class ETGraph_Unit_Tests
    {
        Eye_Tracking_Graph etg = new Eye_Tracking_Graph(10,true);
        Eye_Tracking_Graph vm = new Eye_Tracking_Graph(10, false);
        Eye_Tracking_Graph etg3d = new Eye_Tracking_Graph(10,false);

        [SetUp]
        public void Init()
        {
            //Images   
            etg._SourceLocation = @"C:\Users\Duran\Desktop\ETGraphTests\2D Test\";
            etg._DestinationPath = @"C:\Users\Duran\Desktop\ETGraphTests\2D Test\";
            etg._modelName = "2dTest";
            etg._height = 1080;
            etg._width = 1920;

            //video
            vm._SourceLocation = @"C:\Users\Duran\Desktop\ETGraphTests\Video Test\videoTest.wmv";
            vm._DestinationPath = @"C:\Users\Duran\Desktop\ETGraphTests\Video Test\";
            vm._modelName = "videoTest";
            vm._height = 720;
            vm._width = 1280;

            //3D           
            etg3d._SourceLocation = @"C:\Users\Duran\Desktop\ETGraphTests\3D Test\";
            etg3d._DestinationPath = @"C:\Users\Duran\Desktop\ETGraphTests\3D Test\";
            etg3d._modelName = "3dTest";
            etg3d._height = 1080;
            etg3d._width = 1920;
        }

        [Test]
        public void TestETGraph2D()
        {
            etg.SaveETGraph2D();
        }

        [Test]
        public void TestETGraphOnto2D()
        {
            etg.SaveETGraphOntoModel2D();
        }

        [Test]
        public void TestETGraphVideo()
        {
            vm.SaveETGraphVideo();
        }

        [Test]
        public void TestETGraphOntoVideo()
        {
            vm.SaveETGraphOntoModelVideo();
        }

        [Test]
        public void TestETGraph3D()
        {
            etg3d.SaveETGraph3D();
        }

        [Test]
        public void TestETGraphOnto3D()
        {
            etg3d.SaveETGraphOntoModel3D();
        }
    }
}

