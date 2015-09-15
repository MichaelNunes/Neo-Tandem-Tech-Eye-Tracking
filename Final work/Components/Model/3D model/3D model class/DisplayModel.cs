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
        public void Run(string source, string texture, string filePath, bool flyThrough)
        {
            try
            {
                if(flyThrough == true)
                {
                    FlyThroughWindow window = new FlyThroughWindow(filePath);
                    GameObject scene = Converter.fromOBJ(source, texture);
                    window.Add(scene);
                    window.Run(30, 30);
                }
                else
                {
                    PictureWindow window = new PictureWindow(filePath);
                    GameObject scene = Converter.fromOBJ(source, texture);
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
            string source = @"C:\Users\COS301\Documents\Objects\Susan.obj";
            string texture = "";
            string filePath = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\TestImages\";

            DisplayModel dm = new DisplayModel();
            dm.Run(source, texture, filePath, false);
        }
    }
}
