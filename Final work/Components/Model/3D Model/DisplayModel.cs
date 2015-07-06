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
            string source = "C:/Users/COS301/Documents/Objects/Susan.obj"; ;

            Model3D tebogo = Converter.fromOBJ(source, null);
            tebogo.Model3DWindow.Run(30, 30);
        }
    }
}
