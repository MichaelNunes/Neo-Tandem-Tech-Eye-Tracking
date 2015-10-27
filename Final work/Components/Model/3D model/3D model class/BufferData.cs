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
using System;
using System.Collections.Generic;
#endregion

namespace DisplayModel
{
	/// <summary>
	/// A simpe container for the: vertex position, uv co-ordrinate,
    /// and vertex normal values of a 3D model.
	/// </summary>
	public class BufferData
    {
		#region Fields
        public Vector3[] Vertex;
        public Vector2[] Texture;
        public Vector3[] Normal;
        public Vector4[] Colour;
        public Matrix4 ModelViewMatrix;
		#endregion

		#region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public BufferData()
        {
            Vertex = new Vector3[0];
            Texture = new Vector2[0];
            Normal = new Vector3[0];
            Colour = new Vector4[0];
            ModelViewMatrix = Matrix4.Identity;
        }

        /// <summary>
        /// Create a buffer data object from the values passed and the indices provided.
        /// </summary>
        /// <param name='vertexPoint'> A list of vertex points. </param>
        /// <param name='texturePoint'> A list of texture points. </param>
        /// <param name='normalPoint'> A list of normal points. </param>
        /// <param name='vertexIndex'> A list of vertex indices. </param>
        /// <param name='textureIndex'> A list of texture indices. </param>
        /// <param name='normalIndex'> A list of normal indices. </param>
        /// <param name="tc"> A list of texture indices indices. </param>
        public BufferData(ref List<Vector3> vertexPoint, ref List<Vector2> texturePoint, ref List<Vector3> normalPoint, int[] vertexIndex, int[] textureIndex, int[] normalIndex)
        {
            int size = vertexIndex.Length;
            Vertex = new Vector3[size];
            Texture = new Vector2[textureIndex != null ? size : 0];
            Normal = new Vector3[normalIndex != null ? size : 0];
            Colour = new Vector4[size];
            Color4 colour = Color4.LightGray;
            ModelViewMatrix = Matrix4.Identity;

            for (int i = 0; i < size; ++i)
            {
                Vertex[i] = vertexPoint[vertexIndex[i] - 1];
                if (textureIndex != null) Texture[i] = texturePoint[textureIndex[i] - 1];
                if (normalIndex != null) Normal[i] = normalPoint[normalIndex[i] - 1];
                Colour[i] = new Vector4(colour.R, colour.G, colour.B, colour.A);
            }
		}
        #endregion

        #region Colour Assignment
        /// <summary>
        /// Updates the models base colour by assigning the values in the colour array.
        /// </summary>
        /// <param name="newColour"> RGBA values of the new colour. </param>
        public void SetColour(Vector3 newColour, float alpha)
        {
            for (int i = 0; i < Colour.Length; ++i)
                Colour[i] = new Vector4(newColour, alpha);
        }
		#endregion
	}
}