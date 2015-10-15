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
        public static void Run(string source, string filePath, bool flyThrough)
        {
            try
            {
                if (flyThrough == true)
                {
                    FlyThroughWindow window = new FlyThroughWindow(filePath);
                    GameObject scene = Converter.fromOBJ(source, false);
                    window.Add(scene);
                    window.Run(30, 30);
                }
                else
                {
                    PictureWindow window = new PictureWindow(filePath);
                    GameObject scene = Converter.fromOBJ(source, true);
                    window.Add(scene);
                    window.Run(30, 30);
                }
            }
            catch (Exception e) {}
        }
#if DEBUG
        //Here for testing purposes
        public static void Main(string[] args)
        {
            string objectpath = @"C:\Users\COS301\Desktop\Blender Testing\Paris\Paris.obj";
            string filePath = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\TestImages\";

            Run(objectpath, filePath, true);
        }
#endif
    }
}
