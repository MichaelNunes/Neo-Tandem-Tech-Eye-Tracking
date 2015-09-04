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
            id.VertexShader     = loadShader("Shaders/VertexShader.glsl", ShaderType.VertexShader, id.Program);
            id.FragmentShader   = loadShader("Shaders/FragmentShader.glsl", ShaderType.FragmentShader, id.Program);
            GL.LinkProgram(id.Program);

            // ATTRIBUTES
            attribute.VertexPosition    = GL.GetAttribLocation(id.Program, "aVertexPosition");
            attribute.VertexNormal      = GL.GetAttribLocation(id.Program, "aVertexNormal");
            attribute.VertexColour      = GL.GetAttribLocation(id.Program, "aVertexColour");
            attribute.VertexTexture     = GL.GetAttribLocation(id.Program, "aVertexTexture");

            // UNIFORMS
            uniform.ProjectionMatrix    = GL.GetUniformLocation(id.Program, "uProjectionMatrix");
            uniform.ModelViewMatrix     = GL.GetUniformLocation(id.Program, "uModelViewMatrix");
            uniform.NormalMatrix        = GL.GetUniformLocation(id.Program, "uNormalMatrix");

            uniform.UseLighting = GL.GetUniformLocation(id.Program, "uUseLighting");
            uniform.UseTexture  = GL.GetUniformLocation(id.Program, "uUseTexture");

            uniform.AmbientLightColour          = GL.GetUniformLocation(id.Program, "uAmbientLight_Colour");
            uniform.DirectionalLightColour      = GL.GetUniformLocation(id.Program, "uDirectionalLight_Colour");
            uniform.DirectionalLightDirection   = GL.GetUniformLocation(id.Program, "uDirectionalLight_Direction");
            uniform.PointLightDiffuseColour     = GL.GetUniformLocation(id.Program, "uPointLight_DiffuseColour");
            uniform.PointLightSpecularColour    = GL.GetUniformLocation(id.Program, "uPointLight_SpecularColour");
            uniform.PointLightPosition          = GL.GetUniformLocation(id.Program, "uPointLight_Position");
            uniform.PointLightShininess         = GL.GetUniformLocation(id.Program, "uPointLight_Shininess");
            uniform.Sampler                     = GL.GetUniformLocation(id.Program, "uSampler");

            AmbientLight_Colour         = new Vector3(0.0f, 0.0f, 0.0f);
            DirectionalLight_Colour     = new Vector3(1.0f, 1.0f, 1.0f);
            DirectionalLight_Direction  = new Vector3(0.0f, 0.0f, 0.0f);
            PointLight_DiffuseColour    = new Vector3(1.0f, 1.0f, 1.0f);
            PointLight_SpecularColour   = new Vector3(0.0f, 0.0f, 0.0f);
            PointLight_Shininess        = 250.0f;
            PointLight_Position         = new Vector3(0.0f, 0.0f, -5.0f);
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
            Initailize(ref gameobject);
            BindBuffers(ref gameobject);
            SetUniforms(ref gameobject.bufferData);
            Finish(ref gameobject);
        }

        private void Initailize(ref GameObject gameobject)
        {
            float radians = OpenTK.MathHelper.DegreesToRadians(degrees++);

            ModelViewMatrix = gameobject.bufferData.ModelViewMatrix;

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref ModelViewMatrix);
        }

        private void Finish(ref GameObject gameobject)
        {
            GL.DrawElements(PrimitiveType.Triangles, gameobject.BufferData.Index.Length, DrawElementsType.UnsignedInt, 0);

            GL.DisableVertexAttribArray(attribute.VertexPosition);
            GL.DisableVertexAttribArray(attribute.VertexNormal);
            GL.DisableVertexAttribArray(attribute.VertexColour);
            GL.DisableVertexAttribArray(attribute.VertexTexture);

            GL.Flush();
        }

        private void BindBuffers(ref GameObject gameobject)
        {
            BufferData bufferData = gameobject.BufferData;

            // Binding the vertices
            GL.BindBuffer(BufferTarget.ArrayBuffer, gameobject.Buffer.Position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(bufferData.Vertex.Length * Vector3.SizeInBytes), bufferData.Vertex, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(attribute.VertexPosition);
            GL.VertexAttribPointer(attribute.VertexPosition, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            // Binding the vertices

            //Binding the normals
            GL.BindBuffer(BufferTarget.ArrayBuffer, gameobject.Buffer.Normal);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(bufferData.Normal.Length * Vector3.SizeInBytes), bufferData.Normal, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(attribute.VertexNormal);
            GL.VertexAttribPointer(attribute.VertexNormal, 3, VertexAttribPointerType.Float, true, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //Binding the normals

            //Binding the colors
            GL.BindBuffer(BufferTarget.ArrayBuffer, gameobject.Buffer.Colour);
            GL.BufferData<Vector4>(BufferTarget.ArrayBuffer, (IntPtr)(bufferData.Colour.Length * Vector4.SizeInBytes), bufferData.Colour, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(attribute.VertexColour);
            GL.VertexAttribPointer(attribute.VertexColour, 4, VertexAttribPointerType.Float, true, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //Binding the colors

            if (gameobject.Material.TextureId != -1)
            {
                //Binding the textures
                GL.BindBuffer(BufferTarget.ArrayBuffer, gameobject.Buffer.Texture);
                GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(bufferData.Texture.Length * Vector2.SizeInBytes), bufferData.Texture, BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(attribute.VertexTexture);
                GL.VertexAttribPointer(attribute.VertexTexture, 2, VertexAttribPointerType.Float, true, 0, 0);

                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                //Binding the textures
            }
            
            //Binding the indices
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, gameobject.Buffer.Index);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(bufferData.Index.Length * sizeof(int)), bufferData.Index, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //Binding the indices

            GL.UseProgram(id.Program);
        }

        private void SetUniforms(ref BufferData bufferData)
        {
            //The basic boolean uniforms
            GL.Uniform1(uniform.UseLighting, 1);
            GL.Uniform1(uniform.UseTexture, 0);
            GL.Uniform1(uniform.Sampler, 0);
            //The basic boolean uniforms

            //The lighting uniforms
            GL.Uniform3(uniform.AmbientLightColour, AmbientLight_Colour);

            GL.Uniform3(uniform.DirectionalLightColour, DirectionalLight_Colour);
            GL.Uniform3(uniform.DirectionalLightDirection, DirectionalLight_Direction);

            GL.Uniform3(uniform.PointLightDiffuseColour, PointLight_DiffuseColour);
            GL.Uniform3(uniform.PointLightSpecularColour, PointLight_SpecularColour);
            GL.Uniform1(uniform.PointLightShininess, PointLight_Shininess);
            GL.Uniform3(uniform.PointLightPosition, PointLight_Position);
            //The lighting uniforms


            //The matrix uniforms
            NormalMatrix = Matrix3.Transpose(new Matrix3(Matrix4.Invert(ModelViewMatrix)));

            GL.UniformMatrix3(uniform.NormalMatrix, false, ref NormalMatrix);
            GL.UniformMatrix4(uniform.ModelViewMatrix, false, ref ModelViewMatrix);
            GL.UniformMatrix4(uniform.ProjectionMatrix, false, ref ProjectionMatrix);
            //The matrix uniforms
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