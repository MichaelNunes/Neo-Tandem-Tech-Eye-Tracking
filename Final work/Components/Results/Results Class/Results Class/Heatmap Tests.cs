﻿using System;
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
            hm._fileLocation = @"C:\Users\Duran\Desktop\NTT tests\2D Test";
            hm._modelName = "test2.jpg";
            hm._height = 1080;
            hm._width = 1920;
            int arraySize = 1000;
            float[] x = new float[arraySize];
            float[] y = new float[arraySize];

            for (int i = 0; i < arraySize; i++ )
            {
                x[i] = r.Next(0, hm._width);
                y[i] = r.Next(0,hm._height);
            }

            //float[] x = { 2, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654 };
            //float[] y = { 2, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876 };        
            hm.px.AddRange(x);
            hm.py.AddRange(y);

            //video
            vm._fileLocation = @"C:\Users\Duran\Desktop\NTT tests\Video Test";
            vm._modelName = "test.jpg";
            vm._height = 1200;
            vm._width = 1920;
            vm.px.AddRange(x);
            vm.py.AddRange(y);

            
            //3D           
            m3d._fileLocation = @"C:\Users\Duran\Desktop\NTT tests\3D Test";
            m3d._modelName = "Test";
            m3d._height = 1080;
            m3d._width = 1920;
            int arraySize3 = 100000;
            Random r1 = new Random();
            for(int f = 0; f < 6; f++)
            {
                List<string> test = new List<string>();
                float[] x3 = new float[arraySize3];
                float[] y3 = new float[arraySize3];
                
                for (int i = 0; i < arraySize; i++)
                {
                    x3[i] = r1.Next(0, m3d._width);
                    y3[i] = r1.Next(0, m3d._height);
                    
                    test.Add(x3[i] + "," + y[i]);
                }
                System.IO.File.WriteAllLines(m3d._fileLocation + "\\" + m3d._modelName + " "+ f+".txt", test);
            }
            
            //float[] x = { 2, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 989, 123, 765, 654, 987, 123, 765, 963, 123, 654 };
            //float[] y = { 2, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876, 5, 6, 90, 12, 67, 84, 23, 57, 34, 12, 77, 14, 76, 55, 234, 265, 147, 280, 258, 163, 185, 158, 421, 876 };        
            
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

        //[Test]
        //public void TestHeatMapsVideo()
        //{

        //}

        //[Test]
        //public void TestHeatMapsOntoVideo()
        //{

        //}

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