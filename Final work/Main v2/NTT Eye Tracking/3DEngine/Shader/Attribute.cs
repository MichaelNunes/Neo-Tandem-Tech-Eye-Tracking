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

namespace DisplayModel
{
    /// <summary>
    /// Container for the shader attributes.
    /// </summary>
    public struct Attribute
    {
        /// <summary>
        /// Pointer to the vertex position attribute in the shader.
        /// </summary>
        public int VertexPosition { get; set; }

        /// <summary>
        /// Pointer to the vertex normal attribute in the shader.
        /// </summary>
        public int VertexNormal { get; set; }

        /// <summary>
        /// Pointer to the vertex colour attribute in the shader.
        /// </summary>
        public int VertexColour { get; set; }

        /// <summary>
        /// Pointer to the vertex texture attribute in the shader.
        /// </summary>
        public int VertexTexture { get; set; }
    }
}
