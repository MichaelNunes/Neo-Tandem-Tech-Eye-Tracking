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
        Vector3 cameraPosition = new Vector3(0f, 0f, 0f);
        const float walkSpeed = 1.0f;
        const float runSpeed = 5.0f;
        float cameraSpeed = 0.0f;
        float yaw = (float)MathHelper.DegreesToRadians(-90);
        float pitch = 0.0f;

        //Video recording field(s)
        Bitmap videoImage;
        int frameNumber = 0;
        bool isRecording = false;
        bool flyThrough;

        //Misc
        

        public Window(string _imagePath, bool _flyThrough)
            : base(720, 405, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            shaderData = new Shader();
            objects = new List<GameObject>();
            imagePath = _imagePath;
            flyThrough = _flyThrough;


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

            if (e.KeyChar == 'v')
            {
                if(isRecording)
                {
                    isRecording = false;
                }
                else
                {
                    isRecording = true;
                    videoImage = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            shaderData.initProgram();
            System.Windows.Forms.Cursor.Hide();

            Title = "3D model viewer";

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

            if(isRecording)
            {
                RecordVideo();
            }

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
                objects[i].bufferData.ModelViewMatrix = Matrix4.CreateTranslation(0f, 0f, 0f) * Matrix4.LookAt(cameraPosition, cameraPosition + new Vector3((float)Math.Cos(yaw), pitch, (float)Math.Sin(yaw)), new Vector3(0f, 1f, 0f));
            }
        }

        /// <summary>
        /// This method facilitates the camera movements
        /// </summary>
        /// <param name="time">This is the time that the OnUpdateFrame() method takes to make an update so that camera movement can be made to move at a constant speed regardless of the number of updates a second.</param>
        public void UpdateCamera(double time)
        {
            KeyboardUpdate(time);
            JoystickUpdate(time);
        }

        void KeyboardUpdate(double time)
        {
            cameraSpeed = (Keyboard[OpenTK.Input.Key.ShiftLeft]) ? runSpeed : walkSpeed;

            if (Keyboard[OpenTK.Input.Key.W])
            {
                cameraPosition.X += (float)Math.Cos(yaw) * cameraSpeed * (float)time;
                cameraPosition.Z += (float)Math.Sin(yaw) * cameraSpeed * (float)time;
            }

            if (Keyboard[OpenTK.Input.Key.S])
            {
                cameraPosition.X -= (float)Math.Cos(yaw) * cameraSpeed * (float)time;
                cameraPosition.Z -= (float)Math.Sin(yaw) * cameraSpeed * (float)time;
            }

            if (Keyboard[OpenTK.Input.Key.A])
            {
                cameraPosition.X -= (float)Math.Cos(yaw + Math.PI / 2) * cameraSpeed * (float)time;
                cameraPosition.Z -= (float)Math.Sin(yaw + Math.PI / 2) * cameraSpeed * (float)time;
            }

            if (Keyboard[OpenTK.Input.Key.D])
            {
                cameraPosition.X += (float)Math.Cos(yaw + Math.PI / 2) * cameraSpeed * (float)time;
                cameraPosition.Z += (float)Math.Sin(yaw + Math.PI / 2) * cameraSpeed * (float)time;
            }

            //Replace yaw and pitch with mouseYaw and mousePitch
            if (Keyboard[OpenTK.Input.Key.Left])
            {
                yaw -= 1.0f * (float)time;
            }

            if (Keyboard[OpenTK.Input.Key.Right])
            {
                yaw += 1.0f * (float)time;
            }

            if (Keyboard[OpenTK.Input.Key.Up])
            {
                pitch += 1.0f * (float)time;
            }

            if (Keyboard[OpenTK.Input.Key.Down])
            {
                pitch -= 1.0f * (float)time;
            }
        }

        void JoystickUpdate(double time)
        {
            OpenTK.Input.JoystickState state = OpenTK.Input.Joystick.GetState(0);

            if (state.GetButton(OpenTK.Input.JoystickButton.Button4) == OpenTK.Input.ButtonState.Pressed ||
                state.GetButton(OpenTK.Input.JoystickButton.Button5) == OpenTK.Input.ButtonState.Pressed)
                cameraPosition.Y += (float)(cameraSpeed * time);

            if (state.GetButton(OpenTK.Input.JoystickButton.Button6) == OpenTK.Input.ButtonState.Pressed ||
                state.GetButton(OpenTK.Input.JoystickButton.Button7) == OpenTK.Input.ButtonState.Pressed)
                cameraPosition.Y -= (float)(cameraSpeed  * time);

            double x1 = state.GetAxis(OpenTK.Input.JoystickAxis.Axis0);
            double x2 = state.GetAxis(OpenTK.Input.JoystickAxis.Axis1);

            double y1 = state.GetAxis(OpenTK.Input.JoystickAxis.Axis2);
            double y2 = state.GetAxis(OpenTK.Input.JoystickAxis.Axis3);

            cameraPosition.X += (float)(Math.Cos(yaw) * x2 * cameraSpeed * time);
            cameraPosition.Z += (float)(Math.Sin(yaw) * x2 * cameraSpeed * time);

            cameraPosition.X += (float)(Math.Cos(yaw + Math.PI / 2) * x1 * cameraSpeed * time);
            cameraPosition.Z += (float)(Math.Sin(yaw + Math.PI / 2) * x1 * cameraSpeed * time);

            yaw += (float)(y1 * time);
            pitch += (float)(y2 * time);
        }

        protected override void OnMouseMove(OpenTK.Input.MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            /*if(e.X >= Width)
            {
                System.Windows.Forms.Cursor.Position = new Point(0, e.Y);
                return;
            }

            if(e.X <= 0)
            {
                System.Windows.Forms.Cursor.Position = new Point(Width, e.Y);
                return;
            }

            if(e.Y >= Height)
            {
                System.Windows.Forms.Cursor.Position = new Point(e.X, 0);
                return;
            }

            if (e.Y <= 0)
            {
                System.Windows.Forms.Cursor.Position = new Point(e.X, Height);
                return;
            }*/

            //System.Windows.Forms.Cursor.Position = new Point(Width/2, Height/2);
            
            yaw += e.XDelta / 500.0f;
            pitch -= e.YDelta / 500.0f;
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
