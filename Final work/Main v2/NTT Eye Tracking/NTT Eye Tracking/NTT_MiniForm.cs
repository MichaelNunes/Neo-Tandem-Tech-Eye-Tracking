﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Settings_Class;
using System.IO;

namespace NTT_Eye_Tracking
{
    public partial class NTT_MiniForm : Form
    {
        public NTT_MiniForm()
        {
            InitializeComponent();
        }

        //internal int globals.modelIndex = 0;

        private void NTT_MiniForm_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            //customize form design
            this.Height = panelNewOldProject.Height;
            this.Width = panelNewOldProject.Width;
            panelNewOldProject.Left = 0;
            panelNewOldProject.Top = 0;

            this.BackColor = GlobalStyles.ForegroundColours;
            panelNewOldProject.BackColor = GlobalStyles.ForegroundColours;
            globals.modelIndex = 0;
            caro_models.Image = GlobalStyles.models.ElementAt(0).modelImage;
            red_ModelDescription.Text = GlobalStyles.models.ElementAt(0).modelName;
            this.CenterToScreen();

            modSel = false;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            transitionForward(panelNewOldProject, panel_createNew);        
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Eye Project (.eye)|*.eye";
                openFileDialog1.FileName = "";
                openFileDialog1.ShowDialog();

                string path = openFileDialog1.FileName;

                if (path == "")
                {

                }
                else
                {
                    // MessageBox.Show(path);
                    string[] settings = File.ReadAllLines(path);
                    globals.m.ProjectName = settings[0];
                    string[] array = settings[1].Split('\\');
                    string dir = "";
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        dir += array[i] + "\\";
                    }
                    globals.m.Directory = dir;
                    Settings_Class.ProjectSettings ps = new ProjectSettings(settings[0], settings[1]);
                    string[] settingsss = File.ReadAllLines(dir + "\\" + settings[3]);
                    Settings_Class.ModelSettings3D ms = new ModelSettings3D(settingsss[0], Convert.ToInt32(settingsss[1]), Convert.ToBoolean(settingsss[2]), Convert.ToBoolean(settingsss[3]));
                    globals.m.SettingsProject = ps;
                    globals.m.SettingsModel = ms;

                    transitionForward(panelNewOldProject, panel_modelSelect);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            //transitionForward(panelNewOldProject, panel_createNew);
        }

        string tempPath = "";
        private void buttonAdv3_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowDialog();
                tempPath = fbd.SelectedPath;
                if(tempPath == "")
                {
                    //MessageBox.Show("");
                    buttonAdv2.Enabled = false;
                }
                else
                {
                    buttonAdv2.Enabled = true;
                }
                textBox1.Text = fbd.SelectedPath;
            }
            catch (DirectoryNotFoundException m)
            {
                MessageBox.Show("The directory could not be found");
            }
            catch (Exception k)
            {
                MessageBox.Show(k.Message);
            }
        }

        public void transitionForward(Panel oldPanel, Panel newPanel )
        {
            newPanel.Left = oldPanel.Width;
            newPanel.Top = 0;
            while(oldPanel.Left >= 0 - oldPanel.Width)
            {
                newPanel.Left = newPanel.Left - 1;
                oldPanel.Left = oldPanel.Left - 1;
            }
           
        }

        public void transitionBack(Panel oldPanel, Panel newPanel)
        {
            newPanel.Left = 0- oldPanel.Width;
            newPanel.Top = 0;
            while (newPanel.Left != 0)
            {
                newPanel.Left = newPanel.Left + 1;
                oldPanel.Left = oldPanel.Left + 1;
            }
        }

        private void buttonAdv1_Click(object sender, EventArgs e)
        {
            transitionBack(panel_createNew, panelNewOldProject);
        }

        private void buttonAdv2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    Exception m = new Exception("Text boxes are empty please fill them in");
                    throw m;
                }
                globals.m.ProjectName = textBox2.Text;
                globals.m.Directory = textBox1.Text;
                globals.m.createSubDirectories();

                transitionForward(panel_createNew, panel_modelSelect);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }


        }

        private void buttonAdv4_Click(object sender, EventArgs e)
        {
            transitionBack(panel_modelSelect, panelNewOldProject);
        }

        bool modSel = false;
        private void btnCreateRecording_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(globals.modelIndex.ToString());
            modSel = true;
            try
            {
                string name = "";
                ProjectSolution t = globals.m;
                if (tbx_RecordingName.Text == "")
                {
                    DateTime time = DateTime.Today;
                    name = "Recording" + time.Day + "_" + time.Month + "_" + time.Year + "_" + time.Second + "_" + time.Minute + "_" + time.Hour;
                }
                else
                {
                    name = tbx_RecordingName.Text;
                }
                if (globals.modelIndex == 2)//2D models
                {
                    string rec = System.IO.Path.Combine(t.Directory, "Recordings");
                    System.IO.Directory.CreateDirectory(rec);
                    string sub = System.IO.Path.Combine(rec, "2DModel");
                    sub = System.IO.Path.Combine(sub, name);
                    System.IO.Directory.CreateDirectory(sub);
                    globals.currentRecordingpath = sub;
                    globals.typeOfRecording = "2DModel";
                    globals.name = name;
                    this.Close();

                }
                else if (globals.modelIndex == 0 || globals.modelIndex == 1)//3D Models
                {
                    string rec = System.IO.Path.Combine(globals.m.Directory, "Recordings");
                    System.IO.Directory.CreateDirectory(rec);
                    string sub = System.IO.Path.Combine(rec, "3DModel");
                    sub = System.IO.Path.Combine(sub, name);
                    System.IO.Directory.CreateDirectory(sub);
                    globals.currentRecordingpath = sub;
                    globals.typeOfRecording = "3DModel";
                    this.Close();
                }
                else if (globals.modelIndex == 3)//Video Models
                {
                    string rec = System.IO.Path.Combine(globals.m.Directory, "Recordings");
                    System.IO.Directory.CreateDirectory(rec);
                    string sub = System.IO.Path.Combine(rec, "Video");
                    sub = System.IO.Path.Combine(sub, name);
                    System.IO.Directory.CreateDirectory(sub);
                    globals.currentRecordingpath = sub;
                    globals.typeOfRecording = "Video";
                    globals.name = name;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect recording type chosen");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }


        }

        private void buttonAdv5_Click(object sender, EventArgs e)
        {
        }

        private void btnImageForward_Click(object sender, EventArgs e)
        {
            if(globals.modelIndex == GlobalStyles.models.Count()-1)
            {
                globals.modelIndex = 0;
                caro_models.Image = GlobalStyles.models.ElementAt(globals.modelIndex).modelImage;
                red_ModelDescription.Text = GlobalStyles.models.ElementAt(globals.modelIndex).modelName;
            }
            else
            {
                globals.modelIndex++;
                caro_models.Image = GlobalStyles.models.ElementAt(globals.modelIndex).modelImage;
                red_ModelDescription.Text = GlobalStyles.models.ElementAt(globals.modelIndex).modelName;
            }
        }

        private void btnImageBack_Click(object sender, EventArgs e)
        {
            if (globals.modelIndex == 0)
            {
                globals.modelIndex =  GlobalStyles.models.Count()-1;
                caro_models.Image = GlobalStyles.models.ElementAt(globals.modelIndex).modelImage;
                red_ModelDescription.Text = GlobalStyles.models.ElementAt(globals.modelIndex).modelName;
            }
            else
            {
                globals.modelIndex--;
                caro_models.Image = GlobalStyles.models.ElementAt(globals.modelIndex).modelImage;
                red_ModelDescription.Text = GlobalStyles.models.ElementAt(globals.modelIndex).modelName;
            }
        }

        private void NTT_MiniForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!modSel)
            {
                globals.modelIndex = 999;
            }
            else return;
        }

        
    }
}
