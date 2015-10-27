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
    /// Container for the shader uniforms.
    /// </summary>
    public struct Uniform
    {
        #region Fields
        public int Sampler;

        public int ModelViewMatrix;
        public int ProjectionMatrix;
        public int NormalMatrix;

        public int UseTexture;
        public int UseLighting;

        public int AmbientLightColour;
        public int DirectionalLightColour;
        public int DirectionalLightDirection;
        public int PointLightDiffuseColour;
        public int PointLightSpecularColour;
        public int PointLightShininess;
        public int PointLightPosition;

        public int Alpha;
        #endregion
    }
}
