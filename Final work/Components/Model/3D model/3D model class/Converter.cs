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

        public static   int current_index;
		public static 	float scale_factor;
        #endregion

        /// <summary>
		/// Converts the object and material files into a Model object.
		/// </summary>
		/// <param name='obj'> The filepath to the object file. </param>
		/// <param name='dir'> The filepath to the object's texture file. </param>
		public static GameObject fromOBJ(string obj, bool scaling)
		{   
			GameObject root = new GameObject();
            scale_factor = 0.0f;

            string mtl = obj.Substring(0, obj.Length - 3) + "mtl";
            int index = 0;
            for (int i = 0; i < obj.Length; ++i)
                if (obj[i] == '\\')
                    index = i;
            string tex = obj.Substring(0, index+1);

            parseOBJ(obj);
            parseMTL(mtl, tex);
            generateChildren(ref root);
            
            if (scaling)
            {
                List<GameObject> list = new List<GameObject>();
                GameObject current = root;

                for (int i = 0; i < current.Children.Count; ++i)
                {
                    current = current.Children[i];
                    if (!list.Contains(current))
                    {
                        list.Add(current);
                        findMax(current.bufferData);
                    }
                }

                for (int i = 0; i < list.Count; ++i)
                    scaleObject(list[i].bufferData);                
            }

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
                        p_vertices.Add(new Vector3(float.Parse(sections[1]), float.Parse(sections[2]),float.Parse(sections[3])));
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

            int i = 0;

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
                        Console.WriteLine(material_texture.Count);
                        ++i;
                        break;
                }
            }

            filereader.Close();

            Console.WriteLine("Number of textures: " + i);
        }

        public static void generateChildren(ref GameObject root)
        {
            int children = t_indices.Count - 1;

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

                root.AddChild(newChild);
            }
        }

		private static void findMax(BufferData bufferData)
        {
            for (int i = 0; i < bufferData.Vertex.Length; i++)
            {
                for (int j = 0; j < bufferData.Vertex[i].Length; ++j)
                {
                    if(Math.Abs(bufferData.Vertex[i].X) > scale_factor)
                    {
                        scale_factor = Math.Abs(bufferData.Vertex[i].X);
                    }
                    if (Math.Abs(bufferData.Vertex[i].Y) > scale_factor)
                    {
                        scale_factor = Math.Abs(bufferData.Vertex[i].Y);
                    }
                    if (Math.Abs(bufferData.Vertex[i].Z) > scale_factor)
                    {
                        scale_factor = Math.Abs(bufferData.Vertex[i].Z);
                    }
                }
            }
		}
		
        private static void scaleObject(BufferData bufferData)
		{
            for (int i = 0; i < bufferData.Vertex.Length; i++)
            {
                for (int j = 0; j < bufferData.Vertex[i].Length; ++j)
                {
                    bufferData.Vertex[i].X /= scale_factor;
                    bufferData.Vertex[i].Y /= scale_factor;
                    bufferData.Vertex[i].Z /= scale_factor;
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