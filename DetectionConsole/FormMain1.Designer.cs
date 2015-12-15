namespace DetectorDemo
{
    partial class FormMain1
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
            this.GroupBoxSignalPreview = new System.Windows.Forms.GroupBox();
            this.pictureBoxSignalPreview = new System.Windows.Forms.PictureBox();
            this.GroupBoxControlPanel = new System.Windows.Forms.GroupBox();
            this.listBoxBarCodesExtracted = new System.Windows.Forms.ListBox();
            this.labelBarCodesExtracted = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDeviceTimeout = new System.Windows.Forms.TextBox();
            this.labelBarCodeScanningRepeats = new System.Windows.Forms.Label();
            this.textBoxBarCodeScanningRepeats = new System.Windows.Forms.TextBox();
            this.labelTimerInterval = new System.Windows.Forms.Label();
            this.textBoxTimerInterval = new System.Windows.Forms.TextBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.GroupBoxSignalPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSignalPreview)).BeginInit();
            this.GroupBoxControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxSignalPreview
            // 
            this.GroupBoxSignalPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxSignalPreview.Controls.Add(this.pictureBoxSignalPreview);
            this.GroupBoxSignalPreview.Location = new System.Drawing.Point(6, 6);
            this.GroupBoxSignalPreview.Name = "GroupBoxSignalPreview";
            this.GroupBoxSignalPreview.Size = new System.Drawing.Size(550, 550);
            this.GroupBoxSignalPreview.TabIndex = 0;
            this.GroupBoxSignalPreview.TabStop = false;
            this.GroupBoxSignalPreview.Text = "Signal Preview";
            // 
            // pictureBoxSignalPreview
            // 
            this.pictureBoxSignalPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxSignalPreview.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxSignalPreview.Name = "pictureBoxSignalPreview";
            this.pictureBoxSignalPreview.Size = new System.Drawing.Size(544, 531);
            this.pictureBoxSignalPreview.TabIndex = 0;
            this.pictureBoxSignalPreview.TabStop = false;
            // 
            // GroupBoxControlPanel
            // 
            this.GroupBoxControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxControlPanel.Controls.Add(this.buttonStop);
            this.GroupBoxControlPanel.Controls.Add(this.listBoxBarCodesExtracted);
            this.GroupBoxControlPanel.Controls.Add(this.labelBarCodesExtracted);
            this.GroupBoxControlPanel.Controls.Add(this.buttonStart);
            this.GroupBoxControlPanel.Controls.Add(this.label1);
            this.GroupBoxControlPanel.Controls.Add(this.textBoxDeviceTimeout);
            this.GroupBoxControlPanel.Controls.Add(this.labelBarCodeScanningRepeats);
            this.GroupBoxControlPanel.Controls.Add(this.textBoxBarCodeScanningRepeats);
            this.GroupBoxControlPanel.Controls.Add(this.labelTimerInterval);
            this.GroupBoxControlPanel.Controls.Add(this.textBoxTimerInterval);
            this.GroupBoxControlPanel.Location = new System.Drawing.Point(560, 6);
            this.GroupBoxControlPanel.Name = "GroupBoxControlPanel";
            this.GroupBoxControlPanel.Size = new System.Drawing.Size(220, 550);
            this.GroupBoxControlPanel.TabIndex = 1;
            this.GroupBoxControlPanel.TabStop = false;
            this.GroupBoxControlPanel.Text = "Control Panel";
            // 
            // listBoxBarCodesExtracted
            // 
            this.listBoxBarCodesExtracted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxBarCodesExtracted.FormattingEnabled = true;
            this.listBoxBarCodesExtracted.Location = new System.Drawing.Point(7, 266);
            this.listBoxBarCodesExtracted.Name = "listBoxBarCodesExtracted";
            this.listBoxBarCodesExtracted.Size = new System.Drawing.Size(205, 277);
            this.listBoxBarCodesExtracted.TabIndex = 8;
            // 
            // labelBarCodesExtracted
            // 
            this.labelBarCodesExtracted.AutoSize = true;
            this.labelBarCodesExtracted.Location = new System.Drawing.Point(10, 242);
            this.labelBarCodesExtracted.Name = "labelBarCodesExtracted";
            this.labelBarCodesExtracted.Size = new System.Drawing.Size(107, 13);
            this.labelBarCodesExtracted.TabIndex = 7;
            this.labelBarCodesExtracted.Text = "Bar Codes Extracted:";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(10, 193);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Device Timeout (Milliseconds):";
            // 
            // textBoxDeviceTimeout
            // 
            this.textBoxDeviceTimeout.Location = new System.Drawing.Point(6, 156);
            this.textBoxDeviceTimeout.Name = "textBoxDeviceTimeout";
            this.textBoxDeviceTimeout.Size = new System.Drawing.Size(208, 20);
            this.textBoxDeviceTimeout.TabIndex = 4;
            this.textBoxDeviceTimeout.Text = "6000";
            // 
            // labelBarCodeScanningRepeats
            // 
            this.labelBarCodeScanningRepeats.AutoSize = true;
            this.labelBarCodeScanningRepeats.Location = new System.Drawing.Point(7, 75);
            this.labelBarCodeScanningRepeats.Name = "labelBarCodeScanningRepeats";
            this.labelBarCodeScanningRepeats.Size = new System.Drawing.Size(145, 13);
            this.labelBarCodeScanningRepeats.TabIndex = 3;
            this.labelBarCodeScanningRepeats.Text = "Bar Code Scanning Repeats:";
            // 
            // textBoxBarCodeScanningRepeats
            // 
            this.textBoxBarCodeScanningRepeats.Location = new System.Drawing.Point(6, 98);
            this.textBoxBarCodeScanningRepeats.Name = "textBoxBarCodeScanningRepeats";
            this.textBoxBarCodeScanningRepeats.Size = new System.Drawing.Size(208, 20);
            this.textBoxBarCodeScanningRepeats.TabIndex = 2;
            this.textBoxBarCodeScanningRepeats.Text = "75";
            // 
            // labelTimerInterval
            // 
            this.labelTimerInterval.AutoSize = true;
            this.labelTimerInterval.Location = new System.Drawing.Point(7, 20);
            this.labelTimerInterval.Name = "labelTimerInterval";
            this.labelTimerInterval.Size = new System.Drawing.Size(140, 13);
            this.labelTimerInterval.TabIndex = 1;
            this.labelTimerInterval.Text = "Timer Interval (Milliseconds):";
            // 
            // textBoxTimerInterval
            // 
            this.textBoxTimerInterval.Location = new System.Drawing.Point(6, 43);
            this.textBoxTimerInterval.Name = "textBoxTimerInterval";
            this.textBoxTimerInterval.Size = new System.Drawing.Size(208, 20);
            this.textBoxTimerInterval.TabIndex = 0;
            this.textBoxTimerInterval.Text = "15000";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(91, 193);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 9;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.GroupBoxControlPanel);
            this.Controls.Add(this.GroupBoxSignalPreview);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detector Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.GroupBoxSignalPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSignalPreview)).EndInit();
            this.GroupBoxControlPanel.ResumeLayout(false);
            this.GroupBoxControlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBoxSignalPreview;
        private System.Windows.Forms.GroupBox GroupBoxControlPanel;
        private System.Windows.Forms.PictureBox pictureBoxSignalPreview;
        private System.Windows.Forms.TextBox textBoxTimerInterval;
        private System.Windows.Forms.Label labelTimerInterval;
        private System.Windows.Forms.Label labelBarCodeScanningRepeats;
        private System.Windows.Forms.TextBox textBoxBarCodeScanningRepeats;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDeviceTimeout;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelBarCodesExtracted;
        private System.Windows.Forms.ListBox listBoxBarCodesExtracted;
        private System.Windows.Forms.Button buttonStop;
    }
}

