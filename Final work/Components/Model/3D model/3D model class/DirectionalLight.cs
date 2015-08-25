using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace DisplayModel
{
    class DirectionalLight : Light
    {
        public DirectionalLight() : base(LightType.DIRECTIONAL)
        {

        }
        public override void addLight()
        {
            Console.WriteLine("I'm directionaly lighting bro!");
        }

        public Vector3 Colour { get; set; }
        public Vector3 Direction { get; set; }
    }
}
