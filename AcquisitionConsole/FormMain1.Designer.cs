namespace AcquisitionStationDemo
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
            this.GroupBoxControlPanel = new System.Windows.Forms.GroupBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxDefaultImageStore = new System.Windows.Forms.TextBox();
            this.labelDefaultImageStore = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.listBoxImagesAcquiredAndSaved = new System.Windows.Forms.ListBox();
            this.labelImagesAcquiredAndSaved = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDeviceTimeout = new System.Windows.Forms.TextBox();
            this.GroupBoxImagePreview = new System.Windows.Forms.GroupBox();
            this.pictureBoxImagePreview = new System.Windows.Forms.PictureBox();
            this.folderBrowserDialogImageStore = new System.Windows.Forms.FolderBrowserDialog();
            this.GroupBoxControlPanel.SuspendLayout();
            this.GroupBoxImagePreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupBoxControlPanel
            // 
            this.GroupBoxControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxControlPanel.Controls.Add(this.buttonBrowse);
            this.GroupBoxControlPanel.Controls.Add(this.textBoxDefaultImageStore);
            this.GroupBoxControlPanel.Controls.Add(this.labelDefaultImageStore);
            this.GroupBoxControlPanel.Controls.Add(this.buttonStop);
            this.GroupBoxControlPanel.Controls.Add(this.listBoxImagesAcquiredAndSaved);
            this.GroupBoxControlPanel.Controls.Add(this.labelImagesAcquiredAndSaved);
            this.GroupBoxControlPanel.Controls.Add(this.buttonStart);
            this.GroupBoxControlPanel.Controls.Add(this.label1);
            this.GroupBoxControlPanel.Controls.Add(this.textBoxDeviceTimeout);
            this.GroupBoxControlPanel.Location = new System.Drawing.Point(559, 5);
            this.GroupBoxControlPanel.Name = "GroupBoxControlPanel";
            this.GroupBoxControlPanel.Size = new System.Drawing.Size(220, 550);
            this.GroupBoxControlPanel.TabIndex = 3;
            this.GroupBoxControlPanel.TabStop = false;
            this.GroupBoxControlPanel.Text = "Control Panel";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(155, 41);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(58, 23);
            this.buttonBrowse.TabIndex = 15;
            this.buttonBrowse.Text = "Browse...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxDefaultImageStore
            // 
            this.textBoxDefaultImageStore.Location = new System.Drawing.Point(5, 43);
            this.textBoxDefaultImageStore.Name = "textBoxDefaultImageStore";
            this.textBoxDefaultImageStore.ReadOnly = true;
            this.textBoxDefaultImageStore.Size = new System.Drawing.Size(145, 20);
            this.textBoxDefaultImageStore.TabIndex = 14;
            // 
            // labelDefaultImageStore
            // 
            this.labelDefaultImageStore.AutoSize = true;
            this.labelDefaultImageStore.Location = new System.Drawing.Point(6, 22);
            this.labelDefaultImageStore.Name = "labelDefaultImageStore";
            this.labelDefaultImageStore.Size = new System.Drawing.Size(104, 13);
            this.labelDefaultImageStore.TabIndex = 13;
            this.labelDefaultImageStore.Text = "Default Image Store:";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(91, 141);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 9;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // listBoxImagesAcquiredAndSaved
            // 
            this.listBoxImagesAcquiredAndSaved.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxImagesAcquiredAndSaved.FormattingEnabled = true;
            this.listBoxImagesAcquiredAndSaved.Location = new System.Drawing.Point(7, 216);
            this.listBoxImagesAcquiredAndSaved.Name = "listBoxImagesAcquiredAndSaved";
            this.listBoxImagesAcquiredAndSaved.Size = new System.Drawing.Size(205, 329);
            this.listBoxImagesAcquiredAndSaved.TabIndex = 8;
            // 
            // labelImagesAcquiredAndSaved
            // 
            this.labelImagesAcquiredAndSaved.AutoSize = true;
            this.labelImagesAcquiredAndSaved.Location = new System.Drawing.Point(7, 190);
            this.labelImagesAcquiredAndSaved.Name = "labelImagesAcquiredAndSaved";
            this.labelImagesAcquiredAndSaved.Size = new System.Drawing.Size(144, 13);
            this.labelImagesAcquiredAndSaved.TabIndex = 7;
            this.labelImagesAcquiredAndSaved.Text = "Images Acquired and Saved:";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(6, 141);
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
            this.label1.Location = new System.Drawing.Point(7, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Device Timeout (Milliseconds):";
            // 
            // textBoxDeviceTimeout
            // 
            this.textBoxDeviceTimeout.Location = new System.Drawing.Point(6, 103);
            this.textBoxDeviceTimeout.Name = "textBoxDeviceTimeout";
            this.textBoxDeviceTimeout.Size = new System.Drawing.Size(208, 20);
            this.textBoxDeviceTimeout.TabIndex = 4;
            this.textBoxDeviceTimeout.Text = "6000";
            // 
            // GroupBoxImagePreview
            // 
            this.GroupBoxImagePreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxImagePreview.Controls.Add(this.pictureBoxImagePreview);
            this.GroupBoxImagePreview.Location = new System.Drawing.Point(5, 5);
            this.GroupBoxImagePreview.Name = "GroupBoxImagePreview";
            this.GroupBoxImagePreview.Size = new System.Drawing.Size(550, 550);
            this.GroupBoxImagePreview.TabIndex = 2;
            this.GroupBoxImagePreview.TabStop = false;
            this.GroupBoxImagePreview.Text = "Image Preview";
            // 
            // pictureBoxImagePreview
            // 
            this.pictureBoxImagePreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxImagePreview.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxImagePreview.Name = "pictureBoxImagePreview";
            this.pictureBoxImagePreview.Size = new System.Drawing.Size(544, 531);
            this.pictureBoxImagePreview.TabIndex = 0;
            this.pictureBoxImagePreview.TabStop = false;
            // 
            // folderBrowserDialogImageStore
            // 
            this.folderBrowserDialogImageStore.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.GroupBoxControlPanel);
            this.Controls.Add(this.GroupBoxImagePreview);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acquisition Station Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.GroupBoxControlPanel.ResumeLayout(false);
            this.GroupBoxControlPanel.PerformLayout();
            this.GroupBoxImagePreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagePreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBoxControlPanel;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ListBox listBoxImagesAcquiredAndSaved;
        private System.Windows.Forms.Label labelImagesAcquiredAndSaved;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDeviceTimeout;
        private System.Windows.Forms.GroupBox GroupBoxImagePreview;
        private System.Windows.Forms.PictureBox pictureBoxImagePreview;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxDefaultImageStore;
        private System.Windows.Forms.Label labelDefaultImageStore;
        protected System.Windows.Forms.FolderBrowserDialog folderBrowserDialogImageStore;
    }
}

