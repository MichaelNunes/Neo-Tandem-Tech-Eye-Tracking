using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;

namespace NTT_Eyetracking
{
    [TestFixture]
    class ProjectSolutionTesting
    {
        public static ProjectSolution test = new ProjectSolution("NewProject", Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
        Settings_Class.ProjectSettings testProject = test.SettingsProject;
            
        [Test]
        public void Tester()
        {
            Assert.AreEqual("NewProject", test.ProjectName);
            //Projects
            Assert.AreEqual(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), test.Directory);
            Assert.AreEqual("NewProject", testProject.ProjectName1);
            //Model creation tests
            Assert.AreEqual(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), test.SettingsProject.ProjectLocation1);
            Assert.AreEqual(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory),test.SettingsModel.ModelLocation1);
            Assert.AreEqual(30,test.SettingsModel.FPS1);
            Assert.AreEqual(false, test.SettingsModel.Textures1);
            Assert.AreEqual(true, test.SettingsModel.Lighting1);
        }

    }
}
