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
        }
        List<List<String>> settings = new List<List<String>>();
        List<Color> theme = new List<Color>(10);
     
        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.AnyColor = false;
            colorDialog1.AnyColor = false;
            colorDialog1.SolidColorOnly = false;
            colorDialog1.FullOpen = false;
           // MessageBox.Show(colorDialog1.FullOpen.ToString();
            colorDialog1.ShowDialog();
            Color colstr = colorDialog1.Color;
            MessageBox.Show(colstr.Name);
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox1.SelectedItem.ToString();
            switch(comboBox1.SelectedItem.ToString())
            {
                case "Light":
                    {
                        
                        theme[0] = Color.Azure;
                        theme[1]= Color.WhiteSmoke;
                        theme[2]= Color.Black ;
                        //MessageBox.Show(theme[1]);
                        button2.BackColor =theme[1];

                        break;
                    }
                case "Dark":
                    {
                        theme[0] =  Color.LightSlateGray ;
                        theme[1] = Color.Silver ;
                        theme[2] = Color.White ;
                        //MessageBox.Show(theme[1]);
                        button2.BackColor = theme[1];
                        break;
                    }
                case "NTT":
                    {
                        theme[0] =  Color.FromArgb(1,209,100,7) ;
                        theme[1] = Color.Orange ;
                        theme[2] = Color.Black ;
                        //button2.BackColor = Color.FromName(theme[1]);
                        button2.BackColor = theme[1];
                        break;
                    }
                 default:
                    {
                        theme[0] =  Color.Azure ;
                        theme[1] = Color.WhiteSmoke ;
                        theme[2] = Color.Black ;
                       // MessageBox.Show(theme[1]);
                        button2.BackColor = theme[1];
                        break;
                    }

            }
            
            
        }
    }
}
