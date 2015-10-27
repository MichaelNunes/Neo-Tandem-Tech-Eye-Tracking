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
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
#endregion

namespace DisplayModel
{
    /// <summary>
    /// Represents a 3D object model.
    /// </summary>
    public class GameObject 
    {
        #region Fields
        public Buffer Buffer;
        public Material Material;
        public BufferData BufferData;
        public List<GameObject> Children;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an empty game object.
        /// </summary>
        public GameObject()
        {
            Buffer = new Buffer();
            Material = new Material();
            BufferData = new BufferData();
            Children = new List<GameObject>();
        }

        /// <summary>
        /// Generates a new game object with a material and buffer data.
        /// </summary>
        /// <param name="material"> Material containing colour/image information of the model. </param>
        /// <param name="bufferData"> Object containing the vertices, normals, and texture co-ordinates of the model. </param>
        public GameObject(Material material, BufferData bufferData)
        {
            Buffer = new Buffer();
            Material = material;
            BufferData = bufferData;
            Children = new List<GameObject>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates the buffer id for the objects,
        /// generates the textures, and assigns the base colour,
        /// for the object.
        /// </summary>
        public void Initialize()
        {
            Buffer.Position = GL.GenBuffer();
            Buffer.Normal = GL.GenBuffer();
            Buffer.Colour = GL.GenBuffer();
            Buffer.Texture = GL.GenBuffer();
            Buffer.Index = GL.GenBuffer();

            Material.Setup();
            BufferData.SetColour(Material.Diffuse, Material.Alpha);

            foreach (GameObject child in Children)
                child.Initialize();
        }

        /// <summary>
        /// Empties out the list of children objects.
        /// </summary>
        public void Clear()
        {
            foreach (GameObject child in Children)
                child.Clear();

            Children.Clear();
        }
        #endregion
    }
}