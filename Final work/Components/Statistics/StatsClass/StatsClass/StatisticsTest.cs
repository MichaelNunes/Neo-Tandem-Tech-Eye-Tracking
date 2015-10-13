using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace StatsClass
{
     [TestFixture]
    class StatisticsTest
    {
        [Test]
        public void createPDF()
        {
            Statistics stats = new Statistics(@"Libraries\Documents\", @"Libraries\Documents\", "statsTest", 1920, 1080, "2D", "");
            stats.createPDF();
            
        }

        [Test]
        public void testHeightandWidthCalculations()
        {
            Statistics stats = new Statistics(@"Libraries\Documents\", @"Libraries\Documents\", "statsTest", 900, 1200, "2D", "");
            Assert.AreEqual(400,stats.getGridHeight());
            Assert.AreEqual(300, stats.getGridWidth());     
        }

        [Test]
        public void testTestforGridpopulation()
        {
            Statistics stats = new Statistics(@"Libraries\Documents\", @"Libraries\Documents\", "statsTest", 900, 1200, "2D", "");

        }
        [Test]
        public void testcount()
        {
            Statistics stats = new Statistics(@"Libraries\Documents\", @"Libraries\Documents\", "statsTest", 900, 1200, "2D", "");
            stats.getgridsCount();
            int c = stats.px.Count;
            float x = stats.px.ElementAt<float>(15);
            float y = stats.py.IndexOf(70);

            
            float m =  (float)943.1598;
            Assert.AreEqual(c, 70);
            Assert.AreEqual(x, m);

        }
        [Test]
        public void test3D()
        {
            
            Statistics stats = new Statistics(@"Libraries\Documents\", @"Libraries\Documents\", "Test one", 900, 1200, "3d", "");
            Statistics stats2 = new Statistics(@"Libraries\Documents\", @"Libraries\Documents\", "This_is_a_long_string_to_use_as_an_examp", 900, 1200, "3d", "");
            stats.createPDF3d();
            stats2.createPDF3d();
          //  int c = stats.px.Count;
            //float x = stats.px.ElementAt<float>(0);
          //  float y = stats.py.IndexOf(70);


          //  float m = (float)943.1598;
          //  Assert.AreEqual(c, 70);
          //  Assert.AreEqual(x, m);

        }
    }
}
