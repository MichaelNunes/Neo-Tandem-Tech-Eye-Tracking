namespace DisplayModel
{
    public struct ID
    {
        /// <summary>
        /// Pointer to the current shader program.
        /// </summary>
        public int Program { get; set; }

        /// <summary>
        /// Pointer to the vertex shader.
        /// </summary>
        public int VertexShader { get; set; }

        /// <summary>
        /// Pointer to the fragment shader.
        /// </summary>
        public int FragmentShader { get; set; }
    }
}
