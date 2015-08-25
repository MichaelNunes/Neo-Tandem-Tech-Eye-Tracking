namespace DisplayModel
{
    public struct Uniform
    {
        /// <summary>
        /// Pointer to the model view matrix uniform attribute in the shader.
        /// </summary>
        public int ModelViewMatrix { get; set; }

        /// <summary>
        /// Pointer to the projection matrix uniform attribute in the shader.
        /// </summary>
        public int ProjectionMatrix { get; set; }

        /// <summary>
        /// Pointer to the normal matrix uniform attribute in the shader.
        /// </summary>
        public int NormalMatrix { get; set; }

        /// <summary>
        /// Pointer to the use texture uniform attribute in the shader.
        /// </summary>
        public int UseTexture { get; set; }

        /// <summary>
        /// Pointer to the use lighting uniform attribute in the shader.
        /// </summary>
        public int UseLighting { get; set; }

        /// <summary>
        /// Pointer to the ambient light colour uniform attribute in the shader.
        /// </summary>
        public int AmbientLightColour { get; set; }

        /// <summary>
        /// Pointer to the ambient light colour uniform attribute in the shader.
        /// </summary>
        public int DirectionalLightColour { get; set; }

        /// <summary>
        /// Pointer to the directional light direction uniform attribute in the shader.
        /// </summary>
        public int DirectionalLightDirection { get; set; }

        /// <summary>
        /// Pointer to the point light diffuse colour uniform attribute in the shader.
        /// </summary>
        public int PointLightDiffuseColour { get; set; }

        /// <summary>
        /// Pointer to the point light specular colour uniform attribute in the shader.
        /// </summary>
        public int PointLightSpecularColour { get; set; }

        /// <summary>
        /// Pointer to the point light shininess uniform attribute in the shader.
        /// </summary>
        public int PointLightShininess { get; set; }

        /// <summary>
        /// Pointer to the point light position uniform attribute in the shader.
        /// </summary>
        public int PointLightPosition { get; set; }

        /// <summary>
        /// Pointer to the texture sampler uniform attribute in the shader.
        /// </summary>
        public int Sampler { get; set; }
    }
}
