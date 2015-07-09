﻿using System;
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

            string source = "Objects/Susan_Smooth.obj";
            string texture = "";
            GameObject susan = Converter.fromOBJ(source, texture);

            window.Add(susan);
            window.Run(30, 30);
        }
    }
}
