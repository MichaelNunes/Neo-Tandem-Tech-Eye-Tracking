using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisplayModel
{
    public abstract class Light
    {
        public enum LightType { AMBIENT, DIRECTIONAL, POINT }
        private LightType type;

        public Light(LightType _type)
        {
            type = _type;
        }
        /// <summary>
        /// Adds light into the scene within the game window.
        /// </summary>
        public abstract void addLight();
        public LightType Type { get { return type; } }
    }
}
