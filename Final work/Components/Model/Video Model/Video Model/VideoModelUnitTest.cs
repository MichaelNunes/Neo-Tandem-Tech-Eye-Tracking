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

        string imagesDir = @"C:\Users\Duran\Desktop\NTT tests\Recordings\Video\sanFran\";
        string videosDir = @"C:\Users\Duran\Desktop\NTT tests\Recordings\Video\sanFran\";
        string testVideo = @"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv";

        [SetUp]
        public void Init()
        {
            ig = new ImageGenerator(testVideo, imagesDir);

            vg = new VideoGenerator(imagesDir, videosDir + "SanFransico_v1.wmv", 1920, 888, 30);
        }

        [Test]
        public void createVideo()
        {
            vg.DestinationPath = vg.ModelName;
            vg.ModelName = "SanFransico_v1.wmv";
            vg.createVideo();

            Assert.IsTrue(File.Exists(@"C:\Users\Public\Videos\Sample Videos\TestData\Video\UnitTestVideo.wmv"));
         
        }

        [Test]
        public void createImages()
        {
            int imageCount = ig.createImages();

            Assert.IsTrue(File.Exists(@"C:\Users\Public\Videos\Sample Videos\TestData\Images\frame0.jpg"));
        }

        [Test]
        public void deleteImages()
        {
            ig.deleteImages();

            Assert.IsFalse(File.Exists(@"C:\Users\Public\Videos\Sample Videos\TestData\Images\frame0.jpg"));
        }

    }
}
