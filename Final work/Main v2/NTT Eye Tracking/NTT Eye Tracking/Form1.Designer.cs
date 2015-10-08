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
            this.pic_slideshowPreview = new System.Windows.Forms.Panel();
            this.btnImageForward = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnImageBack = new Syncfusion.Windows.Forms.ButtonAdv();
            this.wmp_VideoPreview = new AxWMPLib.AxWindowsMediaPlayer();
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
            this.pic_caro = new System.Windows.Forms.PictureBox();
            this.pic_model2DPreview = new System.Windows.Forms.PictureBox();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.createNewProjectCtrlNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectCtrlOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.pic_slideshowPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wmp_VideoPreview)).BeginInit();
            this.panel3.SuspendLayout();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_caro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_model2DPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.pic_slideshowPreview);
            this.panel1.Controls.Add(this.wmp_VideoPreview);
            this.panel1.Controls.Add(this.pic_model2DPreview);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1090, 730);
            this.panel1.TabIndex = 19;
            // 
            // pic_slideshowPreview
            // 
            this.pic_slideshowPreview.Controls.Add(this.pic_caro);
            this.pic_slideshowPreview.Controls.Add(this.btnImageForward);
            this.pic_slideshowPreview.Controls.Add(this.btnImageBack);
            this.pic_slideshowPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_slideshowPreview.Location = new System.Drawing.Point(0, 0);
            this.pic_slideshowPreview.Name = "pic_slideshowPreview";
            this.pic_slideshowPreview.Size = new System.Drawing.Size(856, 730);
            this.pic_slideshowPreview.TabIndex = 4;
            this.pic_slideshowPreview.Visible = false;
            // 
            // btnImageForward
            // 
            this.btnImageForward.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2007;
            this.btnImageForward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.btnImageForward.BeforeTouchSize = new System.Drawing.Size(34, 730);
            this.btnImageForward.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnImageForward.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImageForward.IsBackStageButton = false;
            this.btnImageForward.Location = new System.Drawing.Point(822, 0);
            this.btnImageForward.Name = "btnImageForward";
            this.btnImageForward.Size = new System.Drawing.Size(34, 730);
            this.btnImageForward.TabIndex = 23;
            this.btnImageForward.Text = ">";
            this.btnImageForward.UseVisualStyle = true;
            this.btnImageForward.Click += new System.EventHandler(this.btnImageForward_Click);
            // 
            // btnImageBack
            // 
            this.btnImageBack.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2007;
            this.btnImageBack.BeforeTouchSize = new System.Drawing.Size(34, 730);
            this.btnImageBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnImageBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImageBack.IsBackStageButton = false;
            this.btnImageBack.Location = new System.Drawing.Point(0, 0);
            this.btnImageBack.Name = "btnImageBack";
            this.btnImageBack.Size = new System.Drawing.Size(34, 730);
            this.btnImageBack.TabIndex = 24;
            this.btnImageBack.Text = "<";
            this.btnImageBack.UseVisualStyle = true;
            this.btnImageBack.Click += new System.EventHandler(this.btnImageBack_Click);
            // 
            // wmp_VideoPreview
            // 
            this.wmp_VideoPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wmp_VideoPreview.Enabled = true;
            this.wmp_VideoPreview.Location = new System.Drawing.Point(0, 0);
            this.wmp_VideoPreview.Name = "wmp_VideoPreview";
            this.wmp_VideoPreview.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmp_VideoPreview.OcxState")));
            this.wmp_VideoPreview.Size = new System.Drawing.Size(856, 730);
            this.wmp_VideoPreview.TabIndex = 3;
            this.wmp_VideoPreview.Visible = false;
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
            this.panel3.Location = new System.Drawing.Point(856, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(234, 730);
            this.panel3.TabIndex = 1;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // btnViewResults
            // 
            this.btnViewResults.BeforeTouchSize = new System.Drawing.Size(180, 43);
            this.btnViewResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewResults.IsBackStageButton = false;
            this.btnViewResults.Location = new System.Drawing.Point(25, 498);
            this.btnViewResults.Name = "btnViewResults";
            this.btnViewResults.Size = new System.Drawing.Size(180, 43);
            this.btnViewResults.TabIndex = 42;
            this.btnViewResults.Text = "View Results";
            this.btnViewResults.Click += new System.EventHandler(this.btnViewResults_Click);
            // 
            // btnReport
            // 
            this.btnReport.BeforeTouchSize = new System.Drawing.Size(180, 43);
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.IsBackStageButton = false;
            this.btnReport.Location = new System.Drawing.Point(25, 449);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(180, 43);
            this.btnReport.TabIndex = 41;
            this.btnReport.Text = "Print report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnGazepoint
            // 
            this.btnGazepoint.BeforeTouchSize = new System.Drawing.Size(180, 43);
            this.btnGazepoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGazepoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGazepoint.IsBackStageButton = false;
            this.btnGazepoint.Location = new System.Drawing.Point(25, 400);
            this.btnGazepoint.Name = "btnGazepoint";
            this.btnGazepoint.Size = new System.Drawing.Size(180, 43);
            this.btnGazepoint.TabIndex = 40;
            this.btnGazepoint.Text = "Print model gaze-plot";
            this.btnGazepoint.Click += new System.EventHandler(this.btnGazepoint_Click);
            // 
            // btnOverlays
            // 
            this.btnOverlays.BeforeTouchSize = new System.Drawing.Size(180, 43);
            this.btnOverlays.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOverlays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOverlays.IsBackStageButton = false;
            this.btnOverlays.Location = new System.Drawing.Point(25, 302);
            this.btnOverlays.Name = "btnOverlays";
            this.btnOverlays.Size = new System.Drawing.Size(180, 43);
            this.btnOverlays.TabIndex = 39;
            this.btnOverlays.Text = "Print overlays";
            this.btnOverlays.Click += new System.EventHandler(this.btnOverlays_Click);
            // 
            // btnHeatmaps
            // 
            this.btnHeatmaps.BeforeTouchSize = new System.Drawing.Size(180, 43);
            this.btnHeatmaps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeatmaps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHeatmaps.IsBackStageButton = false;
            this.btnHeatmaps.Location = new System.Drawing.Point(25, 351);
            this.btnHeatmaps.Name = "btnHeatmaps";
            this.btnHeatmaps.Size = new System.Drawing.Size(180, 43);
            this.btnHeatmaps.TabIndex = 38;
            this.btnHeatmaps.Text = "Print model heat-maps";
            this.btnHeatmaps.Click += new System.EventHandler(this.btnHeatmaps_Click);
            // 
            // btnChooseModel
            // 
            this.btnChooseModel.BeforeTouchSize = new System.Drawing.Size(180, 43);
            this.btnChooseModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseModel.IsBackStageButton = false;
            this.btnChooseModel.Location = new System.Drawing.Point(25, 93);
            this.btnChooseModel.Name = "btnChooseModel";
            this.btnChooseModel.Size = new System.Drawing.Size(180, 43);
            this.btnChooseModel.TabIndex = 37;
            this.btnChooseModel.Text = "Choose model";
            this.btnChooseModel.Click += new System.EventHandler(this.btnChooseModel_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.BeforeTouchSize = new System.Drawing.Size(180, 43);
            this.btnRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecord.IsBackStageButton = false;
            this.btnRecord.Location = new System.Drawing.Point(25, 142);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(180, 43);
            this.btnRecord.TabIndex = 36;
            this.btnRecord.Text = "Begin recording";
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnCal
            // 
            this.btnCal.BeforeTouchSize = new System.Drawing.Size(180, 43);
            this.btnCal.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.None;
            this.btnCal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCal.IsBackStageButton = false;
            this.btnCal.Location = new System.Drawing.Point(25, 44);
            this.btnCal.Name = "btnCal";
            this.btnCal.Size = new System.Drawing.Size(180, 43);
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
            // pic_caro
            // 
            this.pic_caro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pic_caro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_caro.Location = new System.Drawing.Point(34, 0);
            this.pic_caro.Name = "pic_caro";
            this.pic_caro.Size = new System.Drawing.Size(788, 730);
            this.pic_caro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_caro.TabIndex = 21;
            this.pic_caro.TabStop = false;
            // 
            // pic_model2DPreview
            // 
            this.pic_model2DPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_model2DPreview.Location = new System.Drawing.Point(0, 0);
            this.pic_model2DPreview.Name = "pic_model2DPreview";
            this.pic_model2DPreview.Size = new System.Drawing.Size(856, 730);
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
            this.createNewProjectCtrlNToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.createNewProjectCtrlNToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.createNewProjectCtrlNToolStripMenuItem.Text = "Create New Project";
            this.createNewProjectCtrlNToolStripMenuItem.Click += new System.EventHandler(this.createNewProjectCtrlNToolStripMenuItem_Click);
            // 
            // openProjectCtrlOToolStripMenuItem
            // 
            this.openProjectCtrlOToolStripMenuItem.Name = "openProjectCtrlOToolStripMenuItem";
            this.openProjectCtrlOToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openProjectCtrlOToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.openProjectCtrlOToolStripMenuItem.Text = "Open Project";
            this.openProjectCtrlOToolStripMenuItem.Click += new System.EventHandler(this.openProjectCtrlOToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(215, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
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
            this.pic_slideshowPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wmp_VideoPreview)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_caro)).EndInit();
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
        private System.Windows.Forms.Panel pic_slideshowPreview;
        private System.Windows.Forms.PictureBox pic_caro;
        private Syncfusion.Windows.Forms.ButtonAdv btnImageForward;
        private Syncfusion.Windows.Forms.ButtonAdv btnImageBack;

    }
}

