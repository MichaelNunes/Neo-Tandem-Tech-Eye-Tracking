using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TETCSharpClient;

namespace NTT_Eyetracking
{
    public partial class Calibration : Form
    {
        public Calibration()
        {
            InitializeComponent();
        }

        private void Calibration_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "The following page is to open up the calibration for the eyetribe camera.This will launch the application.Please ensure that it is installed at the following directory......";
            richTextBox1.Text = "\n\r";
            richTextBox1.Text = "Click the start calibration button to start the calibration program";
            richTextBox1.Text = "\n\r";
            richTextBox1.Text = "Click Done when you are happy with the calibration";
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\EyeTribe\Client\EyeTribeUIWin.exe");
        }
    }
}
