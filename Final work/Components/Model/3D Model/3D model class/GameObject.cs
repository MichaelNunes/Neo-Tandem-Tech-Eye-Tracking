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
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;

using DisplayModel;

namespace DisplayModel
{
    /// <summary>
    /// Represents a 3D object model.
    /// </summary>
    public class GameObject 
    {
        #region Fields
        private Transform transform;
        private Material material;
        public BufferData bufferData;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public GameObject()
        {
            transform = new Transform();
            material = new Material();
            bufferData = new BufferData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="m"></param>
        /// <param name="bd"></param>
        public GameObject(Transform t, Material m, BufferData bd)
        {
            transform = t;
            material = m;
            bufferData = bd;
        }
        #endregion

        #region Attributes
        /// <summary>
        /// 
        /// </summary>
        public Transform Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Material Material
        {
            get { return material; }
            set { material = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public BufferData BufferData
        {
            get { return bufferData; }
            set { bufferData = value; }
        }
        #endregion
    }
}