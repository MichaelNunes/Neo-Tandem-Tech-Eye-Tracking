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
using DisplayModel;
using System;
#endregion

namespace DisplayModel
{
    /// <summary>
    /// Provides a simplified interface that renders out a 3d scene.
    /// The render mode may be:
    /// 	1: Nine screenshots at specified angles are taken of the mo
    ///		   model or,
    ///		2: A fully immersive and interactive 3d scene.
    /// </summary>
    public class DisplayModel
    {
        #region Window Entrance Point
        /// <summary>
        /// Starts the 3d model rendering loop.
        /// </summary>
        /// <param name="source"> File path to the object file. </param>
        /// <param name="filePath"> Directory of where the screenshots will be saved to. </param>
        /// <param name="flyThrough">
        /// Boolean to determine the window type.
        ///     False: PictureWindow
        ///     True : FlyThroughWindow.
        /// </param>
        public bool Run(string source, string filePath, bool flyThrough)
        {
            try
            {
                if (flyThrough == true)
                {
                    FlyThroughWindow window = new FlyThroughWindow(filePath);
                    GameObject scene = Converter.fromOBJ(source, false);
                    window.Add(scene);
                    window.Run(30, 30);
                }
                else
                {
                    PictureWindow window = new PictureWindow(filePath);
                    GameObject scene = Converter.fromOBJ(source, true);
                    window.Add(scene);
                    window.Run(30, 30);
                }
            }
            catch (Exception e) { }
            return true;
        }
        #endregion

#if DEBUG
        /// <summary>
        /// Here for testing purposes
        /// </summary>
        /// <param name="args"> Not used. </param>
        public static void Main(string[] args)
        {
            string object_path = @"C:\Users\COS301\Desktop\Blender Testing\Medeiveal City\Medieval_City2.obj";
            string file_path = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\3D model\3D model class\bin\Debug\TestImages\";

            new DisplayModel().Run(object_path, file_path, true);
        }
#endif
    }
}
