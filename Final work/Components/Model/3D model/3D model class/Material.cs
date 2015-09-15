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
		private Color4 colour;
        private int textureId;
        private string filepath;
		#endregion

		#region Constructors
        public Material()
        {
            colour = Color4.LightGray;
            textureId = -1;
            filepath = string.Empty;
        }
		/// <summary>
		/// Creates a material with the color provided.
		/// </summary>
		/// <param name='_color'> The color of the material. </param>
		public Material(Color4 _color)
		{
            colour = _color;
            filepath = string.Empty;
            textureId = -1;
		}

		/// <summary>
		/// Creates a material with an image texture.
		/// </summary>
		/// <param name='_filepath'> The path to the texture of the object. </param>
		public Material(string _filepath)
        {
            colour = Color4.LightGray;
            filepath = _filepath;
            textureId = -1;
		}

		/// <summary>
		/// Creates a material with an image texture.
		/// </summary>
		/// <param name='_color'> The color of the material. </param>
		/// <param name='_filepath'> The path to the texture of the object. </param>
		public Material(Color4 _color, string _filepath)
		{
            colour = _color;
            filepath = _filepath;
            textureId = -1;
		}
		#endregion

        #region Setup
        /// <summary>
        /// Sets up the texture for the current object containing this material.
        /// </summary>
        public void Setup()
        {
            if (filepath == string.Empty)
                return;

            textureId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            // We will not upload mipmaps, so disable mipmapping (otherwise the texture will not appear).
            // We can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create
            // mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Bitmap bmp = new Bitmap(filepath);
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);
        }
        #endregion

        #region Attributes
        public Color4 Colour { get { return colour; } }
        /// <summary>
        /// The id of the material's texture.
        /// </summary>
        public int TextureId { get { return textureId; } }
        #endregion
    }
}