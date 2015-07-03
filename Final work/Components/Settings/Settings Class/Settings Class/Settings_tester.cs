using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Settings_Class
{
    [TestFixture]
    class Settings_tester
    {
        [Test]
        public void testerForProjSettings()
        {
            ProjectSettings proj = new ProjectSettings("name1", "local");

            Assert.AreEqual("name1", proj.ProjectName1);
            Assert.AreEqual("local", proj.ProjectLocation1);
        }

        [Test]
        public void testerForProjSettingsDefault()
        {
            ModelSettings3D model = new ModelSettings3D();
            Assert.AreEqual("",model.ModelLocation1);
            Assert.AreEqual(30,model.FPS1);
            Assert.AreEqual(true,model.Lighting1);
            Assert.AreEqual(false,model.Textures1);
        }

        [Test]
        public void testerForProjSettingsOneParam()
        {
            ModelSettings3D model = new ModelSettings3D("MyLocation");
            Assert.AreEqual("MyLocation", model.ModelLocation1);
            Assert.AreEqual(30, model.FPS1);
            Assert.AreEqual(true, model.Lighting1);
            Assert.AreEqual(false, model.Textures1);
        }

        [Test]
        public void testerForProjSettingsAllParams()
        {
            ModelSettings3D model = new ModelSettings3D("MyLocation",60,true,false);
            Assert.AreEqual("MyLocation", model.ModelLocation1);
            Assert.AreEqual(60, model.FPS1);
            Assert.AreEqual(false, model.Lighting1);
            Assert.AreEqual(true, model.Textures1);
        }
    }
}
