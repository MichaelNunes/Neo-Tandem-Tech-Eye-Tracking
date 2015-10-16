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

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace DisplayModel
{
    /// <summary>
    /// Represents a 3D object model.
    /// </summary>
    public class GameObject 
    {
        public static int degrees = 0;

        #region Fields
        private Material material;
        private Buffer buffer;
        public BufferData bufferData;

        private List<GameObject> children;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public GameObject()
        {
            material = new Material();
            bufferData = new BufferData();
            children = new List<GameObject>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="m"></param>
        /// <param name="bd"></param>
        public GameObject(Material m, BufferData bd)
        {
            material = m;
            bufferData = bd;
            children = new List<GameObject>();
        }
        #endregion

        public void Initialize()
        {
            // IMPLEMENT
            // DOES THE SETUP OF THE BUFFERS, MATERIALS, AND SO FORTH

            //GENERATING BUFFERS
            buffer.Position = GL.GenBuffer();
            buffer.Normal = GL.GenBuffer();
            buffer.Colour = GL.GenBuffer();
            buffer.Texture = GL.GenBuffer();
            buffer.Index = GL.GenBuffer();

            Material.Setup();
            BufferData.SetColour(Material.Colour);

            foreach (GameObject child in children)
                child.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(GameObject child) { children.Add(child); }

        #region Attributes
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

        /// <summary>
        /// 
        /// </summary>
        public Buffer Buffer { get { return buffer; } }

        /// <summary>
        /// The sub-objects of the current object
        /// </summary>
        public List<GameObject> Children { get { return children; } set { children = value; } }
        #endregion
    }
}