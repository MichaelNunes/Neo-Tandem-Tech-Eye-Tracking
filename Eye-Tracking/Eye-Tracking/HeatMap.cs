using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Eye_Tracking
{
    class HeatMap
    {
        private int ID;
        private string Type;

        public HeatMap()
        {
            ID = 0;
            Type = "";
        }

        public HeatMap(int id,string typeH)
        {
            ID = id;
            Type = typeH;
        }

        private void CreateHeatMap(int[] array)
        {
            //this is where the creation of the heat map is.
        }

        private HeatMap ReadHeatmap(int id)
        {
            if(id == this.ID)
            {
                return this;
            }
            else
            {
                 HeatMap empty = new HeatMap();
                return empty;
            }
        }

        private void saveHeatMap(int[] array)
        {
            //saves the heat map
        }

        private void saveRawInformation(int[] data)
        {
            //saves data into a file
        }

        private void readRawInformation(FileStream filelocation)
        {
            //reads data from file
        }
    }
}
