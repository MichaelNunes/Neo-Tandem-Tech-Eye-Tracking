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

        //Default model view field(s)
        int viewNumber = 0;
        int degrees = 45;
        int rotater = 0;

        //Camera feature(s)(make into class)
        Vector3 cameraPosition = new Vector3(0f, 0f, -2f);
        float cameraSpeed = 1.0f;
        float yaw = 0.0f;
        float pitch = 0.0f;

        //Video recording field(s)
        Bitmap videoImage;
        int frameNumber = 0;

        //Screenshot field(s)
        Bitmap screenShot;

        public Window()
            : base(720, 405, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 16))
        {
            shaderData = new Shader();
            objects = new List<GameObject>();
        }

        public Window(string _imagePath)
            : base(720, 405, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 16))
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

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if(e.KeyChar == 'q')
            {
                Exit();
            }

            if (e.KeyChar == 'g')
            {
                GrabScreenshot();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            shaderData.initProgram();
            videoImage = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            screenShot = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            Title = "3D model viewer";

            WindowBorder = WindowBorder.Hidden;
            WindowState = WindowState.Fullscreen;
            Visible = true;

            GL.ClearColor(Color.Bisque);
            GL.Enable(EnableCap.DepthTest);

            GL.PointSize(5f);
        }

        /// <summary>
        /// Updates to data performed irrespective of rendered frame.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            UpdateCamera(e.Time);
        }
        
        /// <summary>
        /// The current frame to be rendered.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            defaultView();
            for (int i = 0; i < objects.Count; ++i )
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
        /// <summary>
        /// This provides the default view with camera control enabled.
        /// </summary>
        public void defaultView()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].bufferData.ModelViewMatrix = Matrix4.LookAt(cameraPosition, cameraPosition + new Vector3((float)Math.Cos(yaw), pitch, (float)Math.Sin(yaw)),new Vector3(0f, 1f,0f));
            }
        }

        /// <summary>
        /// This method facilitates the camera movements
        /// </summary>
        /// <param name="time">This is the time that the OnUpdateFrame() method takes to make an update so that camera movement can be made to move at a constant speed regardless of the number of updates a second.</param>
        public void UpdateCamera(double time)
        {
            if (Keyboard[OpenTK.Input.Key.W])
            {
                //cameraPosition.Z += cameraSpeed * (float)time;
                cameraPosition.X += (float)Math.Cos(yaw) * cameraSpeed * (float)time;
                cameraPosition.Z += (float)Math.Sin(yaw) * cameraSpeed * (float)time;
            }

            if (Keyboard[OpenTK.Input.Key.S])
            {
                //cameraPosition.Z -= cameraSpeed * (float)time;
                cameraPosition.X -= (float)Math.Cos(yaw) * cameraSpeed * (float)time;
                cameraPosition.Z -= (float)Math.Sin(yaw) * cameraSpeed * (float)time;
            }

            if (Keyboard[OpenTK.Input.Key.A])
            {
                //cameraPosition.X += cameraSpeed * (float)time;
                cameraPosition.X -= (float)Math.Cos(yaw + Math.PI / 2) * cameraSpeed * (float)time;
                cameraPosition.Z -= (float)Math.Sin(yaw + Math.PI / 2) * cameraSpeed * (float)time;
            }

            if (Keyboard[OpenTK.Input.Key.D])
            {
                //cameraPosition.X -= cameraSpeed * (float)time;
                cameraPosition.X += (float)Math.Cos(yaw + Math.PI / 2) * cameraSpeed * (float)time;
                cameraPosition.Z += (float)Math.Sin(yaw + Math.PI / 2) * cameraSpeed * (float)time;
            }

            //Replace yaw and pitch with mouseYaw and mousePitch
            if (Keyboard[OpenTK.Input.Key.Left])
            {
                //cameraPosition.X += cameraSpeed * (float)time;
                yaw -= 1.0f * (float)time;
            }

            if (Keyboard[OpenTK.Input.Key.Right])
            {
                //cameraPosition.X -= cameraSpeed * (float)time;
                yaw += 1.0f * (float)time;
            }

            if (Keyboard[OpenTK.Input.Key.Up])
            {
                //cameraPosition.X += cameraSpeed * (float)time;
                pitch += 1.0f * (float)time;
            }

            if (Keyboard[OpenTK.Input.Key.Down])
            {
                //cameraPosition.X -= cameraSpeed * (float)time;
                pitch -= 1.0f * (float)time;
            }
        }

        public void GrabScreenshot()
        {
            if (OpenTK.Graphics.GraphicsContext.CurrentContext == null)
                throw new OpenTK.Graphics.GraphicsContextMissingException();

            System.Drawing.Imaging.BitmapData data =
                screenShot.LockBits(this.ClientRectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
            screenShot.UnlockBits(data);

            screenShot.RotateFlip(RotateFlipType.RotateNoneFlipY);
            screenShot.Save(imagePath + @"view" + viewNumber++ + ".bmp");
        }

        public void RecordVideo()
        {
            if (OpenTK.Graphics.GraphicsContext.CurrentContext == null)
                throw new OpenTK.Graphics.GraphicsContextMissingException();

            System.Drawing.Imaging.BitmapData data =
                videoImage.LockBits(this.ClientRectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
            videoImage.UnlockBits(data);

            //bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            videoImage.Save(imagePath + @"frame" + (frameNumber++) + ".bmp");
        }
    }
}
