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

        private Matrix4 modelMatrix;
        private Matrix4 viewProjectionMatrix;
        private Matrix4 modelViewProjectionMatrix;
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a buffer data object.
		/// </summary>
        /// <param name='v'> An array of vertex points. </param>
        /// <param name='t'> An array of texture points. </param>
        /// <param name='n'> An array of normal points. </param>s
		public BufferData(float[] v, float[] t, float[] n, Material mat)
		{
            modelMatrix = Matrix4.Identity;
            viewProjectionMatrix = Matrix4.Identity;
            modelViewProjectionMatrix = Matrix4.Identity;

            vertex = new Vector3[v.Length];
            colour = new Vector4[v.Length];
            texture = new Vector2[t.Length];
            normal = new Vector3[n.Length];
            index = new int[v.Length];

            for(int i = 0; i < v.Length; i += 3)
                vertex[ i ] = new Vector3(v[i], v[i + 1], v[i + 2]);

            for (int i = 0; i < colour.Length; ++i)
                colour[i] = new Vector4(mat.Colour.R,mat.Colour.G,mat.Colour.B, mat.Colour.A);

            for(int i = 0; i < t.Length; i += 2)
                texture[ i ] = new Vector2(t[i], t[i + 1]);

            for(int i = 0; i < n.Length; i += 3)
            	vertex[ i ] = new Vector3(n[i], n[i + 1], n[i + 2]);
		}

        /// <summary>
        /// Creates a buffer data object from the values passed.
        /// </summary>
        /// <param name='v'> A list of vertex points. </param>
        /// <param name='t'> A list of texture points. </param>
        /// <param name='n'> A list of normal points. </param>
        public BufferData(List<float> v, List<float> t, List<float> n, Material mat)
        {
            modelMatrix = Matrix4.Identity;
            viewProjectionMatrix = Matrix4.Identity;
            modelViewProjectionMatrix = Matrix4.Identity;

            vertex = new Vector3[v.Count];
            colour = new Vector4[v.Count];
            texture = new Vector2[t.Count];
            normal = new Vector3[n.Count];
            index = new int[v.Count];

            for (int i = 0; i < v.Count; i += 3)
                vertex[i] = new Vector3(v[i], v[i + 1], v[i + 2]);

            for (int i = 0; i < colour.Length; ++i)
                colour[i] = new Vector4(mat.Colour.R, mat.Colour.G, mat.Colour.B, mat.Colour.A);

            for (int i = 0; i < t.Count; i += 2)
                texture[i] = new Vector2(t[i], t[i + 1]);

            for (int i = 0; i < n.Count; i += 3)
                vertex[i] = new Vector3(n[i], n[i + 1], n[i + 2]);
		}

		/// <summary>
		/// Create a buffer data object from the values passed and the indices provided.
		/// </summary>
        /// <param name='vp'> An array of vertex points. </param>
        /// <param name='tp'> An array of texture points. </param>
        /// <param name='np'> An array of normal points. </param>
        /// <param name='vi'> An array of vertex indices. </param>
        /// <param name='ti'> An array of texture indices. </param>
        /// <param name='ni'> An array of normal indices. </param>
        public BufferData(float[] vp, float[] tp, float[] np, int[] vi, int[] ti, int[] ni, Material mat)
        {
            modelMatrix = Matrix4.Identity;
            viewProjectionMatrix = Matrix4.Identity;
            modelViewProjectionMatrix = Matrix4.Identity;

            vertex = new Vector3[vi.Length];
            colour = new Vector4[vi.Length];
            texture = new Vector2[ti.Length];
            normal = new Vector3[ni.Length];
            index = new int[vi.Length];

            for(int i = 0; i < vi.Length; i += 3)
                vertex[i] = new Vector3(vp[vi[i]], vp[vi[i + 1]], vp[vi[i + 2]]);

            for (int i = 0; i < colour.Length; ++i)
                colour[i] = new Vector4(mat.Colour.R, mat.Colour.G, mat.Colour.B, mat.Colour.A);

            for(int i = 0; i < ti.Length; i += 2)
                texture[ i ] = new Vector2(tp[ti[i]], tp[ti[i + 1]]);

            for(int i = 0; i < ni.Length; i += 3)
                normal[ i ] = new Vector3(np[ni[i]], np[ni[i + 1]], np[ni[i + 2]]);
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
        public BufferData(List<Vector3> vp, List<Vector2> tp, List<Vector3> np, List<int> vi, List<int> ti, List<int> ni, Material mat)
        {
            modelMatrix = Matrix4.Identity;
            viewProjectionMatrix = Matrix4.Identity;
            modelViewProjectionMatrix = Matrix4.Identity;

            vertex = new Vector3[vi.Count];
            colour = new Vector4[vi.Count];
            texture = new Vector2[ti.Count];
            normal = new Vector3[ni.Count];
            index = new int[vi.Count];
            int count;

            /*List<int> sorted = new List<int>();

            for (int i = 0; i < vi.Count; i++)
            {
                index[i] = vi[i];
            }

            for (int i = 0; i < vp.Count; i++)
            {
                vertex[i] = vp[i];
            }

            for (int i = 0; i < ti.Count; i++)
            {
                texture[ti[i]-1] = tp[ti[i]-1];
            }

            for (int i = 0; i < ni.Count; i++)
            {
                normal[ni[i]-1] = np[ni[i]-1];
            }

            Console.WriteLine("VI = "+vi.Count);
            Console.WriteLine("VP = "+vp.Count);
            Console.WriteLine("Vertex = " + vertex.Length);
            Console.WriteLine("Indices = " + index.Length);

            for (int i = 0; i < index.Length; i++)
            {
                Console.Write(index[i]+", ");
            }
            Console.WriteLine();

            for (int i = 0; i < vertex.Length; i++)
            {
                Console.Write(vertex[i] + ", ");
            }
            Console.WriteLine();

            for (int i = 0; i < colour.Length; i++)
                colour[i] = new Vector4(mat.Colour.R, mat.Colour.G, mat.Colour.B, mat.Colour.A);*/


            count = 0;
            for (int i = 0; i < vi.Count; i++)
                vertex[count++] = vp[vi[i]-1];
            Console.WriteLine(count);
            for (int i = 0; i < colour.Length; i++)
                colour[i] = new Vector4(mat.Colour.R, mat.Colour.G, mat.Colour.B, mat.Colour.A);

            count = 0;
            for (int i = 0; i < ti.Count; i++)
                texture[count++] = tp[ti[i]-1];

            count = 0;
            for (int i = 0; i < ni.Count; i++)
                normal[count++] = np[ni[i]-1];
		}
		#endregion

		#region Attributes
		/// <summary>
		/// 
		/// </summary>
        public Vector3[] Vertex
		{
			get { return vertex; }
		}

        /// <summary>
        /// 
        /// </summary>
        public Vector2[] Texture
        {
            get { return texture; }
        }

		/// <summary>
		/// 
		/// </summary>
        public Vector3[] Normal
		{
			get { return normal; }
		}

        /// <summary>
        /// 
        /// </summary>
        public Vector4[] Colour
        {
            get { return colour; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int[] Index
        {
            get { return index; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Matrix4 ModelMatrix
        {
            get { return modelMatrix; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Matrix4 ViewProjectionMatrix
        {
            get { return viewProjectionMatrix; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Matrix4 ModelViewProjectionMatrix
        {
            get { return modelViewProjectionMatrix; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public bool Valid
        {
            get { return !( (vertex != null) && (normal != null) ); }
        }
		#endregion
	}
}