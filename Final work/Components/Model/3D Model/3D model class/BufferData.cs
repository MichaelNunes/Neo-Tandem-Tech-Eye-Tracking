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
using System;
using System.Collections.Generic;

namespace DisplayModel
{
	/// <summary>
	/// Contains the vertex, uv, and normal values of a 3D model.
	/// </summary>
	public struct BufferData
	{
		#region Fields
        private Vector3[] vertex;
        private Vector2[] texture;
        private Vector3[] normal;
        private Vector4[] colour;
        private int[] index;
        public Matrix4 ModelViewMatrix;
		#endregion

		#region Constructor
        /// <summary>
        /// Create a buffer data object from the values passed and the indices provided.
        /// </summary>
        /// <param name='vp'> A list of vertex points. </param>
        /// <param name='tp'> A list of texture points. </param>
        /// <param name='np'> A list of normal points. </param>
        /// <param name='vi'> A list of vertex indices. </param>
        /// <param name='ti'> A list of texture indices. </param>
        /// <param name='ni'> A list of normal indices. </param>
        public BufferData(List<Vector3> vp, List<Vector2> tp, List<Vector3> np, List<int> vi, List<int> ti, List<int> ni, Material mat)
        {
            vertex = new Vector3[vi.Count];
            colour = new Vector4[vi.Count];
            texture = new Vector2[ti.Count];
            normal = new Vector3[ni.Count];
            index = null;
            ModelViewMatrix = Matrix4.Identity;

            int count = 0;
            for (int i = 0; i < vi.Count; i++)
                vertex[count++] = vp[vi[i]-1];

            for (int i = 0; i < colour.Length; i++)
                colour[i] = new Vector4(mat.Colour.R, mat.Colour.G, mat.Colour.B, mat.Colour.A);

            count = 0;
            for (int i = 0; i < ti.Count; i++)
                texture[count++] = tp[ti[i]-1];

            Console.WriteLine(count);

            count = 0;
            for (int i = 0; i < ni.Count; i++)
                normal[count++] = np[ni[i]-1];
		}
		#endregion

        #region Attributes
        /// <summary>
		/// The vertex position array of the object.
		/// </summary>
        public Vector3[] Vertex { get { return vertex; } }

        /// <summary>
        /// The texture co-ordinate array of the object.
        /// </summary>
        public Vector2[] Texture { get { return texture; } }

		/// <summary>
		/// The normal array of the object.
		/// </summary>
        public Vector3[] Normal { get { return normal; } }

        /// <summary>
        /// The colour array of the object.
        /// </summary>
        public Vector4[] Colour { get { return colour; } }
		#endregion
	}
}