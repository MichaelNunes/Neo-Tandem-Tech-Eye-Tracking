using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Settings_Class;

namespace Settings
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProjectSettings ps = new ProjectSettings("TestProject", @"C:\Users\Public\TestFolder");
            ModelSettings3D ms = new ModelSettings3D(@"C:\Users\Public\TestFolder", "testModel", 30, true, true);
            ps.SaveSettings();
            ms.SaveSettings();
        }
    }
}
