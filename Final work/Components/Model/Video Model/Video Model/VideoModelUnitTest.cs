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
        string testVideo = @"C:\Users\Public\Videos\Sample Videos\WildlifeAfter.wmv";

        [SetUp]
        public void Init()
        {
            ig = new ImageGenerator(testVideo, imagesDir);

            vg = new VideoGenerator(imagesDir, videosDir + "UnitTestVideo", 1280, 720, 30);

            /*vMaker = new VideoGenerator();

            vMaker.ImagePath = @"C:\Users\Duran\Desktop\NTT tests\Video Test";
            vMaker.ModelName = "Test";
            vMaker.FrameWidth = 720;
            vMaker.FrameHeight = 480;
            vMaker.Fps = 20;

            iMaker = new ImageGenerator();
            iMaker.VideoPath = @"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv";*/
        }

        [Test]
        public void createVideo()
        {
            vg.createVideo();
            Assert.IsTrue(File.Exists(@"C:\Users\Public\Videos\Sample Videos\TestData\Videos\UnitTestVideo.wmv"));
            /*vMaker.createVideo();
            Assert.AreEqual(@"C:\Users\Duran\Desktop\NTT tests\Video Test", vMaker.ImagePath);
            Assert.AreEqual("Test", vMaker.ModelName);
            Assert.AreEqual(720, vMaker.FrameWidth);
            Assert.AreEqual(480, vMaker.FrameHeight);
            Assert.AreEqual(20, vMaker.Fps);
            Assert.IsTrue(File.Exists(@"C:\Users\Duran\Desktop\NTT tests\Video Test" + vMaker.ModelName + ".wmv"));*/
        }

        [Test]
        public void createImages()
        {
            int imageCount = ig.createImages();

            Assert.IsTrue(File.Exists(@"C:\Users\Public\Videos\Sample Videos\TestData\Images\frame0.jpg"));

            /*for (int i = 0; i < imageCount; i++)
            {
                Assert.IsTrue(File.Exists(@"C:\Users\Public\Videos\Sample Videos\TestData\Images\frame" + i + ".jpg"));
            }*/
            
            /*iMaker.createImages();
            Assert.AreEqual(@"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv", iMaker.VideoPath);
            Assert.AreEqual(@"C:\Users\Public\Pictures\Sample Pictures", iMaker.DestinationPath);
            Assert.AreEqual(664, iMaker.ImageWidth);
            Assert.AreEqual(246, iMaker.ImageHeight);
            Assert.AreEqual(16, iMaker.BitCount);
            Assert.AreEqual(30, iMaker.Ips);*/
        }

        [Test]
        public void deleteImages()
        {
            ig.deleteImages();

            Assert.IsFalse(File.Exists(@"C:\Users\Public\Videos\Sample Videos\TestData\Images\frame0.jpg"));
            /*iMaker.deleteImages();
            Assert.IsTrue(!File.Exists(@"C:\Users\Public\Pictures\Sample Pictures\frame0.jpg"));*/
        }

    }
}
