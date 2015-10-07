using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Threading;

namespace DisplayModel
{
    class FlyThroughWindow : GameWindow
    {
        protected Shader shaderData;
        protected List<GameObject> objects;
        protected string imagePath;

        //Camera
        protected Camera camera = new Camera();

        //Screenshots counter
        protected int viewNumber = 0;

        //Video recording field(s)
        protected Bitmap videoFrame;
        List<Bitmap> videoFrames = new List<Bitmap>();
        System.Drawing.Imaging.BitmapData data;

        //Background image saving
        Thread oThread;
        bool IsSavingFrames = false;

        protected int frameNumber = 0;

        public FlyThroughWindow(string _imagePath)
            : base(720, 405, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            shaderData = new Shader();
            objects = new List<GameObject>();
            imagePath = _imagePath;
        }

        /// <summary>
        /// Add a gameObject to be rendered to the scene.
        /// </summary>
        /// <param name="gameObject"></param>
        public void Add(GameObject gameObject)
        {
            if (!objects.Contains(gameObject))
                objects.Add(gameObject);
        }

        /// <summary>
        /// This holds all the initializations that need to take place.
        /// </summary>
        /// <param name="e"></param>
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

            for (int i = 0; i < objects.Count; ++i)
            {
                GameObject current = objects[i];

                for (int j = 0; j < current.Children.Count; ++j)
                {
                    GameObject child = current.Children[j];
                    if (!objects.Contains(child))
                        objects.Add(child);
                }
            }
        }

        /// <summary>
        /// Operations done once the fly-through is aborted.
        /// The joining of the threads for saving images is
        /// done here.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            System.Windows.Forms.Cursor.Show();

            if(oThread != null)
            {
                oThread.Join();
            }
            
            saveFrames();
        }

        /// <summary>
        /// Actions taken to update a frame.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            UpdateCamera(e.Time);
        }

        /// <summary>
        /// Actions taken to render a frame.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            defaultView();
            for (int i = 0; i < objects.Count; ++i)
                shaderData.Draw(objects[i]);

            RecordVideo();

            SwapBuffers();
        }

        /// <summary>
        /// Sets the perspective field of view when ever a frame is rendered.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            shaderData.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)MathHelper.PiOver4, Width / (float)Height, 0.1f, 10000.0f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref shaderData.ProjectionMatrix);
        }

        /// <summary>
        /// This method facilitates the camera movements via input from various devices.
        /// </summary>
        /// <param name="time">This is the time that the OnUpdateFrame() method takes to make an update so that camera movement can be made to move at a constant speed regardless of the number of updates a second.</param>
        public void UpdateCamera(double time)
        {
            KeyboardUpdate(time);
            JoystickUpdate(time);
        }

        /// <summary>
        /// Used to update the state of the camera via the state of the keyboard.
        /// </summary>
        /// <param name="time"></param>
        public void KeyboardUpdate(double time)
        {
            OpenTK.Input.KeyboardState state = OpenTK.Input.Keyboard.GetState();
            
            if (state.IsKeyDown(OpenTK.Input.Key.Escape))
            {
                Exit();
            }

            if (state.IsKeyDown(OpenTK.Input.Key.G))
            {
                GrabScreenshot();
            }

            if (state.IsKeyDown(OpenTK.Input.Key.ShiftLeft))
            {
                camera.currentSpeed = camera.runSpeed;
            }
            else if (state.IsKeyDown(OpenTK.Input.Key.ControlLeft))
            {
                camera.currentSpeed = camera.flySpeed;
            }
            else
            {
                camera.currentSpeed = camera.walkSpeed;
            }

            if (state.IsKeyDown(OpenTK.Input.Key.PageUp))
            {
                camera.position.Y += camera.currentSpeed * (float)time;
            }

            if (state.IsKeyDown(OpenTK.Input.Key.PageDown))
            {
                camera.position.Y -= camera.currentSpeed * (float)time;
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

        /// <summary>
        /// Used to update the state of the camera via the state of the joystick.
        /// </summary>
        /// <param name="time"></param>
        public void JoystickUpdate(double time)
        {
            OpenTK.Input.JoystickState state = OpenTK.Input.Joystick.GetState(0);

            if (state.GetButton(OpenTK.Input.JoystickButton.Button4) == OpenTK.Input.ButtonState.Pressed)
            {
                camera.currentSpeed = camera.runSpeed;
            }
            else if (state.GetButton(OpenTK.Input.JoystickButton.Button6) == OpenTK.Input.ButtonState.Pressed)
            {
                camera.currentSpeed = camera.flySpeed;
            }
            else
            {
                camera.currentSpeed = camera.walkSpeed;
            }

            if (state.GetButton(OpenTK.Input.JoystickButton.Button5) == OpenTK.Input.ButtonState.Pressed)
            {
                camera.position.Y += camera.currentSpeed * (float)time;
            }

            if (state.GetButton(OpenTK.Input.JoystickButton.Button7) == OpenTK.Input.ButtonState.Pressed)
            {
                camera.position.Y -= camera.currentSpeed * (float)time;
            }

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

        /// <summary>
        /// Used to update the orientation of the camera via the movement of the mouse.
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// Default view of the camera at the statr of the fly-through.
        /// </summary>
        public void defaultView()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].bufferData.ModelViewMatrix = Matrix4.CreateTranslation(0f, 0f, 0f) * Matrix4.LookAt(camera.position, camera.position + new Vector3((float)Math.Cos(camera.yaw), camera.pitch, (float)Math.Sin(camera.yaw)), new Vector3(0f, 1f, 0f));
            }
        }

        /// <summary>
        /// This is used to save each frame produced into an array of bitmaps.
        /// Once a limit has been reached a worker thread is started to save
        /// the images in the background with the lowest priority.
        /// </summary>
        public void RecordVideo()
        {
            if (OpenTK.Graphics.GraphicsContext.CurrentContext == null)
                throw new OpenTK.Graphics.GraphicsContextMissingException();

            videoFrame = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            data = videoFrame.LockBits(this.ClientRectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
            videoFrame.UnlockBits(data);

            videoFrames.Add(videoFrame);
            if(videoFrames.Count >= 50 && IsSavingFrames == false)
            {
                oThread = new Thread(this.saveFrames);
                oThread.Priority = ThreadPriority.Lowest;

                IsSavingFrames = true;
                
                oThread.Start();
            }
        }

        /// <summary>
        /// This is used to save the images for video recording on to disk either at the
        /// end of the fly-through or during the fly-through once the maximum amount in memory
        /// has been reached.
        /// </summary>
        public void saveFrames()
        {
            while(videoFrames.Count > 0)
            {
                videoFrames[0].RotateFlip(RotateFlipType.RotateNoneFlipY);
                videoFrames[0].Save(imagePath + @"frame" + (frameNumber++) + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                videoFrames[0].Dispose();

                videoFrames.RemoveAt(0);
            }

            IsSavingFrames = false;
        }

        /// <summary>
        /// This is used to take a screenshot of the current rendered frame and to save it to disk.
        /// </summary>
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
            screenShot.Save(imagePath + @"view" + (viewNumber++) + ".bmp");
            screenShot.Dispose();
        }
    }
}
