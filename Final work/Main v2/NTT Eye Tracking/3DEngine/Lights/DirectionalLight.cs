#region Legal
/*
 * Copyright (c) 2015 The University of Pretoria.
 *
 * The following was designed for the Centre of GeoInformation
 * Science (CGIS), University of Pretoria. All code is property
 * of the University of Pretoria and is available under the 
 * Creative Commons Attribution-ShareAlike (CC BY-SA) see:
 * "https://creativecommons.org/licenses/"
 *
 * Author: Duran Cole
 * Email: u13329414@tuks.co.za
 * Author: Michael Nunes
 * Email: u12104592@tuks.co.za
 * Author: Molefe Molefe
 * Email: u12260429@tuks.co.za
 * Author: Tebogo Christopher Seshibe
 * Email: u13181442@tuks.co.za
 * Author: Timothy Snayers
 * Email: u13397134@tuks.co.za
 */
#endregion

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace DisplayModel
{
    /// <summary>
    /// Represents an object that adds light to a scene
    /// in a certain direction.
    /// </summary>
    public class DirectionalLight : Light
    {
        #region Fields
        /// <summary>
        /// The colour of the light.
        /// </summary>
        public Vector3 Colour;

        /// <summary>
        /// The direction in which the light is lighting a scene.
        /// </summary>
        public Vector3 Direction;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DirectionalLight()
        {
            Colour = Vector3.One;
            Direction = Vector3.Zero;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="colour"> </param>
        /// <param name="direction"> </param>
        public DirectionalLight(Color4 colour, Vector3 direction)
        {
            Colour = new Vector3(colour.R * colour.A, colour.G * colour.A, colour.B * colour.A);
            Direction = direction;
        }
        #endregion

        #region Override
        public void addLight(int colour, int direction)
        {
            GL.Uniform3(colour, Colour);
            GL.Uniform3(direction, Direction);
        }
        #endregion
    }
}
