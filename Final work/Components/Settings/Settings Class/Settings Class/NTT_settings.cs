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

        public string ProjectLocation1
        {
            get { return ProjectLocation; }
            set { ProjectLocation = value; }
        }
        string ProjectName;

        public string ProjectName1
        {
            get { return ProjectName; }
            set { ProjectName = value; }
        }


        

        public ProjectSettings(string projectName, string projectLocation)
        {
            ProjectName = projectName;
            ProjectLocation = projectLocation;
        }

        public void SaveSettings()
        {
            string[] settings = { ProjectName, ProjectLocation };
            System.IO.File.WriteAllLines(ProjectLocation + "\\" + ProjectName + ".set", settings);
        }

        public void ReadSettings()
        {
            string[] lines = System.IO.File.ReadAllLines(ProjectLocation + "\\" + ProjectName + ".set");
            ProjectLocation = lines[1];
            ProjectName = lines[0];
        }
    }

    public class ModelSettings3D
    {
        string ModelLocation;

        public string ModelLocation1
        {
            get { return ModelLocation; }
            set { ModelLocation = value; }
        }
        int FPS;

        public int FPS1
        {
            get { return FPS; }
            set { FPS = value; }
        }
        bool Textures;

        public bool Textures1
        {
            get { return Textures; }
            set { Textures = value; }
        }
        bool Lighting;

        public bool Lighting1
        {
            get { return Lighting; }
            set { Lighting = value; }
        }

        

        public ModelSettings3D()
        {
            ModelLocation = "";
            FPS = 30;
            Textures = false;
            Lighting = true;
        }

        public ModelSettings3D(string modelLocation)
        {
            ModelLocation = modelLocation;
            FPS = 30;
            Textures = false;
            Lighting = true;
        }

        public ModelSettings3D(string modelLocation, int fps, bool textures, bool lighting)
        {
            ModelLocation = modelLocation;
            FPS = fps;
            Textures = textures;
            Lighting = lighting;
        }

        public void SaveSettings()
        {
            string[] settings = { ModelLocation, FPS.ToString(), Textures.ToString(), Lighting.ToString() };
            System.IO.File.WriteAllLines(ModelLocation + "\\" + "3DModel.set", settings);
        }

        public void ReadSettings()
        {
            string[] lines = System.IO.File.ReadAllLines(ModelLocation + "\\" + "3DModel.set");
            ModelLocation = lines[0];
            FPS = Convert.ToInt32(lines[1]);
            Textures = Convert.ToBoolean(lines[2]);
            Lighting = Convert.ToBoolean(lines[3]);
        }
    }   
}
