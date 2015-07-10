﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splicer.Timeline;
using Splicer.Renderer;
using Splicer.WindowsMedia;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Video_Model
{
    public class VideoGenerator
    {
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

        public VideoGenerator()
        {
            imagePath = "";
            modelName = "";
            destinationPath = @"C:\Users\Public\Videos\Sample Videos\";
            frameWidth = 720;
            frameHeight = 480;
            fps = 25;
        }

        public VideoGenerator(string path)
        {
            imagePath = path;
            modelName = "VideoModeli";
            frameWidth = 720;
            frameHeight = 480;
            fps = 25;
        }

        public VideoGenerator(string path, string name, int width, int height, int framesPS)
        {
            imagePath = path;
            modelName = name;
            frameWidth = width;
            frameHeight = height;
            fps = framesPS;
        }

        public void createVideo()
        {
            destinationPath += modelName + ".wmv";
            using (ITimeline timeline = new DefaultTimeline(fps))
            {
                string[] files = Directory.GetFiles(imagePath);
                List<string> images = new List<string>();
                List<string> dataFiles = new List<string>();
                foreach(string str in files)
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

                IGroup group = timeline.AddVideoGroup(32, frameWidth, frameHeight);

                ITrack videoTrack = group.AddTrack();

                // load images
                IClip[] clips = new IClip[files.Length];
                for (int i = 0; i < images.Count(); i++)
                {
                    clips[i] = videoTrack.AddImage(images.ElementAt(i), 0, 2);
                }

                ITrack audioTrack = timeline.AddAudioGroup().AddTrack();
                //IClip audio = audioTrack.AddAudio(@"C:\Users\Public\Music\Sample Music\Kalimba.mp3", 0, videoTrack.Duration);
                
                //output video profile
                string profilePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Splicer_Profile_1280x720.prx";
                string profile = new StreamReader(profilePath).ReadToEnd();

                using (WindowsMediaRenderer renderer = new WindowsMediaRenderer(timeline, destinationPath, profile))
                {
                    renderer.Render();
                }
            }
        }


    }
}
