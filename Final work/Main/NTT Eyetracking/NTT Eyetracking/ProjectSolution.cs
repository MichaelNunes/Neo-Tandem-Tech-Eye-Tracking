using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Settings_Class;

namespace NTT_Eyetracking
{
    class ProjectSolution
    {
        string projectName;

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        string directory;

        public string Directory
        {
            get { return directory; }
            set { directory = value; }
        }

        private ProjectSettings settingsProject;

        public ProjectSettings SettingsProject
        {
            get { return settingsProject; }
            set { settingsProject = value; }
        }

  
        private ModelSettings3D settingsModel;

        public ModelSettings3D SettingsModel
        {
            get { return settingsModel; }
            set { settingsModel = value; }
        }

        public ProjectSolution()
        {

        }
        public ProjectSolution(string name,string dir)
        {
            projectName = name;
            directory = dir;
            settingsProject = new ProjectSettings(name,dir);
            settingsModel = new ModelSettings3D(dir,30,false,true);
            settingsProject.SaveSettings();
            settingsModel.SaveSettings();
        }
    }
}
