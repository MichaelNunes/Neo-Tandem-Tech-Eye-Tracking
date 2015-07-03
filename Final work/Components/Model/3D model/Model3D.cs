#region Legal
/*
 * Copyright (c) 2015 The University of Pretoria.
 *
 * The following was designed for the Centre of GeoInformation
 * Science (CGIS), University of Pretoria. All code is property
 * of the University of Pretoria and is available under the 
 * Creative Commons Attribution-ShareAlike (CC BY-SA) see:
 * "https://creativecommons.org/licenses/"
 *
 * Author: Duran Cole
 * Email: u13329414@tuks.co.za
 * Author: Michael Nunes
 * Email: u12104592@tuks.co.za
 * Author: Molefe Molefe
 * Email: u12260429@tuks.co.za
 * Author: Tebogo Christopher Seshibe
 * Email: u13181442@tuks.co.za
 * Author: Timothy Snayers
 * Email: u13397134@tuks.co.za
 */
#endregion

using System;
using System.Collections.Generic;

using OpenTK;

using DisplayModel;

namespace DisplayModel
{
    /// <summary>
    /// Represents a 3D object model.
    /// </summary>
    public class Model3D : Model, IDisposable
    {
        #region Fields
        private uint framerate;
        private List<Keyframe> keyframes;
        private List<Vector3> changes;

        private Transform transform;
        private Material material;
        private Model3DWindow gameWindow;
        #endregion

        #region Constructors
        public Model3D()
        {
            keyframes = new List<Keyframe>();

            transform = new Transform();
            material = new Material();
            gameWindow = new Model3DWindow();
        }
        #endregion

        #region Model Superclass Methods
        protected override void startRecording()
        {
            // IMPLEMENT
        }

        protected override void stopRecording()
        {
            // IMPLEMENT
        }

        protected override void saveToFile()
        {
            // IMPLEMENT
        }
        #endregion

        #region Model Specific Methods
        public void addKeyframe()
        {

        }

        public void removeKeyframe()
        {

        }

        public void Update()
        {

        }
        #endregion

        #region Attributes
        /// <summary>
        /// 
        /// </summary>
        public Transform Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Material Material
        {
            get { return material; }
            set { material = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Model3DWindow Model3DWindow
        {
            get { return gameWindow; }
            set { gameWindow = value; }
        }
        #endregion

        public override string ToString()
        {
            return "Hello";
        }
        void IDisposable.Dispose()
        {
            Console.WriteLine("Thrown");
        }
    }
}