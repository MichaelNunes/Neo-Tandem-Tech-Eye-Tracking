﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Settings_Class;

namespace NTT_Eye_Tracking
{
    public class globals
    {    
        internal static ProjectSolution m = new ProjectSolution();
        internal static string typeOfRecording = "";
        internal static string currentRecordingpath = "";
        internal static string name = "";
        internal static int modelIndex = 0;
        internal static int recordTime = 5000;
    }
}