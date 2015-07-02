using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTT_Eyetracking
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Results.Form1 m = new Results.Form1();
            m.Show();    }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog Saver = new SaveFileDialog();
                if (textBox2.Text == "")
                {
                    Saver.FileName = "Project";
                    textBox2.Text = "Project";
                }
                else
                {
                    Saver.FileName = textBox2.Text;

                }
                Saver.DefaultExt = ".eye";
                Saver.Filter = "Eye Project (.eye)|*.eye";
                Saver.ShowDialog();
                string FullDirectory = Saver.FileName;
                textBox1.Text = FullDirectory;
                string[] array = FullDirectory.Split('\\');
                string dir = "";
                for(int i=0;i<array.Length-1;i++)
                {
                    dir += array[i] + "\\";
                }
                string[] save = new string[4];
                save[0] = textBox2.Text;
                save[1] = dir;
                save[2] = textBox2.Text + ".set";
                save[3] = "3DModel.set";
                File.WriteAllLines(Saver.FileName, save);
            }
            catch(DirectoryNotFoundException m)
            {
                MessageBox.Show(m.Message);
            }
            catch(Exception k)
            {
                MessageBox.Show(k.Message);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;
            //MessageBox.Show(m);
            string[] settings = File.ReadAllLines(path);
        }
    }
}
