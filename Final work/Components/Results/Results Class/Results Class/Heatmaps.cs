using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeatMap.NET;
using System.Drawing;
using System.Drawing.Imaging;
using Exception;
using Video_Model; 

namespace Results_Class
{
    public class Heatmaps
    {
        #region properties
        string FileLocation;

        public string _fileLocation
        {
            get { return FileLocation; }
            set { FileLocation = value; }
        }

        string ModelName;
        public string _modelName
        {
            get { return ModelName; }
            set { ModelName = value; }
        }


        public List<float> px = new List<float>();
        public List<float> Px
        {
            get { return px; }
            set { px = value; }
        }

        public List<float> py = new List<float>();
        public List<float> Py
        {
            get { return py; }
            set { py = value; }
        }
        #endregion

        public Heatmaps()
        {

        }

        //readfile
        public void OpenHeatmapData(string fileLocation, string modelName)
        {
            px = new List<float>();
            py = new List<float>();
            string[] lines = System.IO.File.ReadAllLines(fileLocation + "\\" + modelName + ".txt");
            foreach (string item in lines)
            {
                px.Add((float)Convert.ToDouble(item.Substring(0, item.IndexOf(",")-1)));
                py.Add((float)Convert.ToDouble(item.Substring(item.IndexOf(",")+1, item.Length-1)));
            }
        }

        //saveOverlay
        public void saveOverlay(string ModelType)
        {
            if(ModelType == "3D")
            {
                SaveHeatmap3D();
            }
            else if(ModelType == "Video")
            {
                SaveHeatmapVideo();
            }
            else if(ModelType == "2D")
            {
                SaveHeatmap2D();
            }
        }

        public void SaveHeatmap3D()
        {
            
        }

        public void SaveHeatmapVideo()
        {
            VideoGenerator vm = new VideoGenerator();
            List<float> x = new List<float>();
            List<float> y = new List<float>();

            for(int i = 0; i < px.Count; i++)
            {
                x.Add(px.ElementAt(i));
                y.Add(py.ElementAt(i));

                Image im = new Bitmap(FileLocation + "\\" + ModelName);
                if (py.Count == 0 || px.Count == 0)
                {
                    throw new ArgumentNullException();
                }
                Image canvas = HeatMap.NET.HeatMap.GenerateHeatMap(im, px.ToArray(), py.ToArray());
                canvas.Save(FileLocation + "\\" + ModelName + " " + i +".Heated.Jpg", ImageFormat.Jpeg);   
            }
            //call create video
        }

        public void SaveHeatmap2D()
        {
            Image im = new Bitmap(FileLocation + "\\" + ModelName);
            if (py.Count == 0 || px.Count == 0)
            {
                throw new ArgumentNullException();
            }
            Image canvas = HeatMap.NET.HeatMap.GenerateHeatMap(im, px.ToArray(), py.ToArray());
            canvas.Save(FileLocation + "\\" + ModelName + ".Heated.Jpg", ImageFormat.Jpeg);   
        }
    }
}
