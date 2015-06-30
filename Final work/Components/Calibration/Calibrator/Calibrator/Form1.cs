using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TETCSharpClient;
using TETControls.Calibration;
using TETControls.Cursor;
using TETControls.TrackBox;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Windows.Interop;

namespace Calibrator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            CalibrationRunner calRunner = new CalibrationRunner();
            calRunner.OnResult += calRunner_OnResult;
            calRunner.Start();
        }

        private void calRunner_OnResult(object sender, CalibrationRunnerEventArgs e)
        {
            switch (e.Result)
            {
                case CalibrationRunnerResult.Success:
                    MessageBox.Show(this, "Calibration success " + e.CalibrationResult.AverageErrorDegree);
                    break;

                case CalibrationRunnerResult.Abort:
                    MessageBox.Show(this, "The calibration was aborted. Reason: " + e.Message);
                    break;

                case CalibrationRunnerResult.Error:
                    MessageBox.Show(this, "An error occured during calibration. Reason: " + e.Message);
                    break;

                case CalibrationRunnerResult.Failure:
                    MessageBox.Show(this, "Calibration failed. Reason: " + e.Message);
                    break;

                case CalibrationRunnerResult.Unknown:
                    MessageBox.Show(this, "Calibration exited with unknown state. Reason: " + e.Message);
                    break;
            }
        }


        public class ListenToMe : ICalibrationResultListener
        {
            public ListenToMe()
            {
                GazeManager.Instance.Activate(GazeManager.ApiVersion.VERSION_1_0, GazeManager.ClientMode.Push);
                GazeManager.Instance.AddCalibrationResultListener(this);
            }

            private void Calibrate()
            {
                ////Run the calibration on 'this' monitor
                //var ActiveScreen = Screen.FromHandle(new WindowInteropHelper(this).Handle);

                //// Initialize and start the calibration
                //CalibrationRunner calRunner = new CalibrationRunner(ActiveScreen, ActiveScreen.Bounds.Size, 9);

                //bool isCalibrated = calRunner.Start();
                //if (!isCalibrated) return;

                //// Show overall accuracy (in degrees) of last calibration (wait here for it)
                //Console.Out.WriteLine(GazeManager.Instance.LastCalibrationResult.AverageErrorDegree);
            }

            // Interface method (callback)
            public void OnCalibrationChanged(bool isCalibrated, CalibrationResult calibResult)
            {
                Console.Out.WriteLine(calibResult.AverageErrorDegree);
            }
        }


    }
}
