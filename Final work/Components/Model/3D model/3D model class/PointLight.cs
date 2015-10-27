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

#region Using Clauses
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
#endregion

namespace DisplayModel
{
    /// <summary>
    /// Represents a light object that adds light from a point in space.
    /// </summary>
    public class PointLight : Light
    {
        #region Fields
        public Vector3 Diffuse;
        public Vector3 Specular;
        public Vector3 Position;
        public float Shininess;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a default point light object.
        /// The base and reflective colours are set to white,
        /// with the base being half as bright.
        /// The shininess is set set to a default value while 
        /// the position is the center of the 3d space.
        /// </summary>
        public PointLight()
        {
            Diffuse = new Vector3(0.5f);
            Specular = new Vector3(1.0f);
            Position = new Vector3(0.0f);
            Shininess = 10;
        }

        /// <summary>
        /// Creates a point light object with the value providede.
        /// </summary>
        /// <param name="diffuse"> The base colour of the light. </param>
        /// <param name="shininess"> The amount of reflection the specular light provides. </param>
        /// <param name="position"> The position of the light in 3d space. </param>
        public PointLight(Color4 diffuse, Color4 specular, Vector3 position, float shininess)
        {
            Diffuse = new Vector3(diffuse.R * diffuse.A, diffuse.G * diffuse.A, diffuse.B * diffuse.A);
            Specular = new Vector3(specular.R * specular.A, specular.G * specular.A, specular.B * specular.A);
            Shininess = shininess;
            Position = position;
        }
        #endregion

        #region Lighting
        /// <summary>
        /// Adds a point light colour to the scene.
        /// </summary>
        /// <param name="colour"> Uniform id for the colour of the light.</param>
        /// <param name="shininess"> Uniform id for the shinines of the light. </param>
        /// <param name="position"> Uniform id for the position for the light. </param>
        public override void AddLight(params int[] uniforms)
        {
            GL.Uniform3(uniforms[0], Diffuse);
            GL.Uniform3(uniforms[1], Specular);
            GL.Uniform3(uniforms[2], Position);
            GL.Uniform1(uniforms[3], Shininess);
        }
        #endregion
    }
}
