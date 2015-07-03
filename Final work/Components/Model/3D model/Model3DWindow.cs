using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DisplayModel
{
    public class Model3DWindow : GameWindow
    {
        private ShaderData shaderData;
        private List<BufferData> objects;

        public Model3DWindow()
        {
            shaderData = new ShaderData();
            objects = new List<BufferData>();
        }

        public void add( BufferData bufferData )
        {
            if (!objects.Contains(bufferData))
                objects.Add( bufferData );
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            shaderData.initProgram();

            Title = "Hello OpenTK!";

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
                shaderData.Draw(objects[i]);
            
            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            Matrix4 PMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 1000.0f);

            GL.MatrixMode(MatrixMode.Projection);

            GL.LoadMatrix(ref PMatrix);
        }

        public BufferData this[int index]
        {
            get
            {
                if(index < 0 || index > objects.Count)
                    throw new IndexOutOfRangeException("You tried to access this vector at index: " + index);
                return objects[index];
            }
            set
            {
                if (index < 0 || index > objects.Count)
                    throw new IndexOutOfRangeException("You tried to access this vector at index: " + index);
                objects[index] = value;
            }
        }

        public int Length
        {
            get { return objects.Count; }
        }
    }
}
