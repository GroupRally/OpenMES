namespace BarCodeSigleMachineSharedDeviceDemo
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.GroupBoxSignalPreview = new System.Windows.Forms.GroupBox();
            this.pictureBoxSignalPreview = new System.Windows.Forms.PictureBox();
            this.GroupBoxControlPanel = new System.Windows.Forms.GroupBox();
            this.labelRotationDegree = new System.Windows.Forms.Label();
            this.textBoxRotationDegree = new System.Windows.Forms.TextBox();
            this.comboBoxImageFormats = new System.Windows.Forms.ComboBox();
            this.labelImageFormat = new System.Windows.Forms.Label();
            this.labelGain = new System.Windows.Forms.Label();
            this.textBoxGain = new System.Windows.Forms.TextBox();
            this.labelExposureTime = new System.Windows.Forms.Label();
            this.textBoxExposureTime = new System.Windows.Forms.TextBox();
            this.listBoxBarCodesExtracted = new System.Windows.Forms.ListBox();
            this.labelBarCodesExtracted = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDeviceTimeout = new System.Windows.Forms.TextBox();
            this.labelBarCodeScanningRepeats = new System.Windows.Forms.Label();
            this.textBoxBarCodeScanningRepeats = new System.Windows.Forms.TextBox();
            this.labelTimerInterval = new System.Windows.Forms.Label();
            this.textBoxTimerInterval = new System.Windows.Forms.TextBox();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTest = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xiViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barCodeReaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frequencyDetectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTools = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonTest = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonxiViewer = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBarCodeReader = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFrequencyDetector = new System.Windows.Forms.ToolStripButton();
            this.statusStripCurrentStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBarCurrentProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelCurrentStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.GroupBoxSignalPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSignalPreview)).BeginInit();
            this.GroupBoxControlPanel.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.toolStripTools.SuspendLayout();
            this.statusStripCurrentStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxSignalPreview
            // 
            this.GroupBoxSignalPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxSignalPreview.Controls.Add(this.pictureBoxSignalPreview);
            this.GroupBoxSignalPreview.Location = new System.Drawing.Point(6, 49);
            this.GroupBoxSignalPreview.Name = "GroupBoxSignalPreview";
            this.GroupBoxSignalPreview.Size = new System.Drawing.Size(550, 507);
            this.GroupBoxSignalPreview.TabIndex = 0;
            this.GroupBoxSignalPreview.TabStop = false;
            this.GroupBoxSignalPreview.Text = "Signal Preview";
            // 
            // pictureBoxSignalPreview
            // 
            this.pictureBoxSignalPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxSignalPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxSignalPreview.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxSignalPreview.Name = "pictureBoxSignalPreview";
            this.pictureBoxSignalPreview.Size = new System.Drawing.Size(544, 488);
            this.pictureBoxSignalPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSignalPreview.TabIndex = 0;
            this.pictureBoxSignalPreview.TabStop = false;
            // 
            // GroupBoxControlPanel
            // 
            this.GroupBoxControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxControlPanel.Controls.Add(this.labelRotationDegree);
            this.GroupBoxControlPanel.Controls.Add(this.textBoxRotationDegree);
            this.GroupBoxControlPanel.Controls.Add(this.comboBoxImageFormats);
            this.GroupBoxControlPanel.Controls.Add(this.labelImageFormat);
            this.GroupBoxControlPanel.Controls.Add(this.labelGain);
            this.GroupBoxControlPanel.Controls.Add(this.textBoxGain);
            this.GroupBoxControlPanel.Controls.Add(this.labelExposureTime);
            this.GroupBoxControlPanel.Controls.Add(this.textBoxExposureTime);
            this.GroupBoxControlPanel.Controls.Add(this.listBoxBarCodesExtracted);
            this.GroupBoxControlPanel.Controls.Add(this.labelBarCodesExtracted);
            this.GroupBoxControlPanel.Controls.Add(this.label1);
            this.GroupBoxControlPanel.Controls.Add(this.textBoxDeviceTimeout);
            this.GroupBoxControlPanel.Controls.Add(this.labelBarCodeScanningRepeats);
            this.GroupBoxControlPanel.Controls.Add(this.textBoxBarCodeScanningRepeats);
            this.GroupBoxControlPanel.Controls.Add(this.labelTimerInterval);
            this.GroupBoxControlPanel.Controls.Add(this.textBoxTimerInterval);
            this.GroupBoxControlPanel.Location = new System.Drawing.Point(560, 49);
            this.GroupBoxControlPanel.Name = "GroupBoxControlPanel";
            this.GroupBoxControlPanel.Size = new System.Drawing.Size(220, 507);
            this.GroupBoxControlPanel.TabIndex = 1;
            this.GroupBoxControlPanel.TabStop = false;
            this.GroupBoxControlPanel.Text = "Control Panel";
            // 
            // labelRotationDegree
            // 
            this.labelRotationDegree.AutoSize = true;
            this.labelRotationDegree.Location = new System.Drawing.Point(7, 234);
            this.labelRotationDegree.Name = "labelRotationDegree";
            this.labelRotationDegree.Size = new System.Drawing.Size(88, 13);
            this.labelRotationDegree.TabIndex = 16;
            this.labelRotationDegree.Text = "Rotation Degree:";
            // 
            // textBoxRotationDegree
            // 
            this.textBoxRotationDegree.Location = new System.Drawing.Point(6, 252);
            this.textBoxRotationDegree.Name = "textBoxRotationDegree";
            this.textBoxRotationDegree.Size = new System.Drawing.Size(208, 20);
            this.textBoxRotationDegree.TabIndex = 15;
            this.textBoxRotationDegree.Text = "0";
            // 
            // comboBoxImageFormats
            // 
            this.comboBoxImageFormats.FormattingEnabled = true;
            this.comboBoxImageFormats.Items.AddRange(new object[] {
            "MONO8 = 0",
            "MONO16 = 1",
            "RGB24 = 2",
            "RGB32 = 3",
            "RGBPLANAR = 4",
            "RAW8 = 5",
            "RAW16 = 6"});
            this.comboBoxImageFormats.Location = new System.Drawing.Point(8, 296);
            this.comboBoxImageFormats.Name = "comboBoxImageFormats";
            this.comboBoxImageFormats.Size = new System.Drawing.Size(206, 21);
            this.comboBoxImageFormats.TabIndex = 14;
            // 
            // labelImageFormat
            // 
            this.labelImageFormat.AutoSize = true;
            this.labelImageFormat.Location = new System.Drawing.Point(5, 279);
            this.labelImageFormat.Name = "labelImageFormat";
            this.labelImageFormat.Size = new System.Drawing.Size(74, 13);
            this.labelImageFormat.TabIndex = 13;
            this.labelImageFormat.Text = "Image Format:";
            // 
            // labelGain
            // 
            this.labelGain.AutoSize = true;
            this.labelGain.Location = new System.Drawing.Point(7, 191);
            this.labelGain.Name = "labelGain";
            this.labelGain.Size = new System.Drawing.Size(53, 13);
            this.labelGain.TabIndex = 12;
            this.labelGain.Text = "Gain (db):";
            // 
            // textBoxGain
            // 
            this.textBoxGain.Location = new System.Drawing.Point(6, 209);
            this.textBoxGain.Name = "textBoxGain";
            this.textBoxGain.Size = new System.Drawing.Size(208, 20);
            this.textBoxGain.TabIndex = 11;
            this.textBoxGain.Text = "0";
            // 
            // labelExposureTime
            // 
            this.labelExposureTime.AutoSize = true;
            this.labelExposureTime.Location = new System.Drawing.Point(7, 147);
            this.labelExposureTime.Name = "labelExposureTime";
            this.labelExposureTime.Size = new System.Drawing.Size(171, 13);
            this.labelExposureTime.TabIndex = 10;
            this.labelExposureTime.Text = "Exposure Time (Microseconds/us):";
            // 
            // textBoxExposureTime
            // 
            this.textBoxExposureTime.Location = new System.Drawing.Point(6, 167);
            this.textBoxExposureTime.Name = "textBoxExposureTime";
            this.textBoxExposureTime.Size = new System.Drawing.Size(208, 20);
            this.textBoxExposureTime.TabIndex = 9;
            this.textBoxExposureTime.Text = "1800";
            // 
            // listBoxBarCodesExtracted
            // 
            this.listBoxBarCodesExtracted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxBarCodesExtracted.FormattingEnabled = true;
            this.listBoxBarCodesExtracted.Location = new System.Drawing.Point(7, 357);
            this.listBoxBarCodesExtracted.Name = "listBoxBarCodesExtracted";
            this.listBoxBarCodesExtracted.Size = new System.Drawing.Size(205, 134);
            this.listBoxBarCodesExtracted.TabIndex = 8;
            // 
            // labelBarCodesExtracted
            // 
            this.labelBarCodesExtracted.AutoSize = true;
            this.labelBarCodesExtracted.Location = new System.Drawing.Point(7, 331);
            this.labelBarCodesExtracted.Name = "labelBarCodesExtracted";
            this.labelBarCodesExtracted.Size = new System.Drawing.Size(107, 13);
            this.labelBarCodesExtracted.TabIndex = 7;
            this.labelBarCodesExtracted.Text = "Bar Codes Extracted:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Device Timeout (Milliseconds):";
            // 
            // textBoxDeviceTimeout
            // 
            this.textBoxDeviceTimeout.Location = new System.Drawing.Point(6, 121);
            this.textBoxDeviceTimeout.Name = "textBoxDeviceTimeout";
            this.textBoxDeviceTimeout.Size = new System.Drawing.Size(208, 20);
            this.textBoxDeviceTimeout.TabIndex = 4;
            this.textBoxDeviceTimeout.Text = "6000";
            // 
            // labelBarCodeScanningRepeats
            // 
            this.labelBarCodeScanningRepeats.AutoSize = true;
            this.labelBarCodeScanningRepeats.Location = new System.Drawing.Point(7, 61);
            this.labelBarCodeScanningRepeats.Name = "labelBarCodeScanningRepeats";
            this.labelBarCodeScanningRepeats.Size = new System.Drawing.Size(145, 13);
            this.labelBarCodeScanningRepeats.TabIndex = 3;
            this.labelBarCodeScanningRepeats.Text = "Bar Code Scanning Repeats:";
            // 
            // textBoxBarCodeScanningRepeats
            // 
            this.textBoxBarCodeScanningRepeats.Location = new System.Drawing.Point(6, 80);
            this.textBoxBarCodeScanningRepeats.Name = "textBoxBarCodeScanningRepeats";
            this.textBoxBarCodeScanningRepeats.Size = new System.Drawing.Size(208, 20);
            this.textBoxBarCodeScanningRepeats.TabIndex = 2;
            this.textBoxBarCodeScanningRepeats.Text = "75";
            // 
            // labelTimerInterval
            // 
            this.labelTimerInterval.AutoSize = true;
            this.labelTimerInterval.Location = new System.Drawing.Point(7, 18);
            this.labelTimerInterval.Name = "labelTimerInterval";
            this.labelTimerInterval.Size = new System.Drawing.Size(140, 13);
            this.labelTimerInterval.TabIndex = 1;
            this.labelTimerInterval.Text = "Timer Interval (Milliseconds):";
            // 
            // textBoxTimerInterval
            // 
            this.textBoxTimerInterval.Location = new System.Drawing.Point(6, 35);
            this.textBoxTimerInterval.Name = "textBoxTimerInterval";
            this.textBoxTimerInterval.Size = new System.Drawing.Size(208, 20);
            this.textBoxTimerInterval.TabIndex = 0;
            this.textBoxTimerInterval.Text = "8000";
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(784, 24);
            this.menuStripMain.TabIndex = 2;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemTest,
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItemTest
            // 
            this.toolStripMenuItemTest.Name = "toolStripMenuItemTest";
            this.toolStripMenuItemTest.Size = new System.Drawing.Size(98, 22);
            this.toolStripMenuItemTest.Text = "Test";
            this.toolStripMenuItemTest.Click += new System.EventHandler(this.toolStripMenuItemTest_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(95, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xiViewerToolStripMenuItem,
            this.barCodeReaderToolStripMenuItem,
            this.frequencyDetectorToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // xiViewerToolStripMenuItem
            // 
            this.xiViewerToolStripMenuItem.Name = "xiViewerToolStripMenuItem";
            this.xiViewerToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.xiViewerToolStripMenuItem.Text = "xiViewer";
            this.xiViewerToolStripMenuItem.Click += new System.EventHandler(this.xiViewerToolStripMenuItem_Click);
            // 
            // barCodeReaderToolStripMenuItem
            // 
            this.barCodeReaderToolStripMenuItem.Name = "barCodeReaderToolStripMenuItem";
            this.barCodeReaderToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.barCodeReaderToolStripMenuItem.Text = "Bar Code Reader";
            this.barCodeReaderToolStripMenuItem.Click += new System.EventHandler(this.barCodeReaderToolStripMenuItem_Click);
            // 
            // frequencyDetectorToolStripMenuItem
            // 
            this.frequencyDetectorToolStripMenuItem.Name = "frequencyDetectorToolStripMenuItem";
            this.frequencyDetectorToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.frequencyDetectorToolStripMenuItem.Text = "Frequency Detector";
            // 
            // toolStripTools
            // 
            this.toolStripTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonTest,
            this.toolStripButtonStart,
            this.toolStripButtonStop,
            this.toolStripButtonxiViewer,
            this.toolStripButtonBarCodeReader,
            this.toolStripButtonFrequencyDetector});
            this.toolStripTools.Location = new System.Drawing.Point(0, 24);
            this.toolStripTools.Name = "toolStripTools";
            this.toolStripTools.Size = new System.Drawing.Size(784, 25);
            this.toolStripTools.TabIndex = 3;
            this.toolStripTools.Text = "toolStrip1";
            // 
            // toolStripButtonTest
            // 
            this.toolStripButtonTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTest.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonTest.Image")));
            this.toolStripButtonTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTest.Name = "toolStripButtonTest";
            this.toolStripButtonTest.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonTest.Text = "Test";
            this.toolStripButtonTest.Click += new System.EventHandler(this.toolStripMenuItemTest_Click);
            // 
            // toolStripButtonStart
            // 
            this.toolStripButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStart.Image")));
            this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStart.Name = "toolStripButtonStart";
            this.toolStripButtonStart.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonStart.Text = "Start";
            this.toolStripButtonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStop.Image")));
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonStop.Text = "Stop";
            this.toolStripButtonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // toolStripButtonxiViewer
            // 
            this.toolStripButtonxiViewer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonxiViewer.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonxiViewer.Image")));
            this.toolStripButtonxiViewer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonxiViewer.Name = "toolStripButtonxiViewer";
            this.toolStripButtonxiViewer.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonxiViewer.Text = "xiViewer";
            this.toolStripButtonxiViewer.Click += new System.EventHandler(this.xiViewerToolStripMenuItem_Click);
            // 
            // toolStripButtonBarCodeReader
            // 
            this.toolStripButtonBarCodeReader.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBarCodeReader.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBarCodeReader.Image")));
            this.toolStripButtonBarCodeReader.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBarCodeReader.Name = "toolStripButtonBarCodeReader";
            this.toolStripButtonBarCodeReader.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonBarCodeReader.Text = "Bar Code Reader";
            this.toolStripButtonBarCodeReader.Click += new System.EventHandler(this.barCodeReaderToolStripMenuItem_Click);
            // 
            // toolStripButtonFrequencyDetector
            // 
            this.toolStripButtonFrequencyDetector.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFrequencyDetector.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFrequencyDetector.Image")));
            this.toolStripButtonFrequencyDetector.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFrequencyDetector.Name = "toolStripButtonFrequencyDetector";
            this.toolStripButtonFrequencyDetector.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonFrequencyDetector.Text = "Frequency Detector";
            // 
            // statusStripCurrentStatus
            // 
            this.statusStripCurrentStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBarCurrentProgress,
            this.toolStripStatusLabelCurrentStatus});
            this.statusStripCurrentStatus.Location = new System.Drawing.Point(0, 539);
            this.statusStripCurrentStatus.Name = "statusStripCurrentStatus";
            this.statusStripCurrentStatus.Size = new System.Drawing.Size(784, 22);
            this.statusStripCurrentStatus.TabIndex = 4;
            // 
            // toolStripProgressBarCurrentProgress
            // 
            this.toolStripProgressBarCurrentProgress.Name = "toolStripProgressBarCurrentProgress";
            this.toolStripProgressBarCurrentProgress.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBarCurrentProgress.Step = 20;
            this.toolStripProgressBarCurrentProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBarCurrentProgress.Visible = false;
            // 
            // toolStripStatusLabelCurrentStatus
            // 
            this.toolStripStatusLabelCurrentStatus.Name = "toolStripStatusLabelCurrentStatus";
            this.toolStripStatusLabelCurrentStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.statusStripCurrentStatus);
            this.Controls.Add(this.toolStripTools);
            this.Controls.Add(this.GroupBoxControlPanel);
            this.Controls.Add(this.GroupBoxSignalPreview);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detector Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.GroupBoxSignalPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSignalPreview)).EndInit();
            this.GroupBoxControlPanel.ResumeLayout(false);
            this.GroupBoxControlPanel.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripTools.ResumeLayout(false);
            this.toolStripTools.PerformLayout();
            this.statusStripCurrentStatus.ResumeLayout(false);
            this.statusStripCurrentStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label labelBarCodesExtracted;
        private System.Windows.Forms.ListBox listBoxBarCodesExtracted;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xiViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barCodeReaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frequencyDetectorToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripTools;
        private System.Windows.Forms.ToolStripButton toolStripButtonStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripButton toolStripButtonxiViewer;
        private System.Windows.Forms.ToolStripButton toolStripButtonBarCodeReader;
        private System.Windows.Forms.ToolStripButton toolStripButtonFrequencyDetector;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label labelExposureTime;
        private System.Windows.Forms.TextBox textBoxExposureTime;
        private System.Windows.Forms.Label labelGain;
        private System.Windows.Forms.TextBox textBoxGain;
        private System.Windows.Forms.Label labelImageFormat;
        private System.Windows.Forms.ComboBox comboBoxImageFormats;
        private System.Windows.Forms.Label labelRotationDegree;
        private System.Windows.Forms.TextBox textBoxRotationDegree;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTest;
        private System.Windows.Forms.ToolStripButton toolStripButtonTest;
        private System.Windows.Forms.StatusStrip statusStripCurrentStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurrentStatus;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarCurrentProgress;
    }
}

