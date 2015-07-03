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

using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DisplayModel
{
    public struct ShaderData
    {
        #region IDs
        private int ProgramId;
        private int VertexShaderId;
        private int FragmentShaderId;
        #endregion

        #region Shader Attributes
        private int aVertexPosition;
        private int aVertexNormal;
        private int aVertexColour;
        private int aVertexTexture;
        #endregion

        #region Shader Uniforms
        private int uProjectionMatrix;
        private int uModelViewMatrix;
        private int uNormalMatrix;

        private int uAmbientLight_Color;
        private int uDirectionalLight_Color;
        private int uDirectionalLight_Direction;
        private int uPointLight_DiffuseColor;
        private int uPointLight_SpecularColor;
        private int uPointLight_Position;
        private int uPointLight_Shininess;
        private int uSampler;
        #endregion

        #region Buffers
        private int positionBuffer;
        private int colourBuffer;
        private int modelViewBuffer;
        private int projectionBuffer;

        #endregion

        private float degrees;
        #region Matrices
        private Matrix4 ModelViewMatrix;
        private Matrix4 ProjectionMatrix;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void initProgram()
        {
            degrees = 0.0f;
            ProgramId = GL.CreateProgram();

            // SHADERS
            loadShader("Shaders/VertexShader.glsl", ShaderType.VertexShader, ProgramId, out VertexShaderId);
            loadShader("Shaders/FragmentShader.glsl", ShaderType.FragmentShader, ProgramId, out FragmentShaderId);
            GL.LinkProgram(ProgramId);

            // ATTRIBUTES
            aVertexPosition = GL.GetAttribLocation(ProgramId, "aVertexPosition");
            aVertexNormal = GL.GetAttribLocation(ProgramId, "aVertexNormal");
            aVertexColour = GL.GetAttribLocation(ProgramId, "aVertexColour");
            aVertexTexture = GL.GetAttribLocation(ProgramId, "aVertexTexture");

            // UNIFORMS
            uProjectionMatrix = GL.GetUniformLocation(ProgramId, "uProjectionMatrix");
            uModelViewMatrix = GL.GetUniformLocation(ProgramId, "uModelViewMatrix");
            uNormalMatrix = GL.GetUniformLocation(ProgramId, "uNormalMatrix");

            uAmbientLight_Color = GL.GetUniformLocation(ProgramId, "uAmbientLight_Color");
            uDirectionalLight_Color = GL.GetUniformLocation(ProgramId, "uDirectionalLight_Color");
            uDirectionalLight_Direction = GL.GetUniformLocation(ProgramId, "uDirectionalLight_Direction");
            uPointLight_DiffuseColor = GL.GetUniformLocation(ProgramId, "uPointLight_DiffuseColor");
            uPointLight_SpecularColor = GL.GetUniformLocation(ProgramId, "uPointLight_SpecularColor");
            uPointLight_Position = GL.GetUniformLocation(ProgramId, "uPointLight_Position");
            uPointLight_Shininess = GL.GetUniformLocation(ProgramId, "uPointLight_Shininess");
            uSampler = GL.GetUniformLocation(ProgramId, "uSampler");

            if (aVertexPosition == -1 || aVertexNormal == -1 || aVertexColour == -1 || aVertexTexture == -1)
            {
                Console.WriteLine("Error binding attributes");
                Console.WriteLine
                (
                    "Attributes: {0} {1} {2} {3}",
                    aVertexPosition,
                    aVertexNormal,
                    aVertexColour,
                    aVertexTexture
                );
                Console.WriteLine
                (
                    "Uniforms: {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}",
                    uProjectionMatrix,
                    uModelViewMatrix,
                    uNormalMatrix,
                    uAmbientLight_Color,
                    uDirectionalLight_Color,
                    uDirectionalLight_Direction,
                    uPointLight_DiffuseColor,
                    uPointLight_SpecularColor,
                    uPointLight_Position,
                    uPointLight_Shininess,
                    uSampler
                );
            }

            //GENERATING BUFFERS
            GL.GenBuffers(1, out positionBuffer);
            GL.GenBuffers(1, out colourBuffer);
            GL.GenBuffers(1, out modelViewBuffer);
            GL.GenBuffers(1, out projectionBuffer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="type"></param>
        /// <param name="program"></param>
        /// <param name="address"></param>
        private void loadShader(String filename, ShaderType type, int program, out int address)
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

        #region Model Rendering
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bufferData"></param>
        public void Draw(BufferData bufferData)
        {
            //print(bufferData);
            float radians = OpenTK.MathHelper.DegreesToRadians(degrees++);

            ModelViewMatrix = bufferData.ModelMatrix * Matrix4.CreateScale(0.5f, 0.5f, 0.5f) * Matrix4.CreateRotationY(radians) * Matrix4.CreateRotationX(0) * Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref ModelViewMatrix);

            //Vertices
            GL.BindBuffer(BufferTarget.ArrayBuffer, positionBuffer);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(bufferData.Vertex.Length * Vector3.SizeInBytes), bufferData.Vertex, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(aVertexPosition, 3, VertexAttribPointerType.Float, false, 0, 0);

            //Colors
            GL.BindBuffer(BufferTarget.ArrayBuffer, colourBuffer);
            GL.BufferData<Vector4>(BufferTarget.ArrayBuffer, (IntPtr)(bufferData.Colour.Length * Vector4.SizeInBytes), bufferData.Colour, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(aVertexColour, 4, VertexAttribPointerType.Float, true, 0, 0);

            GL.UseProgram(ProgramId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.EnableVertexAttribArray(aVertexPosition);
            GL.EnableVertexAttribArray(aVertexColour);

            //Set matrix uniforms
            GL.UniformMatrix4(uModelViewMatrix, false, ref ModelViewMatrix);
            GL.UniformMatrix4(uProjectionMatrix, false, ref ProjectionMatrix);

            GL.DrawArrays(PrimitiveType.Triangles, 0, bufferData.Vertex.Length);

            GL.DisableVertexAttribArray(aVertexPosition);
            GL.DisableVertexAttribArray(aVertexColour);
            GL.Flush();
        }
        #endregion
    }
}