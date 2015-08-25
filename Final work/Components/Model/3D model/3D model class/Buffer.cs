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
    public struct Buffer
    {
        /// <summary>
        /// Buffer for the vertices of an object.
        /// </summary>
        public int Position
        { get; set; }

        /// <summary>
        /// Buffer for the normals of an object.
        /// </summary>
        public int Normal
        { get; set; }

        /// <summary>
        /// Buffer for the colour of an object.
        /// </summary>
        public int Colour
        { get; set; }

        /// <summary>
        /// Buffer for the texture of an object.
        /// </summary>
        public int Texture
        { get; set; }

        /// <summary>
        /// Buffer for the indices of the vertices, normals, colour and, texture arrays.
        /// </summary>
        public int Index
        { get; set; }
    }
}
