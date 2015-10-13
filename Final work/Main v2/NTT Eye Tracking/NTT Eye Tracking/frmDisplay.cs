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
        public frmDisplay(int modelType, string filePath, string[] filePaths)
        {
            System.Timers.Timer myTimer = new System.Timers.Timer();
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            mtype = modelType;
            switch (modelType)
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

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                globals.recording._recording = false;
                if(mtype == 3)
                {
                    wmp_Display.Ctlcontrols.stop();
                }
                globals.recording.saveToFile();
                globals.recording.close();
                this.Close();
            }
            return base.ProcessDialogKey(keyData);
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }

        private void wmp_Display_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //ProcessDialogKey(Keys.Escape);
        }
    }
}
