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

namespace NTT_Eye_Tracking
{
    public partial class frmDisplay : Form
    {
        int mtype;
        public frmDisplay(int modelType, string filePath, string[] filePaths)
        {
            System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
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
                        wmp_Display.Visible = false;
                        picDisplay.Visible = true;
                        picDisplay.ImageLocation = filePath;
                        myTimer.Interval = globals.recordTime;
                        myTimer.Start();
                        
                        //this.Close();                        
                        break;
                    }
                case 3: //Video
                    {
                        wmp_Display.Visible = true;
                        picDisplay.Visible = false;
                        wmp_Display.URL = filePath;
                        wmp_Display.Dock = DockStyle.Fill;                        
                        wmp_Display.Ctlcontrols.play();

                        break;
                    }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                if(mtype == 3)
                {
                    wmp_Display.Ctlcontrols.stop();
                }
                this.Close();
            }
            return base.ProcessDialogKey(keyData);
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }
    }
}
