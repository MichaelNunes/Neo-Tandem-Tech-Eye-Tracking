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
	public struct BufferData
	{
		#region Fields
        private Vector3[] vertex;
        private Vector2[] texture;
        private Vector3[] normal;
        private Vector4[] colour;
        private uint[] index;
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
        public BufferData(List<Vector3> vp, List<Vector2> tp, List<Vector3> np, List<int> vi, List<int> ti, List<int> ni, Color4 color)
        {
            vertex = new Vector3[vp.Count];
            colour = new Vector4[vp.Count];
            texture = new Vector2[tp.Count];
            normal = new Vector3[np.Count];
            index = new uint[vi.Count];
            ModelViewMatrix = Matrix4.Identity;

            Console.WriteLine(vi.Count);
            Console.WriteLine(ti.Count);
            Console.WriteLine(ni.Count);

            for (int i = 0; i < vi.Count; i++)
                index[i] = (uint)(vi[i] - 1);

            for (int i = 0; i < vp.Count; i++)
                vertex[i] = vp[i];

            for (int i = 0; i < colour.Length; i++)
                colour[i] = new Vector4(color.R, color.G, color.B, color.A);

            for (int i = 0; i < tp.Count; i++)
                texture[i] = tp[i];

            for (int i = 0; i < np.Count; i++)
                normal[i] = np[i];

            Console.WriteLine(vertex.Length);
            Console.WriteLine(texture.Length);
            Console.WriteLine(normal.Length);
            Console.WriteLine(index[0]);
		}
		#endregion

        public override string ToString()
        {
            string v = "[";
            foreach (Vector3 j in vertex)
                v += j + ",";
            v += "\b]";

            string t = "[";
            foreach (Vector2 j in texture)
                t += j + ",";
            t += "\b]";

            string n = "[";
            foreach (Vector3 j in normal)
                n += j + ",";
            n += "\b]";

            string i = "[";
            foreach (int j in index)
                i += j + ",";
            i += "\b]";

            return v + "\n" + t + "\n" + n + "\n" + i;
        }

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

        /// <summary>
        /// The index array of the object.
        /// </summary>
        public uint[] Index { get { return index; } }
		#endregion
	}
}