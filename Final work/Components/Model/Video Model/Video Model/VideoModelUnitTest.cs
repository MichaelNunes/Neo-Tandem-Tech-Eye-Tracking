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

            vMaker.ImagePath = @"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\Video Model Test\images\";
            vMaker.ModelName = "TestVideoModel";
            vMaker.FrameWidth = 720;
            vMaker.FrameHeight = 480;
            vMaker.Fps = 27;
            //vMaker.createVideo();

            iMaker = new ImageGenerator();
            iMaker.createImages();
        }

        [Test]
        public void createVideo()
        {
            Assert.AreEqual(@"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\Video Model Test\images\", vMaker.ImagePath);
            Assert.AreEqual("TestVideoModel", vMaker.ModelName);
            Assert.AreEqual(720, vMaker.FrameWidth);
            Assert.AreEqual(480, vMaker.FrameHeight);
            Assert.AreEqual(27, vMaker.Fps);
            Assert.IsTrue(File.Exists(@"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\Video Model Test\videos\output.wmv"));
        }

        [Test]
        public void createImage()
        {
            Assert.AreEqual(@"C:\Users\COS301\Documents\GitHub\Neo-Tandem-Tech-Eye-Tracking\Final work\Components\Model\Video Model Test\videos\output.wmv", iMaker.VideoPath);
            Assert.AreEqual(664, iMaker.ImageWidth);
            Assert.AreEqual(246, iMaker.ImageHeight);
            Assert.AreEqual(16, iMaker.BitCount);
        }
    }
}
