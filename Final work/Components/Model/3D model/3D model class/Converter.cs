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
		/// <summary>
		/// Converts the object and material files into a Model object.
		/// </summary>
		/// <param name='obj'> The filepath to the object file. </param>
		/// <param name='tex'> The filepath to the object's texture file. </param>
		public static GameObject fromOBJ(string obj, string tex)
		{
            List<Vector3>
				p_vertices = new List<Vector3>(),
                p_normals = new List<Vector3>();
            List<Vector2>
                p_uvs = new List<Vector2>();

			List<int>
				f_vertices = new List<int>(),
				f_uvs = new List<int>(),
				f_normals = new List<int>();
            
            StreamReader filereader;
            string line, name;
            string[] sections;

		    filereader = new StreamReader(obj);

			while ((line = filereader.ReadLine()) != null)
			{
				sections = line.Split(' ');
				
				switch(sections[0])
				{
					case "o":
						name = sections[1];
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
				}
			}

            filereader.Close();

            GameObject temp = new GameObject();

            if (tex != "")
                temp.Material = new Material(tex);
            else
                temp.Material = new Material(Color4.LightGray);

            temp.BufferData = new BufferData(p_vertices, p_uvs, p_normals, f_vertices, f_uvs, f_normals, temp.Material);
            Console.WriteLine(temp);
            scaleObject(temp);
            return temp;
		}

        private static void scaleObject(GameObject obj)
        {
            float max = 0f;
            for (int i = 0; i < obj.bufferData.Vertex.Length; i++)
            {
                if(Math.Abs(obj.bufferData.Vertex[i].X) > max)
                {
                    max = Math.Abs(obj.bufferData.Vertex[i].X);
                }
                if (Math.Abs(obj.bufferData.Vertex[i].Y) > max)
                {
                    max = Math.Abs(obj.bufferData.Vertex[i].Y);
                }
                if (Math.Abs(obj.bufferData.Vertex[i].Z) > max)
                {
                    max = Math.Abs(obj.bufferData.Vertex[i].Z);
                }
            }
            Console.WriteLine("Largest vertex value: "+max);
            for (int i = 0; i < obj.bufferData.Vertex.Length; i++)
            {
                obj.bufferData.Vertex[i].X /= max;
                obj.bufferData.Vertex[i].Y /= max;
                obj.bufferData.Vertex[i].Z /= max;
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
                }

                if (line.Length == 5)
                {
                    texels = line[4].Split('/');

                    v.Add(v[v.Count - 3]);
                    v.Add(v[v.Count - 2]);
                    v.Add(int.Parse(texels[0]));

                    if (texels[1] != string.Empty)
                    {
                        t.Add(t[t.Count - 2]);
                        t.Add(t[t.Count - 2]);
                        t.Add(int.Parse(texels[1]));
                    }

                    n.Add(n[n.Count - 3]);
                    n.Add(n[n.Count - 2]);
                    n.Add(int.Parse(texels[2]));
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

		/// <summary>
		/// Converts the object and material files into a Model object.
		/// </summary>
		/// <param name='obj'> The filepath to the object file. </param>
		public static GameObject fromDAE(string obj)
		{
            return null;
		}

		/// <summary>
		/// Converts the object and material files into a Model object.
		/// </summary>
		/// <param name='obj'> The filepath to the object file. </param>
		public static GameObject fromX3D(string obj)
        {
            return null;
		}
	}
}