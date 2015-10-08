using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace settingsform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                theme.Add(Color.White);
            }
            for (int i = 0; i < 6; i++)
            {
                settings.Add("");
            }
        }
        System.Collections.ArrayList settings = new System.Collections.ArrayList(10);
        List<Color> theme = new List<Color>(10);
     /// <summary>
     /// Index item list
     /// 
     /// 0= Colour list
     /// 1= 3D slideshow time
     /// 2= show numbers on gaze plot points
     /// 3= GP point history
     /// 4= yes/no directional light
     /// 5= yes/no textures
     /// </summary>
     /// <param name="sender"></param>
     /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(settings.Count.ToString());
            settings[0] = theme;
            settings[1] = numericUpDown1.Value;
            settings[2] = checkBox1.Checked;
            settings[3] = numericUpDown2.Value;
            settings[4] = checkBox2.Checked;
            settings[5] = checkBox3.Checked;

            MessageBox.Show(settings[3].ToString() + settings[5].ToString());

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
      
            switch(comboBox1.SelectedItem.ToString())
            {
                case "Light":
                    {
                        
                        theme[0] = Color.Azure;
                        theme[1]= Color.WhiteSmoke;
                        theme[2]= Color.Black ;
                        //MessageBox.Show(theme[1]);
                        
                        textBox1.BackColor = theme[0];
                        button2.BackColor = theme[1];
                        textBox1.ForeColor = theme[2];

                        break;
                    }
                case "Dark":
                    {
                        theme[0] =  Color.LightSlateGray ;
                        theme[1] = Color.Silver ;
                        theme[2] = Color.White ;
                        textBox1.BackColor = theme[0];
                        button2.BackColor = theme[1];
                        textBox1.ForeColor = theme[2];
                        break;
                    }
                case "NTT":
                    {
                        theme[0] =  Color.FromArgb(209,100,7) ;
                        theme[1] = Color.Orange ;
                        theme[2] = Color.Black ;
                        textBox1.BackColor = theme[0];
                        button2.BackColor = theme[1];
                        textBox1.ForeColor = theme[2];
                        break;
                    }
                 default:
                    {
                        theme[0] =  Color.Azure ;
                        theme[1] = Color.WhiteSmoke ;
                        theme[2] = Color.Black ;
                        textBox1.BackColor = theme[0];
                        button2.BackColor = theme[1];
                        textBox1.ForeColor = theme[2];
                        break;
                    }

            }
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.Azure;
            button2.BackColor = Color.WhiteSmoke;
            textBox1.ForeColor = Color.Black;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
