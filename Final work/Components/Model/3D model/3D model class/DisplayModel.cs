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
            /*
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
            }*/
        }
        
        //Here for testing purposes
        public static void Main(string[] args)
        {
            Window window = new Window(@"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\TestImages\");

            try
            {
                //string obj = @"C:\Users\COS301\Desktop\Blender Testing\Cube.obj";
                //string mtl = @"C:\Users\COS301\Desktop\Blender Testing\Cube.mtl";
                //string tex = @"C:\Users\COS301\Desktop\Blender Testing\";

                //string obj = @"C:\Users\COS301\Desktop\Blender Testing\Multi-textured plane.obj";
                //string mtl = @"C:\Users\COS301\Desktop\Blender Testing\Multi-textured plane.mtl";
                //string tex = @"C:\Users\COS301\Desktop\Blender Testing\";

                //string obj = @"C:\Users\COS301\Desktop\Blender Testing\Multi-textured cube.obj";
                //string mtl = @"C:\Users\COS301\Desktop\Blender Testing\Multi-textured cube.mtl";
                //string tex = @"C:\Users\COS301\Desktop\Blender Testing\";

                // string obj = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\Objects\International City_0_2.obj";
                // string dir = string.Empty;

                string obj = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\Objects\Medeiveal City\Medieval_City2.obj";
                string mtl = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\Objects\Medeiveal City\Medieval_City2.mtl";
                string tex = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\Objects\Medeiveal City\";
                
                GameObject test = Converter.fromOBJ(obj, mtl, tex);

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
