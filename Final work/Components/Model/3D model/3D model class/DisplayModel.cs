using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenTK;

using DisplayModel;

namespace DisplayModel
{
    public class DisplayModel
    {
        public void Run(string[] args)
        {
            string source = args[0];
            string texture = "";

            Window window = new Window(args[1]);

            try
            {
                GameObject scene = Converter.fromOBJ(source, texture);
                window.Add(scene);
                window.Run(30, 30);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }
        
        //Here for testing purposes
        public static void Main(string[] args)
        {
            Window window = new Window(@"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\TestImages\");


            try
            {
                
                  List<Vector3> v = new List<Vector3> { new Vector3(1f, 0.5f, 0f), new Vector3(1f, -1.5f, 0f), new Vector3(-1f, -1.5f, 0f), new Vector3(-1f, 0.5f, 0f) };
                  List<Vector2> t = new List<Vector2> { };
                  List<Vector3> n = new List<Vector3> { new Vector3(0f, 0f, 1f) };
                  
                  List<int> a = new List<int> { 1, 2, 3, 1, 3, 4 };
                  List<int> b = new List<int> { 1, 2, 3, 1, 3, 4 };
                  List<int> c = new List<int> { 1, 2, 3, 1, 3, 4 };

                  GameObject test = new GameObject();
                  test.Transform = new Transform();
                  test.Material = new Material();
                  test.BufferData = new BufferData(v, t, n, a, b, c, test.Material.Colour);

                Console.WriteLine(test.BufferData);/*
                test = Converter.fromOBJ(@"C:\Users\COS301\Documents\Monkey.obj", "");
                Console.WriteLine(test.BufferData);*/
                window.Add(test);
                 
                window.Run(30, 30);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
            }
        }
    }
}
