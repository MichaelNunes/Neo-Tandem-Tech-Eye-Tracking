using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit;

namespace Record_Class
{
    [TestFixture]
    class RecordingTesting
    {
        [Test]
        public void TestRecording()
        {
            Record record1 = new Record(@"C:\Users\Public\", "ModelOne");
            record1.saveToFile();

            record1._recording = true;
            Assert.AreEqual(true, record1._recording);

            record1._recording = false;
            Assert.AreEqual(false, record1._recording);
            
        }

        [Test]
        public void TestFileForNoZeroes()
        {
            Record record1 = new Record(@"C:\Users\Public\", "ModelOne");
            record1.saveToFile();

            foreach(string e in record1.arrayData)
            {
                Assert.AreNotEqual("0,0", e);
            }


        }
    }
}
