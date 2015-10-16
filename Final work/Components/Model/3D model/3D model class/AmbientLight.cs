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
    /// Represents an object that light the scene evenly a certain colour.
    /// </summary>
    public class AmbientLight : Light
    {
        #region Fields
        /// <summary>
        /// The colour of the light.
        /// </summary>
        public Vector3 Colour;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a default ambient light object.
        /// The colour is set to white with full alpha value.
        /// </summary>
        public AmbientLight()
        {
            Colour = Vector3.One;
        }

        /// <summary>
        /// Create an ambient light object with the colour privided.
        /// </summary>
        /// <param name="colour"></param>
        public AmbientLight(Color4 colour)
        {
            Colour = new Vector3(colour.R * colour.A, colour.G * colour.A, colour.B * colour.A);
        }
        #endregion

        #region Override
        public void addLight(int uniform)
        {
            GL.Uniform3(uniform, Colour);
        }
        #endregion
    }
}
