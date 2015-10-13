using System;
using TETCSharpClient;
using TETCSharpClient.Data;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace Record_Class
{
    public class Record : IGazeListener 
    {
        string path;

        int screenW, screenH;

        public string _path
        {
            get { return path; }
            set { path = value; }
        }

        string modelName;
        public string _modelName
        {
            get { return modelName; }
            set { modelName = value; }
        }

        bool recording;
        public bool _recording
        {
            get { return recording; }
            set { recording = value; }
        }

        public List<string> arrayData = new List<string>();

        public Record(string FilePath, string ModelName, int screenWidth, int screenHeight)
        {
            recording = false;
            path = FilePath;
            modelName = ModelName;
            screenW = screenWidth;
            screenH = screenHeight;
            GazeManager.Instance.Activate(GazeManager.ApiVersion.VERSION_1_0, GazeManager.ClientMode.Push);

            GazeManager.Instance.AddGazeListener(this);
        }

        public void OnGazeUpdate(GazeData gazeData)
        {
            if (!recording)
            {
                saveToFile();
                return;
            }
                
            //throw new Exception();

            // start or stop tracking lost animation
            if ((gazeData.State & GazeData.STATE_TRACKING_GAZE) == 0 &&
                (gazeData.State & GazeData.STATE_TRACKING_PRESENCE) == 0) return;

            // tracking coordinates
            var X = gazeData.SmoothedCoordinates.X;
            var Y = gazeData.SmoothedCoordinates.Y;
            var screenX = (int)Math.Round(X, 0);
            var screenY = (int)Math.Round(Y, 0);

            //// return in case of 0,0 
            //if (screenX == 0 && screenY == 0) return;

            // write data to a file 

            arrayData.Add((X + ":" + Y).ToString());
            float m = (float)X;
            Console.WriteLine(m);
        }

        public void saveToFile()
        {
            
            // tester path
            //path = @"C:\Users\Public\WriteLines.txt";
            string path2 = path +"RecordedData_" + modelName + ".txt";
            try
            {
                if (!File.Exists(path2))
                {
                    File.WriteAllText(path2, screenW.ToString()+"x"+screenH.ToString()+"\n");
                    File.AppendAllLines(path2, arrayData);
                }
                else
                {
                    File.AppendAllLines(path2, arrayData);
                }
            }
            catch (Exception f)
            {
                MessageBox.Show("Error:" + f); 
            }
        }


        public void close()
        {
            GazeManager.Instance.Deactivate();
        }

        public void test()
        {
            arrayData.Add((2.0 + "," + 4.0).ToString());
            arrayData.Add((5.0 + "," + 7.0).ToString());
            arrayData.Add((1.0 + "," + 13.0).ToString());
            recording = false;
            saveToFile();
        }
    }
}
