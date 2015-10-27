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
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
#endregion

namespace DisplayModel
{
    public class Shader
    {
        #region Fields
        #region Shader Fields
        private ID ID;
        private Attribute Attribute;
        private Uniform Uniform;
        #endregion

        #region Matrices
        public Matrix4 ProjectionMatrix;
        private Matrix4 ModelViewMatrix;
        private Matrix3 NormalMatrix;
        #endregion

        #region Lights
        public AmbientLight Ambient;
        public DirectionalLight Directional;
        public PointLight Point;
        #endregion
        #endregion

        #region Initialization
        /// <summary>
        /// Defaulting the lights.
        /// </summary>
        public Shader()
        {
            Color4 White = Color4.White;
            Color4 Yellow = Color4.Yellow;
            White.A = 0.3f;
            Yellow.A = 0.3f;

            Ambient = new AmbientLight(White);
            Directional = new DirectionalLight(White, Vector3.Zero);
            Point = new PointLight(Color4.White, Yellow, Vector3.Zero, 255.0f);
        }

        /// <summary>
        /// Gets the ID, Attribute, and Uniform pointers for the shader.
        /// </summary>
        public void InitProgram()
        {
            Console.WriteLine("Initializing Shader...");

            // Program
            ID.Program = GL.CreateProgram();
            // Program

            // Shaders
            ID.VertexShader = loadShader("Shaders/VertexShader.glsl", ShaderType.VertexShader, ID.Program);
            ID.FragmentShader = loadShader("Shaders/FragmentShader.glsl", ShaderType.FragmentShader, ID.Program);
            GL.LinkProgram(ID.Program);
            // Shaders

            // Attributes
            Attribute.VertexPosition = GL.GetAttribLocation(ID.Program, "aVertexPosition");
            Attribute.VertexNormal = GL.GetAttribLocation(ID.Program, "aVertexNormal");
            Attribute.VertexColour = GL.GetAttribLocation(ID.Program, "aVertexColour");
            Attribute.VertexTexture = GL.GetAttribLocation(ID.Program, "aVertexTexture");
            // Attributes

            // Uniforms
            Uniform.ProjectionMatrix = GL.GetUniformLocation(ID.Program, "uProjectionMatrix");
            Uniform.ModelViewMatrix = GL.GetUniformLocation(ID.Program, "uModelViewMatrix");
            Uniform.NormalMatrix = GL.GetUniformLocation(ID.Program, "uNormalMatrix");

            Uniform.UseLighting = GL.GetUniformLocation(ID.Program, "uUseLighting");
            Uniform.UseTexture = GL.GetUniformLocation(ID.Program, "uUseTexture");

            Uniform.AmbientLightColour = GL.GetUniformLocation(ID.Program, "uAmbientLight_Colour");
            Uniform.DirectionalLightColour = GL.GetUniformLocation(ID.Program, "uDirectionalLight_Colour");
            Uniform.DirectionalLightDirection = GL.GetUniformLocation(ID.Program, "uDirectionalLight_Direction");
            Uniform.PointLightDiffuseColour = GL.GetUniformLocation(ID.Program, "uPointLight_DiffuseColour");
            Uniform.PointLightSpecularColour = GL.GetUniformLocation(ID.Program, "uPointLight_SpecularColour");
            Uniform.PointLightPosition = GL.GetUniformLocation(ID.Program, "uPointLight_Position");
            Uniform.PointLightShininess = GL.GetUniformLocation(ID.Program, "uPointLight_Shininess");
            Uniform.Sampler = GL.GetUniformLocation(ID.Program, "uSampler");
            Uniform.Alpha = GL.GetUniformLocation(ID.Program, "uAlpha");
            // Uniforms
        }

        /// <summary>
        /// Compiles and retrieves a pointer to the appropriate shader in memory.
        /// </summary>
        /// <param name="filename"> File path to the shader to be compiled. </param>
        /// <param name="type"> The type of shader we are compiling. </param>
        /// <param name="program"> Id of the program using the shader. </param>
        /// <returns> The address of the compiled shader. </returns>
        private int loadShader(String filename, ShaderType type, int program)
        {
            int address = GL.CreateShader(type);

            using (StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }

            GL.CompileShader(address);
            GL.AttachShader(program, address);

            return address;
        }
        #endregion

        #region Model Rendering
        /// <summary>
        /// Draws a 3d model object to the screen.
        /// </summary>
        /// <param name="bufferData"> The buffer data of the object to be drawn. </param>
        public void Draw(GameObject gameObject)
        {
            Initailize(ref gameObject);
            BindBuffers(ref gameObject);
            SetUniforms(ref gameObject);
            Render(ref gameObject);
        }

        /// <summary>
        /// Sets up the attributes and model view matrix to render the
        /// object.
        /// </summary>
        /// <param name="gameObject"> The object currently being rendered. </param>
        private void Initailize(ref GameObject gameObject)
        {
            ModelViewMatrix = gameObject.BufferData.ModelViewMatrix;

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref ModelViewMatrix);
        }

        /// <summary>
        /// Generates the triangles that make up the object in the window.
        /// </summary>
        /// <param name="gameObject"> The object currently being rendered. </param>
        private void Render(ref GameObject gameObject)
        {
            GL.DrawArrays(PrimitiveType.Triangles, 0, gameObject.BufferData.Vertex.Length);

            GL.DisableVertexAttribArray(Attribute.VertexPosition);
            GL.DisableVertexAttribArray(Attribute.VertexNormal);
            GL.DisableVertexAttribArray(Attribute.VertexColour);
            GL.DisableVertexAttribArray(Attribute.VertexTexture);

            GL.Flush();
        }
        
        /// <summary>
        /// Bind the object's attributes to the appropriate buffers.
        /// </summary>
        /// <param name="gameObject"> The object currently being rendered. </param>
        private void BindBuffers(ref GameObject gameObject)
        {
            BufferData buffer_data = gameObject.BufferData;

            // Binding the vertices
            GL.EnableVertexAttribArray(Attribute.VertexPosition);
            GL.BindBuffer(BufferTarget.ArrayBuffer, gameObject.Buffer.Position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(buffer_data.Vertex.Length * Vector3.SizeInBytes), buffer_data.Vertex, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(Attribute.VertexPosition, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            // Binding the vertices

            if (gameObject.BufferData.Texture.Length > 0)
            {
                //Binding the textures
                GL.EnableVertexAttribArray(Attribute.VertexTexture);
                GL.BindBuffer(BufferTarget.ArrayBuffer, gameObject.Buffer.Texture);
                GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(buffer_data.Texture.Length * Vector2.SizeInBytes), buffer_data.Texture, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(Attribute.VertexTexture, 2, VertexAttribPointerType.Float, true, 0, 0);

                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, gameObject.Material.TextureID);

                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                //Binding the textures
            }

            if (gameObject.BufferData.Normal.Length > 0)
            {
                //Binding the normals
                GL.EnableVertexAttribArray(Attribute.VertexNormal);
                GL.BindBuffer(BufferTarget.ArrayBuffer, gameObject.Buffer.Normal);
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(buffer_data.Normal.Length * Vector3.SizeInBytes), buffer_data.Normal, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(Attribute.VertexNormal, 3, VertexAttribPointerType.Float, true, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                //Binding the normals
            }

            //Binding the colours
            GL.EnableVertexAttribArray(Attribute.VertexColour);
            GL.BindBuffer(BufferTarget.ArrayBuffer, gameObject.Buffer.Colour);
            GL.BufferData<Vector4>(BufferTarget.ArrayBuffer, (IntPtr)(buffer_data.Colour.Length * Vector4.SizeInBytes), buffer_data.Colour, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(Attribute.VertexColour, 4, VertexAttribPointerType.Float, true, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //Binding the colours

            GL.UseProgram(ID.Program);
        }

        /// <summary>
        /// Assigns the uniform values of the shader that allow settings such
        /// as: lighting, and the use of textures.
        /// </summary>
        /// <param name="gameObject"> The object currently being rendered. </param>
        private void SetUniforms(ref GameObject gameObject)
        {
            //The basic boolean uniforms
            GL.Uniform1(Uniform.UseLighting, 1);
            GL.Uniform1(Uniform.UseTexture, (gameObject.Material.TextureID > -1 ? 1 : 0));
            GL.Uniform1(Uniform.Sampler, 0);
            //The basic boolean uniforms

            //The lighting uniforms
            Ambient.AddLight(Uniform.AmbientLightColour);
            Directional.AddLight(Uniform.DirectionalLightColour, Uniform.DirectionalLightDirection);
            Point.AddLight(Uniform.PointLightDiffuseColour, Uniform.PointLightSpecularColour, Uniform.PointLightPosition, Uniform.PointLightShininess);

            GL.Uniform3(Uniform.PointLightSpecularColour, gameObject.Material.Specular);
            GL.Uniform1(Uniform.Alpha, gameObject.Material.Alpha);
            //The lighting uniforms


            //The matrix uniforms
            NormalMatrix = Matrix3.Transpose(new Matrix3(Matrix4.Invert(ModelViewMatrix)));

            GL.UniformMatrix3(Uniform.NormalMatrix, false, ref NormalMatrix);
            GL.UniformMatrix4(Uniform.ModelViewMatrix, false, ref ModelViewMatrix);
            GL.UniformMatrix4(Uniform.ProjectionMatrix, false, ref ProjectionMatrix);
            //The matrix uniforms
        }
        #endregion
    }
}