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
using OpenTK.Graphics;
using System;
using System.IO;
using System.Collections.Generic;

namespace DisplayModel
{
	/// <summary>
	/// Used to convert the input object files into a Model3D object.
	/// <summary>
    public static class Converter
    {
        #region List items
        public static List<Vector3>                 p_vertices, p_normals;
        public static List<Vector2>                 p_uvs;
        public static List<List<int>>               t_indices;
        public static List<int>                     f_vertices, f_uvs, f_normals;
        public static Dictionary<string, string>    material_texture;
        public static List<string>                  materials;
        public static List<GameObject>              list;

        public static int                           material_index;
        public static float                         scale_factor = 1.0f;
        #endregion

        /// <summary>
        /// Converts the object and material files into a Model object.
        /// </summary>
        /// <param name='obj'> The filepath to the object file. </param>
        /// <param name='dir'> The filepath to the object's texture file. </param>
        public static GameObject fromOBJ(string obj, bool scaling)
        {
            Init();

            GameObject root = new GameObject();

            int index = 0;
            for (int i = 0; i < obj.Length; ++i)
                if (obj[i] == '\\')
                    index = i;
            string mtl = obj.Substring(0, index);
            string tex = obj.Substring(0, index) + '\\';

            parseOBJ(obj, ref mtl);
            parseMTL(mtl, tex);
            generateChildren(ref root);

            if (scaling)
            {
                scale_factor /= 2.5f;
                scaleObject(ref root);
            }


            Console.WriteLine("Generating materials...");
            root.Initialize();
            Clear();

            Console.WriteLine("Returning object...");
            return root;
        }

        /// <summary>
        /// Intialises the lists use to parse the files
        /// </summary>
        private static void Init()
        {
            p_vertices = new List<Vector3>();
            p_normals = new List<Vector3>();

            p_uvs = new List<Vector2>();

            t_indices = new List<List<int>>();
            f_vertices = new List<int>();
            f_uvs = new List<int>();
            f_normals = new List<int>();

            material_texture = new Dictionary<string, string>();

            materials = new List<string>();
            list = new List<GameObject>();

            material_index = 0;
            scale_factor = 1.0f;
        }

        /// <summary>
        /// Empties out the lists used to parse the files.
        /// </summary>
        private static void Clear()
        {
            p_vertices.Clear();
            p_normals.Clear();

            p_uvs.Clear();

            t_indices.Clear();
            f_vertices.Clear();
            f_uvs.Clear();
            f_normals.Clear();

            material_texture.Clear();

            materials.Clear();
            list.Clear();

            material_index = 0;
            scale_factor = 1.0f;
        }

        /// <summary>
        /// Scales down the vertices of the parsed model to fit within the
        /// screen.
        /// </summary>
        /// <param name="root"> The root model object. </param>
        private static void scaleObject(ref GameObject root)
        {
            Console.WriteLine("Scaling objects...");
            for (int i = 0; i < root.Children.Count; ++i)
            {
                GameObject current = root.Children[i];
                for (int j = 0; j < current.Children.Count; ++j)
                {
                    GameObject child = current.Children[j];
                    for (int k = 0; k < current.BufferData.Vertex.Length; ++k)
                    {
                        child.bufferData.vertex[k].X /= scale_factor;
                        child.bufferData.vertex[k].Y /= scale_factor;
                        child.bufferData.vertex[k].Z /= scale_factor;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"> Path to the obj file. </param>
        /// <param name="mtl"> Path to the material file, determined from the obj file. </param>
        private static void parseOBJ(string obj, ref string mtl)
        {
            StreamReader filereader;
            string line, type, section;
            string[] sections;
            
            try { filereader = new StreamReader(obj); }
            catch (Exception e) { throw new Exception("Exception: .obj file not found."); }

            Console.WriteLine("Parsing .obj file...");
            while ((line = filereader.ReadLine()) != null)
            {
                int space = line.IndexOf(' ');
                if (0 < space && space < line.Length)
                {
                    type = line.Substring(0, space);
                    section = line.Substring(space, line.Length - space).Trim();
                    sections = line.Split(' ');

                    switch (type)
                    {
                        case "o":
                            if (t_indices.Count > 0)
                                t_indices[t_indices.Count - 1].Add(material_index);

                            t_indices.Add(new List<int>());
                            break;

                        case "mtllib":
                            mtl = mtl + '\\' + section;
                            break;

                        case "v":
                            Vector3 temp = new Vector3(float.Parse(sections[1]), float.Parse(sections[2]), float.Parse(sections[3]));
                            if (Math.Abs(temp.X) > scale_factor) scale_factor = Math.Abs(temp.X);
                            if (Math.Abs(temp.Y) > scale_factor) scale_factor = Math.Abs(temp.Y);
                            if (Math.Abs(temp.Z) > scale_factor) scale_factor = Math.Abs(temp.Z);
                            p_vertices.Add(temp);
                            break;

                        case "vt":
                            p_uvs.Add(new Vector2(float.Parse(sections[1]), float.Parse(sections[2])));
                            break;

                        case "vn":
                            p_normals.Add(new Vector3(float.Parse(sections[1]), float.Parse(sections[2]), float.Parse(sections[3])));
                            break;

                        case "f":
                            addFace(sections);
                            break;

                        case "usemtl":
                            if (t_indices.Count == 0)
                                t_indices.Add(new List<int>());

                            t_indices[t_indices.Count - 1].Add(material_index);
                            materials.Add(section);
                            break;
                    }
                }
            }

            filereader.Close();
            t_indices[t_indices.Count - 1].Add(material_index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mtl"></param>
        /// <param name="tex"></param>
        private static void parseMTL(string mtl, string tex)
        {
            StreamReader filereader;
            string line, type, sections, image_path = "";
            string next_material = "";

            try { filereader = new StreamReader(mtl); }
            catch (Exception e) { Console.WriteLine(".mtl file not found."); return; }

            Console.WriteLine("Parsing .mtl file...");
            while ((line = filereader.ReadLine()) != null)
            {
                int space = line.IndexOf(' ');
                if (0 < space && space < line.Length)
                {
                    type = line.Substring(0, space);
                    sections = line.Substring(space, line.Length - space).Trim();

                    switch (type)
                    {
                        case "newmtl":
                            next_material = sections;
                            break;

                        case "Kd":
                            image_path = "C" + sections;
                            material_texture.Add(next_material, image_path);
                            break;

                        case "Ks":
                            image_path += " " + sections;
                            break;

                        case "map_Kd":
                            image_path = "T" + (sections.Contains(":\\") ? sections : tex + sections);
                            
                            if (material_texture.ContainsKey(next_material))
                                material_texture.Remove(next_material);

                            material_texture.Add(next_material, image_path);
                            break;
                    }
                }
            }
            filereader.Close();
        }

        /// <summary>
        /// Adds faces from the file source. 
        /// </summary>
        /// <param name="v"> The vertex point list. </param>
        /// <param name="t"> The texture point list. </param>
        /// <param name="n"> The normal direction list. </param>
        /// <param name="line"> An array of face index values. </param>
        private static void addFace(string[] line)
        {
            string[] first, second;
            int start = f_vertices.Count;

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
            for (int i = 3; i < line.Length; i += 1)
            {
                first = line[i - 1].Split('/');
                second = line[i].Split('/');


                f_vertices.Add(start_v);
                f_vertices.Add(int.Parse(first[0]));
                f_vertices.Add(int.Parse(second[0]));

                if (first.Length == 3 && start_t != -1)
                {
                    f_uvs.Add(start_t);
                    f_uvs.Add(int.Parse(first[1]));
                    f_uvs.Add(int.Parse(second[1]));
                }

                if (start_n != -1)
                {
                    f_normals.Add(start_n);
                    f_normals.Add(int.Parse(first[first.Length == 2 ? 1 : 2]));
                    f_normals.Add(int.Parse(second[first.Length == 2 ? 1 : 2]));
                }
                material_index += 3;
            }
            // Fanning Loop
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        public static void generateChildren(ref GameObject root)
        {
            Console.WriteLine("Generating children objects...");
            int children = t_indices.Count;
            int count = 0;

            for (int child = 0; child < children; ++child)
            {
                int allmaterials = t_indices[child].Count - 1;
                GameObject newChild = new GameObject();

                for (int material = 0; material < allmaterials; ++material)
                {
                    int start = t_indices[child][material];
                    int end = t_indices[child][material + 1];
                    int range = end - start;

                    int[]   vi = null,
                            ti = null,
                            ni = null;

                    vi = new int[range];
                    if (start + range <= f_uvs.Count)
                        ti = new int[range];
                    if (start + range <= f_normals.Count)
                        ni = new int[range];

                    f_vertices.CopyTo(start, vi, 0, range);
                    if (ti != null) f_uvs.CopyTo(start, ti, 0, range);
                    if (ni != null) f_normals.CopyTo(start, ni, 0, range);

                    string h = materials[count];
                    string filepath;

                    try { filepath = material_texture[h]; }
                    catch (Exception e) { filepath = string.Empty; }


                    BufferData bd = new BufferData(ref p_vertices, ref p_uvs, ref p_normals, vi, ti, ni);
                    Material m = new Material(filepath);
                    GameObject newSubChild = new GameObject(m, bd);

                    newChild.AddChild(newSubChild);
                    ++count;
                }

                root.AddChild(newChild);
            }
        }
    }
}