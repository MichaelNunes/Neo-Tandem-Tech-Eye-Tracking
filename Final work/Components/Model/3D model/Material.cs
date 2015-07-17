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
using System.IO;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Math;

namespace DisplayModel
{
	/// <summary>
	/// Represents a the visual aspect of the 3D model. The model
	/// can either be coloured by a Red,Green,Blue triple (rgb) or
	/// an image texture.
	/// </summary>
	public struct Material
	{
		#region Fields
		private Color4 colour;
        private int textureId;
        private Bitmap image;
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a material with the color provided.
		/// </summary>
		/// <param name='_color'> The color of the material. </param>
		public Material(Color4 _color)
		{
            colour = _color;
            image = null;
            textureId = -1;
		}

		/// <summary>
		/// Creates a material with an image texture.
		/// </summary>
		/// <param name='_filepath'> The path to the texture of the object. </param>
		public Material(string filepath)
        {
            colour = Color4.LightGray;
            image = new Bitmap(filepath);
            textureId = -1;
		}

		/// <summary>
		/// Creates a material with an image texture.
		/// </summary>
		/// <param name='_color'> The color of the material. </param>
		/// <param name='_filepath'> The path to the texture of the object. </param>
		public Material(Color4 _color, string filepath)
		{
            colour = _color;
            image = new Bitmap(filepath);
            textureId = -1;
		}
		#endregion

        #region Setup
        /// <summary>
        /// Sets up the texture for the current object containing this material.
        /// </summary>
        public void Setup()
        {
            if (image == null)
                return;

            // Do stuff;
        }
        #endregion

        #region Attributes
        public Color4 Colour
        {
            get { return colour; }
        }

        public int getTextureId
        {
            get { return textureId; }
        }
        #endregion
    }
}