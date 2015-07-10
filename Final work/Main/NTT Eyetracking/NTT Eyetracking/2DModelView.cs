using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Record_Class;
using Results;
using _2D_Model;

namespace NTT_Eyetracking
{
    public partial class _2DModelView : Form
    {
        string imagepath = "";
        bool fullscreen = false;
        public _2DModelView()
        {
            InitializeComponent();
        }

        private void _2DModelView_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        string name = "";
        private void button1_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "Image (.jpg)|*.jpg";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            name = openFileDialog1.SafeFileName;
            string path = openFileDialog1.FileName;
            imagepath = globals.currentRecordingpath+"\\"+name;
            System.IO.File.Copy(path, imagepath, true);
            pictureBox1.ImageLocation = imagepath;

        }
        Record m = null;
        private void button2_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Dock = DockStyle.Fill;
            fullscreen = true;
            m = new Record(globals.m.Directory, name);
            m._recording = true;
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == Keys.Escape.ToString())
            {
                if (fullscreen == false)
                {
                    m._recording = false;
                    m.close();
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    fullscreen = false;

                    

                }
            }
        }
    }
}
