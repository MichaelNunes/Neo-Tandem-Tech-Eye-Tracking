using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DisplayModel
{
    class FlyThroughWindow : GameWindow
    {
        protected Shader shaderData;
        protected List<GameObject> objects;
        protected string imagePath;

        //Camera feature(s)(make into class)
        protected Camera camera = new Camera();

        //Video recording field(s)
        protected Bitmap videoImage;
        protected int frameNumber = 0;
        protected bool isRecording = false;
        protected bool flyThrough;

        public FlyThroughWindow(string _imagePath)
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

            Title = "3D model viewer (Fly through)";

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

            UpdateCamera(e.Time);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            defaultView();
            for (int i = 0; i < objects.Count; ++i)
                shaderData.Draw(objects[i]);

            if (isRecording)
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

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == 'q')
            {
                Exit();
            }

            if (e.KeyChar == 'v')
            {
                if (isRecording)
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

        /// <summary>
        /// This method facilitates the camera movements
        /// </summary>
        /// <param name="time">This is the time that the OnUpdateFrame() method takes to make an update so that camera movement can be made to move at a constant speed regardless of the number of updates a second.</param>
        public void UpdateCamera(double time)
        {
            KeyboardUpdate(time);
            JoystickUpdate(time);
        }

        public void KeyboardUpdate(double time)
        {
            OpenTK.Input.KeyboardState state = OpenTK.Input.Keyboard.GetState();

            if (state.IsKeyDown(OpenTK.Input.Key.ShiftLeft))
            {
                camera.currentSpeed = camera.runSpeed;
            }
            else
            {
                camera.currentSpeed = camera.walkSpeed;
            }

            if (state.IsKeyDown(OpenTK.Input.Key.W))
            {
                camera.position.X += (float)Math.Cos(camera.yaw) * camera.currentSpeed * (float)time;
                camera.position.Z += (float)Math.Sin(camera.yaw) * camera.currentSpeed * (float)time;
            }

            if (state.IsKeyDown(OpenTK.Input.Key.S))
            {
                camera.position.X -= (float)Math.Cos(camera.yaw) * camera.currentSpeed * (float)time;
                camera.position.Z -= (float)Math.Sin(camera.yaw) * camera.currentSpeed * (float)time;
            }

            if (state.IsKeyDown(OpenTK.Input.Key.A))
            {
                camera.position.X -= (float)Math.Cos(camera.yaw + Math.PI / 2) * camera.currentSpeed * (float)time;
                camera.position.Z -= (float)Math.Sin(camera.yaw + Math.PI / 2) * camera.currentSpeed * (float)time;
            }

            if (state.IsKeyDown(OpenTK.Input.Key.D))
            {
                camera.position.X += (float)Math.Cos(camera.yaw + Math.PI / 2) * camera.currentSpeed * (float)time;
                camera.position.Z += (float)Math.Sin(camera.yaw + Math.PI / 2) * camera.currentSpeed * (float)time;
            }

            //Replace yaw and pitch with mouseYaw and mousePitch
            if (state.IsKeyDown(OpenTK.Input.Key.Left))
            {
                camera.yaw -= 1.0f * (float)time;
            }

            if (state.IsKeyDown(OpenTK.Input.Key.Right))
            {
                camera.yaw += 1.0f * (float)time;
            }

            if (state.IsKeyDown(OpenTK.Input.Key.Up))
            {
                camera.pitch += 1.0f * (float)time;
            }

            if (state.IsKeyDown(OpenTK.Input.Key.Down))
            {
                camera.pitch -= 1.0f * (float)time;
            }
        }

        public void JoystickUpdate(double time)
        {
            OpenTK.Input.JoystickState state = OpenTK.Input.Joystick.GetState(0);

            if (state.GetButton(OpenTK.Input.JoystickButton.Button4) == OpenTK.Input.ButtonState.Pressed ||
                state.GetButton(OpenTK.Input.JoystickButton.Button5) == OpenTK.Input.ButtonState.Pressed)
                camera.position.Y += (float)(camera.currentSpeed * time);

            if (state.GetButton(OpenTK.Input.JoystickButton.Button6) == OpenTK.Input.ButtonState.Pressed ||
                state.GetButton(OpenTK.Input.JoystickButton.Button7) == OpenTK.Input.ButtonState.Pressed)
                camera.position.Y -= (float)(camera.currentSpeed * time);

            double x1 = state.GetAxis(OpenTK.Input.JoystickAxis.Axis0);
            double x2 = state.GetAxis(OpenTK.Input.JoystickAxis.Axis1);

            double y1 = state.GetAxis(OpenTK.Input.JoystickAxis.Axis2);
            double y2 = state.GetAxis(OpenTK.Input.JoystickAxis.Axis3);

            camera.position.X += (float)(Math.Cos(camera.yaw) * x2 * camera.currentSpeed * time);
            camera.position.Z += (float)(Math.Sin(camera.yaw) * x2 * camera.currentSpeed * time);

            camera.position.X += (float)(Math.Cos(camera.yaw + Math.PI / 2) * x1 * camera.currentSpeed * time);
            camera.position.Z += (float)(Math.Sin(camera.yaw + Math.PI / 2) * x1 * camera.currentSpeed * time);

            camera.yaw += (float)(y1 * time);
            camera.pitch += (float)(y2 * time);
        }

        protected override void OnMouseMove(OpenTK.Input.MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            /*if(e.X >= Width)
            {
                System.Windows.Forms.Cursor.Position = new Point(1, e.Y);
                return;
            }

            if(e.X <= 0)
            {
                System.Windows.Forms.Cursor.Position = new Point(Width-1, e.Y);
                return;
            }

            if(e.Y >= Height)
            {
                System.Windows.Forms.Cursor.Position = new Point(e.X, 1);
                return;
            }

            if (e.Y <= 0)
            {
                System.Windows.Forms.Cursor.Position = new Point(e.X, Height-1);
                return;
            }*/

            //System.Windows.Forms.Cursor.Position = new Point(Width/2, Height/2);

            camera.yaw += e.XDelta / 500.0f;
            camera.pitch -= e.YDelta / 500.0f;
        }

        public void defaultView()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].bufferData.ModelViewMatrix = Matrix4.CreateTranslation(0f, 0f, 0f) * Matrix4.LookAt(camera.position, camera.position + new Vector3((float)Math.Cos(camera.yaw), camera.pitch, (float)Math.Sin(camera.yaw)), new Vector3(0f, 1f, 0f));
            }
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
