using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DisplayModel
{
    class PictureWindow : GameWindow
    {
        protected Shader shaderData;
        protected List<GameObject> objects;
        protected string imagePath;

        //Camera feature(s)(make into class)
        protected Camera camera = new Camera();

        //Default model view field(s)
        protected int viewNumber = 0;
        protected int degrees = 45;
        protected int rotater = 0;

        public PictureWindow(string _imagePath)
            : base(720, 405, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            shaderData = new Shader();
            objects = new List<GameObject>();
            imagePath = _imagePath;
        }

        public void Add(GameObject gameObject)
        {
            if (!objects.Contains(gameObject))
                objects.Add(gameObject);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            shaderData.initProgram();
            System.Windows.Forms.Cursor.Hide();

            Title = "3D model viewer (Pictures)";

            WindowBorder = WindowBorder.Hidden;
            WindowState = WindowState.Fullscreen;
            Visible = true;

            GL.ClearColor(Color.Bisque);
            GL.Enable(EnableCap.DepthTest);

            GL.PointSize(5f);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            System.Windows.Forms.Cursor.Show();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            defaultView();
            for (int i = 0; i < objects.Count; ++i)
                shaderData.Draw(objects[i]);

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            shaderData.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)MathHelper.PiOver4, Width / (float)Height, 0.1f, 100.0f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref shaderData.ProjectionMatrix);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == 'q')
            {
                Exit();
            }
        }

        public void defaultView()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].bufferData.ModelViewMatrix = Matrix4.Identity * Matrix4.CreateRotationY(rotater++/5) * Matrix4.CreateTranslation(0f, 0f, -4);
            }
        }

        public void GrabScreenshot()
        {
            if (OpenTK.Graphics.GraphicsContext.CurrentContext == null)
                throw new OpenTK.Graphics.GraphicsContextMissingException();

            Bitmap screenShot = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            System.Drawing.Imaging.BitmapData data =
                screenShot.LockBits(this.ClientRectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
            screenShot.UnlockBits(data);

            screenShot.RotateFlip(RotateFlipType.RotateNoneFlipY);
            screenShot.Save(imagePath + @"view" + viewNumber++ + ".bmp");
            screenShot.Dispose();
        }
    }
}
