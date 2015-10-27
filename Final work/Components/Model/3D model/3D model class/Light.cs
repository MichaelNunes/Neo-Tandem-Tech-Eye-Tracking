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

namespace DisplayModel
{
    /// <summary>
    /// Represents an object that adds light to a scence.
    /// </summary>
    public abstract class Light
    {
        #region Lighting
        /// <summary>
        /// Adds light to the 3d scene by assigning the appropriate
        /// type-specific attribute to the corresponding shader
        /// uniforms.
        /// </summary>
        /// <param name="uniforms"> An array of uniform identifiers. </param>
        public abstract void AddLight(params int[] uniforms);
        #endregion
    }
}
