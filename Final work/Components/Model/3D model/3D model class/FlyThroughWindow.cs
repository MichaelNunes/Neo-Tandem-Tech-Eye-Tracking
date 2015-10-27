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
using System.Threading;
#endregion

namespace DisplayModel
{
    /// <summary>
    /// A window that allows the user to move around the 3d model
    /// scene.
    /// The user can move using:
    ///     W: Move Forward
    ///     A: Move Left
    ///     S: Move Backwards
    ///     D: Move Right
    ///     PageUp: Move Up
    ///     PageDown: Move Down
    /// The user can change the orientation od the camera with the mouse
    /// of the keyboard:
    ///     Up: Look Up
    ///     Left: Turn Left
    ///     Down: Look Down
    ///     Right: Turn Right
    /// A controller with analog sticks can also be used.
    /// </summary>
    class FlyThroughWindow : GameWindow
    {
        #region Fields
        #region Renderer Fields
        private Camera Camera;
        private Shader Shader;
        private List<GameObject> Objects;
        #endregion

        #region Video Frame Fields
        private string ImagePath;
        private int FrameNumber;
        private const int MaxFrames = 900;

        private Bitmap VideoFrame;
        private List<Bitmap> VideoFrames;
        private System.Drawing.Imaging.BitmapData ImageData;
        #endregion

        #region Thread Fields
        private Thread oThread;
        private bool IsSavingFrames;
        #endregion
        #endregion

        #region Constructor
        public FlyThroughWindow(string imagePath)
            : base(720, 405, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            Camera = new Camera();
            Shader = new Shader();
            Objects = new List<GameObject>();

            ImagePath = imagePath;
            FrameNumber = 0;
            VideoFrames = new List<Bitmap>();

            IsSavingFrames = false;
        }
        #endregion

        #region Methods
        #region Helper Methods
        /// <summary>
        /// Adds a GameObject to be rendered to the scene.
        /// </summary>
        /// <param name="gameObject"> The new object to be rendered. </param>
        public void Add(GameObject gameObject)
        {
            if (!Objects.Contains(gameObject))
                Objects.Add(gameObject);
        }

        /// <summary>
        /// This method facilitates the camera movements via input from 
        /// various devices.
        /// </summary>
        /// <param name="time">
        /// This is the time that the OnUpdateFrame() method takes to make 
        /// an update so that camera movement can be made to move at a constant
        /// speed regardless of the number of updates a second.
        /// </param>
        private void UpdateCamera(double time)
        {
            KeyboardUpdate(time);
            JoystickUpdate(time);
            Shader.Point.Position.X = Camera.Position.X;
            Shader.Point.Position.Y = Camera.Position.Y;
            Shader.Point.Position.Z = Camera.Position.Z - 0.4f;
        }

        /// <summary>
        /// Used to update the state of the camera via the state of the 
        /// keyboard.
        /// </summary>
        /// <param name="time"></param>
        private void KeyboardUpdate(double time)
        {
            OpenTK.Input.KeyboardState state = OpenTK.Input.Keyboard.GetState();

            if (state.IsKeyDown(OpenTK.Input.Key.Escape))
                Exit();

            // Camera Movement
            if (state.IsKeyDown(OpenTK.Input.Key.ShiftLeft))
                Camera.CurrentSpeed = Camera.RunSpeed;

            else if (state.IsKeyDown(OpenTK.Input.Key.ControlLeft))
                Camera.CurrentSpeed = Camera.FlySpeed;

            else
                Camera.CurrentSpeed = Camera.WalkSpeed;

            if (state.IsKeyDown(OpenTK.Input.Key.PageUp))
                Camera.Position.Y += Camera.CurrentSpeed * (float)time;

            if (state.IsKeyDown(OpenTK.Input.Key.PageDown))
                Camera.Position.Y -= Camera.CurrentSpeed * (float)time;
            // Camera Movement

            // Camera Position
            if (state.IsKeyDown(OpenTK.Input.Key.W))
            {
                Camera.Position.X += (float)Math.Cos(Camera.Yaw) * Camera.CurrentSpeed * (float)time;
                Camera.Position.Z += (float)Math.Sin(Camera.Yaw) * Camera.CurrentSpeed * (float)time;
            }

            if (state.IsKeyDown(OpenTK.Input.Key.S))
            {
                Camera.Position.X -= (float)Math.Cos(Camera.Yaw) * Camera.CurrentSpeed * (float)time;
                Camera.Position.Z -= (float)Math.Sin(Camera.Yaw) * Camera.CurrentSpeed * (float)time;
            }

            if (state.IsKeyDown(OpenTK.Input.Key.A))
            {
                Camera.Position.X -= (float)Math.Cos(Camera.Yaw + Math.PI / 2) * Camera.CurrentSpeed * (float)time;
                Camera.Position.Z -= (float)Math.Sin(Camera.Yaw + Math.PI / 2) * Camera.CurrentSpeed * (float)time;
            }

            if (state.IsKeyDown(OpenTK.Input.Key.D))
            {
                Camera.Position.X += (float)Math.Cos(Camera.Yaw + Math.PI / 2) * Camera.CurrentSpeed * (float)time;
                Camera.Position.Z += (float)Math.Sin(Camera.Yaw + Math.PI / 2) * Camera.CurrentSpeed * (float)time;
            }
            // Camera Position

            // Camera Orientation
            if (state.IsKeyDown(OpenTK.Input.Key.Left))
                Camera.Yaw -= 1.0f * (float)time;

            if (state.IsKeyDown(OpenTK.Input.Key.Right))
                Camera.Yaw += 1.0f * (float)time;

            if (state.IsKeyDown(OpenTK.Input.Key.Up))
                Camera.Pitch += 1.0f * (float)time;

            if (state.IsKeyDown(OpenTK.Input.Key.Down))
                Camera.Pitch -= 1.0f * (float)time;
            // Camera Orientation
        }

        /// <summary>
        /// Used to update the state of the camera via the state of the joystick.
        /// </summary>
        /// <param name="time"></param>
        private void JoystickUpdate(double time)
        {
            OpenTK.Input.JoystickState state = OpenTK.Input.Joystick.GetState(0);

            // Camera Movement
            if (state.GetButton(OpenTK.Input.JoystickButton.Button4) == OpenTK.Input.ButtonState.Pressed)
                Camera.CurrentSpeed = Camera.RunSpeed;

            else if (state.GetButton(OpenTK.Input.JoystickButton.Button6) == OpenTK.Input.ButtonState.Pressed)
                Camera.CurrentSpeed = Camera.FlySpeed;

            else
                Camera.CurrentSpeed = Camera.WalkSpeed;
            // Camera Movement

            // Camera Position
            if (state.GetButton(OpenTK.Input.JoystickButton.Button5) == OpenTK.Input.ButtonState.Pressed)
                Camera.Position.Y += Camera.CurrentSpeed * (float)time;

            if (state.GetButton(OpenTK.Input.JoystickButton.Button7) == OpenTK.Input.ButtonState.Pressed)
                Camera.Position.Y -= Camera.CurrentSpeed * (float)time;

            double x1 = state.GetAxis(OpenTK.Input.JoystickAxis.Axis0);
            double x2 = state.GetAxis(OpenTK.Input.JoystickAxis.Axis1);

            double y1 = state.GetAxis(OpenTK.Input.JoystickAxis.Axis2);
            double y2 = state.GetAxis(OpenTK.Input.JoystickAxis.Axis3);

            Camera.Position.X += (float)(Math.Cos(Camera.Yaw) * x2 * Camera.CurrentSpeed * time);
            Camera.Position.Z += (float)(Math.Sin(Camera.Yaw) * x2 * Camera.CurrentSpeed * time);

            Camera.Position.X += (float)(Math.Cos(Camera.Yaw + Math.PI / 2) * x1 * Camera.CurrentSpeed * time);
            Camera.Position.Z += (float)(Math.Sin(Camera.Yaw + Math.PI / 2) * x1 * Camera.CurrentSpeed * time);
            // Camera Position

            // Camera Orientation
            Camera.Yaw += (float)(y1 * time);
            Camera.Pitch += (float)(y2 * time);
            // Camera Orientation
        }

        /// <summary>
        /// Default view of the camera at the start of the fly-through.
        /// </summary>
        private void UpdateView()
        {
            for (int i = 0; i < Objects.Count; i++)
                Objects[i].BufferData.ModelViewMatrix = Matrix4.CreateTranslation(0f, 0f, 0f) *
                                                        Matrix4.LookAt(Camera.Position, Camera.Position + new Vector3((float)Math.Cos(Camera.Yaw),
                                                                        Camera.Pitch, (float)Math.Sin(Camera.Yaw)),
                                                                        new Vector3(0f, 1f, 0f));
        }

        /// <summary>
        /// This is used to save each frame produced into an array of bitmaps.
        /// Once a limit has been reached a worker thread is started to save
        /// the images in the background with the lowest priority.
        /// </summary>
        private void RecordVideoFrame()
        {
            if (OpenTK.Graphics.GraphicsContext.CurrentContext == null)
                throw new OpenTK.Graphics.GraphicsContextMissingException();

            VideoFrame = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            ImageData = VideoFrame.LockBits(this.ClientRectangle,
                                            System.Drawing.Imaging.ImageLockMode.WriteOnly,
                                            System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Bgr, PixelType.UnsignedByte, ImageData.Scan0);
            VideoFrame.UnlockBits(ImageData);

            VideoFrames.Add(VideoFrame);
            if (VideoFrames.Count >= 50 && IsSavingFrames == false)
            {
                oThread = new Thread(this.SaveFrames);
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
        private void SaveFrames()
        {
            while (VideoFrames.Count > 0)
            {
                VideoFrames[0].RotateFlip(RotateFlipType.RotateNoneFlipY);
                VideoFrames[0].Save(ImagePath + @"frame" + (FrameNumber++) + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                VideoFrames[0].Dispose();

                VideoFrames.RemoveAt(0);
            }

            IsSavingFrames = false;

            if (FrameNumber >= MaxFrames)
                Exit();
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// This holds all the initializations that need to take place.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            System.Windows.Forms.Cursor.Hide();

            Title = "3D model viewer (Fly through)";

            WindowBorder = WindowBorder.Hidden;
            WindowState = WindowState.Fullscreen;
            Visible = true;

            GL.ClearColor(Color.Bisque);
            GL.Enable(EnableCap.AlphaTest);
            GL.Enable(EnableCap.Blend);
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
        /// Operations done once the fly-through is aborted.
        /// The joining of the threads for saving images is
        /// done here.
        /// </summary>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            System.Windows.Forms.Cursor.Show();

            if(oThread != null)
                oThread.Join();
            
            SaveFrames();
            Objects[0].Clear();
            Objects.Clear();
        }

        /// <summary>
        /// Update the direction the face is facing.
        /// </summary>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            UpdateCamera(e.Time);
        }

        /// <summary>
        /// Actions taken to render a frame.
        /// </summary>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            UpdateView();

            for (int i = 0; i < Objects.Count; ++i)
                Shader.Draw(Objects[i]);

            RecordVideoFrame();
            SwapBuffers();
        }

        /// <summary>
        /// Sets the perspective field of view when ever a frame is rendered.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);
            Shader.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)MathHelper.PiOver4, 
                                                                            Width / (float)Height,
                                                                            0.1f,
                                                                            10000.0f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref Shader.ProjectionMatrix);
        }

        /// <summary>
        /// Used to update the orientation of the camera via the movement of the mouse.
        /// </summary>
        protected override void OnMouseMove(OpenTK.Input.MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            Camera.Yaw += e.XDelta / 500.0f;
            Camera.Pitch -= e.YDelta / 500.0f;
        }
        #endregion
        #endregion
    }
}
