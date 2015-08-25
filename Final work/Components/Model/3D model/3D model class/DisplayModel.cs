﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
#if DEBUG
            string source = @"C:\Users\COS301\Documents\cube.obj";//@"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\Objects\city.obj";
            string texture = @"C:\Users\COS301\Documents\cube.png";//@"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\Objects\Cube.jpg";
#else
            string source = args[0];
            string texture = args[1];
#endif

            try
            {
                GameObject susan = Converter.fromOBJ(source, texture);
                Console.WriteLine(susan.Material.TextureId);
                susan.Material.Setup();
                Console.WriteLine(susan.Material.TextureId);
                //Console.ReadLine();

                window.Add(susan);
                window.Run(30, 30);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
