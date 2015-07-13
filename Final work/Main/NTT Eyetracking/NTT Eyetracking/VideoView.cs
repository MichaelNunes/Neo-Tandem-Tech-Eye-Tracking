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
    public partial class VideoView : Form
    {
        private bool fullscreen = false;
        bool side = false;
        public VideoView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            axWindowsMediaPlayer1.URL = openFileDialog1.FileName;
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            fullscreen = true;
            axWindowsMediaPlayer1.Dock = DockStyle.Fill;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            side = true;
            axWindowsMediaPlayer1.Dock = DockStyle.Left;
            axWindowsMediaPlayer2.Dock = DockStyle.Right;
            axWindowsMediaPlayer1.Width = (this.Width / 2) - 1;
            axWindowsMediaPlayer2.Width = (this.Width / 2) - 1;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                if (fullscreen == true)
                {
                    axWindowsMediaPlayer1.Dock = DockStyle.None;
                    axWindowsMediaPlayer1.Height = 200;
                    axWindowsMediaPlayer1.Width = 400;
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    fullscreen = false;

                }
                else if (side == true)
                {
                    axWindowsMediaPlayer1.Dock = DockStyle.None;
                    axWindowsMediaPlayer2.Dock = DockStyle.Left;
                    axWindowsMediaPlayer2.Dock = DockStyle.None;
                    axWindowsMediaPlayer1.Height = 200;
                    axWindowsMediaPlayer1.Width = 400;
                    axWindowsMediaPlayer2.Top += axWindowsMediaPlayer1.Height + 25;
                    axWindowsMediaPlayer2.Height = 200;
                    axWindowsMediaPlayer2.Width = 400;
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    side = false;
                }
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
