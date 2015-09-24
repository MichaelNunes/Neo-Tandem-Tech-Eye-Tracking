﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using Video_Model;
using System.IO;
using System.Windows.Media.Imaging;
using AForge.Video;
using AForge.Video.FFMPEG;
using System.Threading;

namespace Results_Class
{
    class Eye_Tracking_Graph
    {

        /// <summary>
        /// Represents file(s) locations that will be used for the ETGraph processess.
        /// </summary>
        string SourceLocation;
        public string _SourceLocation
        {
            get { return SourceLocation; }
            set { SourceLocation = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        string DestinationPath;
        public string _DestinationPath
        {
            get { return DestinationPath; }
            set { DestinationPath = value; }
        }

        /// <summary>
        /// Represents the name of the model that results will be made for. 
        /// </summary>
        string ModelName;
        public string _modelName
        {
            get { return ModelName; }
            set { ModelName = value; }
        }

        /// <summary>
        /// List<float> containing all X axis co-ordinates.
        /// </summary>
        public List<float> px = new List<float>();
        public List<float> Px
        {
            get { return px; }
            set { px = value; }
        }

        /// <summary>
        /// List<float> containing all Y axis co-ordinates.
        /// </summary>
        public List<float> py = new List<float>();
        public List<float> Py
        {
            get { return py; }
            set { py = value; }
        }

        /// <summary>
        /// The height of the image, ETGraph will be generated for
        /// </summary>
        private int height;
        public int _height
        {
            get { return height; }
            set { height = value; }
        }


        /// <summary>
        /// The width of the image, ETGraph will be generated for.
        /// </summary>
        private int width;
        public int _width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// This specifies the type of model that will be worked with.
        /// </summary>
        private string modelType;
        public string _modelType
        {
            get { return modelType; }
            set { modelType = value; }
        }

        public Eye_Tracking_Graph()
        {

        }

        public Eye_Tracking_Graph(int Width, int Height)
        {
            width = Width;
            height = Height;
        }

        public Eye_Tracking_Graph(string NameOfModel, string FilePath, int Width, int Height, string ModelType)
        {
            width = Width;
            height = Height;
            ModelName = NameOfModel;
            SourceLocation = FilePath;
            modelType = ModelType;
        }

        public Eye_Tracking_Graph(string NameOfModel, string FilePath)
        {
            ModelName = NameOfModel;
            SourceLocation = FilePath;
        }

        /// <summary>
        /// traces a graph (points connected with lines) on the model
        /// </summary>
        /// <param name="img"></param> source image to graph onto
        /// <param name="X"></param> x co-ordinate of point
        /// <param name="Y"></param> y co-ordinate of point
        /// <param name="currentPoint"></param> point currently selected/observed in model
        /// <param name="pointHistory"></param> how many points to trace back to while graphing them. 
        /// <returns></returns>
        Image createETGraph(Image img, List<float> X, List<float> Y, int currentPoint, int pointHistory)
        {
            using (var graphics = Graphics.FromImage(img))
            {
                int startPoint = currentPoint - pointHistory;

                if(startPoint < 0)
                {
                    startPoint = 0;
                }

                int endPoint = currentPoint;
                List<int> growingPoints = new List<int>();
                List<int> growingPointsArea = new List<int>();
                List<int> growingPointsClusterCount = new List<int>();

                SolidBrush redBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                Font drawFont = new System.Drawing.Font("Arial", 16);
                SolidBrush greenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
                Pen redPen = new Pen(Color.Red, 1);

                int pointNumber = 0;
                for (int i = startPoint; i <= endPoint; i++)
                {
                    if (growingPoints.Count == 0)
                    {
                        growingPoints.Add(i);
                        growingPointsArea.Add(10);
                        growingPointsClusterCount.Add(0);
                        graphics.FillEllipse(redBrush, new Rectangle((int)X[i] - 5, (int)Y[i] - 5, 10, 10));
                        graphics.DrawString(pointNumber.ToString(), drawFont, greenBrush, X[i] - 5, Y[i] - 5);
                    }
                    else
                    {
                        bool newGrowingPoint = false;
                        for (int j = 0; j < growingPoints.Count; j++)
                        {
                            int gp = growingPoints.ElementAt(j);
                            int gpa = growingPointsArea.ElementAt(j);
                            int gpcc = growingPointsClusterCount.ElementAt(j);
                            if ((X[i] > (X[gp] - gpa)) && (X[i] < (X[gp] + gpa)) &&
                                (Y[i] > (Y[gp] - gpa)) && (Y[i] < (Y[gp] + gpa)))
                            {
                                gpa += 10;
                                growingPointsArea.Insert(j,gpa);
                                growingPointsClusterCount.Insert(j,gpcc+1);
                                graphics.FillEllipse(redBrush, new Rectangle((int)X[gp] - (gpa / 2), (int)Y[gp] - (gpa / 2), gpa, gpa));
                                graphics.DrawString(pointNumber.ToString(), drawFont, greenBrush, X[gp] - 5, Y[gp] - 5);
                                newGrowingPoint = false;
                            }
                            else
                            {
                                if (gpa > 10)
                                {
                                    gpa -= 10;
                                    growingPointsArea.Insert(j, gpa);
                                    graphics.FillEllipse(redBrush, new Rectangle((int)X[gp] - (gpa / 2), (int)Y[gp] - (gpa / 2), gpa, gpa));
                                }
                                newGrowingPoint = true;

                            }
                        }
                        if (newGrowingPoint)
                        {
                            graphics.FillEllipse(redBrush, new Rectangle((int)X[i] - 5, (int)Y[i] - 5, 10, 10));
                            if (i != startPoint)
                            {
                                graphics.DrawLine(redPen, X[i], Y[i], X[i - 1], Y[i - 1]);
                                growingPoints.Add(i);
                                growingPointsArea.Add(10);
                                growingPointsClusterCount.Add(0);
                            }
                            pointNumber += 1;
                            graphics.DrawString(pointNumber.ToString(), drawFont, greenBrush, X[i] - 5, Y[i] - 5);
                        }
                            
                        
                    }
                }
             }
            return img;
        }

        /// <summary>
        /// Opens the ETGraph data for creation of ETGraph to relevent image.
        /// </summary>
        /// <param name="fileLocation"> specifies file location</param>
        /// <param name="modelName">specifies model name</param>
        public void OpenETGraphData(string fileLocation, string modelName)
        {
            px = null;
            py = null;
            px = new List<float>();
            py = new List<float>();
            string[] lines = System.IO.File.ReadAllLines(fileLocation + "\\RecordedData_" + modelName + ".txt");
            foreach (string item in lines)
            {
                px.Add((float)Convert.ToDouble(item.Substring(0, item.IndexOf(":"))));

                int temp1 = item.IndexOf(":") + 1;
                int temp2 = item.Length - temp1;
                py.Add((float)Convert.ToDouble(item.Substring(temp1, temp2)));
            }
        }

        /// <summary>
        /// Mediates which model is being worked with.
        /// </summary>
        public void saveETGraph()
        {
            if (modelType == "3D")
            {
                SaveETGraph3D();
            }
            else if (modelType == "Video")
            {
                SaveETGraphVideo();
            }
            else if (modelType == "2D")
            {
                SaveETGraph2D();
            }
        }

        /// <summary>
        /// Saves only the ETGraph as an image for 3D models
        /// </summary>
        public void SaveETGraph3D()
        {
            string[] files = Directory.GetFiles(SourceLocation);
            List<string> images = new List<string>();
            List<string> dataFiles = new List<string>();

            //check that all files are images
            foreach (string str in files)
            {
                if (Path.GetExtension(str).Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) || Path.GetExtension(str).Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase))
                {
                    images.Add(str);
                }
                else if (Path.GetExtension(str).Equals(".txt", StringComparison.InvariantCultureIgnoreCase))
                {
                    dataFiles.Add(str);
                }
                else
                {

                }
            }
            try
            {
                for (int i = 0; i < dataFiles.Count; i++)
                {
                    OpenETGraphData(SourceLocation, (ModelName + "" + i));
                    Bitmap bitmap = new Bitmap(width, height);
                    if (py.Count == 0 || px.Count == 0)
                    {
                        throw new ArgumentNullException();
                    }
                    Image canvas = createETGraph(bitmap, px, py, i, 10);
                    canvas.Save(SourceLocation + "\\" + ModelName + "" + (i + 2) + ".ETGraph.jpg", ImageFormat.Jpeg);
                }
            }
            catch (Exception ef)
            {
                return;
            }
        }

        /// <summary>
        /// Saves only the ETGraph as an image for Video models
        /// </summary>
        public void SaveETGraphVideo()
        {
            //get heights and widths of video
            VideoFileReader vid = new VideoFileReader();
            vid.Open(SourceLocation/* + ModelName + ".wmv"*/);
            height = vid.Height;
            width = vid.Width;
            vid.Close();

            VideoGenerator vm = new VideoGenerator();
            List<float> x = new List<float>();
            List<float> y = new List<float>();

            if (py.Count() == 0 || px.Count() == 0)
            {
                throw new ArgumentNullException();
            }

            //Image im = new Bitmap(FileLocation + "\\" + ModelName);            
            //bitmap.Save(FileLocation +"\\"+ ModelName + ".jpg");

            List<Thread> tl = new List<Thread>();
            //Implement Currently
            for (int i = 0; i < px.Count(); i++)
            {
                try
                {
                    Bitmap bitmap = new Bitmap(width, height);
                    if (x.Count > 150)
                    {
                        x.Add(px.ElementAt(i));
                        y.Add(py.ElementAt(i));
                        x.RemoveAt(0);
                        y.RemoveAt(0);
                    }
                    else
                    {
                        x.Add(px.ElementAt(i));
                        y.Add(py.ElementAt(i));
                    }


                    /*Thread t = new Thread(() =>*/
                    SaveETGraphImage(bitmap, x, y, i);//);
                    /*tl.Add(t);
                    //2 Threads = 3:47
                    //10 threads = 3:27
                    //40 Threads = 3:30
                    //100 Threads = 3:37
                    //No join statement = to long
                    if(tl.Count% px.Count == 0)
                    {
                        foreach (Thread thr in tl)
                        {
                            t.Start();
                        }
                        foreach (Thread thr in tl)
                        {
                            thr.Join();
                        }
                        tl = null;
                        tl = new List<Thread>();
                    }*/
                }
                catch (Exception k)
                {
                    break;
                }
            }


            //call create video
            vm.ImagePath = DestinationPath + "\\";
            vm.DestinationPath = DestinationPath + "\\";
            vm.ModelName = ModelName + ".ETGraph";
            vm.FrameWidth = width;
            vm.FrameHeight = height;
            vm.Fps = 30;
            vm.createVideo();
        }

        public void SaveETGraphImage(Bitmap bitmap, List<float> x, List<float> y, int i)
        {
            try
            {
                Image canvas = createETGraph(bitmap, px, py, i, 10);
                canvas.Save(DestinationPath + "\\" + ModelName + ".ETGraph" + "frame" + i + ".jpg", ImageFormat.Jpeg);
                bitmap.Dispose();
            }
            catch (Exception e)
            {
                SaveETGraphImage(bitmap, x, y, i);
            }
        }

        /// <summary>
        /// Saves only the ETGraph as an image for 2D models
        /// </summary>
        public void SaveETGraph2D()
        {
            Bitmap bitmap = new Bitmap(width, height);
            if (py.Count == 0 || px.Count == 0)
            {
                throw new ArgumentNullException();
            }

            Image canvas = createETGraph(bitmap, px, py, 0, 10);
            canvas.Save(SourceLocation + "\\" + ModelName + ".ETGraph.jpg", ImageFormat.Jpeg);
        }

        /// <summary>
        /// Mediates which model is being worked with.
        /// </summary>
        public void saveETGraphOntoModel()
        {
            if (modelType == "3D")
            {
                SaveETGraphOntoModel3D();
            }
            else if (modelType == "Video")
            {
                SaveETGraphOntoModelVideo();
            }
            else if (modelType == "2D")
            {
                SaveETGraphOntoModel2D();
            }
        }

        /// <summary>
        /// Saves the ETGraph onto the model for 3D models.
        /// </summary>
        public void SaveETGraphOntoModel3D()
        {
            string[] files = Directory.GetFiles(SourceLocation);
            List<string> images = new List<string>();
            List<string> dataFiles = new List<string>();
            //check that all files are images
            foreach (string str in files)
            {
                if (Path.GetExtension(str).Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) || Path.GetExtension(str).Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase))
                {
                    images.Add(str);
                }
                else if (Path.GetExtension(str).Equals(".txt", StringComparison.InvariantCultureIgnoreCase))
                {
                    dataFiles.Add(str);
                }
                else
                {

                }
            }
            try
            {
                for (int i = 0; i < dataFiles.Count; i++)
                {
                    OpenETGraphData(SourceLocation, (ModelName + "" + i));
                    Bitmap bitmap = new Bitmap(SourceLocation + "\\" + ModelName + "" + (i + 2) + ".jpg");
                    if (py.Count == 0 || px.Count == 0)
                    {
                        throw new ArgumentNullException();
                    }

                    Image canvas = createETGraph(bitmap, px, py, i, 10);
                    canvas.Save(SourceLocation + "\\" + ModelName + "" + (i + 2) + ".ETGraph.jpg", ImageFormat.Jpeg);
                }
            }
            catch (Exception er)
            {
                return;
            }
        }

        /// <summary>
        /// Saves the ETGraph onto the model for Video models
        /// </summary>
        public void SaveETGraphOntoModelVideo()
        {
            //get heights and widths of video
            VideoFileReader vid = new VideoFileReader();
            vid.Open(SourceLocation/*+ModelName+".wmv"*/);
            height = vid.Height;
            width = vid.Width;
            vid.Close();

            List<Thread> tl = new List<Thread>();
            ImageGenerator ig = new ImageGenerator();
            try
            {
                ig.VideoPath = SourceLocation;
                ig.DestinationPath = DestinationPath + "\\";
                ig.ModelName = ModelName;
                ig.createImages();
            }
            catch (Exception e)
            {
                throw new TypeLoadException();
            }

            if (py.Count == 0 || px.Count == 0)
            {
                throw new ArgumentNullException();
            }

            VideoGenerator vm = new VideoGenerator();
            List<float> x = new List<float>();
            List<float> y = new List<float>();

            //ImageGenerator im = new ImageGenerator();
            x.Add(0);
            y.Add(0);
            for (int i = 0; i < px.Count; i++)
            {

                try
                {
                    Bitmap bitmap = new Bitmap(width, height);
                    if (x.Count > 150)
                    {
                        x.Add(px.ElementAt(i));
                        y.Add(py.ElementAt(i));
                        x.RemoveAt(0);
                        y.RemoveAt(0);
                    }
                    else
                    {
                        x.Add(px.ElementAt(i));
                        y.Add(py.ElementAt(i));
                    }

                    Image im = new Bitmap(DestinationPath + "\\" + ModelName + "frame" + (i) + ".jpg");

                    Image canvas = createETGraph(im, px, py, i, 10);
                    //im.Dispose();
                    canvas.Save(DestinationPath + "\\" + ModelName + ".ETGraph" + "frame" + (i) + ".jpg", ImageFormat.Jpeg);
                    im.Dispose();
                }
                catch (Exception k)
                {
                    break;
                }
            }

            //call create video
            vm.ImagePath = DestinationPath + "\\";
            vm.DestinationPath = DestinationPath + "\\";
            vm.ModelName = ModelName + ".ETGraph";
            vm.FrameWidth = width;
            vm.FrameHeight = height;
            vm.createVideo();
            ig.deleteImages();
        }

        /// <summary>
        /// Saves the ETGraph onto the model for 2D models
        /// </summary>
        public void SaveETGraphOntoModel2D()
        {
            Image im = new Bitmap(SourceLocation + "\\" + ModelName);
            if (py.Count == 0 || px.Count == 0)
            {
                throw new ArgumentNullException();
            }
            Image canvas = createETGraph(im, px, py, 0, 10);
            canvas.Save(SourceLocation + "\\" + ModelName + ".ETGraph.Jpg", ImageFormat.Jpeg);
        }

        static bool HasCorrectExtension(string filename)
        {
            // add other possible extensions here
            return Path.GetExtension(filename).Equals(".jpg", StringComparison.InvariantCultureIgnoreCase)
                || Path.GetExtension(filename).Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase)
                || Path.GetExtension(filename).Equals(".txt", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
