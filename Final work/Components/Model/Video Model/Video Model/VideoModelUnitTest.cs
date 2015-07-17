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

        string imagesDir = @"C:\Users\Public\Videos\Sample Videos\TestData\Images\";
        string videosDir = @"C:\Users\Public\Videos\Sample Videos\TestData\Videos\";
        string testVideo = @"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv";

        [SetUp]
        public void Init()
        {
            ig = new ImageGenerator(testVideo, imagesDir);

            vg = new VideoGenerator(imagesDir, videosDir + "UnitTestVideo", 1280, 720, 30);
        }

        [Test]
        public void createVideo()
        {
            vg.createVideo();

            Assert.IsTrue(File.Exists(@"C:\Users\Public\Videos\Sample Videos\TestData\Videos\UnitTestVideo.wmv"));
         
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
