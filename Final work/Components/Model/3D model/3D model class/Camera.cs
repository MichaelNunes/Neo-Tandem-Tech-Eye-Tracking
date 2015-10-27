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
#endregion

namespace DisplayModel
{
    /// <summary>
    /// Represents a camera that defines what portions of the 
    /// 3d scene gets rendered.
    /// </summary>
    public class Camera
    {
        #region Fields
        public Vector3 Position;        
        public float Yaw;
        public float Pitch;        
        public float CurrentSpeed;

        public const float WalkSpeed = 1.0f;
        public const float RunSpeed = 10.0f;
        public const float FlySpeed = 50.0f;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises the camera's position, movement speed,
        /// and orientation to the centre of the scene.
        /// </summary>
        public Camera()
        {
            Position = Vector3.Zero;
            Yaw = (float)MathHelper.DegreesToRadians(-90);
            Pitch = 0.0f;
            CurrentSpeed = 0.0f;
        }
        #endregion
    }
}
