﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DisplayModel;

namespace NTT_Eyetracking
{
    public partial class _3DModelView : Form
    {
        bool fullscreen = false;
        string[] imglocation = new string[9];
        int counter;
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


        }

        private void button2_Click(object sender, EventArgs e)
        {
            counter = 0;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            fullscreen = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Dock = DockStyle.Fill;
            timer1.Interval = 3000;
            timer1.Start();
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
            if (counter == imglocation.Length)
            {
                timer1.Stop();
            }
            else
            {
                pictureBox1.ImageLocation = imglocation[counter];
                counter++;
            }
        }
    }
}
