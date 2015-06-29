using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace _3D_model
{
    class Model3d : GameWindow
    {
        //Objects
        List<CustomObject> customObjects = new List<CustomObject>();

        //Program
        int pgmID;

        //Shaders
        int vertexShaderID;
        int fragmentShaderID;

        //Shader variables and attributes
        int attribute_vertexColor;
        int attribute_vPosition;
        int uniform_mView;
        int uniform_pView;

        //addresses
        int vbo_position;
        int vbo_color;
        int vbo_mview;
        int vbo_pview;

        //Buffer data(included in DisplayObject)
        Vector3[] vertices;
        Vector3[] colors;
        Matrix4 MVMatrix;
        Matrix4 PMatrix;

        //Temp
        float degrees = 0;
        float radians = 0;

        void initProgram()
        {
            pgmID = GL.CreateProgram();

            loadShader("Shaders/VertexShader.glsl", ShaderType.VertexShader, pgmID, out vertexShaderID);
            loadShader("Shaders/FragmentShader.glsl", ShaderType.FragmentShader, pgmID, out fragmentShaderID);
            GL.LinkProgram(pgmID);
            Console.WriteLine(GL.GetProgramInfoLog(pgmID));

            attribute_vPosition = GL.GetAttribLocation(pgmID, "vPosition");
            attribute_vertexColor = GL.GetAttribLocation(pgmID, "vertexColor");
            uniform_mView = GL.GetUniformLocation(pgmID, "uMVMatrix");
            uniform_pView = GL.GetUniformLocation(pgmID, "uPMatrix");

            if (attribute_vPosition == -1 || attribute_vertexColor == -1 || uniform_mView == -1 || uniform_pView == -1)//uniform pview cause problem
            {
                Console.WriteLine("Error binding attributes");
            }

            GL.GenBuffers(1, out vbo_position);
            GL.GenBuffers(1, out vbo_color);
            GL.GenBuffers(1, out vbo_mview);
            GL.GenBuffers(1, out vbo_pview);
        }

        void loadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            initProgram();

            vertices = new Vector3[] { 
                new Vector3(-0.8f, -0.8f, 0f),
                new Vector3( 0.8f, -0.8f, 0f),
                new Vector3( 0f,  0.8f, 0f)
            };

            colors = new Vector3[] { 
                new Vector3(1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f,  1f, 0f)
            };

            customObjects.Add(new ModelFactory().FromOBJ("cube.obj"));
            //objects.Add(new CustomObject("C:/Users/COS301/Downloads/MobileNokiaC5.obj"));
            //objects.Add(new CustomObject());
            //objects[0].SetVerts(vertices);
            //objects[0].SetColors(colors);

            Title = "Hello OpenTK!";

            GL.ClearColor(Color.CornflowerBlue);
            GL.Enable(EnableCap.DepthTest);

            GL.PointSize(5f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            //Game Logic
            radians = OpenTK.MathHelper.DegreesToRadians(degrees++);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            DrawCustomObjects(customObjects);

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

        private void DrawCustomObject(CustomObject custom)
        {
            MVMatrix = custom.ModelMatrix * Matrix4.CreateScale(0.5f, 0.5f, 0.5f) * Matrix4.CreateRotationY(radians) * Matrix4.CreateRotationX(radians) * Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref MVMatrix);

            //Vertices
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(custom.GetVerts().Length * Vector3.SizeInBytes), custom.GetVerts(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vPosition, 3, VertexAttribPointerType.Float, false, 0, 0);

            //Colors
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_color);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(custom.GetColors().Length * Vector3.SizeInBytes), custom.GetColors(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vertexColor, 3, VertexAttribPointerType.Float, true, 0, 0);

            GL.UseProgram(pgmID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.EnableVertexAttribArray(attribute_vPosition);
            GL.EnableVertexAttribArray(attribute_vertexColor);

            //Set matrix uniforms
            GL.UniformMatrix4(uniform_mView, false, ref MVMatrix);
            GL.UniformMatrix4(uniform_pView, false, ref PMatrix);

            GL.DrawArrays(PrimitiveType.Triangles, 0, custom.GetVerts().Length);

            GL.DisableVertexAttribArray(attribute_vPosition);
            GL.DisableVertexAttribArray(attribute_vertexColor);
            GL.Flush();
        }

        private void DrawCustomObjects(List<CustomObject> customs)
        {
            for (int i = 0; i < customs.Count; ++i)
            {
                DrawCustomObject(customs[i]);
            }
        }

        public static void Main()
        {
            using (Model3d window = new Model3d())
            {
                window.Run(30, 30);
            }
        }
    }
}