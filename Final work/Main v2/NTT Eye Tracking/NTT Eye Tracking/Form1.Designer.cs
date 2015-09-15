namespace NTT_Eye_Tracking
{
    partial class NTT_EyeTracker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NTT_EyeTracker));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnViewResults = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnReport = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnGazepoint = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnOverlays = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnHeatmaps = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnChooseModel = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnRecord = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnCal = new Syncfusion.Windows.Forms.ButtonAdv();
            this.tsMain = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.wmp_VideoPreview = new AxWMPLib.AxWindowsMediaPlayer();
            this.pic_model2DPreview = new System.Windows.Forms.PictureBox();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.createNewProjectCtrlNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectCtrlOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wmp_VideoPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_model2DPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.wmp_VideoPreview);
            this.panel1.Controls.Add(this.pic_model2DPreview);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1090, 730);
            this.panel1.TabIndex = 19;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnViewResults);
            this.panel3.Controls.Add(this.btnReport);
            this.panel3.Controls.Add(this.btnGazepoint);
            this.panel3.Controls.Add(this.btnOverlays);
            this.panel3.Controls.Add(this.btnHeatmaps);
            this.panel3.Controls.Add(this.btnChooseModel);
            this.panel3.Controls.Add(this.btnRecord);
            this.panel3.Controls.Add(this.btnCal);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(890, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 730);
            this.panel3.TabIndex = 1;
            // 
            // btnViewResults
            // 
            this.btnViewResults.BeforeTouchSize = new System.Drawing.Size(122, 43);
            this.btnViewResults.IsBackStageButton = false;
            this.btnViewResults.Location = new System.Drawing.Point(39, 527);
            this.btnViewResults.Name = "btnViewResults";
            this.btnViewResults.Size = new System.Drawing.Size(122, 43);
            this.btnViewResults.TabIndex = 42;
            this.btnViewResults.Text = "View Results";
            this.btnViewResults.Click += new System.EventHandler(this.btnViewResults_Click);
            // 
            // btnReport
            // 
            this.btnReport.BeforeTouchSize = new System.Drawing.Size(122, 43);
            this.btnReport.IsBackStageButton = false;
            this.btnReport.Location = new System.Drawing.Point(39, 478);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(122, 43);
            this.btnReport.TabIndex = 41;
            this.btnReport.Text = "Print report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnGazepoint
            // 
            this.btnGazepoint.BeforeTouchSize = new System.Drawing.Size(122, 43);
            this.btnGazepoint.IsBackStageButton = false;
            this.btnGazepoint.Location = new System.Drawing.Point(39, 429);
            this.btnGazepoint.Name = "btnGazepoint";
            this.btnGazepoint.Size = new System.Drawing.Size(122, 43);
            this.btnGazepoint.TabIndex = 40;
            this.btnGazepoint.Text = "Print model gaze-plot";
            this.btnGazepoint.Click += new System.EventHandler(this.btnGazepoint_Click);
            // 
            // btnOverlays
            // 
            this.btnOverlays.BeforeTouchSize = new System.Drawing.Size(122, 43);
            this.btnOverlays.IsBackStageButton = false;
            this.btnOverlays.Location = new System.Drawing.Point(39, 331);
            this.btnOverlays.Name = "btnOverlays";
            this.btnOverlays.Size = new System.Drawing.Size(122, 43);
            this.btnOverlays.TabIndex = 39;
            this.btnOverlays.Text = "Print overlays";
            this.btnOverlays.Click += new System.EventHandler(this.btnOverlays_Click);
            // 
            // btnHeatmaps
            // 
            this.btnHeatmaps.BeforeTouchSize = new System.Drawing.Size(122, 43);
            this.btnHeatmaps.IsBackStageButton = false;
            this.btnHeatmaps.Location = new System.Drawing.Point(39, 380);
            this.btnHeatmaps.Name = "btnHeatmaps";
            this.btnHeatmaps.Size = new System.Drawing.Size(122, 43);
            this.btnHeatmaps.TabIndex = 38;
            this.btnHeatmaps.Text = "Print model heat-maps";
            this.btnHeatmaps.Click += new System.EventHandler(this.btnHeatmaps_Click);
            // 
            // btnChooseModel
            // 
            this.btnChooseModel.BeforeTouchSize = new System.Drawing.Size(122, 43);
            this.btnChooseModel.IsBackStageButton = false;
            this.btnChooseModel.Location = new System.Drawing.Point(39, 233);
            this.btnChooseModel.Name = "btnChooseModel";
            this.btnChooseModel.Size = new System.Drawing.Size(122, 43);
            this.btnChooseModel.TabIndex = 37;
            this.btnChooseModel.Text = "Choose model";
            this.btnChooseModel.Click += new System.EventHandler(this.btnChooseModel_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.BeforeTouchSize = new System.Drawing.Size(122, 43);
            this.btnRecord.IsBackStageButton = false;
            this.btnRecord.Location = new System.Drawing.Point(39, 282);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(122, 43);
            this.btnRecord.TabIndex = 36;
            this.btnRecord.Text = "Begin recording";
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnCal
            // 
            this.btnCal.BeforeTouchSize = new System.Drawing.Size(122, 43);
            this.btnCal.IsBackStageButton = false;
            this.btnCal.Location = new System.Drawing.Point(39, 184);
            this.btnCal.Name = "btnCal";
            this.btnCal.Size = new System.Drawing.Size(122, 43);
            this.btnCal.TabIndex = 35;
            this.btnCal.Text = "Calibrate";
            this.btnCal.Click += new System.EventHandler(this.btnCal_Click);
            // 
            // tsMain
            // 
            this.tsMain.ForeColor = System.Drawing.Color.MidnightBlue;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Image = null;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.tsMain.LauncherStyle = Syncfusion.Windows.Forms.Tools.LauncherStyle.Office12;
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Office12Mode = false;
            this.tsMain.ShowLauncher = false;
            this.tsMain.Size = new System.Drawing.Size(1090, 25);
            this.tsMain.TabIndex = 21;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // wmp_VideoPreview
            // 
            this.wmp_VideoPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wmp_VideoPreview.Enabled = true;
            this.wmp_VideoPreview.Location = new System.Drawing.Point(0, 0);
            this.wmp_VideoPreview.Name = "wmp_VideoPreview";
            this.wmp_VideoPreview.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmp_VideoPreview.OcxState")));
            this.wmp_VideoPreview.Size = new System.Drawing.Size(890, 730);
            this.wmp_VideoPreview.TabIndex = 3;
            // 
            // pic_model2DPreview
            // 
            this.pic_model2DPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_model2DPreview.Location = new System.Drawing.Point(0, 0);
            this.pic_model2DPreview.Name = "pic_model2DPreview";
            this.pic_model2DPreview.Size = new System.Drawing.Size(890, 730);
            this.pic_model2DPreview.TabIndex = 2;
            this.pic_model2DPreview.TabStop = false;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewProjectCtrlNToolStripMenuItem,
            this.openProjectCtrlOToolStripMenuItem,
            this.toolStripSeparator1,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // createNewProjectCtrlNToolStripMenuItem
            // 
            this.createNewProjectCtrlNToolStripMenuItem.Name = "createNewProjectCtrlNToolStripMenuItem";
            this.createNewProjectCtrlNToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.createNewProjectCtrlNToolStripMenuItem.Text = "Create New Project (Ctrl + N)";
            // 
            // openProjectCtrlOToolStripMenuItem
            // 
            this.openProjectCtrlOToolStripMenuItem.Name = "openProjectCtrlOToolStripMenuItem";
            this.openProjectCtrlOToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.openProjectCtrlOToolStripMenuItem.Text = "Open Project (Ctrl + O)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(225, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // NTT_EyeTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1090, 755);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tsMain);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.IsMdiContainer = true;
            this.Name = "NTT_EyeTracker";
            this.Text = "Neo Tandem Technology - Eye Tracking";
            this.TransparencyKey = System.Drawing.Color.MintCream;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.NTT_EyeTracker_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wmp_VideoPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_model2DPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private Syncfusion.Windows.Forms.ButtonAdv btnViewResults;
        private Syncfusion.Windows.Forms.ButtonAdv btnReport;
        private Syncfusion.Windows.Forms.ButtonAdv btnGazepoint;
        private Syncfusion.Windows.Forms.ButtonAdv btnOverlays;
        private Syncfusion.Windows.Forms.ButtonAdv btnHeatmaps;
        private Syncfusion.Windows.Forms.ButtonAdv btnChooseModel;
        private Syncfusion.Windows.Forms.ButtonAdv btnRecord;
        private Syncfusion.Windows.Forms.ButtonAdv btnCal;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx tsMain;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem createNewProjectCtrlNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectCtrlOToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.PictureBox pic_model2DPreview;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private AxWMPLib.AxWindowsMediaPlayer wmp_VideoPreview;

    }
}

