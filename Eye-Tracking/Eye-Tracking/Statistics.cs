using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye_Tracking
{
    class Statistics
    {
        private int ID;
        private HeatMap heatmap;

        public Statistics()
        {
            ID = 0;
            heatmap = null;
        }

        public void createStats(int[] data)
        {
            //creates stats for the heatmap
        }
        public void saveStats()
        {
            //save statistical data into a file
        }
    }
}
