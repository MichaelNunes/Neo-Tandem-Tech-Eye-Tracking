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
using Settings_Class;
using Ninject.Modules;


namespace NTT_Eyetracking
{
    
    public partial class Form1 : Form
    {
        //Ninject.IKernel kernal;
        public Form1()
        {
            
            InitializeComponent();
            // kernal = new Ninject.StandardKernel();
             //kernal.Bind<ProjectSolution>().ToSelf();
            //System.Reflection.Assembly m = System.Reflection.Assembly.GetExecutingAssembly();
            Bindings n = new Bindings();
            //kernal.Load(AppDomain.CurrentDomain.GetAssemblies());
             //kernal.Load((System.Collections.Generic.IEnumerable<Bindings>));
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Results.Form1 m = new Results.Form1();
            //m.Show();    
        }

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
                Saver.CheckPathExists = true;
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
                //globals.m = new ProjectSolution(textBox2.Text,dir);
                //globals.m = kernal.Components.Get<Bindings>();
            }
            catch(DirectoryNotFoundException m)
            {
                MessageBox.Show("The directory could not be found");
            }
            catch(Exception k)
            {
                MessageBox.Show(k.Message);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                openFileDialog1.Filter = "Eye Project (.eye)|*.eye";
                openFileDialog1.FileName = "";
                openFileDialog1.ShowDialog();
                
                string path = openFileDialog1.FileName;
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
                //MessageBox.Show("The following is the contents of proj .set" + globals.m.SettingsProject.ProjectLocation1 + " " + globals.m.SettingsProject.ProjectName1);
                //MessageBox.Show("The following is the contents of model .set" + globals.m.SettingsModel.FPS1 + " " + globals.m.SettingsModel.Lighting1+" " + globals.m.SettingsModel.ModelLocation1+" " + globals.m.SettingsModel.Textures1);
                Main show = new Main();
                this.Hide();
                show.ShowDialog();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    Exception m = new Exception("Text boxes are empty please fill them in");
                    throw m;
                }
                globals.m.createSubDirectories();
                this.Hide();
                Main show = new Main();
                show.ShowDialog();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }
    }
}
