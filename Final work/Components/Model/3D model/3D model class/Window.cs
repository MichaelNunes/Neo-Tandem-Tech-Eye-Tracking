using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DisplayModel
{
    public class Window : GameWindow
    {
        private Shader shaderData;
        private List<GameObject> objects;
        private string imagePath;

        int viewNumber = 0;
        int degrees = 45;
        
        GLControl control;

        public Window() : base(720, 405)
        {
            shaderData = new Shader();
            objects = new List<GameObject>();
            control = new GLControl();
        }

        public Window(string _imagePath)
            : base(720, 405)
        {
            shaderData = new Shader();
            objects = new List<GameObject>();
            control = new GLControl();
            imagePath = _imagePath;
        }

        public void Add(GameObject gameObject)
        {
            if (!objects.Contains(gameObject))
                objects.Add(gameObject);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if(e.KeyChar == 'q')
            {
                Exit();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            shaderData.initProgram();


            Title = "Hello OpenTK!";

            WindowBorder = WindowBorder.Hidden;
            WindowState = WindowState.Fullscreen;
            Visible = false;

            GL.ClearColor(Color.Bisque);
            GL.Enable(EnableCap.DepthTest);

            GL.PointSize(5f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            for (int i = 0; i < objects.Count; ++i )
                shaderData.Draw(objects[i].BufferData);
            
            SwapBuffers();
            control.PerformContextUpdate();
            changeView();
            GrabScreenshot();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            shaderData.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)MathHelper.PiOver4, Width / (float)Height, 1.0f, 100.0f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref shaderData.ProjectionMatrix);
        }

        public void changeView()
        {
            if (viewNumber * degrees >= 360 + degrees)
            {
                if (viewNumber > (360/degrees) + 2)
                {
                    Exit();
                }
                else
                {
                    for (int i = 0; i < objects.Count; i++)
                    {
                        objects[i].bufferData.ModelViewMatrix = Matrix4.Identity * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(90)) * Matrix4.CreateTranslation(0f, 0f, -4f);
                    }
                }
                viewNumber++;
            }
            else
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    objects[i].bufferData.ModelViewMatrix = Matrix4.Identity * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(viewNumber * degrees)) * Matrix4.CreateTranslation(0f, 0f, -4f);
                }
                viewNumber++;
            }
        }

        public void GrabScreenshot()
        {
            if (OpenTK.Graphics.GraphicsContext.CurrentContext == null)
                throw new OpenTK.Graphics.GraphicsContextMissingException();

            Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            System.Drawing.Imaging.BitmapData data =
                bmp.LockBits(this.ClientRectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);

            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            bmp.Save(imagePath + @"view" + viewNumber + ".jpg");
        }
    }
}
