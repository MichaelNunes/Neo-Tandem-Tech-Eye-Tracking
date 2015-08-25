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

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace DisplayModel
{
    public struct Shader
    {
        #region Fields
        private ID id;
        private Attribute attribute;
        private Uniform uniform;
        private Buffer buffer;
        #endregion

        private float degrees;

        #region Matrices
        private Matrix4 ModelViewMatrix;
        public Matrix4 ProjectionMatrix;
        private Matrix3 NormalMatrix;
        #endregion
        
        #region Setup
        /// <summary>
        /// Initializes the Shader Program by collecting all ids of the both the vertex and fragment shaders.
        /// </summary>
        public void initProgram()
        {
            degrees = 0.0f;

            // PROGRAM
            id.Program = GL.CreateProgram();

            // SHADERS
            id.VertexShader = loadShader("Shaders/VertexShader.glsl", ShaderType.VertexShader, id.Program);
            id.FragmentShader = loadShader("Shaders/FragmentShader.glsl", ShaderType.FragmentShader, id.Program);
            GL.LinkProgram(id.Program);

            // ATTRIBUTES
            attribute.VertexPosition = GL.GetAttribLocation(id.Program, "aVertexPosition");
            attribute.VertexNormal = GL.GetAttribLocation(id.Program, "aVertexNormal");
            attribute.VertexColour = GL.GetAttribLocation(id.Program, "aVertexColour");
            attribute.VertexTexture = GL.GetAttribLocation(id.Program, "aVertexTexture");

            // UNIFORMS
            uniform.ProjectionMatrix = GL.GetUniformLocation(id.Program, "uProjectionMatrix");
            uniform.ModelViewMatrix = GL.GetUniformLocation(id.Program, "uModelViewMatrix");
            uniform.NormalMatrix = GL.GetUniformLocation(id.Program, "uNormalMatrix");

            uniform.UseLighting = GL.GetUniformLocation(id.Program, "uUseLighting");
            uniform.UseTexture = GL.GetUniformLocation(id.Program, "uUseTexture");

            uniform.AmbientLightColour = GL.GetUniformLocation(id.Program, "uAmbientLight_Colour");
            uniform.DirectionalLightColour = GL.GetUniformLocation(id.Program, "uDirectionalLight_Colour");
            uniform.DirectionalLightDirection = GL.GetUniformLocation(id.Program, "uDirectionalLight_Direction");
            uniform.PointLightDiffuseColour = GL.GetUniformLocation(id.Program, "uPointLight_DiffuseColour");
            uniform.PointLightSpecularColour = GL.GetUniformLocation(id.Program, "uPointLight_SpecularColour");
            uniform.PointLightPosition = GL.GetUniformLocation(id.Program, "uPointLight_Position");
            uniform.PointLightShininess = GL.GetUniformLocation(id.Program, "uPointLight_Shininess");
            uniform.Sampler = GL.GetUniformLocation(id.Program, "uSampler");


            //GENERATING BUFFERS
            buffer.Position = GL.GenBuffer();
            buffer.Normal = GL.GenBuffer();
            buffer.Colour = GL.GenBuffer();
            buffer.Texture = GL.GenBuffer();
            buffer.Index = GL.GenBuffer();

            AmbientLight_Colour = new Vector3(0.0f, 0.0f, 0.0f);
            DirectionalLight_Colour = new Vector3(1f, 1f, 1f);
            DirectionalLight_Direction = new Vector3(0.0f, 0.0f, 0.0f);
            PointLight_DiffuseColour = new Vector3(1.0f, 1.0f, 1.0f);
            PointLight_SpecularColour = new Vector3(0.0f, 0.0f, 0.0f);
            PointLight_Shininess = 250f;
            PointLight_Position = new Vector3(0.0f, 0.0f, -5.0f);
        }

        /// <summary>
        /// Retrieves a pointer to the appropriate shader in memory.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="type"></param>
        /// <param name="program"></param>
        /// <returns>address</returns>
        private int loadShader(String filename, ShaderType type, int program)
        {
            int address = GL.CreateShader(type);

            using (StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));

            return address;
        }
        #endregion

        #region Model Rendering
        /// <summary>
        /// Draws a 3d model object to the screen.
        /// </summary>
        /// <param name="bufferData"> The buffer data of the object to be drawn. </param>
        public void Draw(GameObject gameobject)
        {
            //print(bufferData);
            float radians = OpenTK.MathHelper.DegreesToRadians(degrees++);

            ModelViewMatrix = gameobject.bufferData.ModelViewMatrix;

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref ModelViewMatrix);

            BindBuffers(ref gameobject);
            SetUniforms(ref gameobject.bufferData);

            GL.DrawArrays(PrimitiveType.Triangles, 0, gameobject.bufferData.Vertex.Length);

            GL.DisableVertexAttribArray(attribute.VertexPosition);
            GL.DisableVertexAttribArray(attribute.VertexNormal);
            GL.DisableVertexAttribArray(attribute.VertexColour);
            GL.Flush();
        }

        private void BindBuffers(ref GameObject gameobject)
        {
            BufferData bufferData = gameobject.BufferData;

            //Vertices
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer.Position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(bufferData.Vertex.Length * Vector3.SizeInBytes), bufferData.Vertex, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(attribute.VertexPosition);
            GL.VertexAttribPointer(attribute.VertexPosition, 3, VertexAttribPointerType.Float, false, 0, 0);

            //Normals
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer.Normal);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(bufferData.Normal.Length * Vector3.SizeInBytes), bufferData.Normal, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(attribute.VertexNormal);
            GL.VertexAttribPointer(attribute.VertexNormal, 3, VertexAttribPointerType.Float, true, 0, 0);

            //Colors
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer.Colour);
            GL.BufferData<Vector4>(BufferTarget.ArrayBuffer, (IntPtr)(bufferData.Colour.Length * Vector4.SizeInBytes), bufferData.Colour, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(attribute.VertexColour);
            GL.VertexAttribPointer(attribute.VertexColour, 4, VertexAttribPointerType.Float, true, 0, 0);

            if (gameobject.Material.TextureId != -1)
            {
                //Texture
                GL.BindBuffer(BufferTarget.ArrayBuffer, buffer.Texture);
                GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(bufferData.Texture.Length * Vector2.SizeInBytes), bufferData.Texture, BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(attribute.VertexTexture);
                GL.VertexAttribPointer(attribute.VertexTexture, 2, VertexAttribPointerType.Float, true, 0, 0);

                GL.ActiveTexture(TextureUnit.Texture0);

            }

            GL.UseProgram(id.Program);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        }

        private void SetUniforms(ref BufferData bufferData)
        {
            GL.Uniform1(uniform.UseLighting, 1);
            GL.Uniform1(uniform.UseTexture, 1);
            GL.Uniform1(uniform.Sampler, 0);

            GL.Uniform3(uniform.AmbientLightColour, AmbientLight_Colour);

            GL.Uniform3(uniform.DirectionalLightColour, DirectionalLight_Colour);
            GL.Uniform3(uniform.DirectionalLightDirection, DirectionalLight_Direction);

            GL.Uniform3(uniform.PointLightDiffuseColour, PointLight_DiffuseColour);
            GL.Uniform3(uniform.PointLightSpecularColour, PointLight_SpecularColour);
            GL.Uniform1(uniform.PointLightShininess, PointLight_Shininess);
            GL.Uniform3(uniform.PointLightPosition, PointLight_Position);

            NormalMatrix = Matrix3.Transpose(new Matrix3(Matrix4.Invert(ModelViewMatrix)));

            //Set matrix uniforms
            GL.UniformMatrix3(uniform.NormalMatrix, false, ref NormalMatrix);
            GL.UniformMatrix4(uniform.ModelViewMatrix, false, ref ModelViewMatrix);
            GL.UniformMatrix4(uniform.ProjectionMatrix, false, ref ProjectionMatrix);

        }
        #endregion

        #region Lighting Attributes
        public Vector3 AmbientLight_Colour
        { get; set; }

        public Vector3 DirectionalLight_Colour
        { get; set; }

        public Vector3 DirectionalLight_Direction
        { get; set; }

        public Vector3 PointLight_DiffuseColour
        { get; set; }

        public Vector3 PointLight_SpecularColour
        { get; set; }

        public float PointLight_Shininess
        { get; set; }

        public Vector3 PointLight_Position
        { get; set; }
        #endregion
    }
}