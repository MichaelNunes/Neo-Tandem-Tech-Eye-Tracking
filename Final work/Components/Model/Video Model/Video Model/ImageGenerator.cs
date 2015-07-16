using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.FFMPEG;
using System.Drawing.Imaging;


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

        /// <summary>
        /// Default Constructor, sets basic settings for image generator
        /// </summary>
        public ImageGenerator()
        {
            destinationPath = @"C:\Users\Public\Pictures\Sample Pictures";
            reader = new VideoFileReader();
        }

        public ImageGenerator(string inPath, string outPath)
        {
            videoPath = inPath;
            destinationPath = outPath;
            reader = new VideoFileReader();
            if (File.Exists(videoPath))
            {
                reader.Open(videoPath);
            }
            else
            {
                throw new Exception("Video does not exist"); 
            }
        }

        /// <summary>
        /// generates a sequence of images from a video. 
        /// Amount of images generated depends on the ips (images per second) variable 
        /// </summary>
        public int createImages()
        {
            if (reader.IsOpen == false)
                throw new Exception("Video file is not open.");

            int count = 0;

            try
            {
                while (true)
                {
                    Bitmap videoFrame = reader.ReadVideoFrame();

                    videoFrame.Save(destinationPath + "frame" + count++ + ".jpg", ImageFormat.Jpeg);

                    videoFrame.Dispose();
                }
            }
            catch(Exception e)
            {
                reader.Dispose();
                reader.Close();
                return count;
            }
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
