namespace AcquisitionConsole
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.parameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acquisitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barcodeReaderToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xiViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barcodeReaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frequencyDetectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonTest = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.xiViewerToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.barcodeReaderToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.frequencyDetectorToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBarCurrentProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelCurrentStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.pictureBoxAcquiredImage = new System.Windows.Forms.PictureBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageParameters = new System.Windows.Forms.TabPage();
            this.comboBoxParameters = new System.Windows.Forms.ComboBox();
            this.propertyGridParameters = new System.Windows.Forms.PropertyGrid();
            this.tabPageBarcodes = new System.Windows.Forms.TabPage();
            this.listBoxBarcodes = new System.Windows.Forms.ListBox();
            this.tabPageRecentImages = new System.Windows.Forms.TabPage();
            this.dataGridViewRecentImages = new System.Windows.Forms.DataGridView();
            this.ColumnImageUrl = new System.Windows.Forms.DataGridViewLinkColumn();
            this.saveFileDialogSaveParameters = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogOpenParameters = new System.Windows.Forms.OpenFileDialog();
            this.menuStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAcquiredImage)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.tabPageParameters.SuspendLayout();
            this.tabPageBarcodes.SuspendLayout();
            this.tabPageRecentImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecentImages)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parameterToolStripMenuItem,
            this.acquisitionToolStripMenuItem,
            this.barcodeReaderToolsToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(784, 24);
            this.menuStripMain.TabIndex = 0;
            // 
            // parameterToolStripMenuItem
            // 
            this.parameterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNew,
            this.toolStripMenuItemOpen,
            this.toolStripMenuItemSave,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.parameterToolStripMenuItem.Name = "parameterToolStripMenuItem";
            this.parameterToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.parameterToolStripMenuItem.Text = "Parameter";
            // 
            // toolStripMenuItemNew
            // 
            this.toolStripMenuItemNew.Name = "toolStripMenuItemNew";
            this.toolStripMenuItemNew.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemNew.Text = "New...";
            this.toolStripMenuItemNew.Click += new System.EventHandler(this.toolStripMenuItemNew_Click);
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemOpen.Text = "Open...";
            this.toolStripMenuItemOpen.Click += new System.EventHandler(this.toolStripMenuItemOpen_Click);
            // 
            // toolStripMenuItemSave
            // 
            this.toolStripMenuItemSave.Name = "toolStripMenuItemSave";
            this.toolStripMenuItemSave.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemSave.Text = "Save...";
            this.toolStripMenuItemSave.Click += new System.EventHandler(this.toolStripMenuItemSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // acquisitionToolStripMenuItem
            // 
            this.acquisitionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.acquisitionToolStripMenuItem.Name = "acquisitionToolStripMenuItem";
            this.acquisitionToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.acquisitionToolStripMenuItem.Text = "Acquisition";
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
            // barcodeReaderToolsToolStripMenuItem
            // 
            this.barcodeReaderToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xiViewerToolStripMenuItem,
            this.barcodeReaderToolStripMenuItem,
            this.frequencyDetectorToolStripMenuItem});
            this.barcodeReaderToolsToolStripMenuItem.Name = "barcodeReaderToolsToolStripMenuItem";
            this.barcodeReaderToolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.barcodeReaderToolsToolStripMenuItem.Text = "Tools";
            // 
            // xiViewerToolStripMenuItem
            // 
            this.xiViewerToolStripMenuItem.Name = "xiViewerToolStripMenuItem";
            this.xiViewerToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.xiViewerToolStripMenuItem.Text = "xiViewer";
            this.xiViewerToolStripMenuItem.Click += new System.EventHandler(this.xiViewerToolStripMenuItem_Click);
            // 
            // barcodeReaderToolStripMenuItem
            // 
            this.barcodeReaderToolStripMenuItem.Name = "barcodeReaderToolStripMenuItem";
            this.barcodeReaderToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.barcodeReaderToolStripMenuItem.Text = "Barcode Reader";
            this.barcodeReaderToolStripMenuItem.Click += new System.EventHandler(this.barcodeReaderToolStripMenuItem_Click);
            // 
            // frequencyDetectorToolStripMenuItem
            // 
            this.frequencyDetectorToolStripMenuItem.Name = "frequencyDetectorToolStripMenuItem";
            this.frequencyDetectorToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.frequencyDetectorToolStripMenuItem.Text = "Frequency Detector";
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator3,
            this.toolStripButtonTest,
            this.toolStripButtonStart,
            this.toolStripButtonStop,
            this.toolStripSeparator4,
            this.xiViewerToolStripButton,
            this.barcodeReaderToolStripButton,
            this.frequencyDetectorToolStripButton,
            this.toolStripSeparator,
            this.toolStripSeparator2,
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
            this.newToolStripButton.Click += new System.EventHandler(this.toolStripMenuItemNew_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.toolStripMenuItemOpen_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.toolStripMenuItemSave_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonTest
            // 
            this.toolStripButtonTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTest.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonTest.Image")));
            this.toolStripButtonTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTest.Name = "toolStripButtonTest";
            this.toolStripButtonTest.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonTest.Text = "Test";
            this.toolStripButtonTest.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // toolStripButtonStart
            // 
            this.toolStripButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStart.Image")));
            this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStart.Name = "toolStripButtonStart";
            this.toolStripButtonStart.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonStart.Text = "Start";
            this.toolStripButtonStart.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Enabled = false;
            this.toolStripButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStop.Image")));
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonStop.Text = "Stop";
            this.toolStripButtonStop.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // xiViewerToolStripButton
            // 
            this.xiViewerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xiViewerToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("xiViewerToolStripButton.Image")));
            this.xiViewerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xiViewerToolStripButton.Name = "xiViewerToolStripButton";
            this.xiViewerToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.xiViewerToolStripButton.Text = "xiViewer";
            this.xiViewerToolStripButton.Click += new System.EventHandler(this.xiViewerToolStripMenuItem_Click);
            // 
            // barcodeReaderToolStripButton
            // 
            this.barcodeReaderToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.barcodeReaderToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("barcodeReaderToolStripButton.Image")));
            this.barcodeReaderToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.barcodeReaderToolStripButton.Name = "barcodeReaderToolStripButton";
            this.barcodeReaderToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.barcodeReaderToolStripButton.Text = "Barcode Reader";
            this.barcodeReaderToolStripButton.Click += new System.EventHandler(this.barcodeReaderToolStripMenuItem_Click);
            // 
            // frequencyDetectorToolStripButton
            // 
            this.frequencyDetectorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.frequencyDetectorToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("frequencyDetectorToolStripButton.Image")));
            this.frequencyDetectorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.frequencyDetectorToolStripButton.Name = "frequencyDetectorToolStripButton";
            this.frequencyDetectorToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.frequencyDetectorToolStripButton.Text = "Frequency Detector";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 49);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.pictureBoxAcquiredImage);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControlMain);
            this.splitContainerMain.Size = new System.Drawing.Size(784, 490);
            this.splitContainerMain.SplitterDistance = 527;
            this.splitContainerMain.TabIndex = 3;
            // 
            // pictureBoxAcquiredImage
            // 
            this.pictureBoxAcquiredImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxAcquiredImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxAcquiredImage.Name = "pictureBoxAcquiredImage";
            this.pictureBoxAcquiredImage.Size = new System.Drawing.Size(527, 490);
            this.pictureBoxAcquiredImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAcquiredImage.TabIndex = 0;
            this.pictureBoxAcquiredImage.TabStop = false;
            this.pictureBoxAcquiredImage.DoubleClick += new System.EventHandler(this.pictureBoxAcquiredImage_DoubleClick);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageParameters);
            this.tabControlMain.Controls.Add(this.tabPageBarcodes);
            this.tabControlMain.Controls.Add(this.tabPageRecentImages);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(253, 490);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageParameters
            // 
            this.tabPageParameters.Controls.Add(this.comboBoxParameters);
            this.tabPageParameters.Controls.Add(this.propertyGridParameters);
            this.tabPageParameters.Location = new System.Drawing.Point(4, 22);
            this.tabPageParameters.Name = "tabPageParameters";
            this.tabPageParameters.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageParameters.Size = new System.Drawing.Size(245, 464);
            this.tabPageParameters.TabIndex = 0;
            this.tabPageParameters.Text = "Parameters";
            this.tabPageParameters.UseVisualStyleBackColor = true;
            // 
            // comboBoxParameters
            // 
            this.comboBoxParameters.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxParameters.FormattingEnabled = true;
            this.comboBoxParameters.Location = new System.Drawing.Point(3, 3);
            this.comboBoxParameters.Name = "comboBoxParameters";
            this.comboBoxParameters.Size = new System.Drawing.Size(239, 21);
            this.comboBoxParameters.TabIndex = 3;
            this.comboBoxParameters.SelectedIndexChanged += new System.EventHandler(this.comboBoxParameters_SelectedIndexChanged);
            // 
            // propertyGridParameters
            // 
            this.propertyGridParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridParameters.Location = new System.Drawing.Point(3, 30);
            this.propertyGridParameters.Name = "propertyGridParameters";
            this.propertyGridParameters.Size = new System.Drawing.Size(239, 431);
            this.propertyGridParameters.TabIndex = 2;
            // 
            // tabPageBarcodes
            // 
            this.tabPageBarcodes.Controls.Add(this.listBoxBarcodes);
            this.tabPageBarcodes.Location = new System.Drawing.Point(4, 22);
            this.tabPageBarcodes.Name = "tabPageBarcodes";
            this.tabPageBarcodes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBarcodes.Size = new System.Drawing.Size(245, 464);
            this.tabPageBarcodes.TabIndex = 1;
            this.tabPageBarcodes.Text = "Barcodes";
            this.tabPageBarcodes.UseVisualStyleBackColor = true;
            // 
            // listBoxBarcodes
            // 
            this.listBoxBarcodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxBarcodes.FormattingEnabled = true;
            this.listBoxBarcodes.Location = new System.Drawing.Point(3, 3);
            this.listBoxBarcodes.Name = "listBoxBarcodes";
            this.listBoxBarcodes.Size = new System.Drawing.Size(239, 458);
            this.listBoxBarcodes.TabIndex = 0;
            // 
            // tabPageRecentImages
            // 
            this.tabPageRecentImages.Controls.Add(this.dataGridViewRecentImages);
            this.tabPageRecentImages.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecentImages.Name = "tabPageRecentImages";
            this.tabPageRecentImages.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecentImages.Size = new System.Drawing.Size(245, 464);
            this.tabPageRecentImages.TabIndex = 2;
            this.tabPageRecentImages.Text = "Recent Images";
            this.tabPageRecentImages.UseVisualStyleBackColor = true;
            // 
            // dataGridViewRecentImages
            // 
            this.dataGridViewRecentImages.AllowUserToAddRows = false;
            this.dataGridViewRecentImages.AllowUserToDeleteRows = false;
            this.dataGridViewRecentImages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridViewRecentImages.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridViewRecentImages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewRecentImages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnImageUrl});
            this.dataGridViewRecentImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRecentImages.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewRecentImages.MultiSelect = false;
            this.dataGridViewRecentImages.Name = "dataGridViewRecentImages";
            this.dataGridViewRecentImages.ReadOnly = true;
            this.dataGridViewRecentImages.RowHeadersVisible = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewRecentImages.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewRecentImages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewRecentImages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRecentImages.Size = new System.Drawing.Size(239, 458);
            this.dataGridViewRecentImages.TabIndex = 0;
            this.dataGridViewRecentImages.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRecentImages_CellContentClick);
            // 
            // ColumnImageUrl
            // 
            this.ColumnImageUrl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnImageUrl.DataPropertyName = "Value";
            this.ColumnImageUrl.HeaderText = "Image Url";
            this.ColumnImageUrl.Name = "ColumnImageUrl";
            this.ColumnImageUrl.ReadOnly = true;
            // 
            // saveFileDialogSaveParameters
            // 
            this.saveFileDialogSaveParameters.Filter = "XML Files(*.xml)|*.xml|All Files(*.*)|*.*";
            // 
            // openFileDialogOpenParameters
            // 
            this.openFileDialogOpenParameters.Filter = "XML Files(*.xml)|*.xml|All Files(*.*)|*.*";
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
            this.Text = "Acquisition Console";
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAcquiredImage)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageParameters.ResumeLayout(false);
            this.tabPageBarcodes.ResumeLayout(false);
            this.tabPageRecentImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecentImages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem parameterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barcodeReaderToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageParameters;
        private System.Windows.Forms.PropertyGrid propertyGridParameters;
        private System.Windows.Forms.TabPage tabPageBarcodes;
        private System.Windows.Forms.ListBox listBoxBarcodes;
        private System.Windows.Forms.PictureBox pictureBoxAcquiredImage;
        private System.Windows.Forms.ComboBox comboBoxParameters;
        private System.Windows.Forms.ToolStripButton toolStripButtonTest;
        private System.Windows.Forms.ToolStripButton toolStripButtonStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripButton xiViewerToolStripButton;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton barcodeReaderToolStripButton;
        private System.Windows.Forms.ToolStripButton frequencyDetectorToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNew;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem acquisitionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarCurrentProgress;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurrentStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem xiViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barcodeReaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frequencyDetectorToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSaveParameters;
        private System.Windows.Forms.OpenFileDialog openFileDialogOpenParameters;
        private System.Windows.Forms.TabPage tabPageRecentImages;
        private System.Windows.Forms.DataGridView dataGridViewRecentImages;
        private System.Windows.Forms.DataGridViewLinkColumn ColumnImageUrl;
    }
}