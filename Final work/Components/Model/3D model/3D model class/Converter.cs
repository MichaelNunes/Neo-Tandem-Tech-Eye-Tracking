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
        public static   List<Vector3>
                        p_vertices = new List<Vector3>(),
                        p_normals = new List<Vector3>();

        public static   List<Vector2>
                        p_uvs = new List<Vector2>();

        public static   List<int>
                        t_indices = new List<int>(),
                        f_vertices = new List<int>(),
                        f_uvs = new List<int>(),
                        f_normals = new List<int>();

        public static   Dictionary<string, string>
                        material_texture = new Dictionary<string,string>();

        public static   List<string>
                        materials = new List<string>();
        public static   List<GameObject>
                        list = new List<GameObject>();

        public static   int current_index;
		public static 	float scale_factor = 1.0f;
        #endregion

        /// <summary>
		/// Converts the object and material files into a Model object.
		/// </summary>
		/// <param name='obj'> The filepath to the object file. </param>
		/// <param name='dir'> The filepath to the object's texture file. </param>
		public static GameObject fromOBJ(string obj, bool scaling)
		{   
			GameObject root = new GameObject();

            string mtl = obj.Substring(0, obj.Length - 3) + "mtl";
            int index = 0;
            for (int i = 0; i < obj.Length; ++i)
                if (obj[i] == '\\')
                    index = i;
            string tex = obj.Substring(0, index+1);

            Console.WriteLine("Parsing .obj file...");
            parseOBJ(obj);
            Console.WriteLine("Parsing .mtl file...");
            parseMTL(mtl, tex);
            Console.WriteLine("Generating children objects...");
            generateChildren(ref root);
            
            if (scaling)
            {
                scale_factor /= 5;

                Console.WriteLine("Scaling objects...");
                for (int i = 0; i < root.Children.Count; ++i)
                {
                    GameObject current = root.Children[i];
                    for(int j = 0; j < current.BufferData.Vertex.Length; ++j)
                    {
                        current.bufferData.vertex[j].X /= scale_factor;
                        current.bufferData.vertex[j].Y /= scale_factor;
                        current.bufferData.vertex[j].Z /= scale_factor;
                    }
                }
            }


            Console.WriteLine("Generating materials...");
            root.Initialize();

            Console.WriteLine("Returning object...");
            return root;
        }
        
        private static void parseOBJ(string obj)
        {
            StreamReader filereader;
            string line;
            string[] sections;
            current_index = 0;

		    filereader = new StreamReader(obj);

			while ((line = filereader.ReadLine()) != null)
			{
				sections = line.Split(' ');
				
				switch(sections[0])
				{
					case "o":
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
                        addFace(ref f_vertices, ref f_uvs, ref f_normals, sections);
						break;

                    case "usemtl":
                        t_indices.Add(current_index);
                        materials.Add(sections[1]);
                        break;
				}
			}

            filereader.Close();
            t_indices.Add(current_index);
		}

        private static void parseMTL(string mtl, string tex)
        {
            StreamReader filereader;
            string line;
            string[] sections;
            string next_material = "";

            try { filereader = new StreamReader(mtl); }
            catch (Exception e) { return; }

            while ((line = filereader.ReadLine()) != null)
            {
                sections = line.Split(' ');

                switch (sections[0])
                {
                    case "newmtl":
                        next_material = sections[1];
                        break;

                    case "map_Kd":
                        material_texture.Add(next_material, tex + sections[1]);
                        break;
                }
            }

            filereader.Close();
        }

        public static void generateChildren(ref GameObject root)
        {
            int children = t_indices.Count - 1;
            Console.WriteLine("Number of children: " + children);

            for (int child = 0; child < children; ++child)
            {
                int start = t_indices[child];
                int end = t_indices[child + 1];
                int range = end - start;

                int[] vi = new int[range],
                      ti = new int[f_uvs.Count > 0 ? range : 0],
                      ni = new int[range];

                f_vertices.CopyTo(start, vi, 0, range);
                if (ti.Length > 0)
                    f_uvs.CopyTo(start, ti, 0, range);
                f_normals.CopyTo(start, ni, 0, range);

                string h = materials[child];
                string filepath;

                try { filepath = material_texture[h]; }
                catch (Exception e) { filepath = string.Empty; }


                BufferData bd = new BufferData(p_vertices, p_uvs, p_normals, vi, ti, ni);
                Material m = new Material(filepath);
                GameObject newChild = new GameObject(m, bd);

                root.Children.Add(newChild);
            }
        }

		private static void findMax(BufferData bufferData)
        {
            for (int i = 0; i < bufferData.Vertex.Length; i++)
            {
                for (int j = 0; j < bufferData.Vertex[i].Length; ++j)
                {
                    if(Math.Abs(bufferData.Vertex[i].X) > scale_factor)
                        scale_factor = Math.Abs(bufferData.Vertex[i].X);

                    if (Math.Abs(bufferData.Vertex[i].Y) > scale_factor)
                        scale_factor = Math.Abs(bufferData.Vertex[i].Y);

                    if (Math.Abs(bufferData.Vertex[i].Z) > scale_factor)
                        scale_factor = Math.Abs(bufferData.Vertex[i].Z);
                }
            }
		}
		
        private static void scaleObject(ref BufferData bufferData)
		{
            for (int i = 0; i < bufferData.Vertex.Length; i++)
            {
                for (int j = 0; j < bufferData.Vertex[i].Length; ++j)
                {
                    Console.WriteLine("Before: " + bufferData.vertex[i]);
                    Console.WriteLine("After: " + bufferData.vertex[i]);
                }
            }
        }
		
        /// <summary>
        /// Adds faces from the file source. 
        /// </summary>
        /// <param name="v"> The vertex point list. </param>
        /// <param name="t"> The texture point list. </param>
        /// <param name="n"> The normal direction list. </param>
        /// <param name="line"> An array of face index values. </param>
        private static void addFace(ref List<int> v, ref List<int> t, ref List<int> n, string[] line)
        {
			string[] texels;

            if (line[1].Contains("/"))
            {
                for (int i = 1; i < 4; ++i)
                {
                    texels = line[i].Split('/');

                    v.Add(int.Parse(texels[0]));

                    if (texels[1] != string.Empty)
                        t.Add(int.Parse(texels[1]));

                    n.Add(int.Parse(texels[2]));

                    current_index++;
                }

                if (line.Length == 5)
                {
                    texels = line[4].Split('/');

                    v.Add(v[v.Count - 3]);
                    v.Add(v[v.Count - 2]);
                    v.Add(int.Parse(texels[0]));

                    if (texels[1] != string.Empty)
                    {
                        t.Add(t[t.Count - 3]);
                        t.Add(t[t.Count - 2]);
                        t.Add(int.Parse(texels[1]));
                    }

                    n.Add(n[n.Count - 3]);
                    n.Add(n[n.Count - 2]);
                    n.Add(int.Parse(texels[2]));

                    current_index += 3;
                }

                if (line.Length > 5)
                    throw new Exception("More than 4 vertices per face.");
            }

            else
            {
                if (line.Length == 3)
                {
                    v.Add(int.Parse(line[1]));
                    v.Add(int.Parse(line[2]));
                    v.Add(int.Parse(line[3]));
                }
            }
		}
	}
}