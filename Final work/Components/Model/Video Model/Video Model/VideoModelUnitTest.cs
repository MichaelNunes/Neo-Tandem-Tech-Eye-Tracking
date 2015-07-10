using System;
using NUnit.Framework;
using System.IO;

namespace Video_Model
{

    [TestFixture]
    public class Class1
    {
        VideoGenerator vMaker;
        ImageGenerator iMaker;

        [SetUp]
        public void Init()
        {
            vMaker = new VideoGenerator();

            vMaker.ImagePath = @"C:\Users\Public\Pictures\Sample Pictures";
            vMaker.ModelName = "Test";
            vMaker.FrameWidth = 720;
            vMaker.FrameHeight = 480;
            vMaker.Fps = 23;

            iMaker = new ImageGenerator();
            iMaker.VideoPath = @"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv";
        }

        [Test]
        public void createVideo()
        {
            vMaker.createVideo();
            Assert.AreEqual(@"C:\Users\Public\Pictures\Sample Pictures", vMaker.ImagePath);
            Assert.AreEqual("Test", vMaker.ModelName);
            Assert.AreEqual(720, vMaker.FrameWidth);
            Assert.AreEqual(480, vMaker.FrameHeight);
            Assert.AreEqual(23, vMaker.Fps);
            Assert.IsTrue(File.Exists(@"C:\Users\Public\Videos\Sample Videos\" + vMaker.ModelName + ".wmv"));
        }

        [Test]
        public void createImages()
        {
            iMaker.createImages();
            Assert.AreEqual(@"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv", iMaker.VideoPath);
            Assert.AreEqual(@"C:\Users\Public\Pictures\Sample Pictures", iMaker.DestinationPath);
            Assert.AreEqual(664, iMaker.ImageWidth);
            Assert.AreEqual(246, iMaker.ImageHeight);
            Assert.AreEqual(16, iMaker.BitCount);
            Assert.AreEqual(30, iMaker.Ips);
        }

        [Test]
        public void deleteImages()
        {
            iMaker.deleteImages();
            Assert.IsTrue(!File.Exists(@"C:\Users\Public\Pictures\Sample Pictures\frame0.jpg"));
        }

    }
}
