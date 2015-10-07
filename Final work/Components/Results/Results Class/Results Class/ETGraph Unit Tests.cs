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
        Eye_Tracking_Graph etg = new Eye_Tracking_Graph(true,10);
        Eye_Tracking_Graph vm = new Eye_Tracking_Graph(true, 10);
        Eye_Tracking_Graph etg3d = new Eye_Tracking_Graph(true, 10);

        [SetUp]
        public void Init()
        {
            Random r = new Random(28);

            //Images   
            etg._SourceLocation = @"C:\Users\COS301\Documents\ETG Tests\2D Test";
            etg._DestinationPath = @"C:\Users\COS301\Documents\ETG Tests\2D Test";
            etg._modelName = "test.jpg";
            etg._height = 1080;
            etg._width = 1920;
            int arraySize = 1000;
            float[] x = new float[arraySize];
            float[] y = new float[arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                x[i] = r.Next(0, etg._width);
                y[i] = r.Next(0, etg._height);
            }

            etg.px.AddRange(x);
            etg.py.AddRange(y);

            //video
            vm._SourceLocation = @"C:\Users\COS301\Documents\ETG Tests\Video Test\Test.wmv";
            vm._DestinationPath = @"C:\Users\COS301\Documents\ETG Tests\Video Test";
            vm._modelName = "Test";
            vm._height = 720;
            vm._width = 1280;
            vm.OpenETGraphData(@"C:\Users\COS301\Documents\ETG Tests\Video Test", vm._modelName);

            //3D           
            etg3d._SourceLocation = @"C:\Users\COS301\Documents\ETG Tests\3D Test";
            etg3d._DestinationPath = @"C:\Users\COS301\Documents\ETG Tests\3D Test";
            etg3d._modelName = "Test";
            etg3d._height = 1080;
            etg3d._width = 1920;
            int arraySize3 = 100000;
            Random r1 = new Random();
            for (int f = 0; f < 6; f++)
            {
                List<string> test = new List<string>();
                float[] x3 = new float[arraySize3];
                float[] y3 = new float[arraySize3];

                for (int i = 0; i < arraySize; i++)
                {
                    x3[i] = r1.Next(0, etg3d._width);
                    y3[i] = r1.Next(0, etg3d._height);

                    test.Add(x3[i] + ":" + y3[i]);
                }
                System.IO.File.WriteAllLines(etg3d._SourceLocation + "\\RecordedData_" + etg3d._modelName + "" + f + ".txt", test);
            }
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

