using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Results_Class
{
    [TestFixture]
    class Heatmap_Tests
    {
        Heatmaps hm = new Heatmaps();
        Heatmaps vm = new Heatmaps();
        Heatmaps m3d = new Heatmaps();

        [SetUp]
        public void Init()
        {
            Random r = new Random(28);

            //Images   
            hm._SourceLocation = @"C:\Users\COS301\Documents\ETG Tests\2D Test";
            hm._DestinationPath = @"C:\Users\COS301\Documents\ETG Tests\2D Test";
            hm._modelName = "test";
            hm._height = 1080;
            hm._width = 1920;

            //video
            vm._SourceLocation = @"C:\Users\COS301\Documents\ETG Tests\Video Test\";
            vm._DestinationPath = @"C:\Users\COS301\Documents\ETG Tests\Video Test\";
            vm._modelName = "Test";
            vm._height = 720;
            vm._width = 1280;

            //3D           
            m3d._SourceLocation = @"C:\Users\COS301\Documents\ETG Tests\3D Test\";
            m3d._DestinationPath = @"C:\Users\COS301\Documents\ETG Tests\3D Test\";
            m3d._modelName = "Test";
            m3d._height = 1080;
            m3d._width = 1920;
        }

        [Test]
        public void TestHeatMaps2D()
        {
            hm.SaveHeatmap2D();
        }

        [Test]
        public void TestHeatMapsOnto2D()
        {            
            hm.SaveHeatmapOntoModel2D();
        }

        [Test]
        public void TestHeatMapsVideo()
        {
            vm.SaveHeatmapVideo();
        }

        [Test]
        public void TestHeatMapsOntoVideo()
        {
            vm.SaveHeatmapOntoModelVideo();
        }

        [Test]
        public void TestHeatMaps3D()
        {
            m3d.SaveHeatmap3D();
        }

        [Test]
        public void TestHeatMapsOnto3D()
        {

            m3d.SaveHeatmapOntoModel3D();
        }
    }
}
