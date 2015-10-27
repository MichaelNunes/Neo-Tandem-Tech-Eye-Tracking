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
using System.Drawing;
using System.Drawing.Imaging;
#endregion

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
        public Vector3 Ambient;
        public Vector3 Diffuse;
        public Vector3 Specular;

        public float Alpha;
        public string File;
        public int TextureID;
		#endregion

        #region Constructors
        /// <summary>
        /// Creates a empty material.
        /// </summary>
        public Material()
        {
            Ambient = new Vector3(0.0f);
		    Diffuse = new Vector3(0.5f);
            Specular = new Vector3(1.0f);
            File = null;
            TextureID = -1;
        }

		/// <summary>
		/// Creates a material with an array of image textures.
		/// </summary>
        /// <param name='info'> List of paths to the textures of the object and colour. </param>
		public Material(string info)
        {
            TextureID = -1;
            Ambient = new Vector3(0.0f);
            Diffuse = new Vector3(0.5f);
            Specular = new Vector3(1.0f);

            if (info != string.Empty)
            {
                string[] colour = null;
                string has_image = info.Split(' ')[0];

                switch (has_image)
                {
                    case "true":
                        File = info.Substring(5).Split('\n')[0];
                        colour = info.Substring(5).Split('\n')[1].Split(' ');
                        break;

                    case "false":
                        File = null;
                        colour = info.Substring(6).Split(' ');
                        break;
                }

                Ambient = new Vector3(float.Parse(colour[0]), float.Parse(colour[1]), float.Parse(colour[2]));
                Diffuse = new Vector3(float.Parse(colour[3]), float.Parse(colour[4]), float.Parse(colour[5]));
                Specular = new Vector3(float.Parse(colour[6]), float.Parse(colour[7]), float.Parse(colour[8]));
                Alpha = float.Parse(colour[9]);
            }
		}
		#endregion

        #region Texture Creation Methods
        /// <summary>
        /// Sets up the texture for the current object containing this material.
        /// </summary>
        public void Setup()
        {
            if (File != null)
            {
                CreateTexture();
                File = null;
            }
        }

        /// <summary>
        /// Reads the image and generates a texture and attaches it to
        /// the material.
        /// </summary>
        private void CreateTexture()
        {
            TextureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TextureID);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            Bitmap texture = new Bitmap(File);
            texture.RotateFlip(RotateFlipType.Rotate180FlipX);

            BitmapData texture_data = texture.LockBits(new Rectangle(0, 0, texture.Width, texture.Height),
                                                    ImageLockMode.ReadOnly,
                                                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                            texture_data.Width, texture_data.Height, 0,
                            OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                            PixelType.UnsignedByte, texture_data.Scan0);

            texture.UnlockBits(texture_data);
            texture.Dispose();
        }
        #endregion
    }
}