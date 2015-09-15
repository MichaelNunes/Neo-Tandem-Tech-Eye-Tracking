using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTT_Eyetracking
{
    public partial class PDFViewer : Form
    {
        string pdf;
        public PDFViewer()
        {
            InitializeComponent();
        }
        public PDFViewer(string local)
        {
            InitializeComponent();
            axAcroPDF1.src = local;
            pdf = local;
        }


        private void PDFViewer_Load(object sender, EventArgs e)
        {

        }
    }
}
