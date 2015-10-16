using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using Record_Class;

namespace NTT_Eye_Tracking
{
    public partial class frmDisplay : Form
    {
        int mtype;
        string[] imgLocations = new string[9];
        public frmDisplay(int modelType, string filePath, string[] filePaths)
        {
            try
            {
                //System.Timers.Timer timer1 = new System.Timers.Timer();
                InitializeComponent();
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                mtype = modelType;
                switch (modelType)
                {
                    //in each case we must show the appropriate model previewer and hide the others
                    case 0: //3D model
                        {
                            for (int i = 0; i < imgLocations.Length; i++)
                            {
                                imgLocations[i] = filePaths[i];
                            }
                            wmp_Display.Visible = false;
                            picDisplay.Visible = true;
                            picDisplay.ImageLocation = filePaths[0];
                            timer1.Interval = 3000;
                            timer1.Start();
                            break;
                        }
                    case 1: //flythrough
                        {
                            break;
                        }
                    case 2: //2D models
                        {
                            globals.recording._recording = true;
                            wmp_Display.Visible = false;
                            picDisplay.Visible = true;
                            picDisplay.ImageLocation = filePath;


                            //myTimer = new System.Timers.Timer(globals.recordTime);

                            //myTimer.Start();

                            //while(myTimer.Elapsed == globals.recordTime)
                            //{
                            //    if(myTimer.)
                            //}
                            //Thread.Sleep(5000);

                            //if(myTimer.Elapsed += )

                            //ProcessDialogKey(Keys.Escape);
                            //globals.recording._recording = false;
                            //this.Close();                        
                            break;
                        }
                    case 3: //Video
                        {
                            globals.recording._recording = true;
                            wmp_Display.Visible = true;
                            picDisplay.Visible = false;
                            wmp_Display.URL = filePath;
                            wmp_Display.Dock = DockStyle.Fill;
                            wmp_Display.Ctlcontrols.play();
                            wmp_Display.stretchToFit = true;
                            //globals.recording._recording = false;
                            break;
                        }
                }
            }
            catch (Exception f) { }
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
                {
                    globals.recording._recording = false;
                    if (mtype == 3)
                    {
                        wmp_Display.Ctlcontrols.stop();
                    }
                    globals.recording.saveToFile();
                    globals.recording.close();
                    this.Close();
                }
                return base.ProcessDialogKey(keyData);
            }
            catch (Exception f) { return false; }
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }

        private void wmp_Display_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //ProcessDialogKey(Keys.Escape);
        }


        int counters = 0;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            try
            {
                if (counters == imgLocations.Length)
                {
                    timer1.Stop();
                    ProcessDialogKey(Keys.Escape);
                }
                else
                {
                    globals.recording._recording = false;
                    globals.recording.saveToFile();
                    globals.recording.close();
                    picDisplay.ImageLocation = imgLocations[counters];
                    counters++;
                    Size res = this.GetDpiSafeResolution();
                    globals.recording = new Record(globals.currentRecordingpath + @"\", "view" + counters, res.Width, res.Height);
                    globals.recording._recording = true;
                }
            }
            catch (Exception f) { }
        }

        private Size GetDpiSafeResolution()
        {
            try
            {
                using (Graphics graphics = this.CreateGraphics())
                {
                    return new Size((Screen.PrimaryScreen.Bounds.Width * (int)graphics.DpiX) / 96
                      , (Screen.PrimaryScreen.Bounds.Height * (int)graphics.DpiY) / 96);
                }
            }
            catch (Exception f) { return new Size(0, 0); }
        }

    }
}
