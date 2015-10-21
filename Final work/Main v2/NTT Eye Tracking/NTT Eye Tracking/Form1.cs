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
using System.IO;
using Video_Model;
using OpenTK.Graphics.OpenGL;
using StatsClass;
using Spinningprogbar;

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
        //string mtl;
        string img;
        bool flythrough;

        BackgroundWorker bw = new BackgroundWorker();

        Spinningprogbar.Form1 spinner = new Spinningprogbar.Form1();
        #endregion

        public NTT_EyeTracker()
        {
            InitializeComponent();
            pic_model2DPreview.Visible = false;
            wmp_VideoPreview.Visible = false;
        }

        private void NTT_EyeTracker_Load(object sender, EventArgs e)
        {
            try
            {
                disableMainButtons();
                initializeGlobalStyles();
                this.BackColor = GlobalStyles.ForegroundColours;
                NTT_MiniForm ntt_mini = new NTT_MiniForm();
                ntt_mini.ShowDialog(this);
                btnCal.Enabled = true;
                switch (globals.modelIndex)
                {
                    //in each case we must show the appropriate model previewer and hide the others
                    case 0: //3D model
                        {
                            pic_slideshowPreview.Visible = true;
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
                            pic_slideshowPreview.Visible = false;
                            pic_model2DPreview.Visible = true;
                            wmp_VideoPreview.Visible = false;
                            break;
                        }
                    case 3: //Video
                        {

                            wmp_VideoPreview.Dock = DockStyle.Fill;
                            wmp_VideoPreview.stretchToFit = true;
                            pic_slideshowPreview.Visible = false;
                            pic_model2DPreview.Visible = false;
                            wmp_VideoPreview.Visible = true;
                            wmp_VideoPreview.Dock = DockStyle.Fill;
                            break;
                        }
                }
            }
            catch (Exception f)
            {
                MessageBox.Show("An error has occured when trying to open the application, please try again. If your problem persists pleas consider reinstalling this software. Please also ensure that all system requirements have been met to ensure that NTT runs optimally.");
            }
        }

        public void initializeGlobalStyles()
        {
            try
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
                GlobalStyles.ForegroundColours = Color.White;//FromArgb(77,80,87);
            }
            catch (Exception f)
            {
                MessageBox.Show("An error has occured when trying to load application styles. Please try restarting the program or by resetting the styles by mmaking use of the settings menu.");
            }
        }

        public void formInitialization()
        {

        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"C:\Program Files (x86)\EyeTribe\Client\EyeTribeUIWin.exe");
                btnChooseModel.Enabled = true;
            }
            catch (Exception f)
            {
                MessageBox.Show("An error has occured when trying to open \"The Eye Tribe calibration software and server. Please ensure that you have the application installed correctly and try again.");
            }

        }

        private void btnChooseModel_Click(object sender, EventArgs e)
        {
            try
            {
                switch (globals.modelIndex)
                {
                    case 0: //3D model
                        {
                            openFileDialog1.Title = "Please select an object file";
                            openFileDialog1.Filter = "object (.obj)|*.obj";
                            openFileDialog1.FileName = "";
                            openFileDialog1.ShowDialog();
                            name = openFileDialog1.SafeFileName;
                            obj = openFileDialog1.FileName;

                            if (name != "" || obj != "")
                            {
                                DisplayModel.DisplayModel.Run(obj, globals.currentRecordingpath + @"\" + name, flythrough);
                                int counter = 1;
                                for (int i = 0; i < 9; i++)
                                {
                                    imglocation[i] = globals.currentRecordingpath + @"\" + name + "view" + counter + ".bmp";
                                    counter++;
                                }
                                img = Path.GetDirectoryName(obj) + "\\";
                                pic_caro.ImageLocation = imglocation[0];
                                flythrough = false;
                                btnRecord.Enabled = true;
                            }


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

                            if (name != "" || obj != "")
                            {
                                img = Path.GetDirectoryName(obj) + "\\";
                                flythrough = true;
                                btnRecord.Enabled = true;
                            }
                            break;
                        }
                    case 2: //2D
                        {
                            openFileDialog1.Filter = "Image (.jpg)|*.jpg";
                            openFileDialog1.FileName = "";
                            openFileDialog1.ShowDialog();
                            name = openFileDialog1.SafeFileName;
                            string path = openFileDialog1.FileName;

                            if (name != "" || path != "")
                            {
                                ModelPath = globals.currentRecordingpath + "\\" + name;
                                System.IO.File.Copy(path, ModelPath, true);
                                pic_model2DPreview.ImageLocation = ModelPath;
                                btnRecord.Enabled = true;
                            }
                            break;
                        }

                    case 3: //Video
                        {
                            openFileDialog1.Filter = "all files(*.*)|*.*| video (.mp4)|*.mp4| video (.wmv)|*.wmv";
                            openFileDialog1.FileName = "";
                            openFileDialog1.ShowDialog();
                            ModelPath = openFileDialog1.FileName;
                            if (ModelPath != "")
                            {
                                wmp_VideoPreview.URL = openFileDialog1.FileName;
                                wmp_VideoPreview.Dock = DockStyle.Fill;
                                name = openFileDialog1.SafeFileName;
                                wmp_VideoPreview.Ctlcontrols.stop();
                                btnRecord.Enabled = true;
                            }
                            break;
                        }
                }

            }
            catch (Exception f)
            {
                MessageBox.Show("An error has occured when trying to open the chosen model. \n\n Please check the following is correct:\n 1. That a valid model has been chosen. \n");
            }

        }

        private void btnRecord_Click(object sender, EventArgs e)
        {//hideMainButtons();
            try
            {
                Size res = this.GetDpiSafeResolution();
                globals.recording = new Record(globals.currentRecordingpath + @"\", name, res.Width, res.Height);
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
                            bw = new BackgroundWorker();
                            bw.WorkerReportsProgress = true;
                            bw.WorkerSupportsCancellation = true;
                            bw.DoWork += new DoWorkEventHandler(bw_DoWorkRecord);
                            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChangedRecord);
                            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompletedRecord);
                            bw.RunWorkerAsync();

                            bool temp = DisplayModel.DisplayModel.Run(obj, globals.currentRecordingpath + @"\", flythrough);
                            bw.CancelAsync();

                            globals.recording._recording = false;
                            globals.recording.saveToFile();
                            globals.recording.close();
                            vg.DestinationPath = globals.currentRecordingpath + @"\";// +name;
                            // = globals.currentRecordingpath + @"\"
                            vg.ImagePath = globals.currentRecordingpath + @"\";// +name;
                            vg.ModelName = "";//name;
                            vg.Fps = 30;
                            res = this.GetDpiSafeResolution();
                            vg.FrameHeight = res.Height;
                            vg.FrameWidth = res.Width;
                            vg.createVideo();
                            System.IO.File.Move(globals.currentRecordingpath + @"\" + ".wmv", globals.currentRecordingpath + @"\" + name + ".wmv");
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
                enableMainButtons();

            }
            catch (Exception f)
            {
                MessageBox.Show("An error has occured when trying to record on the desired model.\n\n Please ensure that the following is correct: \n 1. The camera is connected correctly.\n 2. Camera has been correctly calibrated.\n 3. The subject (or user) is positioned correctly and is not moving to much. \n");
            }
        }

        private void btnOverlays_Click(object sender, EventArgs e)
        {
            try
            {
                bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                bw.WorkerSupportsCancellation = true;
                bw.DoWork += new DoWorkEventHandler(bw_DoWorkOverlay);
                bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChangedOverlay);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompletedOverlay);

                bw.RunWorkerAsync(1000);
            }
            catch (Exception f)
            {
                MessageBox.Show("An error has occured when trying to create the overlays on the desired model.\n\n Please ensure that the following is correct: \n 1. The camera is connected correctly.\n 2. Camera has been correctly calibrated.\n 3. The subject (or user) is positioned correctly and is not moving to much.\n\n If problems persist then please contact system administrator or developers.");
            }
        }

        private void btnHeatmaps_Click(object sender, EventArgs e)
        {
            try
            {

                bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                bw.WorkerSupportsCancellation = true;
                bw.DoWork += new DoWorkEventHandler(bw_DoWorkHeatmaps);
                bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChangedHeatmaps);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompletedHeatmaps);

                bw.RunWorkerAsync(1000);
            }
            catch (Exception f)
            {
                MessageBox.Show("An error has occured when trying to create the Heatmaps on the desired model.\n\n Please ensure that the following is correct: \n 1. The camera is connected correctly.\n 2. Camera has been correctly calibrated.\n 3. The subject (or user) is positioned correctly and is not moving to much.\n\n If problems persist then please contact system administrator or developers.");
            }
        }

        private void btnGazepoint_Click(object sender, EventArgs e)
        {
            try
            {
                bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                bw.WorkerSupportsCancellation = true;
                bw.DoWork += new DoWorkEventHandler(bw_DoWorkGazePlot);
                bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChangedGazePlot);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompletedGazePlot);

                bw.RunWorkerAsync(1000);
            }
            catch (Exception f)
            {
                MessageBox.Show("An error has occured when trying to create the Heatmaps on the desired model.\n\n Please ensure that the following is correct: \n 1. The camera is connected correctly.\n 2. Camera has been correctly calibrated.\n 3. The subject (or user) is positioned correctly and is not moving to much.\n\n If problems persist then please contact system administrator or developers.");
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                Size res = this.GetDpiSafeResolution();
                Statistics stats = new Statistics(globals.currentRecordingpath, globals.currentRecordingpath, name, res.Width, res.Height, "", "");
                switch (globals.modelIndex)
                {
                    case 0: //3D model
                        {
                            stats.createPDF3d();
                            break;
                        }
                    case 1: //flythrough
                        {
                            stats.createPDF();
                            break;
                        }
                    case 2: //2D models
                        {
                            stats.createPDF();
                            break;
                        }
                    case 3: //Video
                        {
                            stats.createPDF();
                            break;
                        }
                }
            }
            catch (Exception f)
            {
                MessageBox.Show("An error has occured when trying to create the Heatmaps on the desired model.\n\n Please ensure that the following is correct: \n 1. The camera is connected correctly.\n 2. Camera has been correctly calibrated.\n 3. The subject (or user) is positioned correctly and is not moving to much.\n\n If problems persist then please contact system administrator or developers.");
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

        private void disableMainButtons()
        {
            btnCal.Enabled = false;
            btnChooseModel.Enabled = false;
            btnGazepoint.Enabled = false;
            btnHeatmaps.Enabled = false;
            btnOverlays.Enabled = false;
            btnRecord.Enabled = false;
            btnReport.Enabled = false;
            btnViewResults.Enabled = false;
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

        private void enableMainButtons()
        {
            btnCal.Enabled = true;
            btnChooseModel.Enabled = true;
            btnGazepoint.Enabled = true;
            btnHeatmaps.Enabled = true;
            btnOverlays.Enabled = true;
            btnRecord.Enabled = true;
            btnReport.Enabled = true;
            btnViewResults.Enabled = true;
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
                if (globals.recording._recording == true)
                {
                    globals.recording._recording = false;
                    globals.recording.saveToFile();
                    globals.recording.close();
                }
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
            if (imgCounters == imgIndex - 1)
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

        private void bw_DoWorkOverlay(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 1; (i <= 1); i++)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {


                    //spinner.ShowDialog();
                    Size res = this.GetDpiSafeResolution();

                    ImageGenerator ig = new ImageGenerator();
                    ig.DestinationPath = globals.currentRecordingpath;

                    switch (globals.modelIndex)
                    {
                        case 0: //3D model
                            {

                                Eye_Tracking_Graph etg = new Eye_Tracking_Graph(30, false, name, globals.currentRecordingpath, "");
                                etg.OpenETGraphData(globals.currentRecordingpath, name);
                                etg._SourceLocation = globals.currentRecordingpath;
                                etg._DestinationPath = globals.currentRecordingpath;
                                etg.SaveETGraph3D();
                                //ig.deleteImages();

                                Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                                hm._SourceLocation = globals.currentRecordingpath;
                                hm._DestinationPath = globals.currentRecordingpath;
                                hm.SaveHeatmap3D();
                                //ig.deleteImages();
                                break;
                            }
                        case 1: //flythrough
                            {
                                Eye_Tracking_Graph etg = new Eye_Tracking_Graph(30, false, name, globals.currentRecordingpath, "");
                                etg.OpenETGraphData(globals.currentRecordingpath, name);
                                etg._SourceLocation = globals.currentRecordingpath + @"\" + name + ".wmv";
                                etg._DestinationPath = globals.currentRecordingpath;
                                etg.SaveETGraphVideo();
                                //ig.deleteImages();

                                Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                                hm.OpenHeatmapData(globals.currentRecordingpath, name);
                                hm._SourceLocation = globals.currentRecordingpath + @"\" + name + ".wmv";
                                hm._DestinationPath = globals.currentRecordingpath;
                                hm.SaveHeatmapVideo();
                                ig.deleteImages();
                                break;
                            }
                        case 2: //2D models
                            {
                                Eye_Tracking_Graph etg = new Eye_Tracking_Graph(30, false, name, globals.currentRecordingpath, "");
                                etg.OpenETGraphData(globals.currentRecordingpath, name);
                                etg._SourceLocation = globals.currentRecordingpath;
                                etg._DestinationPath = globals.currentRecordingpath;
                                etg._height = res.Height;
                                etg._width = res.Width;
                                etg.SaveETGraph2D();
                                //ig.deleteImages();

                                Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                                hm.OpenHeatmapData(globals.currentRecordingpath, name);
                                hm._SourceLocation = globals.currentRecordingpath;
                                hm._DestinationPath = globals.currentRecordingpath;
                                hm.SaveHeatmap2D();
                                ig.deleteImages();
                                break;
                            }
                        case 3: //Video
                            {
                                Eye_Tracking_Graph etg = new Eye_Tracking_Graph(30, false, name, globals.currentRecordingpath, "");
                                etg.OpenETGraphData(globals.currentRecordingpath, name);
                                etg._SourceLocation = ModelPath;
                                etg._DestinationPath = globals.currentRecordingpath + "\\";
                                etg.SaveETGraphVideo();
                                //ig.deleteImages();

                                Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                                hm.OpenHeatmapData(globals.currentRecordingpath, name);
                                hm._SourceLocation = ModelPath;
                                hm._DestinationPath = globals.currentRecordingpath + "\\";
                                hm.SaveHeatmapVideo();
                                ig.deleteImages();
                                break;
                            }
                    }
                }
            }
        }

        private void bw_RunWorkerCompletedOverlay(object sender, RunWorkerCompletedEventArgs e)
        {
            //spin = false;
            //spinner.Close();

            //ImageGenerator ig = new ImageGenerator();
            //ig.DestinationPath = globals.currentRecordingpath;
            //ig.deleteImages();
            MessageBox.Show("Overlays Created.");
        }

        // bool spin = false;
        private void bw_ProgressChangedOverlay(object sender, ProgressChangedEventArgs e)
        {

            //Spinningprogbar.Form1 spinner = new Spinningprogbar.Form1();

            //if(spin == false)
            //{
            //    spinner.Show(this);
            //    spin = true;
            //}

            //this.tbProgress.Text = (e.ProgressPercentage.ToString() + "%");
        }

        private void bw_DoWorkHeatmaps(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 1; (i <= 1); i++)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    Size res = this.GetDpiSafeResolution();
                    switch (globals.modelIndex)
                    {
                        case 0: //3D model
                            {
                                Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                                hm._SourceLocation = globals.currentRecordingpath + "\\";
                                hm._DestinationPath = globals.currentRecordingpath + "\\";
                                hm.SaveHeatmapOntoModel3D();
                                break;
                            }
                        case 1: //flythrough
                            {
                                Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                                hm.OpenHeatmapData(globals.currentRecordingpath, name);
                                hm._SourceLocation = globals.currentRecordingpath + @"\" + name + ".wmv";
                                hm._DestinationPath = globals.currentRecordingpath + "\\";
                                hm.SaveHeatmapOntoModelVideo();
                                break;
                            }
                        case 2: //2D models
                            {
                                Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                                hm.OpenHeatmapData(globals.currentRecordingpath, name);
                                hm._SourceLocation = globals.currentRecordingpath;
                                hm._DestinationPath = globals.currentRecordingpath + "\\";
                                hm.SaveHeatmapOntoModel2D();
                                break;
                            }
                        case 3: //Video
                            {
                                Heatmaps hm = new Heatmaps(name, globals.currentRecordingpath, res.Width, res.Height, "");
                                hm.OpenHeatmapData(globals.currentRecordingpath, name);
                                hm._SourceLocation = ModelPath;
                                hm._DestinationPath = globals.currentRecordingpath + "\\";
                                hm.SaveHeatmapOntoModelVideo();
                                break;
                            }
                    }
                }
            }
        }

        private void bw_RunWorkerCompletedHeatmaps(object sender, RunWorkerCompletedEventArgs e)
        {

            //ImageGenerator ig = new ImageGenerator();
            //ig.DestinationPath = globals.currentRecordingpath;
            //ig.deleteImages();
            MessageBox.Show("Heat map created.");
        }

        private void bw_ProgressChangedHeatmaps(object sender, ProgressChangedEventArgs e)
        {
            //this.tbProgress.Text = (e.ProgressPercentage.ToString() + "%");
        }

        private void bw_DoWorkGazePlot(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 1; (i <= 1); i++)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    Size res = this.GetDpiSafeResolution();
                    switch (globals.modelIndex)
                    {
                        case 0: //3D model
                            {
                                Eye_Tracking_Graph etg = new Eye_Tracking_Graph(30, false, name, globals.currentRecordingpath, "");
                                etg._SourceLocation = globals.currentRecordingpath;
                                etg._DestinationPath = globals.currentRecordingpath;
                                etg.SaveETGraphOntoModel3D();
                                break;
                            }
                        case 1: //flythrough
                            {
                                Eye_Tracking_Graph etg = new Eye_Tracking_Graph(30, false, name, globals.currentRecordingpath, "");
                                etg.OpenETGraphData(globals.currentRecordingpath, name);
                                etg._SourceLocation = globals.currentRecordingpath + @"\" + name + ".wmv";
                                etg._DestinationPath = globals.currentRecordingpath + "\\";
                                etg.SaveETGraphOntoModelVideo();
                                
                                break;
                            }
                        case 2: //2D models
                            {
                                Eye_Tracking_Graph etg = new Eye_Tracking_Graph(30, false, name, globals.currentRecordingpath, "");
                                etg.OpenETGraphData(globals.currentRecordingpath, name);
                                etg._SourceLocation = globals.currentRecordingpath;
                                etg._DestinationPath = globals.currentRecordingpath;
                                etg._width = res.Width;
                                etg._height = res.Height;
                                etg.SaveETGraphOntoModel2D();
                                break;
                            }
                        case 3: //Video
                            {
                                Eye_Tracking_Graph etg = new Eye_Tracking_Graph(30, false, name, globals.currentRecordingpath, "");
                                etg.OpenETGraphData(globals.currentRecordingpath, name);
                                etg._SourceLocation = ModelPath;
                                etg._DestinationPath = globals.currentRecordingpath + "\\";
                                etg.SaveETGraphOntoModelVideo();
                                break;
                            }
                    }
                }
            }
        }

        private void bw_RunWorkerCompletedGazePlot(object sender, RunWorkerCompletedEventArgs e)
        {
            //ImageGenerator ig = new ImageGenerator();
            //ig.DestinationPath = globals.currentRecordingpath;
            //ig.deleteImages();
            MessageBox.Show("Gaze-plot created.");
        }

        private void bw_ProgressChangedGazePlot(object sender, ProgressChangedEventArgs e)
        {
            //this.tbProgress.Text = (e.ProgressPercentage.ToString() + "%");

        }

        private void bw_DoWorkRecord(object sender, DoWorkEventArgs e)
        {

            //DisplayModel.DisplayModel dm = new DisplayModel.DisplayModel();
            //dm.Run(obj, globals.currentRecordingpath + @"\" + name, flythrough);

            //BackgroundWorker bw1 = new BackgroundWorker();
            //bw1.WorkerReportsProgress = true;
            //bw1.WorkerSupportsCancellation = true;
            //bw1.DoWork += new DoWorkEventHandler(bw_DoWorkRecording);
            //bw1.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChangedRecording);
            //bw1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompletedRecording);
            //bw1.RunWorkerAsync();

            globals.recording._recording = true;
            if (this.bw.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        private void bw_RunWorkerCompletedRecord(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (globals.modelIndex)
            {
                case 0: //3D model
                    {
                        break;
                    }
                case 1: //flythrough
                    {/*
                        globals.recording._recording = false;
                        vg.DestinationPath = globals.currentRecordingpath + @"\";// +name;
                        vg.ImagePath = globals.currentRecordingpath + @"\";// +name;
                        vg.ModelName = "";//name;
                        vg.Fps = 30;
                        Size res = this.GetDpiSafeResolution();
                        vg.FrameHeight = res.Height;
                        vg.FrameWidth = res.Width;
                        vg.createVideo();
                        System.IO.File.Move(globals.currentRecordingpath + @"\" + ".wmv", globals.currentRecordingpath + @"\" + name + ".wmv");*/
                        return;
                    }
                case 2: //2D models
                    {
                        break;
                    }
                case 3: //Video
                    {
                        break;
                    }
            }
        }

        private void bw_ProgressChangedRecord(object sender, ProgressChangedEventArgs e)
        {
            if (globals.recording._recording == false) return;
            MessageBox.Show("HEHEHEHEHEHE");
            //this.tbProgress.Text = (e.ProgressPercentage.ToString() + "%");
        }

        private void bw_DoWorkRecording(object sender, DoWorkEventArgs e)
        {
        }

        private void bw_RunWorkerCompletedRecording(object sender, RunWorkerCompletedEventArgs e)
        { }

        private void bw_ProgressChangedRecording(object sender, ProgressChangedEventArgs e)
        {

            //this.tbProgress.Text = (e.ProgressPercentage.ToString() + "%");
        }
        #endregion

        private void createNewProjectCtrlNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NTT_MiniForm ntt_mini = new NTT_MiniForm();
            ntt_mini.ShowDialog(this);
        }

        private void openProjectCtrlOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NTT_MiniForm ntt_mini = new NTT_MiniForm();
            ntt_mini.ShowDialog(this);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NTT_Settings ntt_mini = new NTT_Settings();
            ntt_mini.ShowDialog(this);

            editStyles();
        }

        public void editStyles()
        {
            //edit form
            this.BackColor = GlobalStyles.BackgroundColours;
            panel3.BackColor = GlobalStyles.ForegroundColours;
            tsMain.BackColor = GlobalStyles.ForegroundColours;
            //buttons
            btnCal.BackColor = GlobalStyles.ButtonColours;
            btnChooseModel.BackColor = GlobalStyles.ButtonColours;
            btnGazepoint.BackColor = GlobalStyles.ButtonColours;
            btnHeatmaps.BackColor = GlobalStyles.ButtonColours;
            btnOverlays.BackColor = GlobalStyles.ButtonColours;
            btnRecord.BackColor = GlobalStyles.ButtonColours;
            btnReport.BackColor = GlobalStyles.ButtonColours;
            btnViewResults.BackColor = GlobalStyles.ButtonColours;

            //text colors
            /*btnCal.ForeColor = GlobalStyles.TextColours;
            btnChooseModel.ForeColor = GlobalStyles.TextColours;
            btnGazepoint.ForeColor = GlobalStyles.TextColours;
            btnHeatmaps.ForeColor = GlobalStyles.TextColours;
            btnOverlays.ForeColor = GlobalStyles.TextColours;
            btnRecord.ForeColor = GlobalStyles.TextColours;
            btnReport.ForeColor = GlobalStyles.TextColours;
            btnViewResults.ForeColor = GlobalStyles.TextColours;*/


        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
