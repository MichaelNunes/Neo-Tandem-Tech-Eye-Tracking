﻿using System;
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
        public void Run(string source, string material, string textures, string filePath, bool flyThrough)
        {
            try
            {
                if(flyThrough == true)
                {
                    FlyThroughWindow window = new FlyThroughWindow(filePath);
                    GameObject scene = Converter.fromOBJ(source, material, textures);
                    window.Add(scene);
                    window.Run(30, 30);
                }
                else
                {
                    PictureWindow window = new PictureWindow(filePath);
                    GameObject scene = Converter.fromOBJ(source, material, textures);
                    window.Add(scene);
                    window.Run(30, 30);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw new Exception(e.Message);
            }
        }
        
        //Here for testing purposes
        public static void Main(string[] args)
        {
            //string source = @"G:\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\Objects\Cube.obj";
            //string material = @"G:\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\Objects\Medeiveal City\Medieval_City2.mtl";
            //string images = @"G:\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\Objects\Medeiveal City\";

            string a = @"C:\Users\COS301\Desktop\Blender Testing\Multi-textured cube.obj";
            string b = @"C:\Users\COS301\Desktop\Blender Testing\Multi-textured cube.mtl";
            string c = @"C:\Users\COS301\Desktop\Blender Testing\";

            string filePath = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\TestImages\";

            DisplayModel dm = new DisplayModel();
            dm.Run(a, b, c, filePath, true);
        }
    }
}
