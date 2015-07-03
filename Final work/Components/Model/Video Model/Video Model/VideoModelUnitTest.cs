using System;
using NUnit.Framework;
using System.IO;

namespace Video_Model
{

    [TestFixture]
    public class Class1
    {
        VideoGenerator maker;

        [SetUp]
        public void Init()
        {
            maker = new VideoGenerator();

            maker.ImagePath = @"C:\Users\COS301\Documents\Visual Studio 2013\Projects\images\";
            maker.ModelName = "TestVideoModel";
            maker.FrameWidth = 720;
            maker.FrameHeight = 480;
            maker.Fps = 27;
            maker.createVideo();
        }

        [Test]
        public void createVideo()
        {
            Assert.AreEqual(@"C:\Users\COS301\Documents\Visual Studio 2013\Projects\images\", maker.ImagePath);
            Assert.AreEqual("TestVideoModel", maker.ModelName);
            Assert.AreEqual(720, maker.FrameWidth);
            Assert.AreEqual(480, maker.FrameHeight);
            Assert.AreEqual(27, maker.Fps);
            Assert.IsTrue(File.Exists(@"C:\Users\COS301\Documents\Visual Studio 2013\Projects\images\output.wmv"));
        }
    }
}
