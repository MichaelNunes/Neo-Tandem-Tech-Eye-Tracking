using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Settings_Class
{    
    public class GeneralSettings
    {

    }

    public class ProjectSettings
    {
        string ProjectLocation;
        string ProjectName;

        public string _ProjectLocation
        {
            set;
            get;
        }

        public string _ProjectName
        {
            set;
            get;
        }

        public ProjectSettings(string projectName, string projectLocation)
        {
            ProjectName = projectName;
            ProjectLocation = projectLocation;
        }

        public void SaveSettings()
        {
            string[] settings = { ProjectName, ProjectLocation };
            System.IO.File.WriteAllLines(ProjectLocation + "\\Settings\\" + ProjectName + ".set", settings);
        }

        public void ReadSettings()
        {
            string[] lines = System.IO.File.ReadAllLines(ProjectLocation + "\\Settings\\" + ProjectName + ".set");
            ProjectLocation = lines[1];
            ProjectName = lines[0];
        }
    }

    public class ModelSettings3D
    {
        string ModelLocation;
        string ModelName;
        int FPS;
        bool Textures;
        bool Lighting;

        public string _ModelLocation
        {
            set;
            get;
        }

        public string _ModelName
        {
            set;
            get;
        }

        public int _FPS
        {
            set;
            get;
        }

        public bool _Textures
        {
            set;
            get;
        }

        public bool _Lighting
        {
            set;
            get;
        }

        public ModelSettings3D()
        {
            FPS = 30;
            Textures = false;
            Lighting = true;
        }

        public ModelSettings3D(string modelLocation, string modelName, int fps, bool textures, bool lighting)
        {
            ModelLocation = modelLocation;
            ModelName = modelName;
            FPS = fps;
            Textures = textures;
            Lighting = lighting;
        }

        public void SaveSettings()
        {
            string[] settings = { ModelName, ModelLocation, FPS.ToString(), Textures.ToString(), Lighting.ToString() };
            System.IO.File.WriteAllLines(ModelLocation + "\\Settings\\" + ModelName + ".set", settings);
        }

        public void ReadSettings()
        {
            string[] lines = System.IO.File.ReadAllLines(ModelLocation + "\\Settings\\" + ModelName + ".set");
            ModelName = lines[0];
            ModelLocation = lines[1];
            FPS = Convert.ToInt32(lines[2]);
            Textures = Convert.ToBoolean(lines[3]);
            Lighting = Convert.ToBoolean(lines[4]);
        }
    }   
}
