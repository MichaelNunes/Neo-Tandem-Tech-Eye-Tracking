﻿#region Legal
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
    /// Represents a light object that adds light from a point in space.
    /// </summary>
    class PointLight : Light
    {
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
            DiffuseColour = new Vector3(0.5f);
            SpecularColour = Vector3.One;
            Shininess = 10;
            Position = Vector3.Zero;
        }

        /// <summary>
        /// Creates a point light object with the value providede.
        /// </summary>
        /// <param name="diffuse"> The base colour of the light. </param>
        /// <param name="specular"> The colour of the reflection. </param>
        /// <param name="shininess"> The amount of reflection the specular light provides. </param>
        /// <param name="position"> The position of the light in 3d space. </param>
        public PointLight(Color4 diffuse, Color4 specular, float shininess, Vector3 position)
        {
            DiffuseColour = new Vector3(diffuse.R * diffuse.A, diffuse.G * diffuse.A, diffuse.B * diffuse.A);
            SpecularColour = new Vector3(specular.R * specular.A, specular.G * specular.A, specular.B * specular.A);
            Shininess = shininess;
            Position = position;
        }
        #endregion

        #region Override
        public override void addLight()
        {
            // IMPLEMENT
        }
        #endregion

        #region Attributes
        /// <summary>
        /// The base colour of the light.
        /// </summary>
        public Vector3 DiffuseColour { get; set; }
        
        /// <summary>
        /// The reflective colour of the light.
        /// </summary>
        public Vector3 SpecularColour { get; set; }
        
        /// <summary>
        /// The amount of shine the specular colour produces.
        /// </summary>
        public float Shininess { get; set; }
        
        /// <summary>
        /// The position of the light in 3d space.
        /// </summary>
        public Vector3 Position { get; set; }
        #endregion
    }
}