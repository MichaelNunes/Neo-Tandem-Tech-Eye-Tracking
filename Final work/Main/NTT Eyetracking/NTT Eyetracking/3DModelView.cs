using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DisplayModel;
using Record_Class;
using Results_Class;

namespace NTT_Eyetracking
{
    public partial class _3DModelView : Form
    {
        bool fullscreen = false;
        string[] imglocation = new string[9];
        int counters;
        public _3DModelView()
        {
            InitializeComponent();
        }
        string name = "";
        private void button1_Click(object sender, EventArgs e)
        {

            //folderBrowserDialog1.ShowDialog();
           // string folder = folderBrowserDialog1.SelectedPath;
            openFileDialog1.Filter = "Object (.obj)|*.obj";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            name = openFileDialog1.SafeFileName;
            string path = openFileDialog1.FileName;
            DisplayModel.DisplayModel dm = new DisplayModel.DisplayModel();
            string[] arg = new string[2];
            arg[0] = path;
            arg[1] = globals.currentRecordingpath+@"\\";
            dm.Run(arg);
            int counter = 2;
            for(int i = 0;i<9;i++)
            {
                imglocation[i] = globals.currentRecordingpath + @"\\"  + "view" + counter +".jpg";
                counter++;
            }


        }
        Record m = null;
        private void button2_Click(object sender, EventArgs e)
        {
            counters = 0;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            fullscreen = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Dock = DockStyle.Fill;
            timer1.Interval = 3000;
            timer1.Start();
            m = new Record(globals.currentRecordingpath + @"\", "view" + counters);
            m._recording = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //TO DO:add functionality to open the folder with all the images in them

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                if (fullscreen == true)
                {
                    button1.Visible = true;
                    button2.Visible = true;
                    button3.Visible = true;
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    fullscreen = false;
                    pictureBox1.Dock = DockStyle.None;

                }

            }
            return base.ProcessDialogKey(keyData);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (counters == imglocation.Length)
            {
                timer1.Stop();
                m._recording = false;
                m.saveToFile();
                m.close();

                ProcessDialogKey(Keys.Escape);

                Heatmaps hm = new Heatmaps("view", globals.currentRecordingpath, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, "gugiog");
                hm.SaveHeatmap3D();
                hm.SaveHeatmapOntoModel3D();
            }
            else
            {
                m._recording = false;
                m.saveToFile();
                m.close();
                pictureBox1.ImageLocation = imglocation[counters];
                m = new Record(globals.currentRecordingpath + @"\", "view" + counters);
                m._recording = true;
                counters++;
            }
        }
    }
}
