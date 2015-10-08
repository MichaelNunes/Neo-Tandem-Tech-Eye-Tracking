using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NTT_Eye_Tracking
{
    public class GlobalStyles
    {
        public static Color ForegroundColours = Color.LightSkyBlue;
        public static Color BackgroundColours = Color.LightSkyBlue;
        public static Color LabelsColours = Color.LightSkyBlue;
        public static Color ButtonColours = Color.LightSkyBlue;
        public static Color TextColours = Color.LightSkyBlue;

        public static List<modelType> models = new List<modelType>();        
    }

    public class modelType
    {
        public modelType()
        {

        }

        public modelType(string name, Image image)
        {
            modelName = name;
            modelImage = image;
        }

        public string modelName;
        public Image modelImage;
    }
}
