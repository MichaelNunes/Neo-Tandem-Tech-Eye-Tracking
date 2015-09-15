using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTT_Eye_Tracking
{
    public partial class frmDisplay : Form
    {
        public frmDisplay(int modelType, string filePath)
        {
            InitializeComponent();
            switch (globals.modelIndex)
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
                        break;
                    }
                case 3: //Video
                    {
                        break;
                    }
            }
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }
    }
}
