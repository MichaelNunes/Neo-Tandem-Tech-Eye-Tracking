using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.FFMPEG;
//using DotImaging;

namespace Video_Model
{
    public class VideoGenerator
    {
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Compiling video...");
            try
            {
                new VideoGenerator(args[0], args[1], Convert.ToInt32(args[2]), Convert.ToInt32(args[3]), Convert.ToInt32(args[4])).createVideo();


            }
            catch(Exception e)
            {
                
                Console.WriteLine(e.Message);
                File.AppendAllText("info.txt", e + " ");
                foreach (var x in args)
                {
                    File.AppendAllText("info.txt", x + " ");
                }
            }
            //new VideoGenerator(@"C:\Users\Duran\Desktop\NTT Testing models\test\", "Wildlife.wmv", 1280, 720, 30).createVideo();
            Console.WriteLine("Finished compiling video.");
        }

        static VideoFileWriter writer;
        static Bitmap videoFrame;

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
            destinationPath = path;
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

            //DotImaging.Image<DotImaging.Rgb> frame;// = new DotImaging.IImage(DestinationPath + modelName + ".wmv");
            
            
            //ImageStreamWriter writer = new VideoWriter(DestinationPath + modelName + ".wmv", new DotImaging.Primitives2D.Size(5, 5));
            //Emgu.CV.VideoWriter writer = new Emgu.CV.VideoWriter(DestinationPath + modelName + ".wmv", 30, frameWidth, frameHeight, true);
            //DotImaging.Bgr<byte> frame = new DotImaging.Bgr<byte>(new Bitmap(""));
            //writer = new VideoWriter(DestinationPath + modelName, new DotImaging.Primitives2D.Size(frameWidth, frameHeight),20f);

            //writer.Open();

            int count = 0;

            try
            {
                while (true)
                {
                    videoFrame = new Bitmap(imagePath + modelName + "frame" + count++ + ".jpg");

                    writer.WriteVideoFrame(videoFrame);

                    videoFrame.Dispose();
                }
                /*while(true)
                {
                    ImageStreamReader reader = new FileCapture(imagePath + modelName + "frame" + count++ + ".jpg");
                    foreach (var x in reader)
                    {
                        writer.Write(x);
                        count++;
                        x.Dispose();
                    }
                    reader.Dispose();
                }*/
            }
            catch(System.AccessViolationException e)
            {
                Console.WriteLine("VIOLATION");
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("VIOLATION");
                //createVideo();
            }
            catch(Exception e)
            {
                Console.WriteLine("Video creation done with image count {0}.", count);
                //Console.WriteLine("EXCEPTION VIDEO: Count=" + count + e);
                //Console.ReadLine();
                //writer.Dispose();
            }
            finally
            {
                writer.Close();
                writer.Dispose();
            }
        }
    }
}
