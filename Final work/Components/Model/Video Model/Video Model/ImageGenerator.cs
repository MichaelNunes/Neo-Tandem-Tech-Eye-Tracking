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
    public class ImageGenerator
    {
        string videoPath;
        VideoFileReader reader;

        public string VideoPath
        {
            get { return videoPath; }
            set { videoPath = value; }
        }

        string destinationPath;

        public string DestinationPath
        {
            get { return destinationPath; }
            set { destinationPath = value; }
        }

        /*int imageHeight;

        public int ImageHeight
        {
            get { return imageHeight; }
            set { imageHeight = value; }
        }
        int imageWidth;

        public int ImageWidth
        {
            get { return imageWidth; }
            set { imageWidth = value; }
        }

        short bitCount;

        public short BitCount
        {
            get { return bitCount; }
            set { bitCount = value; }
        }

        private int ips;

        public int Ips
        {
            get { return ips; }
            set { ips = value; }
        }*/

        /// <summary>
        /// Default Constructor, sets basic settings for image generator
        /// </summary>
        public ImageGenerator()
        {
            destinationPath = @"C:\Users\Public\Pictures\Sample Pictures";
            //imageHeight = 246;
            //imageWidth = 664;
            //bitCount = 16;
            //ips = 30;

            reader = new VideoFileReader();
        }

        /// <summary>
        /// Constructor, sets all image generator settings based on parameter values
        /// </summary>
        /// <param name="inPath"></param>
        /// <param name="outPath"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="_bitCount"></param>
        /// <param name="imagesPS"></param>
        public ImageGenerator(string inPath, string outPath)
        {
            videoPath = inPath;
            destinationPath = outPath;
            //imageHeight = height;
            //imageWidth = width;
            //bitCount = _bitCount;
            //ips = imagesPS;

            reader = new VideoFileReader();
            reader.Open(videoPath);
        }

        /// <summary>
        /// generates a sequence of images from a video. 
        /// Amount of images generated depends on the ips (images per second) variable 
        /// </summary>
        public int createImages()
        {
            if (reader.IsOpen == false)
                throw new Exception("Video file is not open.");

            Image videoFrame;
            int count = 0;

            try
            {
                while (true)
                {
                    videoFrame = reader.ReadVideoFrame();
                    videoFrame.Save(destinationPath + "frame" + count++ + ".jpg");
                }
            }
            catch(AForge.Video.VideoException e)
            {
                reader.Close();
                return count;
            }

            /*if (videoPath == "" || videoPath == null)
            {
                throw new ArgumentNullException();
            }

            using (DefaultTimeline timeline = new DefaultTimeline()) 
            {
                timeline.AddVideoGroup(bitCount, imageWidth, imageHeight).AddTrack(); // image bitcount & dimensions
                timeline.AddVideo(videoPath); // input video
                List<double> thresholds = new List<double>();

                double j = 0;
                for (double i = 0; i < 10000; i++) // set image per second (ips)
                {
                    j += (1 / ips); // set ips
                    thresholds.Add(j);
                } 

                ImagesToDiskParticipant participant = new ImagesToDiskParticipant(bitCount, imageWidth, imageHeight, destinationPath, thresholds.ToArray()); 
                using (NullRenderer render = new NullRenderer(timeline, null, new ICallbackParticipant[] { participant })) 
                { 
                    render.Render(); 
                } 
           } */
        }

        /// <summary>
        /// removes all the files (images) in a directory, excluding subdirectories.
        /// </summary>
        public void deleteImages()
        {
            DirectoryInfo deletionPath = new DirectoryInfo(destinationPath);
            foreach (FileInfo file in deletionPath.GetFiles())
            {
                file.Delete();
            }
        }
    }

    
}
