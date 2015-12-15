namespace BarcodeReaderSample
{
    partial class frmBarcodeReaderSample
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
            this.btnScan = new System.Windows.Forms.Button();
            this.txtNumberScans = new System.Windows.Forms.TextBox();
            this.lblNumberScans = new System.Windows.Forms.Label();
            this.fdFileToScan = new System.Windows.Forms.OpenFileDialog();
            this.pbImageToScan = new System.Windows.Forms.PictureBox();
            this.lstBarcodes = new System.Windows.Forms.ListBox();
            this.lblBarcodes = new System.Windows.Forms.Label();
            this.lbPictureBox = new System.Windows.Forms.Label();
            this.btnRotate = new System.Windows.Forms.Button();
            this.lblDegrees = new System.Windows.Forms.Label();
            this.txtDegrees = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbImageToScan)).BeginInit();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(719, 202);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 23);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // txtNumberScans
            // 
            this.txtNumberScans.Location = new System.Drawing.Point(904, 27);
            this.txtNumberScans.Name = "txtNumberScans";
            this.txtNumberScans.Size = new System.Drawing.Size(100, 20);
            this.txtNumberScans.TabIndex = 1;
            this.txtNumberScans.Text = "75";
            this.txtNumberScans.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumberScans_KeyPress);
            // 
            // lblNumberScans
            // 
            this.lblNumberScans.AutoSize = true;
            this.lblNumberScans.Location = new System.Drawing.Point(716, 30);
            this.lblNumberScans.Name = "lblNumberScans";
            this.lblNumberScans.Size = new System.Drawing.Size(182, 13);
            this.lblNumberScans.TabIndex = 2;
            this.lblNumberScans.Text = "Number Of Scans (50 - 100 is typical)";
            // 
            // fdFileToScan
            // 
            this.fdFileToScan.RestoreDirectory = true;
            // 
            // pbImageToScan
            // 
            this.pbImageToScan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbImageToScan.Location = new System.Drawing.Point(12, 30);
            this.pbImageToScan.Name = "pbImageToScan";
            this.pbImageToScan.Size = new System.Drawing.Size(683, 506);
            this.pbImageToScan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImageToScan.TabIndex = 3;
            this.pbImageToScan.TabStop = false;
            this.pbImageToScan.Click += new System.EventHandler(this.pbImageToScan_Click);
            // 
            // lstBarcodes
            // 
            this.lstBarcodes.FormattingEnabled = true;
            this.lstBarcodes.Location = new System.Drawing.Point(719, 274);
            this.lstBarcodes.Name = "lstBarcodes";
            this.lstBarcodes.Size = new System.Drawing.Size(305, 121);
            this.lstBarcodes.TabIndex = 4;
            // 
            // lblBarcodes
            // 
            this.lblBarcodes.AutoSize = true;
            this.lblBarcodes.Location = new System.Drawing.Point(719, 255);
            this.lblBarcodes.Name = "lblBarcodes";
            this.lblBarcodes.Size = new System.Drawing.Size(85, 13);
            this.lblBarcodes.TabIndex = 5;
            this.lblBarcodes.Text = "Barcodes Found";
            // 
            // lbPictureBox
            // 
            this.lbPictureBox.AutoSize = true;
            this.lbPictureBox.Location = new System.Drawing.Point(9, 9);
            this.lbPictureBox.Name = "lbPictureBox";
            this.lbPictureBox.Size = new System.Drawing.Size(196, 13);
            this.lbPictureBox.TabIndex = 6;
            this.lbPictureBox.Text = "Click on the picturebox to load an image";
            // 
            // btnRotate
            // 
            this.btnRotate.Location = new System.Drawing.Point(719, 154);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(75, 23);
            this.btnRotate.TabIndex = 7;
            this.btnRotate.Text = "Rotate";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // lblDegrees
            // 
            this.lblDegrees.AutoSize = true;
            this.lblDegrees.Location = new System.Drawing.Point(817, 154);
            this.lblDegrees.Name = "lblDegrees";
            this.lblDegrees.Size = new System.Drawing.Size(47, 13);
            this.lblDegrees.TabIndex = 8;
            this.lblDegrees.Text = "Degrees";
            // 
            // txtDegrees
            // 
            this.txtDegrees.Location = new System.Drawing.Point(871, 154);
            this.txtDegrees.Name = "txtDegrees";
            this.txtDegrees.Size = new System.Drawing.Size(100, 20);
            this.txtDegrees.TabIndex = 9;
            this.txtDegrees.Text = "0";
            this.txtDegrees.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDegrees_KeyPress);
            // 
            // frmBarcodeReaderSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 581);
            this.Controls.Add(this.txtDegrees);
            this.Controls.Add(this.lblDegrees);
            this.Controls.Add(this.btnRotate);
            this.Controls.Add(this.lbPictureBox);
            this.Controls.Add(this.lblBarcodes);
            this.Controls.Add(this.lstBarcodes);
            this.Controls.Add(this.pbImageToScan);
            this.Controls.Add(this.lblNumberScans);
            this.Controls.Add(this.txtNumberScans);
            this.Controls.Add(this.btnScan);
            this.Name = "frmBarcodeReaderSample";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBarcodeReaderSample_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbImageToScan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.TextBox txtNumberScans;
        private System.Windows.Forms.Label lblNumberScans;
        private System.Windows.Forms.OpenFileDialog fdFileToScan;
        private System.Windows.Forms.PictureBox pbImageToScan;
        private System.Windows.Forms.ListBox lstBarcodes;
        private System.Windows.Forms.Label lblBarcodes;
        private System.Windows.Forms.Label lbPictureBox;
        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.Label lblDegrees;
        private System.Windows.Forms.TextBox txtDegrees;
    }
}

