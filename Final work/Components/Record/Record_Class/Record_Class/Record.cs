using System;
using TETCSharpClient;
using TETCSharpClient.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Record_Class
{
    public class Record : IGazeListener 
    {
        string path;
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

        bool recording = false;
        public bool _recording
        {
            get; set;
        }

        public List<string> arrayData = new List<string>();

        public Record(string FilePath, string ModelName)
        {
            path = FilePath;
            modelName = ModelName;
            GazeManager.Instance.Activate(GazeManager.ApiVersion.VERSION_1_0, GazeManager.ClientMode.Push);

            GazeManager.Instance.AddGazeListener(this);
        }

        public void OnGazeUpdate(GazeData gazeData)
        {
            if (!recording) return;

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
            arrayData.Add((X + "," + Y).ToString());
            saveToFile();
        }

        public void saveToFile()
        {
            // tester path
            //path = @"C:\Users\Public\WriteLines.txt";
            path = path + "RecordedData_" + modelName + ".txt";
            try
            {
                if (!File.Exists(path))
                {
                    File.WriteAllLines(path, arrayData);
                }
                else
                {
                    File.AppendAllLines(path, arrayData);
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
