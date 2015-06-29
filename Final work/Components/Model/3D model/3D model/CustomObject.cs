using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _3D_model
{
    class CustomObject : DisplayObject
    {
        public Vector3[] vertices;
        public Vector3[] colors;
        public Vector2[] textureCoords;
        public Vector3[] normals;

        public CustomObject()
        {

        }

        public void SetColor(float r, float g, float b)
        {
            colors = new Vector3[vertices.Length];
            for (int i = 0; i < colors.Length; ++i)
            {
                colors[i] = new Vector3(r, g, b);
            }
        }

        public Vector3[] GetVerts()
        {
            return vertices;
        }

        public Vector3[] GetColors()
        {
            return colors;
        }

        public Vector2[] GetTextureData()
        {
            return textureCoords;
        }

        public Vector3[] GetNormalData()
        {
            return normals;
        }

        public void SetVerts(Vector3[] v)
        {
            vertices = v;
        }
        public void SetColors(Vector3[] c)
        {
            colors = c;
        }

        public void SetTextureData(Vector2[] t)
        {
            textureCoords = t;
        }

        public void SetNormalData(Vector3[] n)
        {
            normals = n;
        }

        public override void CalculateModelMatrix()
        {

        }
    }
}
