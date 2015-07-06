using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Splicer.Timeline;
using Splicer.Renderer;
using Splicer.WindowsMedia;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Video_Model
{
    class ImageGenerator
    {
        string videoPath;

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

        int imageHeight;

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

        public ImageGenerator()
        {
            videoPath = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\Video Model Test\videos\output.wmv";
            destinationPath = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\Video Model Test\images\";
            imageHeight = 246;
            imageWidth = 664;
            bitCount = 16;
        }

        public ImageGenerator(string inPath, string outPath, int height, int width, short _bitCount)
        {
            videoPath = inPath;
            destinationPath = outPath;
            imageHeight = height;
            imageWidth = width;
            bitCount = _bitCount;
        }


        public void createImages()
        {
            using (DefaultTimeline timeline = new DefaultTimeline()) 
            {
                timeline.AddVideoGroup(bitCount, imageWidth, imageHeight).AddTrack(); // image bitcount & dimensions
                timeline.AddVideo(videoPath); // input video
                List<double> thresholds = new List<double>();
                double j = 0;
                for (double i = 0; i < 5000; i++) // set image per second (ips)
                {
                    thresholds.Add(j);
                    j += 0.1; //set ips to 10
                } 
                ImagesToDiskParticipant participant = new ImagesToDiskParticipant(bitCount, imageWidth, imageHeight, destinationPath, thresholds.ToArray()); 
                using (NullRenderer render = new NullRenderer(timeline, null, new ICallbackParticipant[] { participant })) 
                { 
                    render.Render(); 
                } 
           } 
       }
    }
}
