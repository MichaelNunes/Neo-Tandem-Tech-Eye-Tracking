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

        public Window() : base(720, 405)
        {
            shaderData = new Shader();
            objects = new List<GameObject>();
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

            GL.ClearColor(Color.CornflowerBlue);
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
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            shaderData.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)MathHelper.PiOver4, Width / (float)Height, 1.0f, 100.0f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref shaderData.ProjectionMatrix);
        }
    }
}
