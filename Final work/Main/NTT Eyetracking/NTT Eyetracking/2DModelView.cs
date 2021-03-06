﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Record_Class;
using Results_Class;
using _2D_Model;
using StatsClass;

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
            button1.Visible = false;
            button2.Visible = false;
            fullscreen = true;
            m = new Record(globals.currentRecordingpath+@"\", name);
            m._recording = true;
            
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == Keys.Escape.ToString())
            {
                if (fullscreen == false)
                {
                   

                    

                }
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                if (fullscreen == true)
                {
                    m._recording = false;
                    m.saveToFile();
                    m.close();
                    button1.Visible = true;
                    button2.Visible = true;
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    //pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                    pictureBox1.Dock = DockStyle.None;
                    fullscreen = false;
                    Size res = this.GetDpiSafeResolution();
                    Results_Class.Heatmaps hm = new Heatmaps(name,globals.currentRecordingpath,res.Width,res.Height,"gugiog");
                    hm.OpenHeatmapData(globals.currentRecordingpath, name);
                    hm.SaveHeatmap2D();
                    hm.SaveHeatmapOntoModel2D();
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private Size GetDpiSafeResolution()
        {
            using (Graphics graphics = this.CreateGraphics())
            {
                return new Size((Screen.PrimaryScreen.Bounds.Width * (int)graphics.DpiX) / 96
                  , (Screen.PrimaryScreen.Bounds.Height * (int)graphics.DpiY) / 96);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StatsClass.Statistics stats = new Statistics(globals.currentRecordingpath, name, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height,"2D","");
            stats.createPDF();
            PDFViewer m = new PDFViewer(globals.currentRecordingpath);
        }
    }
}
