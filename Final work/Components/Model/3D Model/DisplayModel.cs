using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DisplayModel;

namespace DisplayModel
{
    public class DisplayModel
    {
        public static void Main(string[] args)
        {
            Window window = new Window();

            string source = @"C:\Users\COS301\Documents\Objects\Susan.obj";
            string texture = "";
            GameObject susan = Converter.fromOBJ(source, texture);

            window.Add(susan);
            window.Run(30, 30);
        }
    }
}
