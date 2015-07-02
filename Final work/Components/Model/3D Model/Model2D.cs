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

using System;

namespace DisplayModel
{
    /// <summary>
    /// Represents an image model.
    /// </summary>
    public class Model2D : Model
    {
        private uint width;
        private uint height;

        #region Constructors
        public Model2D()
        {
        }
        #endregion

        #region Model Superclass Methods
        protected override void startRecording()
        {
            // IMPLEMENT
        }

        protected override void stopRecording()
        {
            // IMPLEMENT
        }

        protected override void saveToFile()
        {
            // IMPLEMENT
        }
        #endregion

        #region Model Specific Methods
        #endregion
    }
}