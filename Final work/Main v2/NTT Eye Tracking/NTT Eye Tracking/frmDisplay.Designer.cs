namespace NTT_Eye_Tracking
{
    partial class frmDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDisplay));
            this.wmp_Display = new AxWMPLib.AxWindowsMediaPlayer();
            this.picDisplay = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.wmp_Display)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // wmp_Display
            // 
            this.wmp_Display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wmp_Display.Enabled = true;
            this.wmp_Display.Location = new System.Drawing.Point(0, 0);
            this.wmp_Display.Name = "wmp_Display";
            this.wmp_Display.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmp_Display.OcxState")));
            this.wmp_Display.Size = new System.Drawing.Size(921, 590);
            this.wmp_Display.TabIndex = 0;
            this.wmp_Display.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.wmp_Display_PlayStateChange);
            // 
            // picDisplay
            // 
            this.picDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDisplay.Location = new System.Drawing.Point(0, 0);
            this.picDisplay.Name = "picDisplay";
            this.picDisplay.Size = new System.Drawing.Size(921, 590);
            this.picDisplay.TabIndex = 1;
            this.picDisplay.TabStop = false;
            this.picDisplay.Click += new System.EventHandler(this.picDisplay_Click);
            // 
            // frmDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 590);
            this.Controls.Add(this.wmp_Display);
            this.Controls.Add(this.picDisplay);
            this.Name = "frmDisplay";
            this.Text = "Display";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.wmp_Display)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer wmp_Display;
        private System.Windows.Forms.PictureBox picDisplay;
    }
}