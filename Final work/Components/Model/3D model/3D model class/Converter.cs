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

#region Using Cluases
using OpenTK;
using OpenTK.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
#endregion

namespace DisplayModel
{
	/// <summary>
	/// Used to convert the input object files into a Model3D object.
	/// <summary>
    public static class Converter
    {
        #region List Items
        private static List<Vector3> VertexPoint, VertexNormal;
        private static List<Vector2> VertexUV;
        private static List<List<int>> TextureIndices;
        private static List<int> FacePoint, FaceUV, FaceNormal;
        private static Dictionary<string, string> MaterialTexture;
        private static List<string> MaterialName;

        private static int MaterialIndex;
        private static float ScaleFactor = 1.0f;
        private const float ScaleFactorBase = 2.0f;
        private const float VerticalOffset = 0.5f;
        #endregion

        #region Root Parsing
        /// <summary>
        /// Converts the object and material files into a Model object.
        /// </summary>
        /// <param name='obj'> The filepath to the object file. </param>
        /// <param name='scaling'> Whether we scale the objects or not. </param>
        public static GameObject fromOBJ(string obj, bool scaling)
        {
            GameObject root = new GameObject();

            // Getting path to material file and textures
            int index = 0;
            for (int i = 0; i < obj.Length; ++i)
                if (obj[i] == '\\')
                    index = i;

            string mtl = obj.Substring(0, index);
            string tex = obj.Substring(0, index) + '\\';
            // Getting path to material file and textures

            Init();
            ParseOBJ(obj, ref mtl);
            ParseMTL(mtl, tex);
            GenerateChildren(ref root);
            if (scaling) ScaleObject(ref root);
            Clear();


            Console.WriteLine("Generating materials...");
            root.Initialize();

            Console.WriteLine("Returning object...");
            return root;
        }
        #endregion

        #region Setup
        /// <summary>
        /// Intialises the lists use to parse the files.
        /// </summary>
        private static void Init()
        {
            VertexPoint = new List<Vector3>();
            VertexNormal = new List<Vector3>();

            VertexUV = new List<Vector2>();

            TextureIndices = new List<List<int>>();
            FacePoint = new List<int>();
            FaceUV = new List<int>();
            FaceNormal = new List<int>();

            MaterialTexture = new Dictionary<string, string>();

            MaterialName = new List<string>();

            MaterialIndex = 0;
            ScaleFactor = 1.0f;
        }

        /// <summary>
        /// Empties out the lists used to parse the files.
        /// </summary>
        private static void Clear()
        {
            VertexPoint.Clear();
            VertexPoint = null;
            VertexNormal.Clear();
            VertexNormal = null;
            VertexUV.Clear();
            VertexUV = null;

            TextureIndices.Clear();
            TextureIndices = null;
            FacePoint.Clear();
            FacePoint = null;
            FaceUV.Clear();
            FaceUV = null;
            FaceNormal.Clear();
            FaceNormal = null;

            MaterialTexture.Clear();
            MaterialTexture = null;

            MaterialName.Clear();
            MaterialName = null;

            MaterialIndex = 0;
            ScaleFactor = 1.0f;
        }
        #endregion

        #region OBJ Helper Methods
        /// <summary>
        /// Reads through the obj file to get the vertex points, 
        /// face indices, material name and, the path to the material file.
        /// </summary>
        /// <param name="obj"> Path to the object file. </param>
        /// <param name="mtl"> Path to the material file. </param>
        private static void ParseOBJ(string obj, ref string mtl)
        {
            StreamReader filereader;
            string line, type, type_value;
            string[] line_segment;
            
            try { filereader = new StreamReader(obj); }
            catch (Exception e) { return; }

            Console.WriteLine("Parsing .obj file...");
            while ((line = filereader.ReadLine()) != null)
            {
                int space = line.IndexOf(' ');
                if (0 < space && space < line.Length)
                {
                    type = line.Substring(0, space);
                    type_value = line.Substring(space, line.Length - space).Trim();
                    line_segment = line.Split(' ');

                    switch (type)
                    {
                        // Object
                        case "o":
                            if (TextureIndices.Count > 0)
                                TextureIndices[TextureIndices.Count - 1].Add(MaterialIndex);

                            TextureIndices.Add(new List<int>());
                            break;

                        // Material file path
                        case "mtllib":
                            mtl = mtl + '\\' + type_value;
                            break;

                        // Vertex point
                        case "v":
                            Vector3 temp = new Vector3(float.Parse(line_segment[1]), float.Parse(line_segment[2]), float.Parse(line_segment[3]));
                            
                            if (Math.Abs(temp.X) > ScaleFactor) ScaleFactor = Math.Abs(temp.X);
                            if (Math.Abs(temp.Y) > ScaleFactor) ScaleFactor = Math.Abs(temp.Y);
                            if (Math.Abs(temp.Z) > ScaleFactor) ScaleFactor = Math.Abs(temp.Z);
                            
                            VertexPoint.Add(temp);
                            break;

                        // Vertex Texture Co-ordinate
                        case "vt":
                            VertexUV.Add(new Vector2(float.Parse(line_segment[1]), float.Parse(line_segment[2])));
                            break;

                        // Vertex Normal
                        case "vn":
                            VertexNormal.Add(new Vector3(float.Parse(line_segment[1]), float.Parse(line_segment[2]), float.Parse(line_segment[3])));
                            break;

                        // Face indices
                        case "f":
                            AddFace(line_segment);
                            break;

                        // Material
                        case "usemtl":
                            if (TextureIndices.Count == 0)
                                TextureIndices.Add(new List<int>());

                            TextureIndices[TextureIndices.Count - 1].Add(MaterialIndex);
                            MaterialName.Add(type_value);
                            break;

                        default:
                            break;
                    }
                }
            }

            filereader.Close();
            TextureIndices[TextureIndices.Count - 1].Add(MaterialIndex);
        }

        /// <summary>
        /// Reads throught the mtl file to get the diffuse colour, specular colour and,
        /// file path to the textue of the object.
        /// </summary>
        /// <param name="mtl"> Path to the material file. </param>
        /// <param name="tex"> Directory where the textures are contained. </param>
        private static void ParseMTL(string mtl, string tex)
        {
            StreamReader filereader;
            string line, type, type_value, image_path = "", colour = "";
            string next_material = "";

            try { filereader = new StreamReader(mtl); }
            catch (Exception e) { return; }

            Console.WriteLine("Parsing .mtl file...");
            while ((line = filereader.ReadLine()) != null)
            {
                int space = line.IndexOf(' ');
                if (0 < space && space < line.Length)
                {
                    type = line.Substring(0, space);
                    type_value = line.Substring(space, line.Length - space).Trim();

                    switch (type)
                    {
                        // Material Name
                        case "newmtl":
                            next_material = type_value;
                            break;

                        // Ambient Colour
                        case "Ka":
                            colour = "false " + type_value;
                            break;

                        // Diffuse Colour
                        case "Kd":
                            colour += " " + type_value;
                            break;

                        // Specular Colour
                        case "Ks":
                            colour += " " + type_value;
                            break;

                        // Opacity
                        case "d":
                            colour += " " + type_value;
                            MaterialTexture.Add(next_material, colour);
                            break;

                        // Image Path
                        case "map_Kd":
                            colour = colour.Substring(6);
                            image_path = "true " + (type_value.Contains(":\\") ? type_value : tex + type_value) + "\n" + colour;
                            
                            if (MaterialTexture.ContainsKey(next_material))
                                MaterialTexture.Remove(next_material);

                            MaterialTexture.Add(next_material, image_path);
                            break;

                        default:
                            break;
                    }
                }
            }
            filereader.Close();
        }

        /// <summary>
        /// Adds faces from the file source in a clockwise fanning direction.
        /// </summary>
        /// <param name="line"> An array of face index values. </param>
        private static void AddFace(string[] line)
        {
            string[] first, second;
            int start = FacePoint.Count;

            // Initial Point
            first = line[1].Split('/');

            int start_v = int.Parse(first[0]);
            int start_t = -1;
            int start_n = -1;

            if (first.Length == 3)
            { 
                if (first[1] != string.Empty)
                    start_t = int.Parse(first[1]);
                if (first[2] != string.Empty)
                    start_n = int.Parse(first[2]);
            }
            else
            {
                if (first[1] != string.Empty)
                    start_n = int.Parse(first[1]);
            }
            // Initial Point

            // Fanning loop
            for (int i = 3; i < line.Length; i++)
            {
                first = line[i - 1].Split('/');
                second = line[i].Split('/');


                FacePoint.Add(start_v);
                FacePoint.Add(int.Parse(first[0]));
                FacePoint.Add(int.Parse(second[0]));

                if (first.Length == 3 && start_t != -1)
                {
                    FaceUV.Add(start_t);
                    FaceUV.Add(int.Parse(first[1]));
                    FaceUV.Add(int.Parse(second[1]));
                }

                if (start_n != -1)
                {
                    FaceNormal.Add(start_n);
                    FaceNormal.Add(int.Parse(first[first.Length == 2 ? 1 : 2]));
                    FaceNormal.Add(int.Parse(second[first.Length == 2 ? 1 : 2]));
                }

                MaterialIndex += 3;
            }
            // Fanning Loop
        }

        /// <summary>
        /// Creates the subobject in the root object.
        /// Each subobject contains more objects with their own material.
        /// </summary>
        /// <param name="root"></param>
        public static void GenerateChildren(ref GameObject root)
        {
            Console.WriteLine("Generating children objects...");
            int children = TextureIndices.Count;
            int current_material = 0;

            for (int child = 0; child < children; ++child)
            {
                int total_materials = TextureIndices[child].Count - 1;
                GameObject newChild = new GameObject();

                for (int material = 0; material < total_materials; ++material)
                {
                    int start = TextureIndices[child][material];
                    int end = TextureIndices[child][material + 1];
                    int range = end - start;

                    int[]   vi = null,
                            ti = null,
                            ni = null;

                    vi = new int[range];
                    if (start + range <= FaceUV.Count)
                        ti = new int[range];
                    if (start + range <= FaceNormal.Count)
                        ni = new int[range];

                    FacePoint.CopyTo(start, vi, 0, range);
                    if (ti != null) FaceUV.CopyTo(start, ti, 0, range);
                    if (ni != null) FaceNormal.CopyTo(start, ni, 0, range);

                    string h = MaterialName[current_material];
                    string filepath;

                    try { filepath = MaterialTexture[h]; }
                    catch (Exception e) { filepath = string.Empty; }


                    BufferData bd = new BufferData(ref VertexPoint, ref VertexUV, ref VertexNormal, vi, ti, ni);
                    Material m = new Material(filepath);
                    GameObject newSubChild = new GameObject(m, bd);

                    newChild.Children.Add(newSubChild);
                    ++current_material;
                }

                root.Children.Add(newChild);
            }
        }

        /// <summary>
        /// Scales down the vertices of the parsed model to fit within the
        /// viewport.
        /// </summary>
        /// <param name="root"> The root model object. </param>
        private static void ScaleObject(ref GameObject root)
        {
            ScaleFactor /= ScaleFactorBase;

            Console.WriteLine("Scaling objects...");
            for (int i = 0; i < root.Children.Count; ++i)
            {
                GameObject current = root.Children[i];
                for (int j = 0; j < current.Children.Count; ++j)
                {

                    GameObject child = current.Children[j];
                    for (int k = 0; k < child.BufferData.Vertex.Length; ++k)
                    {
                        child.BufferData.Vertex[k].X /= ScaleFactor;
                        child.BufferData.Vertex[k].Y /= ScaleFactor;
                        child.BufferData.Vertex[k].Y -= VerticalOffset;
                        child.BufferData.Vertex[k].Z /= ScaleFactor;
                    }
                }
            }
        }
        #endregion
    }
}