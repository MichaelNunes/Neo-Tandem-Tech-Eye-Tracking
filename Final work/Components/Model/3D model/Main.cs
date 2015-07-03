using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DisplayModel;

namespace DisplayModel
{
    public class Main
    {
        public static void Main(string[] args)
        {
            string source = "Objects/Predator.obj"; ;

            Model3D tebogo = Converter.fromOBJ(source, null);
            tebogo.Model3DWindow.Run(30, 30);
        }
    }
}
