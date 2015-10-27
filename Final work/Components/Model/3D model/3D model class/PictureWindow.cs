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
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Collections.Generic;
#endregion

namespace DisplayModel
{
    /// <summary>
    /// This GameWindow implementation renders all the objects in the seen
    /// 9 time and closes shortly afterwards.
    /// The first 8 shots are taken at 45 degree intervals around the scene.
    /// The final shot is taken from the top of the scene.
    /// </summary>
    public class PictureWindow : GameWindow
    {
        #region Fields
        #region Renderer Fields
        private Camera Camera;
        private Shader Shader;
        private List<GameObject> Objects;
        #endregion

        #region Screenshot Fields
        private string ImagePath;
        private int ViewNumber;
        private int CurrentAngle;

        private const int Angle = 45;
        private const int TopAngle = 90;
        private const int AngleOffset = 15;
        private const int TotalScreenshots = 9;
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises the Shader and GameObject list.
        /// </summary>
        /// <param name="imagePath"> Path to the directory where the screenshots are to be saved. </param>
        public PictureWindow(string imagePath)
            : base(720, 405, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            Camera = new Camera();
            Shader = new Shader();
            Objects = new List<GameObject>();

            ImagePath = imagePath;
            ViewNumber = 0;
            CurrentAngle = 0;
        }
        #endregion

        #region Methods
        #region Helper Methods
        /// <summary>
        /// Add a GameObject to the list of objects to be rendered.
        /// </summary>
        /// <param name="gameObject"> The new GameObject. </param>
        public void Add(GameObject gameObject)
        {
            if (!Objects.Contains(gameObject))
                Objects.Add(gameObject);
        }

        /// <summary>
        /// Rotates the camera around the model at 45 degree angles.
        /// </summary>
        private void ChangeView()
        {
            CurrentAngle = ViewNumber * Angle;

            if (CurrentAngle >= 360)
                for (int i = 0; i < Objects.Count; i++)
                    Objects[i].BufferData.ModelViewMatrix = Matrix4.Identity *
                                                            Matrix4.CreateRotationX(MathHelper.DegreesToRadians(90)) *
                                                            Matrix4.CreateTranslation(0f, 0f, -4f);
            
            else
                for (int i = 0; i < Objects.Count; i++)
                    Objects[i].BufferData.ModelViewMatrix = Matrix4.Identity *
                                                            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(CurrentAngle)) *
                                                            Matrix4.CreateRotationX(MathHelper.DegreesToRadians(AngleOffset)) *
                                                            Matrix4.CreateTranslation(0f, 0f, -5f);

            if (++ViewNumber == TotalScreenshots)
                Exit();
        }

        /// <summary>
        /// Captures the currently rendered screen and saves it as an image.
        /// </summary>
        private void GrabScreenshot()
        {
            if (OpenTK.Graphics.GraphicsContext.CurrentContext == null)
                throw new OpenTK.Graphics.GraphicsContextMissingException();

            Bitmap screenShot = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            System.Drawing.Imaging.BitmapData data = screenShot.LockBits(this.ClientRectangle, 
                                                                        System.Drawing.Imaging.ImageLockMode.WriteOnly,
                                                                        System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
            screenShot.UnlockBits(data);

            screenShot.RotateFlip(RotateFlipType.RotateNoneFlipY);
            screenShot.Save(ImagePath + @"view" + ViewNumber + ".bmp");
            screenShot.Dispose();
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Sets up the window to render the scene.
        /// Extends the objects into a list to be rendered.
        /// Sets the window to border-less and full-screen,
        /// and hides the mouse.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            System.Windows.Forms.Cursor.Hide();

            Title = "3D model viewer (Pictures)";

            WindowBorder = WindowBorder.Hidden;
            WindowState = WindowState.Fullscreen;
            Visible = false;

            GL.ClearColor(Color.Bisque);
            GL.Enable(EnableCap.DepthTest);

            Shader.InitProgram();
            for (int i = 0; i < Objects.Count; ++i)
            {
                GameObject current = Objects[i];

                for (int j = 0; j < current.Children.Count; ++j)
                {
                    GameObject child = current.Children[j];
                    if (!Objects.Contains(child))
                        Objects.Add(child);
                }
            }
        }

        /// <summary>
        /// Clears the object list and returns the mouse to
        /// the screen.
        /// </summary>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            Objects.Clear();
            System.Windows.Forms.Cursor.Show();
        }

        /// <summary>
        /// Clears the current screen and renders the objects in view of
        /// the camera to the screen. After which, a screen-shot is taken.
        /// </summary>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            ChangeView();

            for (int i = 0; i < Objects.Count; ++i)
                Shader.Draw(Objects[i]);

            GrabScreenshot();
            SwapBuffers();
        }

        /// <summary>
        /// Resizes the render window to fit the current context size.
        /// Creates the appropriate projection matrix fore the screen.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            Shader.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)MathHelper.PiOver4,
                                                                            Width / (float)Height,
                                                                            0.1f,
                                                                            100.0f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref Shader.ProjectionMatrix);
        }

        /// <summary>
        /// Allows the use to forcefully close the windows and
        /// exit the window by pressing 'q' or 'Q'.
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == 'q' || e.KeyChar == 'Q')
                Exit();
        }
        #endregion
        #endregion
    }
}
