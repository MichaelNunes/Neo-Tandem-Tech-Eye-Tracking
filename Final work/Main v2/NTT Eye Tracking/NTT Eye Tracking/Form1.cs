﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using Record_Class;
using Results_Class;
using System.IO;
using Video_Model;

namespace NTT_Eye_Tracking
{
    /* ========== Model Type indexes ==========
     * 0 - 3D model
     * 1 - 3D fly through
     * 2 - 2D
     * 3 - Video
     */
    public partial class NTT_EyeTracker : Form
    {
        #region variables
        string name = "";
        string ModelPath = "";
        string[] imglocation = new string[9];
        int imgCounters = 0;
        int imgIndex = 0;
        bool fullscreen = false;
        VideoGenerator vg = new VideoGenerator();

        string obj;
        string mtl;
        string img;
        bool flythrough;
        #endregion

        public NTT_EyeTracker()
        {
            InitializeComponent();
            pic_model2DPreview.Visible = false;
            wmp_VideoPreview.Visible = false;
        }

        private void NTT_EyeTracker_Load(object sender, EventArgs e)
        {
            initializeGlobalStyles();
            this.BackColor = GlobalStyles.mainFormColours;
            NTT_MiniForm ntt_mini = new NTT_MiniForm();
            ntt_mini.ShowDialog(this);

           switch(globals.modelIndex)
           {
                   //in each case we must show the appropriate model previewer and hide the others
               case 0: //3D model
                   {
                       pic_caro.Visible = true;
                       pic_model2DPreview.Visible = false;
                       wmp_VideoPreview.Visible = false;
                       break;
                   }
               case 1: //flythrough
                   {
                       break;
                   }
               case 2: //2D models
                   {
                       pic_caro.Visible = false;
                       pic_model2DPreview.Visible = true;
                       wmp_VideoPreview.Visible = false;
                       break;
                   }
               case 3: //Video
                   {
                       pic_caro.Visible = false;
                       pic_model2DPreview.Visible = false;
                       wmp_VideoPreview.Visible = true;
                       wmp_VideoPreview.Dock = DockStyle.Fill;
                       break;
                   }
           }
        }

        public void initializeGlobalStyles()
        {            
            //field initialization
            GlobalStyles.models = new List<modelType>();

            //Model Types
            modelType model3DImage = new modelType("3D Image", global::NTT_Eye_Tracking.Properties.Resources.blank35);
            //model3DImage.modelImage.Size = new Size(35, 35);
            GlobalStyles.models.Add(model3DImage);
            modelType model3DFlythrough = new modelType("3D Flythrough", global::NTT_Eye_Tracking.Properties.Resources.paper135);
            GlobalStyles.models.Add(model3DFlythrough);
            modelType model2DImage = new modelType("2D Image", global::NTT_Eye_Tracking.Properties.Resources.landscape11);
            GlobalStyles.models.Add(model2DImage);
            modelType modelVideo = new modelType("Video", global::NTT_Eye_Tracking.Properties.Resources.movie42);
            GlobalStyles.models.Add(modelVideo);

            //Form styles
            GlobalStyles.mainFormColours = Color.LightBlue;
        }

        public void formInitialization()
        {

        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\EyeTribe\Client\EyeTribeUIWin.exe");
        }

        private void btnChooseModel_Click(object sender, EventArgs e)
        {
            switch(globals.modelIndex)
            {
                case 0: //3D model
                {
                    openFileDialog1.Title = "Please select an object file";
                    openFileDialog1.Filter = "object (.obj)|*.obj";
                    openFileDialog1.FileName = "";
                    openFileDialog1.ShowDialog();
                    name = openFileDialog1.SafeFileName;
                    obj = openFileDialog1.FileName;

                    openFileDialog1.Title = "Please select a material file file";
                    openFileDialog1.Filter = "object (.mtl)|*.mtl";
                    openFileDialog1.FileName = "";
                    openFileDialog1.ShowDialog();
                    name = openFileDialog1.SafeFileName;
                    mtl = openFileDialog1.FileName;

                    img = Path.GetDirectoryName(obj) + "\\";

                    flythrough = false;
                    break;
                }
                case 1: //flythrough
                {
                    openFileDialog1.Title = "Please select an object file";
                    openFileDialog1.Filter = "object (.obj)|*.obj";
                    openFileDialog1.FileName = "";
                    openFileDialog1.ShowDialog();
                    name = openFileDialog1.SafeFileName;
                    obj = openFileDialog1.FileName;


                    openFileDialog1.Title = "Please select a material file";
                    openFileDialog1.Filter = "object (.mtl)|*.mtl";
                    openFileDialog1.FileName = "";
                    openFileDialog1.ShowDialog();
                    name = openFileDialog1.SafeFileName;
                    mtl = openFileDialog1.FileName;

                    img = Path.GetDirectoryName(obj) + "\\";

                    flythrough = true;
                    break;
                }
                case 2: //2D
                {
                    openFileDialog1.Filter = "Image (.jpg)|*.jpg";
                    openFileDialog1.FileName = "";
                    openFileDialog1.ShowDialog();
                    name = openFileDialog1.SafeFileName;
                    string path = openFileDialog1.FileName;
                    ModelPath = globals.currentRecordingpath + "\\" + name;
                    System.IO.File.Copy(path, ModelPath, true);
                    pic_model2DPreview.ImageLocation = ModelPath;
                    break;
                }

                case 3: //Video
                {
                    openFileDialog1.Filter = "all files(*.*)|*.*| video (.mp4)|*.mp4| video (.wmv)|*.wmv";
                    openFileDialog1.FileName = "";
                    openFileDialog1.ShowDialog();
                    ModelPath = openFileDialog1.FileName;
                    wmp_VideoPreview.URL = openFileDialog1.FileName;
                    wmp_VideoPreview.Dock = DockStyle.Fill;
                    name = openFileDialog1.SafeFileName;
                    wmp_VideoPreview.Ctlcontrols.stop();
                    break;
                }
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            globals.recording = new Record(globals.currentRecordingpath + @"\", name);
            //hideMainButtons();
            switch (globals.modelIndex)
            {
                case 0: //3D model
                    {
                        //recording._recording = true;
                        frmDisplay fd = new frmDisplay(0, null, imglocation);
                        fd.Show(this);
                        //recording._recording = false;
                        break;
                    }
                case 1: //flythrough
                    {
                        //globals.recording._recording = true;
                        DisplayModel.DisplayModel dm = new DisplayModel.DisplayModel();
                        dm.Run(obj, mtl, img, globals.currentRecordingpath + @"\" + name, flythrough);
                        
                        //recording._recording = false;

                        vg.DestinationPath = globals.currentRecordingpath + @"\" + name;
                        vg.ImagePath = globals.currentRecordingpath + @"\" + name;
                        vg.ModelName = name;
                        vg.Fps = 30;
                        Size res = this.GetDpiSafeResolution();
                        vg.FrameHeight = res.Height;
                        vg.FrameWidth = res.Width;
                        vg.createVideo();
                        break;
                    }
                case 2: //2D models
                    {
                        MessageBox.Show("Recording wil begin after this message is closed. The recording will end in " + globals.recordTime / 1000 + " seconds but you can press escape (Esc) to stop the recording at any time.");
                        
                        //recording._recording = true;
                        frmDisplay fd = new frmDisplay(2, ModelPath, null);
                        fd.Show(this);
                        //recording._recording = false;
                        break;
                    }
                case 3: //Video
                    {
                        //recording._recording = true;
                        frmDisplay fd = new frmDisplay(3, ModelPath, null);
                        fd.ShowDialog(this);
                        //recording._recording = false;

                        break;
                    }
            }
            globals.recording.saveToFile();
            globals.recording.close();
        }

        private void btnOverlays_Click(object sender, EventArgs e)
        {
            Size res = this.GetDpiSafeResolution();
            switch (globals.modelIndex)
            {
                case 0: //3D model
                    {

                        Eye_Tracking_Graph etg = new Eye_Tracking_Graph(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        etg.OpenETGraphData(globals.currentRecordingpath,name);
                        etg._SourceLocation = ModelPath;
                        etg._DestinationPath = globals.currentRecordingpath;
                        etg.SaveETGraph3D();

                        Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        hm._SourceLocation = ModelPath;
                        hm._DestinationPath = globals.currentRecordingpath;
                        hm.SaveHeatmapVideo();
                        break;
                    }
                case 1: //flythrough
                    {
                        Eye_Tracking_Graph etg = new Eye_Tracking_Graph(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        etg.OpenETGraphData(globals.currentRecordingpath, name);
                        etg._SourceLocation = ModelPath;
                        etg._DestinationPath = globals.currentRecordingpath;
                        etg.SaveETGraphVideo();

                        Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        hm.OpenHeatmapData(globals.currentRecordingpath, name);
                        hm._SourceLocation = ModelPath;
                        hm._DestinationPath = globals.currentRecordingpath;
                        hm.SaveHeatmap2D();
                        break;
                    }
                case 2: //2D models
                    {
                        Eye_Tracking_Graph etg = new Eye_Tracking_Graph(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        etg.OpenETGraphData(globals.currentRecordingpath, name);
                        etg._SourceLocation = ModelPath;
                        etg._DestinationPath = globals.currentRecordingpath;
                        etg.SaveETGraph2D();

                        Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        hm.OpenHeatmapData(globals.currentRecordingpath, name); 
                        hm._SourceLocation = ModelPath;
                        hm._DestinationPath = globals.currentRecordingpath;
                        hm.SaveHeatmap2D();
                        break;
                    }
                case 3: //Video
                    {
                        Eye_Tracking_Graph etg = new Eye_Tracking_Graph(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        etg.OpenETGraphData(globals.currentRecordingpath, name);
                        etg._SourceLocation = ModelPath;
                        etg._DestinationPath = globals.currentRecordingpath;
                        etg.SaveETGraphVideo();

                        Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        hm.OpenHeatmapData(globals.currentRecordingpath, name);
                        hm._SourceLocation = ModelPath;
                        hm._DestinationPath = globals.currentRecordingpath;
                        hm.SaveHeatmapVideo();
                        break;
                    }
            }
        }

        private void btnHeatmaps_Click(object sender, EventArgs e)
        {
            Size res = this.GetDpiSafeResolution();
            switch (globals.modelIndex)
            {
                case 0: //3D model
                    {
                        Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        hm._SourceLocation = ModelPath;
                        hm._DestinationPath = globals.currentRecordingpath;
                        hm.SaveHeatmapOntoModelVideo();
                        break;
                    }
                case 1: //flythrough
                    {
                        Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        hm.OpenHeatmapData(globals.currentRecordingpath, name);
                        hm._SourceLocation = ModelPath;
                        hm._DestinationPath = globals.currentRecordingpath;
                        hm.SaveHeatmapOntoModelVideo();
                        break;
                    }
                case 2: //2D models
                    {
                        Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        hm.OpenHeatmapData(globals.currentRecordingpath, name); 
                        hm._SourceLocation = ModelPath;
                        hm._DestinationPath = globals.currentRecordingpath;
                        hm.SaveHeatmapOntoModel2D();
                        break;
                    }
                case 3: //Video
                    {
                        Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        hm.OpenHeatmapData(globals.currentRecordingpath, name);
                        hm._SourceLocation = ModelPath;
                        hm._DestinationPath = globals.currentRecordingpath;
                        hm.SaveHeatmapOntoModelVideo();
                        break;
                    }
            }
        }

        private void btnGazepoint_Click(object sender, EventArgs e)
        {
            Size res = this.GetDpiSafeResolution();
            switch (globals.modelIndex)
            {
                case 0: //3D model
                    {
                        Eye_Tracking_Graph etg = new Eye_Tracking_Graph(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        etg._SourceLocation = ModelPath;
                        etg._DestinationPath = globals.currentRecordingpath;
                        etg.SaveETGraphOntoModel3D();
                        break;
                    }
                case 1: //flythrough
                    {
                        Eye_Tracking_Graph etg = new Eye_Tracking_Graph(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        etg.OpenETGraphData(globals.currentRecordingpath, name);
                        etg._SourceLocation = ModelPath;
                        etg._DestinationPath = globals.currentRecordingpath;
                        etg.SaveETGraphOntoModelVideo();
                        break;
                    }
                case 2: //2D models
                    {
                        Eye_Tracking_Graph etg = new Eye_Tracking_Graph(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        etg.OpenETGraphData(globals.currentRecordingpath, name);
                        etg._SourceLocation = ModelPath;
                        etg._DestinationPath = globals.currentRecordingpath;
                        etg.SaveETGraphOntoModel2D();
                        break;
                    }
                case 3: //Video
                    {
                        Eye_Tracking_Graph etg = new Eye_Tracking_Graph(name, globals.currentRecordingpath, res.Width, res.Height, "");
                        etg.OpenETGraphData(globals.currentRecordingpath, name);
                        etg._SourceLocation = ModelPath;
                        etg._DestinationPath = globals.currentRecordingpath;
                        etg.SaveETGraphOntoModelVideo();
                        break;
                    }
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            switch (globals.modelIndex)
            {
                case 0: //3D model
                    {
                        break;
                    }
                case 1: //flythrough
                    {
                        break;
                    }
                case 2: //2D models
                    {
                        pic_model2DPreview.Show();
                        break;
                    }
                case 3: //Video
                    {
                        break;
                    }
            }
        }

        private void btnViewResults_Click(object sender, EventArgs e)
        {
            switch (globals.modelIndex)
            {                
                case 0: //3D model
                    {
                        break;
                    }
                case 1: //flythrough
                    {
                        break;
                    }
                case 2: //2D models
                    {
                        pic_model2DPreview.Show();
                        break;
                    }
                case 3: //Video
                    {
                        break;
                    }
            }
        }

        #region helper functions
        private void hideMainButtons()
        {
            btnCal.Visible = false;
            btnChooseModel.Visible = false;
            btnGazepoint.Visible = false;
            btnHeatmaps.Visible = false;
            btnOverlays.Visible = false;
            btnRecord.Visible = false;
            btnReport.Visible = false;
            btnViewResults.Visible = false;
        }

        private void showMainButtons()
        {
            btnCal.Visible = true;
            btnChooseModel.Visible = true;
            btnGazepoint.Visible = true;
            btnHeatmaps.Visible = true;
            btnOverlays.Visible = true;
            btnRecord.Visible = true;
            btnReport.Visible = true;
            btnViewResults.Visible = true;
        }

        private void fixForm()
        {
            switch (globals.modelIndex)
            {
                case 0: //3D model
                    {
                        break;
                    }
                case 1: //flythrough
                    {
                        break;
                    }
                case 2: //2D models
                    {
                        this.WindowState = FormWindowState.Maximized;
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                        pic_model2DPreview.Dock = DockStyle.Fill;
                        break;
                    }
                case 3: //Video
                    {
                        break;
                    }
            }
        }

        private Size GetDpiSafeResolution()
        {
            using (Graphics graphics = this.CreateGraphics())
            {
                return new Size((Screen.PrimaryScreen.Bounds.Width * (int)graphics.DpiX) / 96
                  , (Screen.PrimaryScreen.Bounds.Height * (int)graphics.DpiY) / 96);
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                if (fullscreen == true)
                {
                    globals.recording._recording = false;
                    globals.recording.saveToFile();
                    globals.recording.close();
                    fixForm();
                    showMainButtons();
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void btnImageForward_Click(object sender, EventArgs e)
        {
            if (imgIndex == imgCounters - 1)
            {
                imgIndex = 0;
                pic_caro.ImageLocation = imglocation[imgIndex];
            }
            else
            {
                imgIndex++;
                pic_caro.ImageLocation = imglocation[imgIndex];
            }
        }

        private void btnImageBack_Click(object sender, EventArgs e)
        {
            if (imgIndex == 0)
            {
                imgIndex = imgCounters - 1;
                pic_caro.ImageLocation = imglocation[imgIndex];
            }
            else
            {
                imgIndex++;
                pic_caro.ImageLocation = imglocation[imgIndex];
            }
        }

        #endregion
    }
}
