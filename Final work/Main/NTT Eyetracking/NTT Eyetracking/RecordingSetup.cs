using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTT_Eyetracking
{
    public partial class RecordingSetup : Form
    {
        public RecordingSetup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                string name = "";
                ProjectSolution t = globals.m;
                if (textBox1.Text == "")
                {
                    DateTime time = DateTime.Today;
                    name = "Recording" + time.Day + "_" + time.Month + "_" + time.Year + "_" + time.Second + "_" + time.Minute + "_" + time.Hour;
                }
                else
                {
                    name = textBox1.Text;
                }
                if (comboBox1.Text == "2DModel")
                {
                    string rec = System.IO.Path.Combine(t.Directory, "Recordings");
                    System.IO.Directory.CreateDirectory(rec);
                    string sub = System.IO.Path.Combine(rec, "2DModel");
                    sub = System.IO.Path.Combine(sub, name);
                    System.IO.Directory.CreateDirectory(sub);
                    globals.currentRecordingpath = sub;
                    globals.typeOfRecording = "2DModel";
                    globals.name = name;
                    this.Hide();
                    _2DModelView view = new _2DModelView();
                    view.ShowDialog();
                    this.Show();

                }
                else if (comboBox1.Text == "3DModel")
                {
                    string rec = System.IO.Path.Combine(globals.m.Directory, "Recordings");
                    System.IO.Directory.CreateDirectory(rec);
                    string sub = System.IO.Path.Combine(rec, "3DModel");
                    sub = System.IO.Path.Combine(sub, name);
                    System.IO.Directory.CreateDirectory(sub);
                    globals.currentRecordingpath = sub;
                    globals.typeOfRecording = "3DModel";
                    this.Hide();
                    _3DModelView view = new _3DModelView();
                    view.ShowDialog();
                    this.Show();
                }
                else if (comboBox1.Text == "Video")
                {
                    string rec = System.IO.Path.Combine(globals.m.Directory, "Recordings");
                    System.IO.Directory.CreateDirectory(rec);
                    string sub = System.IO.Path.Combine(rec, "Video");
                    sub = System.IO.Path.Combine(sub, name);
                    System.IO.Directory.CreateDirectory(sub);
                    globals.currentRecordingpath = sub;
                    globals.typeOfRecording = "Video";
                    this.Hide();
                    VideoView view = new VideoView();
                    view.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Incorrect recording type chosen");
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
