using System;
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

        public VideoGenerator()
        {
            imagePath = "";
            modelName = "";
            frameWidth = 720;
            frameHeight = 480;
            fps = 25;
        }

        public VideoGenerator(string path)
        {
            imagePath = path;
        }

        public VideoGenerator(string path, string name, int width, int height)
        {
            imagePath = path;
            modelName = name;
            frameWidth = width;
            frameHeight = height;
        }

        public void createVideo()
        {
            using (ITimeline timeline = new DefaultTimeline(fps))
            {
                string[] files = Directory.GetFiles(imagePath);

                IGroup group = timeline.AddVideoGroup(32, frameWidth, frameHeight);

                ITrack videoTrack = group.AddTrack();

                // load images
                IClip[] clips = new IClip[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    clips[i] = videoTrack.AddImage(files[i], 0, 2);
                }

                double halfDuration = 0.5;

                // effects
                for (int i = 1; i < files.Length; i++)
                {
                    group.AddTransition(clips[i].Offset - halfDuration, halfDuration, StandardTransitions.CreateFade(), true);
                    group.AddTransition(clips[i].Offset, halfDuration, StandardTransitions.CreateFade(), false);
                }

                // audio track
                ITrack audioTrack = timeline.AddAudioGroup().AddTrack();
                IClip audio = audioTrack.AddAudio(@"C:\Users\COS301\Documents\Visual Studio 2013\Projects\audio\Kalimba.mp3", 0, videoTrack.Duration);

                audioTrack.AddEffect(0, audio.Duration, StandardEffects.CreateAudioEnvelope(1.0, 1.0, 1.0, audio.Duration));

                using (WindowsMediaRenderer renderer = new WindowsMediaRenderer(timeline, @"C:\Users\COS301\Documents\Visual Studio 2013\Projects\images\output.wmv", WindowsMediaProfiles.HighQualityVideo))
                {
                    renderer.Render();
                }
            }
        }
    }
}
