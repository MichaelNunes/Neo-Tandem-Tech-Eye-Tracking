using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DisplayModel;

namespace DisplayModel
{
    public class DisplayModel
    {
        public void Run(string arg)
        {
            Window window = new Window();

            string source = arg;
            string texture = "";

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
            Window window = new Window();
#if DEBUG
            string source = @"C:\Users\COS301\Documents\Objects\Susan.obj";
            string texture = "";
#else
            string source = args[0];
            string texture = args[1];
#endif

            try
            {
                GameObject susan = Converter.fromOBJ(source, texture);
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
