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
            Statistics stats = new Statistics(@"Libraries\Documents\","statsTest",900,900);
            stats.createPDF();
        }

        [Test]
        public void testHeightandWidthCalculations()
        {
            Statistics stats = new Statistics(@"Libraries\Documents\", "statsTest", 900, 1200);
            Assert.AreEqual(400,stats.getGridHeight());
            Assert.AreEqual(300, stats.getGridWidth());     
        }

        [Test]
        public void testTestforGridpopulation()
        {
            Statistics stats = new Statistics(@"Libraries\Documents\", "statsTest", 900, 1200);

        }
    }
}
