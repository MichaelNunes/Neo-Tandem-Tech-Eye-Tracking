using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace _3D_model
{
    class ModelFactory
    {
        public ModelFactory()
        {

        }

        public CustomObject FromOBJ(string filePath)
        {
            System.IO.StreamReader file;

            string line;
            string[] temp;

            List<Vector3> v = new List<Vector3>();
            List<Vector2> vt = new List<Vector2>();
            List<Vector3> vn = new List<Vector3>();
            List<int[]> f = new List<int[]>();

            try
            {
                file = new System.IO.StreamReader(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            while ((line = file.ReadLine()) != null)
            {
                temp = line.Split(' ');
                switch (temp[0])
                {
                    case "v":
                        {
                            v.Add(new Vector3(
                                float.Parse(temp[1]),
                                float.Parse(temp[2]),
                                float.Parse(temp[3])
                            ));
                            break;
                        }
                    case "vt":
                        {
                            vt.Add(new Vector2(
                                float.Parse(temp[1]),
                                float.Parse(temp[2])
                            ));
                            break;
                        }
                    case "vn":
                        {
                            vn.Add(new Vector3(
                                float.Parse(temp[1]),
                                float.Parse(temp[2]),
                                float.Parse(temp[3])
                            ));
                            break;
                        }
                    case "f":
                        {
                            string[] vtn;
                            switch (temp.Length)
                            {
                                case 4://Three vertex faces
                                    {
                                        for (int i = 1; i < temp.Length; ++i)
                                        {
                                            vtn = temp[i].Split('/');
                                            if (vtn[1] == "")
                                            {
                                                f.Add(new int[]{
                                                    int.Parse(vtn[0]),
                                                    int.Parse(vtn[2])
                                                });
                                            }
                                            else
                                            {
                                                f.Add(new int[]{
                                                    int.Parse(vtn[0]),
                                                    int.Parse(vtn[1]),
                                                    int.Parse(vtn[2])
                                                });
                                            }
                                        }
                                        break;
                                    }
                                case 5://Four vertex faces
                                    {
                                        break;
                                    }
                            }

                            break;
                        }
                }
            }

            file.Close();

            CustomObject customObject = new CustomObject();

            customObject.vertices = new Vector3[f.Count];
            customObject.textureCoords = new Vector2[f.Count];
            customObject.normals = new Vector3[f.Count];

            switch (f[0].Length)
            {
                case 2:
                    {
                        for (int i = 0; i < f.Count; ++i)
                        {
                            customObject.vertices[i] = v[f[i][0]];
                            customObject.normals[i] = vn[f[i][2]];
                        }
                        break;
                    }
                case 3:
                    {
                        for (int i = 0; i < f.Count; ++i)
                        {
                            customObject.vertices[i] = v[f[i][0] - 1];
                            customObject.textureCoords[i] = vt[f[i][1] - 1];
                            customObject.normals[i] = vn[f[i][2] - 1];
                        }
                        break;
                    }
                default: throw new Exception("VTN is the wrong size.");
            }

            //Default Color
            customObject.SetColor(0.5f, 0.5f, 0.5f);

            return customObject;
        }
    }
}
