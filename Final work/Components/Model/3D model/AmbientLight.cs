using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics;

namespace DisplayModel
{
    class AmbientLight : Light
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AmbientLight() : base(LightType.AMBIENT)
        {
        }

        public AmbientLight(Vector3 colour) : base(LightType.AMBIENT)
        {
            Colour = colour;
        }

        public AmbientLight(Color color) : base(LightType.AMBIENT)
        {
            Colour = new Vector3(color.R, color.G, color.B);
        }

        public AmbientLight(Color4 color) : base(LightType.AMBIENT)
        {
            Colour = new Vector3(color.R, color.G, color.B);
        }
        #endregion
        public override void addLight()
        {
            Console.WriteLine("I'm ambiently lighting things bro!");
        }

        public Vector3 Colour
        { get; set; }
    }
}
