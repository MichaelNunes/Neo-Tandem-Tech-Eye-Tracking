﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeatMap.NET;
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
    public class Heatmaps
    {
        #region properties and getter/setters
        /// <summary>
        /// Represents file(s) locations that will be used for the heatmapping processess.
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
        /// The height of the image, heatmap will be generated for
        /// </summary>
        private int height;
        public int _height
        {
            get { return height; }
            set { height = value; }
        }


        /// <summary>
        /// The width of the image, heatmap will be generated for.
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
        #endregion

        #region Constructors
        public Heatmaps()
        {

        }

        public Heatmaps(int Width, int Height)
        {
            width = Width;
            height = Height;
        }

        public Heatmaps(string NameOfModel, string FilePath, int Width, int Height, string ModelType)
        {
            width = Width;
            height = Height;
            ModelName = NameOfModel;
            SourceLocation = FilePath;
            modelType = ModelType;
        }

        public Heatmaps(string NameOfModel, string FilePath)
        {
            ModelName = NameOfModel;
            SourceLocation = FilePath;
        }
        #endregion

        //readfile
        /// <summary>
        /// Opens the heatmap data for creation of heatmap to relevent image.
        /// </summary>
        /// <param name="fileLocation"> specifies file location</param>
        /// <param name="modelName">specifies model name</param>
        public void OpenHeatmapData(string fileLocation, string modelName)
        {
            px = null;
            py = null;
            px = new List<float>();
            py = new List<float>();
            string[] lines = System.IO.File.ReadAllLines(fileLocation + "\\RecordedData_" + modelName + ".txt");
            foreach (string item in lines)
            {
                px.Add((float)Convert.ToDouble(item.Substring(0, item.IndexOf(":"))));

                int temp1 = item.IndexOf(":")+1;
                int temp2 = item.Length-temp1;
                py.Add((float)Convert.ToDouble(item.Substring(temp1,temp2)));
            }
        }

        /// <summary>
        /// Mediates which model is being worked with.
        /// </summary>
        public void saveHeatmap()
        {
            if(modelType == "3D")
            {
                SaveHeatmap3D();
            }
            else if (modelType == "Video")
            {
                SaveHeatmapVideo();
            }
            else if (modelType == "2D")
            {
                SaveHeatmap2D();
            }
        }


        /// <summary>
        /// Saves only the heatmap as an image for 3D models
        /// </summary>
        public void SaveHeatmap3D()
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
                    OpenHeatmapData(SourceLocation, (ModelName + "" + i));
                    Bitmap bitmap = new Bitmap(width, height);
                    if (py.Count == 0 || px.Count == 0)
                    {
                        throw new ArgumentNullException();
                    }

                    Image canvas = HeatMap.NET.HeatMap.GenerateHeatMap(bitmap, px.ToArray(), py.ToArray());
                    canvas.Save(SourceLocation + "\\" + ModelName + "" + (i + 2) + ".Heatmap.jpg", ImageFormat.Jpeg);
                }
            }
            catch(Exception ef)
            {
                return;
            }
        }

        /// <summary>
        /// Saves only the heatmap as an image for Video models
        /// </summary>
        public void SaveHeatmapVideo()
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
                    x.Add(px.ElementAt(i));
                    y.Add(py.ElementAt(i));

                    if (py.Count() == 0 || px.Count() == 0)
                    {
                        throw new ArgumentNullException();
                    }
                    
                    Thread t = new Thread(() => SaveHeatmapImage(bitmap, x, y, i)).Start();
                    tl.Add(t);
                }
                catch (Exception k)
                {
                    break;
                }
            }
            foreach(Thread t in tl)
            {
                t.Join();
            }

            //call create video
            vm.ImagePath = DestinationPath + "\\";
            vm.DestinationPath = DestinationPath + "\\";
            vm.ModelName = ModelName + ".Heatmap";
            vm.FrameWidth = width;
            vm.FrameHeight = height;
            vm.Fps = 30;
            vm.createVideo();
        }
 
        public void SaveHeatmapImage(Bitmap bitmap, List<float> x, List<float> y, int i)
        {
            Image canvas = HeatMap.NET.HeatMap.GenerateHeatMap(bitmap, x.ToArray(), y.ToArray());
            canvas.Save(DestinationPath + "\\" + ModelName + ".Heatmap" + "frame" + i + ".jpg", ImageFormat.Jpeg);
            bitmap.Dispose();
        }

        /// <summary>
        /// Saves only the heatmap as an image for 2D models
        /// </summary>
        public void SaveHeatmap2D()
        {
            Bitmap bitmap = new Bitmap(width, height);
            if (py.Count == 0 || px.Count == 0)
            {
                throw new ArgumentNullException();
            }

            Image canvas = HeatMap.NET.HeatMap.GenerateHeatMap(bitmap, px.ToArray(), py.ToArray());
            canvas.Save(SourceLocation + "\\" + ModelName + ".Heatmap.jpg", ImageFormat.Jpeg);   
        }

        /// <summary>
        /// Mediates which model is being worked with.
        /// </summary>
        public void saveHeatmapOntoModel()
        {
            if (modelType == "3D")
            {
                SaveHeatmapOntoModel3D();
            }
            else if (modelType == "Video")
            {
                SaveHeatmapOntoModelVideo();
            }
            else if (modelType == "2D")
            {
                SaveHeatmapOntoModel2D();
            }
        }

        /// <summary>
        /// Saves the heatmap onto the model for 3D models.
        /// </summary>
        public void SaveHeatmapOntoModel3D()
        {
            string[] files = Directory.GetFiles(SourceLocation);
            List<string> images = new List<string>();
            List<string> dataFiles = new List<string>();
            //check that all files are images
            foreach (string str in files)
            {
                if(Path.GetExtension(str).Equals(".jpg", StringComparison.InvariantCultureIgnoreCase)||Path.GetExtension(str).Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase))
                {
                    images.Add(str);
                }
                else if(Path.GetExtension(str).Equals(".txt", StringComparison.InvariantCultureIgnoreCase))
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
                    OpenHeatmapData(SourceLocation, (ModelName + "" + i));
                    Bitmap bitmap = new Bitmap(SourceLocation + "\\" + ModelName + "" + (i + 2) + ".jpg");
                    if (py.Count == 0 || px.Count == 0)
                    {
                        throw new ArgumentNullException();
                    }

                    Image canvas = HeatMap.NET.HeatMap.GenerateHeatMap(bitmap, px.ToArray(), py.ToArray());
                    canvas.Save(SourceLocation + "\\" + ModelName + "" + (i + 2) + ".Heated.jpg", ImageFormat.Jpeg);
                }
            }
            catch(Exception er)
            {
                return;
            }
        }

        /// <summary>
        /// Saves the heatmap onto the model for Video models
        /// </summary>
        public void SaveHeatmapOntoModelVideo()
        {
            //get heights and widths of video
            VideoFileReader vid = new VideoFileReader();
            vid.Open(SourceLocation/*+ModelName+".wmv"*/);
            height = vid.Height;
            width = vid.Width;
            vid.Close();

            ImageGenerator ig = new ImageGenerator();
            try
            {
                ig.VideoPath = SourceLocation;
                ig.DestinationPath = DestinationPath+"\\";
                ig.ModelName =  ModelName; 
                ig.createImages();
            }
            catch(Exception e)
            {
                throw new TypeLoadException();
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
                    x.Add(px.ElementAt(i));
                    y.Add(py.ElementAt(i));

                    Image im = new Bitmap(DestinationPath + "\\" + ModelName + "frame" + (i) + ".jpg");

                    if (py.Count == 0 || px.Count == 0)
                    {
                        throw new ArgumentNullException();
                    }
                    Image canvas = HeatMap.NET.HeatMap.GenerateHeatMap(im, x.ToArray(), y.ToArray());
                    //im.Dispose();
                    canvas.Save(DestinationPath + "\\" + ModelName + ".Heated" + "frame" + (i) + ".jpg", ImageFormat.Jpeg);
                    im.Dispose();
                }
               catch(Exception k)
                {
                    break;
                }
            }

            //call create video
            vm.ImagePath = DestinationPath + "\\";
            vm.DestinationPath = DestinationPath + "\\";
            vm.ModelName = ModelName+".Heated";
            vm.FrameWidth = width;
            vm.FrameHeight = height;
            vm.createVideo();
            ig.deleteImages();
        }

        /// <summary>
        /// Saves the heatmap onto the model for 2D models
        /// </summary>
        public void SaveHeatmapOntoModel2D()
        {
            Image im = new Bitmap(SourceLocation + "\\" + ModelName);
            if (py.Count == 0 || px.Count == 0)
            {
                throw new ArgumentNullException();
            }
            Image canvas = HeatMap.NET.HeatMap.GenerateHeatMap(im, px.ToArray(), py.ToArray());
            canvas.Save(SourceLocation + "\\" + ModelName + ".Heated.Jpg", ImageFormat.Jpeg);
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
