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
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace DisplayModel
{
	/// <summary>
	/// Represents a the visual aspect of the 3D model. The model
	/// can either be coloured by a Red,Green,Blue triple (rgb) or
	/// an image texture.
	/// </summary>
	public class Material
	{
		#region Fields
        public const int MAX_TEXTURES = 32;
        
		private Color4 colour = Color4.LightGray;
        private int textureid;
        private string file;
		#endregion

        #region Constructors
        /// <summary>
        /// Creates a empty material.
        /// </summary>
        public Material()
        {
            textureid = -1;
            file = string.Empty;
        }

		/// <summary>
		/// Creates a material with an array of image textures.
		/// </summary>
		/// <param name='_filepath'> List of paths to the textures of the object. </param>
		public Material(string filepath)
        {
            textureid = -1;
            file = filepath;
		}
		#endregion

        #region Setup
        /// <summary>
        /// Sets up the texture for the current object containing this material.
        /// </summary>
        public void Setup()
        {
            textureid = (file != string.Empty) ? CreateTexture(file) : -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private int CreateTexture(string filepath)
        {
            int textureId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Bitmap bmp = new Bitmap(filepath);
            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            return textureId;
        }
        #endregion

        #region Attributes
        /// <summary>
        /// The color of the material attached to the object.
        /// </summary>
        public Color4 Colour { get { return colour; } }

        /// <summary>
        /// An array of id's for each texture bound.
        /// </summary>
        public int TextureID { get { return textureid; } }
        #endregion
    }
}