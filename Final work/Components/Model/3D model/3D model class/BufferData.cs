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
using System;
using System.Collections.Generic;

namespace DisplayModel
{
	/// <summary>
	/// Contains the vertex, uv, and normal values of a 3D model.
	/// </summary>
	public class BufferData
    {
		#region Fields
        public Vector3[] vertex;
        public Vector2[] texture;
        public Vector3[] normal;
        public Vector4[] colour;

        public Matrix4 ModelViewMatrix;
		#endregion

		#region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public BufferData()
        {
            vertex = new Vector3[0];
            texture = new Vector2[0];
            normal = new Vector3[0];
            colour = new Vector4[0];
            Color4 color = Color4.LightGray;
            ModelViewMatrix = Matrix4.Identity;
        }

        /// <summary>
        /// Create a buffer data object from the values passed and the indices provided.
        /// </summary>
        /// <param name='vp'> A list of vertex points. </param>
        /// <param name='tp'> A list of texture points. </param>
        /// <param name='np'> A list of normal points. </param>
        /// <param name='vi'> A list of vertex indices. </param>
        /// <param name='ti'> A list of texture indices. </param>
        /// <param name='ni'> A list of normal indices. </param>
        /// <param name="tc"> A list of texture indices indices. </param>
        public BufferData(List<Vector3> vp, List<Vector2> tp, List<Vector3> np, int[] vi, int[] ti, int[] ni)
        {
            int size = ti.Length;
            vertex = new Vector3[size];
            texture = new Vector2[size];
            normal = new Vector3[size];
            colour = new Vector4[size];
            Color4 color = Color4.LightGray;
            ModelViewMatrix = Matrix4.Identity;

            for (int i = 0; i < size; ++i)
            {
                vertex[i] = vp[vi[i] - 1];
                if (ti.Length > 0) texture[i] = tp[ti[i] - 1];
                normal[i] = np[ni[i] - 1];
                colour[i] = new Vector4(color.R, color.G, color.B, color.A);
            }
		}
		#endregion

        #region Attributes
        /// <summary>
        /// Array or vertex posiitions
        /// </summary>
        public Vector3[] Vertex { get { return vertex; } }

        /// <summary>
        /// Array or vertex texture co-ordinates
        /// </summary>
        public Vector2[] Texture { get { return texture; } }

        /// <summary>
        /// Array or vertex normals
        /// </summary>
        public Vector3[] Normal { get { return normal; } }

        /// <summary>
        /// Array or vertex colours
        /// </summary>
        public Vector4[] Colour { get { return colour; } }
		#endregion
	}
}