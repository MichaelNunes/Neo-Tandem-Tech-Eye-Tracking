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
        protected Shader shaderData;
        protected List<GameObject> objects;
        protected string imagePath;

        //Camera feature(s)(make into class)
        protected Camera camera = new Camera();

        public Window(string _imagePath)
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
        }
        
        /// <summary>
        /// The current frame to be rendered.
        /// </summary>
        /// <param name="e"></param>
        /*protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            defaultView();
            for (int i = 0; i < objects.Count; ++i)
                shaderData.Draw(objects[i]);

            SwapBuffers();
        }*/

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

        /// <summary>
        /// This provides the default view with camera control enabled.
        /// </summary>
        public void defaultView()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].bufferData.ModelViewMatrix = Matrix4.Identity * Matrix4.CreateTranslation(0f, 0f, -4);
            }
        }
    }
}
