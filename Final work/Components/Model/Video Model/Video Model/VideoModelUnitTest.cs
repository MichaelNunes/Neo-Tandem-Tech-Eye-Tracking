using System;
using NUnit.Framework;
using System.IO;

namespace Video_Model
{

    [TestFixture]
    public class Class1
    {
        //VideoGenerator vMaker;
        //ImageGenerator iMaker;

        ImageGenerator ig;
        VideoGenerator vg;

        string imagesDir = @"C:\Users\Duran\Desktop\videoTracking\test\";
        string videosDir = @"C:\Users\Duran\Desktop\videoTracking\test\";
        string testVideo = @"C:\Users\Duran\Desktop\videoTracking\test\Wildlife.wmv";

        [SetUp]
        public void Init()
        {
            ig = new ImageGenerator(testVideo, imagesDir);

            vg = new VideoGenerator(imagesDir, videosDir + "Wildlife.wmv", 1280, 720, 30);
        }

        [Test]
        public void createVideo()
        {
            vg.DestinationPath = vg.ModelName;
            vg.ModelName = "";
            vg.createVideo();

            Assert.IsTrue(File.Exists(@"C:\Users\Public\Videos\Sample Videos\TestData\Video\UnitTestVideo.wmv"));
         
        }

        [Test]
        public void createImages()
        {
            int imageCount = ig.createImages();

            Assert.IsTrue(File.Exists(@"C:\Users\Duran\Desktop\videoTracking\test\frame0.jpg"));
        }

        [Test]
        public void deleteImages()
        {
            ig.deleteImages();

            Assert.IsFalse(File.Exists(@"C:\Users\Duran\Desktop\videoTracking\test\frame0.jpg"));
        }

    }
}
