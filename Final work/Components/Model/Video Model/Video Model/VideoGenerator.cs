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
        string imagePath;
        VideoFileWriter writer;

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
            fps = 23;

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
            fps = 23;

            writer = new VideoFileWriter();
            writer.Open(modelName, frameWidth, frameHeight, fps, VideoCodec.WMV1 , 6000000);
        }

        /// <summary>
        /// Constructor, sets all video generator settings based on parameter values
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="framesPS"></param>
        public VideoGenerator(string path, string name, int width, int height, int framesPS)
        {
            imagePath = path;
            modelName = name;
            frameWidth = width;
            frameHeight = height;
            fps = framesPS;

            writer = new VideoFileWriter();
            writer.Open(modelName + ".wmv", frameWidth, frameHeight, fps, VideoCodec.WMV1, 6000000);
        }

        /// <summary>
        /// generates a video from a sequence of images. 
        /// Amount of frames per second in the generated video depends on the ips (images per second) variable 
        /// </summary>
        public void createVideo()
        {
            if (writer.IsOpen == false)
                throw new Exception("The video file is not open.");

            int count = 0;

            try
            {
                while (true)
                {
                    writer.WriteVideoFrame(new Bitmap(imagePath + "frame" + count++ + ".jpg"));
                }
            }
            catch(AForge.Video.VideoException e)
            {
                writer.Close();
            }

            Console.ReadLine();

            /*destinationPath += modelName + ".wmv";
            using (ITimeline timeline = new DefaultTimeline(fps))
            {
                string[] files = Directory.GetFiles(imagePath);
                List<string> images = new List<string>();
                foreach(string str in files)
                {
                    if (Path.GetExtension(str).Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) || Path.GetExtension(str).Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase))
                    {
                        images.Add(str);
                    }
                    else 
                    {

                    }
                }
                List<string> sortedImages = new List<string>();
                for(int i = 0; i < images.Count(); i++)
                {
                    try
                    {
                        sortedImages.Add(images.ElementAt(images.BinarySearch(ImagePath + "frame" + i + ".Jpg")));
                    }
                    catch(Exception e)
                    {

                    }
                }
    
                IGroup group = timeline.AddVideoGroup(32, frameWidth, frameHeight);

                ITrack videoTrack = group.AddTrack();

                // load images
                double ips = (double)1/fps;
                IClip[] clips = new IClip[sortedImages.Count()];
                for (int i = 0; i < sortedImages.Count(); i++)
                {
                    clips[i] = videoTrack.AddImage(sortedImages.ElementAt(i), 0, ips);
                }

                ITrack audioTrack = timeline.AddAudioGroup().AddTrack();
                
                //output video profile
                string profilePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Splicer_Profile_1280x720.prx";
                string profile = new StreamReader(profilePath).ReadToEnd();

                using (WindowsMediaRenderer renderer = new WindowsMediaRenderer(timeline, destinationPath, profile))
                {
                    renderer.Render();
                }
            }*/
        }
    }
}
