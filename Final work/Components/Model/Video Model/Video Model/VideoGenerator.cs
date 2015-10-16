using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.FFMPEG;

namespace Video_Model
{
    public class VideoGenerator
    {
        VideoFileWriter writer;

        string imagePath;

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }
        string modelName;

        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }
        int frameWidth;

        public int FrameWidth
        {
            get { return frameWidth; }
            set { frameWidth = value; }
        }

        int frameHeight;

        public int FrameHeight
        {
            get { return frameHeight; }
            set { frameHeight = value; }
        }

        int fps;

        public int Fps
        {
            get { return fps; }
            set { fps = value; }
        }

        string destinationPath;

        public string DestinationPath
        {
            get { return destinationPath; }
            set { destinationPath = value; }
        }

        /// <summary>
        /// Default Constructor, sets basic settings for video generator
        /// </summary>
        public VideoGenerator()
        {
            imagePath = "";
            modelName = "";
            destinationPath = @"C:\Users\Public\Videos\Sample Videos\";
            frameWidth = 720;
            frameHeight = 480;
            fps = 30;

            writer = new VideoFileWriter();
            
        }

        /// <summary>
        /// Constructor, sets the image path to import the images from. 
        /// All other settings are set to default
        /// </summary>
        /// <param name="path"></param>
        public VideoGenerator(string path)
        {
            imagePath = path;
            modelName = "VideoModeli";
            frameWidth = 720;
            frameHeight = 480;
            fps = 30;

            writer = new VideoFileWriter();
        }

        public VideoGenerator(string path, string name, int width, int height, int framesPS)
        {
            imagePath = path;
            modelName = name;
            frameWidth = width;
            frameHeight = height;
            fps = framesPS;

            writer = new VideoFileWriter();
        }

        public void createVideo()
        {
            writer.Open(DestinationPath + modelName + ".wmv", frameWidth, frameHeight, fps, VideoCodec.WMV1, 6000000);
            if (writer.IsOpen == false)
                throw new Exception("The video file is not open.");

            int count = 0;

            try
            {
                while (true)
                {
                    Bitmap videoFrame = new Bitmap(imagePath + modelName + "frame" + count++ + ".jpg");

                    writer.WriteVideoFrame(videoFrame);

                    videoFrame.Dispose();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("EXCEPTION VIDEO: Count=" + count + e);
                Console.ReadLine();
                writer.Dispose();
                writer.Close();
            }
        }
    }
}
