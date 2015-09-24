namespace NTT_Eye_Tracking
{
    partial class NTT_MiniForm
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
            this.panelNewOldProject = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_modelSelect = new System.Windows.Forms.Panel();
            this.btnImageBack = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnImageForward = new Syncfusion.Windows.Forms.ButtonAdv();
            this.red_ModelDescription = new System.Windows.Forms.RichTextBox();
            this.caro_models = new System.Windows.Forms.PictureBox();
            this.buttonAdv4 = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnCreateRecording = new Syncfusion.Windows.Forms.ButtonAdv();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_RecordingName = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel_createNew = new System.Windows.Forms.Panel();
            this.buttonAdv3 = new Syncfusion.Windows.Forms.ButtonAdv();
            this.buttonAdv2 = new Syncfusion.Windows.Forms.ButtonAdv();
            this.buttonAdv1 = new Syncfusion.Windows.Forms.ButtonAdv();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelNewOldProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel_modelSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.caro_models)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbx_RecordingName)).BeginInit();
            this.panel_createNew.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNewOldProject
            // 
            this.panelNewOldProject.Controls.Add(this.pictureBox2);
            this.panelNewOldProject.Controls.Add(this.pictureBox1);
            this.panelNewOldProject.Location = new System.Drawing.Point(44, 33);
            this.panelNewOldProject.Name = "panelNewOldProject";
            this.panelNewOldProject.Size = new System.Drawing.Size(657, 437);
            this.panelNewOldProject.TabIndex = 0;
            this.panelNewOldProject.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::NTT_Eye_Tracking.Properties.Resources.open211;
            this.pictureBox2.Location = new System.Drawing.Point(329, 96);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(175, 149);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NTT_Eye_Tracking.Properties.Resources.round69;
            this.pictureBox1.Location = new System.Drawing.Point(128, 96);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(175, 149);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel_modelSelect
            // 
            this.panel_modelSelect.Controls.Add(this.btnImageBack);
            this.panel_modelSelect.Controls.Add(this.btnImageForward);
            this.panel_modelSelect.Controls.Add(this.red_ModelDescription);
            this.panel_modelSelect.Controls.Add(this.caro_models);
            this.panel_modelSelect.Controls.Add(this.buttonAdv4);
            this.panel_modelSelect.Controls.Add(this.btnCreateRecording);
            this.panel_modelSelect.Controls.Add(this.label2);
            this.panel_modelSelect.Controls.Add(this.tbx_RecordingName);
            this.panel_modelSelect.Controls.Add(this.label1);
            this.panel_modelSelect.Location = new System.Drawing.Point(723, 494);
            this.panel_modelSelect.Name = "panel_modelSelect";
            this.panel_modelSelect.Size = new System.Drawing.Size(657, 437);
            this.panel_modelSelect.TabIndex = 3;
            // 
            // btnImageBack
            // 
            this.btnImageBack.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2007;
            this.btnImageBack.BeforeTouchSize = new System.Drawing.Size(34, 48);
            this.btnImageBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImageBack.IsBackStageButton = false;
            this.btnImageBack.Location = new System.Drawing.Point(205, 257);
            this.btnImageBack.Name = "btnImageBack";
            this.btnImageBack.Size = new System.Drawing.Size(34, 48);
            this.btnImageBack.TabIndex = 20;
            this.btnImageBack.Text = "<";
            this.btnImageBack.UseVisualStyle = true;
            this.btnImageBack.Click += new System.EventHandler(this.btnImageBack_Click);
            // 
            // btnImageForward
            // 
            this.btnImageForward.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2007;
            this.btnImageForward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.btnImageForward.BeforeTouchSize = new System.Drawing.Size(34, 48);
            this.btnImageForward.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImageForward.IsBackStageButton = false;
            this.btnImageForward.Location = new System.Drawing.Point(388, 257);
            this.btnImageForward.Name = "btnImageForward";
            this.btnImageForward.Size = new System.Drawing.Size(34, 48);
            this.btnImageForward.TabIndex = 19;
            this.btnImageForward.Text = ">";
            this.btnImageForward.UseVisualStyle = true;
            this.btnImageForward.Click += new System.EventHandler(this.btnImageForward_Click);
            // 
            // red_ModelDescription
            // 
            this.red_ModelDescription.ForeColor = System.Drawing.Color.Black;
            this.red_ModelDescription.Location = new System.Drawing.Point(245, 257);
            this.red_ModelDescription.Name = "red_ModelDescription";
            this.red_ModelDescription.ReadOnly = true;
            this.red_ModelDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.red_ModelDescription.Size = new System.Drawing.Size(137, 48);
            this.red_ModelDescription.TabIndex = 18;
            this.red_ModelDescription.Text = "";
            // 
            // caro_models
            // 
            this.caro_models.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.caro_models.Location = new System.Drawing.Point(205, 97);
            this.caro_models.Name = "caro_models";
            this.caro_models.Size = new System.Drawing.Size(217, 150);
            this.caro_models.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.caro_models.TabIndex = 17;
            this.caro_models.TabStop = false;
            // 
            // buttonAdv4
            // 
            this.buttonAdv4.BeforeTouchSize = new System.Drawing.Size(105, 37);
            this.buttonAdv4.IsBackStageButton = false;
            this.buttonAdv4.Location = new System.Drawing.Point(22, 13);
            this.buttonAdv4.Name = "buttonAdv4";
            this.buttonAdv4.Size = new System.Drawing.Size(105, 37);
            this.buttonAdv4.TabIndex = 16;
            this.buttonAdv4.Text = "Back";
            this.buttonAdv4.UseVisualStyle = true;
            this.buttonAdv4.Click += new System.EventHandler(this.buttonAdv4_Click);
            // 
            // btnCreateRecording
            // 
            this.btnCreateRecording.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2007;
            this.btnCreateRecording.BackColor = System.Drawing.Color.White;
            this.btnCreateRecording.BeforeTouchSize = new System.Drawing.Size(160, 36);
            this.btnCreateRecording.IsBackStageButton = false;
            this.btnCreateRecording.Location = new System.Drawing.Point(235, 349);
            this.btnCreateRecording.Name = "btnCreateRecording";
            this.btnCreateRecording.Size = new System.Drawing.Size(160, 36);
            this.btnCreateRecording.TabIndex = 4;
            this.btnCreateRecording.Text = "Create Recording";
            this.btnCreateRecording.Click += new System.EventHandler(this.btnCreateRecording_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(174, 323);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "RecordingName";
            // 
            // tbx_RecordingName
            // 
            this.tbx_RecordingName.BeforeTouchSize = new System.Drawing.Size(199, 20);
            this.tbx_RecordingName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbx_RecordingName.Location = new System.Drawing.Point(304, 323);
            this.tbx_RecordingName.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.tbx_RecordingName.Name = "tbx_RecordingName";
            this.tbx_RecordingName.Size = new System.Drawing.Size(199, 20);
            this.tbx_RecordingName.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Default;
            this.tbx_RecordingName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(95, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(483, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please select what kind of model you wish to record on";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel_createNew
            // 
            this.panel_createNew.Controls.Add(this.buttonAdv3);
            this.panel_createNew.Controls.Add(this.buttonAdv2);
            this.panel_createNew.Controls.Add(this.buttonAdv1);
            this.panel_createNew.Controls.Add(this.label5);
            this.panel_createNew.Controls.Add(this.textBox2);
            this.panel_createNew.Controls.Add(this.textBox1);
            this.panel_createNew.Controls.Add(this.label3);
            this.panel_createNew.Controls.Add(this.label4);
            this.panel_createNew.Location = new System.Drawing.Point(723, 33);
            this.panel_createNew.Name = "panel_createNew";
            this.panel_createNew.Size = new System.Drawing.Size(657, 437);
            this.panel_createNew.TabIndex = 2;
            // 
            // buttonAdv3
            // 
            this.buttonAdv3.BeforeTouchSize = new System.Drawing.Size(84, 35);
            this.buttonAdv3.IsBackStageButton = false;
            this.buttonAdv3.Location = new System.Drawing.Point(527, 212);
            this.buttonAdv3.Name = "buttonAdv3";
            this.buttonAdv3.Size = new System.Drawing.Size(84, 35);
            this.buttonAdv3.TabIndex = 15;
            this.buttonAdv3.Text = "Open";
            this.buttonAdv3.Click += new System.EventHandler(this.buttonAdv3_Click);
            // 
            // buttonAdv2
            // 
            this.buttonAdv2.BeforeTouchSize = new System.Drawing.Size(106, 30);
            this.buttonAdv2.IsBackStageButton = false;
            this.buttonAdv2.Location = new System.Drawing.Point(262, 265);
            this.buttonAdv2.Name = "buttonAdv2";
            this.buttonAdv2.Size = new System.Drawing.Size(106, 30);
            this.buttonAdv2.TabIndex = 14;
            this.buttonAdv2.Text = "Create Project";
            this.buttonAdv2.Click += new System.EventHandler(this.buttonAdv2_Click);
            // 
            // buttonAdv1
            // 
            this.buttonAdv1.BeforeTouchSize = new System.Drawing.Size(105, 37);
            this.buttonAdv1.IsBackStageButton = false;
            this.buttonAdv1.Location = new System.Drawing.Point(22, 20);
            this.buttonAdv1.Name = "buttonAdv1";
            this.buttonAdv1.Size = new System.Drawing.Size(105, 37);
            this.buttonAdv1.TabIndex = 13;
            this.buttonAdv1.Text = "Back";
            this.buttonAdv1.UseVisualStyle = true;
            this.buttonAdv1.Click += new System.EventHandler(this.buttonAdv1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(167, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(301, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "Create a new eye tracking project";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(172, 196);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(333, 20);
            this.textBox2.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(172, 227);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(333, 20);
            this.textBox1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(83, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Project Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(110, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Location";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // NTT_MiniForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1439, 1053);
            this.Controls.Add(this.panel_modelSelect);
            this.Controls.Add(this.panelNewOldProject);
            this.Controls.Add(this.panel_createNew);
            this.Name = "NTT_MiniForm";
            this.Load += new System.EventHandler(this.NTT_MiniForm_Load);
            this.panelNewOldProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel_modelSelect.ResumeLayout(false);
            this.panel_modelSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.caro_models)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbx_RecordingName)).EndInit();
            this.panel_createNew.ResumeLayout(false);
            this.panel_createNew.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNewOldProject;
        private System.Windows.Forms.Panel panel_modelSelect;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.ButtonAdv btnCreateRecording;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt tbx_RecordingName;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel_createNew;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Syncfusion.Windows.Forms.ButtonAdv buttonAdv3;
        private Syncfusion.Windows.Forms.ButtonAdv buttonAdv2;
        private Syncfusion.Windows.Forms.ButtonAdv buttonAdv1;
        private Syncfusion.Windows.Forms.ButtonAdv buttonAdv4;
        private Syncfusion.Windows.Forms.ButtonAdv btnImageBack;
        private Syncfusion.Windows.Forms.ButtonAdv btnImageForward;
        private System.Windows.Forms.RichTextBox red_ModelDescription;
        private System.Windows.Forms.PictureBox caro_models;
        private System.Windows.Forms.ImageList imageList1;
    }
}