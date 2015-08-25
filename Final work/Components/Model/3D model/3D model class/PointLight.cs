using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace DisplayModel
{
    class PointLight : Light
    {
        public PointLight() : base(LightType.POINT)
        {

        }

        public override void addLight()
        {
            Console.WriteLine("I'm pointingly lighting bro!");
        }

        public Vector3 DiffuseColour { get; set; }
        public Vector3 SpecularColour { get; set; }
        public float Shininess { get; set; }
        public Vector3 Position { get; set; }
    }
}
