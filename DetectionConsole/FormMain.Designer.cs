namespace DetectionConsole
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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.parameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelSignalSource = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxSignalSource = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.testToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.startToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.stopToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBarCurrentProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelCurrentStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.tabControlSignalDetails = new System.Windows.Forms.TabControl();
            this.tabPageSignalMessage = new System.Windows.Forms.TabPage();
            this.textBoxSignalMessage = new System.Windows.Forms.TextBox();
            this.tabPageSignalChart = new System.Windows.Forms.TabPage();
            this.pictureBoxSignalChart = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxParameters = new System.Windows.Forms.ComboBox();
            this.propertyGridParameters = new System.Windows.Forms.PropertyGrid();
            this.openFileDialogOpenParameters = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogSaveParameters = new System.Windows.Forms.SaveFileDialog();
            this.menuStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tabControlSignalDetails.SuspendLayout();
            this.tabPageSignalMessage.SuspendLayout();
            this.tabPageSignalChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSignalChart)).BeginInit();
            this.tableLayoutPanelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parameterToolStripMenuItem,
            this.signalToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(784, 24);
            this.menuStripMain.TabIndex = 0;
            // 
            // parameterToolStripMenuItem
            // 
            this.parameterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.parameterToolStripMenuItem.Name = "parameterToolStripMenuItem";
            this.parameterToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.parameterToolStripMenuItem.Text = "Parameter";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.newToolStripMenuItem.Text = "New...";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveToolStripMenuItem.Text = "Save...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(109, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // signalToolStripMenuItem
            // 
            this.signalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.signalToolStripMenuItem.Name = "signalToolStripMenuItem";
            this.signalToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.signalToolStripMenuItem.Text = "Signal";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator3,
            this.toolStripLabelSignalSource,
            this.toolStripComboBoxSignalSource,
            this.toolStripSeparator,
            this.testToolStripButton,
            this.startToolStripButton,
            this.stopToolStripButton,
            this.toolStripSeparator1,
            this.helpToolStripButton});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(784, 25);
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabelSignalSource
            // 
            this.toolStripLabelSignalSource.Name = "toolStripLabelSignalSource";
            this.toolStripLabelSignalSource.Size = new System.Drawing.Size(81, 22);
            this.toolStripLabelSignalSource.Text = "Signal Source:";
            // 
            // toolStripComboBoxSignalSource
            // 
            this.toolStripComboBoxSignalSource.Items.AddRange(new object[] {
            "Serial Port",
            "Timer",
            "Kinect Depth Sensor"});
            this.toolStripComboBoxSignalSource.Name = "toolStripComboBoxSignalSource";
            this.toolStripComboBoxSignalSource.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBoxSignalSource.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxSignalSource_SelectedIndexChanged);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // testToolStripButton
            // 
            this.testToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.testToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("testToolStripButton.Image")));
            this.testToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.testToolStripButton.Name = "testToolStripButton";
            this.testToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.testToolStripButton.Text = "Test";
            this.testToolStripButton.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // startToolStripButton
            // 
            this.startToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("startToolStripButton.Image")));
            this.startToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startToolStripButton.Name = "startToolStripButton";
            this.startToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.startToolStripButton.Text = "Start";
            this.startToolStripButton.ToolTipText = "Start";
            this.startToolStripButton.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripButton
            // 
            this.stopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopToolStripButton.Enabled = false;
            this.stopToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("stopToolStripButton.Image")));
            this.stopToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopToolStripButton.Name = "stopToolStripButton";
            this.stopToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.stopToolStripButton.Text = "Stop";
            this.stopToolStripButton.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBarCurrentProgress,
            this.toolStripStatusLabelCurrentStatus});
            this.statusStripMain.Location = new System.Drawing.Point(0, 539);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(784, 22);
            this.statusStripMain.TabIndex = 2;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // toolStripProgressBarCurrentProgress
            // 
            this.toolStripProgressBarCurrentProgress.Name = "toolStripProgressBarCurrentProgress";
            this.toolStripProgressBarCurrentProgress.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBarCurrentProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBarCurrentProgress.Visible = false;
            // 
            // toolStripStatusLabelCurrentStatus
            // 
            this.toolStripStatusLabelCurrentStatus.Name = "toolStripStatusLabelCurrentStatus";
            this.toolStripStatusLabelCurrentStatus.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabelCurrentStatus.Visible = false;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 49);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.tabControlSignalDetails);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tableLayoutPanelRight);
            this.splitContainerMain.Size = new System.Drawing.Size(784, 490);
            this.splitContainerMain.SplitterDistance = 505;
            this.splitContainerMain.TabIndex = 3;
            // 
            // tabControlSignalDetails
            // 
            this.tabControlSignalDetails.Controls.Add(this.tabPageSignalMessage);
            this.tabControlSignalDetails.Controls.Add(this.tabPageSignalChart);
            this.tabControlSignalDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSignalDetails.Location = new System.Drawing.Point(0, 0);
            this.tabControlSignalDetails.Name = "tabControlSignalDetails";
            this.tabControlSignalDetails.SelectedIndex = 0;
            this.tabControlSignalDetails.Size = new System.Drawing.Size(505, 490);
            this.tabControlSignalDetails.TabIndex = 0;
            // 
            // tabPageSignalMessage
            // 
            this.tabPageSignalMessage.Controls.Add(this.textBoxSignalMessage);
            this.tabPageSignalMessage.Location = new System.Drawing.Point(4, 22);
            this.tabPageSignalMessage.Name = "tabPageSignalMessage";
            this.tabPageSignalMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSignalMessage.Size = new System.Drawing.Size(497, 464);
            this.tabPageSignalMessage.TabIndex = 0;
            this.tabPageSignalMessage.Text = "Message";
            this.tabPageSignalMessage.UseVisualStyleBackColor = true;
            // 
            // textBoxSignalMessage
            // 
            this.textBoxSignalMessage.BackColor = System.Drawing.Color.Black;
            this.textBoxSignalMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSignalMessage.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSignalMessage.ForeColor = System.Drawing.Color.LawnGreen;
            this.textBoxSignalMessage.Location = new System.Drawing.Point(3, 3);
            this.textBoxSignalMessage.Multiline = true;
            this.textBoxSignalMessage.Name = "textBoxSignalMessage";
            this.textBoxSignalMessage.ReadOnly = true;
            this.textBoxSignalMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSignalMessage.Size = new System.Drawing.Size(491, 458);
            this.textBoxSignalMessage.TabIndex = 1;
            // 
            // tabPageSignalChart
            // 
            this.tabPageSignalChart.Controls.Add(this.pictureBoxSignalChart);
            this.tabPageSignalChart.Location = new System.Drawing.Point(4, 22);
            this.tabPageSignalChart.Name = "tabPageSignalChart";
            this.tabPageSignalChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSignalChart.Size = new System.Drawing.Size(497, 464);
            this.tabPageSignalChart.TabIndex = 1;
            this.tabPageSignalChart.Text = "Chart";
            this.tabPageSignalChart.UseVisualStyleBackColor = true;
            // 
            // pictureBoxSignalChart
            // 
            this.pictureBoxSignalChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxSignalChart.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxSignalChart.Name = "pictureBoxSignalChart";
            this.pictureBoxSignalChart.Size = new System.Drawing.Size(491, 458);
            this.pictureBoxSignalChart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSignalChart.TabIndex = 0;
            this.pictureBoxSignalChart.TabStop = false;
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.ColumnCount = 1;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelRight.Controls.Add(this.comboBoxParameters, 0, 0);
            this.tableLayoutPanelRight.Controls.Add(this.propertyGridParameters, 0, 1);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 2;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(275, 490);
            this.tableLayoutPanelRight.TabIndex = 0;
            // 
            // comboBoxParameters
            // 
            this.comboBoxParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxParameters.FormattingEnabled = true;
            this.comboBoxParameters.Location = new System.Drawing.Point(3, 3);
            this.comboBoxParameters.Name = "comboBoxParameters";
            this.comboBoxParameters.Size = new System.Drawing.Size(269, 21);
            this.comboBoxParameters.TabIndex = 0;
            this.comboBoxParameters.SelectedIndexChanged += new System.EventHandler(this.comboBoxParameters_SelectedIndexChanged);
            // 
            // propertyGridParameters
            // 
            this.propertyGridParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridParameters.Location = new System.Drawing.Point(3, 30);
            this.propertyGridParameters.Name = "propertyGridParameters";
            this.propertyGridParameters.Size = new System.Drawing.Size(269, 457);
            this.propertyGridParameters.TabIndex = 1;
            // 
            // openFileDialogOpenParameters
            // 
            this.openFileDialogOpenParameters.Filter = "XML Files(*.xml)|*.xml|All Files(*.*)|*.*";
            // 
            // saveFileDialogSaveParameters
            // 
            this.saveFileDialogSaveParameters.Filter = "XML Files(*.xml)|*.xml|All Files(*.*)|*.*";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detection Console";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tabControlSignalDetails.ResumeLayout(false);
            this.tabPageSignalMessage.ResumeLayout(false);
            this.tabPageSignalMessage.PerformLayout();
            this.tabPageSignalChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSignalChart)).EndInit();
            this.tableLayoutPanelRight.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarCurrentProgress;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurrentStatus;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ToolStripMenuItem parameterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton testToolStripButton;
        private System.Windows.Forms.ToolStripButton startToolStripButton;
        private System.Windows.Forms.ToolStripButton stopToolStripButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.ComboBox comboBoxParameters;
        private System.Windows.Forms.PropertyGrid propertyGridParameters;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSignalSource;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSignalSource;
        private System.Windows.Forms.TabControl tabControlSignalDetails;
        private System.Windows.Forms.TabPage tabPageSignalMessage;
        private System.Windows.Forms.TextBox textBoxSignalMessage;
        private System.Windows.Forms.TabPage tabPageSignalChart;
        private System.Windows.Forms.PictureBox pictureBoxSignalChart;
        private System.Windows.Forms.OpenFileDialog openFileDialogOpenParameters;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSaveParameters;
    }
}