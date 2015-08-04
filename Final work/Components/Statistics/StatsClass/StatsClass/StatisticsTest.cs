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
            Statistics stats = new Statistics(@"Libraries\Documents\","statsTest");
            stats.createPDF();
        }
    }
}
