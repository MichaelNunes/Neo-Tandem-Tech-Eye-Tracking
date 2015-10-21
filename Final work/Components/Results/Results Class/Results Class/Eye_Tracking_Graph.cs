using System;
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
using System.Diagnostics;

namespace Results_Class
{
    public class Eye_Tracking_Graph
    {
        /// <summary>
        /// Holds a list of points that have grown due to their close proximity to other points.
        /// </summary>
        List<List<int>> growingPoints = new List<List<int>>(); // [0] = point, [1] = pointSize, [2] = age

        int srcHeight, srcWidth;
        float scaleFactorH, scaleFactorW;
        int pointHistory;

        bool togglePointNumbers;
        public bool _TogglePointNumbers
        {
            get { return togglePointNumbers; }
            set { togglePointNumbers = value; }
        }

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

        public Eye_Tracking_Graph(int pntHistory, bool TogglePntNums)
        {
            pointHistory = pntHistory;
            togglePointNumbers = TogglePntNums;
        }

        public Eye_Tracking_Graph(int pntHistory, bool TogglePntNums, string NameOfModel, string FilePath, string ModelType)
        {
            pointHistory = pntHistory;
            togglePointNumbers = TogglePntNums;
            ModelName = NameOfModel;
            SourceLocation = FilePath;
            modelType = ModelType;
        }

        public Eye_Tracking_Graph(int pntHistory, bool TogglePntNums, string NameOfModel, string FilePath)
        {
            pointHistory = pntHistory;
            togglePointNumbers = TogglePntNums;
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
        Image createETGraph(Image img, List<float> X, List<float> Y, int currentPoint)
        {
            using (var graphics = Graphics.FromImage(img))
            {
                int startPoint = currentPoint - pointHistory;

                if (startPoint < 0)
                {
                    startPoint = 0;
                }

                int endPoint = currentPoint;
                SolidBrush redBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                Font drawFont = new System.Drawing.Font("Arial", 16);
                SolidBrush greenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
                Pen redPen = new Pen(Color.Red, 1);
                int pointNumber = 0, growth = 0, gpa, cpa; //gpa = growpingPointArea, cpa = currentPointArea
                bool inGPList;
                for (int i = startPoint; i < endPoint; i++)
                {
                    gpa = 10;
                    cpa = 10;
                    inGPList = false;

                    for (int j = 0; j < growingPoints.Count; j++) // checks if current point is in growingPoints list
                    {                                             //if yes then get its current growth size.      
                        if (growingPoints[j][0] == i)
                        {
                            cpa = growingPoints[j][1];
                            inGPList = true;
                            break;
                        }
                    }
                    if (i > 0 && // checks if currentPoint is near previous point
                        (X[i] > (X[i - 1] - cpa)) && (X[i] < (X[i - 1] + cpa)) &&
                        (Y[i] > (Y[i - 1] - cpa)) && (Y[i] < (Y[i - 1] + cpa)))
                    {
                        if (growingPoints.Count < 1) //if growingPoints list is empty than use default growth size
                        {
                            growth = 4;
                        }
                        else
                        {
                            for (int j = 0; j < growingPoints.Count; j++) //else check if growingPoints list has previous Point
                            {
                                if (growingPoints[j][0] == (i - 1))
                                {

                                    //if not then add previous Point growth size into current point growth size 
                                    growth = growingPoints[j][1] + 4;
                                    if (growth > (10 + (10 * 4))) { growth = 10; }

                                    break;
                                }
                            }
                            if (growth == 0) { growth = 4; } //if previous point is not in growingPoint list then default growth size
                        }
                        gpa += growth;
                        if (!inGPList) //add new growthPoint to list if its not in list already
                        {
                            List<int> newGP = new List<int>();
                            newGP.Add(i);
                            newGP.Add(growth);
                            newGP.Add(0);
                            growingPoints.Add(newGP);
                        }
                    }
                    graphics.FillEllipse(redBrush, new Rectangle((int)X[i] - (gpa / 2), (int)Y[i] - (gpa / 2), gpa, gpa));
                    if (togglePointNumbers) { graphics.DrawString(pointNumber.ToString(), drawFont, greenBrush, X[i] - 5, Y[i] - 5); }
                    if ((endPoint < 10 && i > 0) || (endPoint >= 10 && i > startPoint))
                    {
                        graphics.DrawLine(redPen, X[i], Y[i], X[i - 1], Y[i - 1]);
                    }
                    pointNumber++;
                }
                for (int j = 0; j < growingPoints.Count; j++)
                {
                    growingPoints[j][2]++;
                    if (growingPoints[j][2] > 9)
                    {
                        growingPoints.RemoveAt(j);
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
            int Count = 0;
            foreach (string item in lines)
            {
                if (Count == 0)
                {
                    srcWidth = (int)Convert.ToInt32(item.Substring(0, item.IndexOf("x")));
                    int temp1 = item.IndexOf("x") + 1;
                    int temp2 = item.Length - temp1;
                    srcHeight = (int)Convert.ToInt32(item.Substring(temp1, temp2));
                    scaleFactorH = ((float)height / (float)srcHeight);
                    scaleFactorW = ((float)width / (float)srcWidth);
                    Count++;
                    Console.WriteLine(scaleFactorH.ToString() + ":" + scaleFactorW.ToString());
                }
                else
                { 
                    px.Add(scaleFactorW * (float)Convert.ToDouble(item.Substring(0, item.IndexOf(":"))));
                    int temp1 = item.IndexOf(":") + 1;
                    int temp2 = item.Length - temp1;
                    py.Add(scaleFactorH * (float)Convert.ToDouble(item.Substring(temp1, temp2)));
                }
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
                if (Path.GetExtension(str).Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) || Path.GetExtension(str).Equals(".bmp", StringComparison.InvariantCultureIgnoreCase))
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
                    Bitmap temp = new Bitmap(SourceLocation + "\\" + ModelName + "view" + (i + 1) + ".bmp");
                    width = temp.Width;
                    height = temp.Height;
                    temp.Dispose();

                    OpenETGraphData(SourceLocation, "view" + (i + 1));
                    Bitmap bitmap = new Bitmap(width, height);//SourceLocation + "\\" + ModelName + "view" + (i + 1) + ".bmp");
                    if (py.Count == 0 || px.Count == 0)
                    {
                        throw new ArgumentNullException();
                    }
                    Image canvas = createETGraph(bitmap, px, py, i);
                    canvas.Save(SourceLocation + "\\" + ModelName + "" + (i + 1) + ".ETGraph.jpg", ImageFormat.Jpeg);

                    bitmap.Dispose();
                    canvas.Dispose();
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
            vid.Open(SourceLocation);
            height = vid.Height;
            width = vid.Width;
            vid.Close();
            OpenETGraphData(DestinationPath, ModelName);
            //VideoGenerator vm = new VideoGenerator();
            List<float> x = new List<float>();
            List<float> y = new List<float>();

            if (py.Count() == 0 || px.Count() == 0)
            {
                throw new ArgumentNullException();
            }

            List<Thread> tl = new List<Thread>();
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
                    SaveETGraphImage(bitmap, x, y, i);

                    bitmap.Dispose();
                }
                catch (Exception k)
                {
                    break;
                }
            }

            //call create video
            /*vm.ImagePath = DestinationPath + "\\";
            vm.DestinationPath = DestinationPath + "\\";
            vm.ModelName = ModelName + ".ETGraph";
            vm.FrameWidth = width;
            vm.FrameHeight = height;
            vm.Fps = 30;
            vm.createVideo();*/
            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = ("\"" + DestinationPath + "\\" + "\"") + " " + ("\"" + ModelName + ".ETGraph" + "\"") + " " + width + " " + height + " " + 30;
            start.FileName = @"Video Model.exe";
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;

            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
            }
        }

        public void SaveETGraphImage(Bitmap bitmap, List<float> x, List<float> y, int i)
        {
            try
            {
                Image canvas = createETGraph(bitmap, px, py, i);
                canvas.Save(DestinationPath + "\\" + ModelName + ".ETGraph" + "frame" + i + ".jpg", ImageFormat.Jpeg);
                
                //bitmap.Dispose();
                canvas.Dispose();
            }
            catch(Exception e)
            {
                SaveETGraphImage(bitmap, x, y, i);
            }
        }

        /// <summary>
        /// Saves only the ETGraph as an image for 2D models
        /// </summary>
        public void SaveETGraph2D()
        {
            Bitmap temp = new Bitmap(SourceLocation + "\\" + ModelName);
            width = temp.Width;
            height = temp.Height;
            temp.Dispose();

            OpenETGraphData(SourceLocation, ModelName);
            Bitmap bitmap = new Bitmap(width, height);
            if (py.Count == 0 || px.Count == 0)
            {
                throw new ArgumentNullException();
            }
            pointHistory = px.Count;
            Image canvas = createETGraph(bitmap, px, py, px.Count);
            canvas.Save(SourceLocation + "\\" + ModelName + ".ETGraph.jpg", ImageFormat.Jpeg);

            canvas.Dispose();
            bitmap.Dispose();
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
                if (Path.GetExtension(str).Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) || Path.GetExtension(str).Equals(".bmp", StringComparison.InvariantCultureIgnoreCase))
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
                    Bitmap bitmap = new Bitmap(SourceLocation + "\\" + ModelName + "view" + (i + 1) + ".bmp");
                    height = bitmap.Height;
                    width = bitmap.Width;
                    OpenETGraphData(SourceLocation, "view" + (i + 1));
                    if (py.Count == 0 || px.Count == 0)
                    {
                        throw new ArgumentNullException();
                    }

                    Image canvas = createETGraph(bitmap, px, py, i);
                    canvas.Save(SourceLocation + "\\" + ModelName + "" + (i + 2) + ".ETGraph.jpg", ImageFormat.Jpeg);

                    bitmap.Dispose();
                    canvas.Dispose();
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
            vid.Open(SourceLocation);
            height = vid.Height;
            width = vid.Width;
            vid.Close();
            OpenETGraphData(DestinationPath, ModelName);

            List<Thread> tl = new List<Thread>();
            ImageGenerator ig = new ImageGenerator();
            try
            {
                ig.VideoPath = SourceLocation;// +ModelName + ".wmv";
                ig.DestinationPath = DestinationPath;
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

            //VideoGenerator vm = new VideoGenerator();
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

                    Image canvas = createETGraph(im, px, py, i);
                    //im.Dispose();
                    canvas.Save(DestinationPath + "\\" + ModelName + ".ETGraph" + "frame" + (i) + ".jpg", ImageFormat.Jpeg);

                    im.Dispose();
                    canvas.Dispose();
                }
                catch (Exception k)
                {
                    break;
                }
            }

            //call create video
            /*vm.ImagePath = DestinationPath;
            vm.DestinationPath = DestinationPath;
            vm.ModelName = ModelName + ".ETGraph";
            vm.FrameWidth = width;
            vm.FrameHeight = height;
            vm.createVideo();
            ig.deleteImages();*/

            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = ("\"" + DestinationPath + "\\" + "\"") + " " + ("\"" + ModelName + ".ETGraph" + "\"") + " " + width + " " + height + " " + 30;
            start.FileName = @"Video Model.exe";
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;

            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
            }
            
        }

        /// <summary>
        /// Saves the ETGraph onto the model for 2D models
        /// </summary>
        public void SaveETGraphOntoModel2D()
        {

            Bitmap temp = new Bitmap(SourceLocation + "\\" + ModelName);
            width = temp.Width;
            height = temp.Height;
            temp.Dispose();

            OpenETGraphData(SourceLocation, ModelName);
            Image im = new Bitmap(SourceLocation + "\\" + ModelName);
            if (py.Count == 0 || px.Count == 0)
            {
                throw new ArgumentNullException();
            }
            pointHistory = px.Count;
            Image canvas = createETGraph(im, px, py, px.Count);
            canvas.Save(SourceLocation + "\\" + ModelName + ".ETGraphOver.Jpg", ImageFormat.Jpeg);

            canvas.Dispose();
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
