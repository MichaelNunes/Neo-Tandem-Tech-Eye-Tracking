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
        public static void Run(string source, string filePath, bool flyThrough)
        {
            try
            {
                if(flyThrough == true)
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw new Exception(e.Message);
            }
        }
#if DEBUG
        //Here for testing purposes
        public static void Main(string[] args)
        {
            string objectpath = @"C:\Users\Duran\Desktop\NTT tests\Objects\Medeiveal City\Medieval_City2.obj";

            string filePath = @"C:\Users\Duran\Desktop\NTT tests\Recordings\3DModel\testing3D\";

            Run(objectpath, filePath, true);
        }
#endif
    }
}
