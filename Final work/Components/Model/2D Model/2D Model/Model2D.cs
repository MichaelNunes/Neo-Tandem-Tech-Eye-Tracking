using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Model
{
    public class Model2D
    {
        string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        string modelName;
        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }

        int imageWidth;
        public int ImageWidth
        {
            get { return imageWidth; }
            set { imageWidth = value; }
        }

        int imageHeight;
        public int ImageHeight
        {
            get { return imageHeight; }
            set { imageHeight = value; }
        }

        public Model2D()
        {

        }

        Model2D(string ModelName, string ModelLocation)
        {
            modelName = ModelName;
            imagePath = ModelLocation;
        }


    }
}
