using System;
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
        string imagepath = "";
        bool fullscreen = false;
        Record recording;
        #endregion

        public NTT_EyeTracker()
        {
            InitializeComponent();
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
                        break;
                    }
                case 1: //flythrough
                    {
                        break;
                    }
                case 2: //2D
                {
                    openFileDialog1.Filter = "Image (.jpg)|*.jpg";
                    openFileDialog1.FileName = "";
                    openFileDialog1.ShowDialog();
                    name = openFileDialog1.SafeFileName;
                    string path = openFileDialog1.FileName;
                    imagepath = globals.currentRecordingpath + "\\" + name;
                    System.IO.File.Copy(path, imagepath, true);
                    pic_model2DPreview.ImageLocation = imagepath;
                    break;
                }

                case 3: //Video
                {
                    openFileDialog1.Filter = "Image (.jpg)|*.jpg";
                    openFileDialog1.FileName = "";
                    openFileDialog1.ShowDialog();
                    wmp_VideoPreview.URL = openFileDialog1.FileName;
                    name = openFileDialog1.SafeFileName;
                    wmp_VideoPreview.Ctlcontrols.stop();
                    break;
                }
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            recording = new Record(globals.currentRecordingpath + @"\", name);
            MessageBox.Show("Recording wil begin after this message is closed. The recording will end in "+ globals.recordTime/1000 +" seconds but you can press escape (Esc) to stop the recording at any time.");
            hideMainButtons();
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
                        //this.FormBorderStyle = FormBorderStyle.None;
                        //this.WindowState = FormWindowState.Maximized;
                        //pic_model2DPreview.SizeMode = PictureBoxSizeMode.StretchImage;
                        //pic_model2DPreview.Dock = DockStyle.Fill;
                        //fullscreen = true;
                        recording._recording = true;

                        //record for amount of seconds before stopping it
                        System.Threading.Thread.Sleep(globals.recordTime);

                        //stop recording an fix form
                        recording._recording = false;
                        //fixForm();
                        //showMainButtons();

                        break;
                    }
                case 3: //Video
                    {
                        //fullscreen = true;
                        //button1.Visible = false;
                        //button2.Visible = false;
                        //button3.Visible = false;
                        //button4.Visible = false;
                        //axWindowsMediaPlayer2.Visible = false;
                        //axWindowsMediaPlayer1.Dock = DockStyle.Fill;
                        //wmp_VideoPreview.Ctlcontrols.play();
                        recording._recording = true;

                        //must stop recording on form exit.

                        break;
                    }
            }
            recording.saveToFile();
            recording.close();
        }

        private void btnOverlays_Click(object sender, EventArgs e)
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

                        Size res = this.GetDpiSafeResolution();
                        Results_Class.Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "gugiog");
                        hm.OpenHeatmapData(globals.currentRecordingpath, name);
                        hm.SaveHeatmap2D();
                        break;
                    }
                case 3: //Video
                    {
                        break;
                    }
            }
        }

        private void btnHeatmaps_Click(object sender, EventArgs e)
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
                        Size res = this.GetDpiSafeResolution();
                        Results_Class.Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "gugiog");
                        hm.OpenHeatmapData(globals.currentRecordingpath, name);
                        hm.SaveHeatmapOntoModel2D();
                        break;
                    }
                case 3: //Video
                    {
                        break;
                    }
            }
        }

        private void btnGazepoint_Click(object sender, EventArgs e)
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
                    recording._recording = false;
                    recording.saveToFile();
                    recording.close();
                    fixForm();
                    showMainButtons();
                }
            }
            return base.ProcessDialogKey(keyData);
        }
        #endregion
    }
}
