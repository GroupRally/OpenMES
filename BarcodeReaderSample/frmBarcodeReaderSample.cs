using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BarcodeReaderSample
{
    public partial class frmBarcodeReaderSample : Form
    {

        // use a form level variable for the origninal bitmap, the picturebox will be a resized version
        Bitmap bmp;

        // 
        string strOpenToDirectory = "C:\\";


        public frmBarcodeReaderSample()
        {
            InitializeComponent();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            
        
            System.Collections.ArrayList BarcodesScanned = new System.Collections.ArrayList();

            if (bmp == null)
            {
                MessageBox.Show("Click on the picture box on the left to load an image");
            }
            else
            {
                BarcodeImaging.FullScanPage(ref BarcodesScanned, bmp, Convert.ToInt32(txtNumberScans.Text));

                if (BarcodesScanned.Count == 0)
                {
                    MessageBox.Show("No Barcodes found");
                }
                else
                {
                    lstBarcodes.DataSource = BarcodesScanned;
                }
            }
           
        }

        private void txtNumberScans_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only accept numbers
            if (char.IsLetter(e.KeyChar) ||
                char.IsSymbol(e.KeyChar) ||
                char.IsWhiteSpace(e.KeyChar) ||
                char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        private void pbImageToScan_Click(object sender, EventArgs e)
        {


            //fdFileToScan.InitialDirectory = strOpenToDirectory;

            DialogResult result = fdFileToScan.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = fdFileToScan.FileName;
                try
                {

                    if (bmp != null)
                    {
                        bmp.Dispose();
                    }

                    // set the form variable to the image
                    bmp = new Bitmap(fdFileToScan.FileName);

                    // 
                    pbImageToScan.Image = bmp;
                   

                }
                catch (IOException)
                {
                }
            }


        }

        private void frmBarcodeReaderSample_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bmp != null)
            {
                bmp.Dispose();
            }

        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            bmp = BarcodeImaging.RotateImage(bmp, (float)Convert.ToDouble(txtDegrees.Text));
            pbImageToScan.Image = bmp;

        }

        private void txtDegrees_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only accept numbers
            if (char.IsLetter(e.KeyChar) ||
                char.IsSymbol(e.KeyChar) || 
                char.IsWhiteSpace(e.KeyChar) 
                //char.IsPunctuation(e.KeyChar) Allow negative numbers and decimals, this lets some funky
           
                )
                e.Handled = true;
        }
    }
}
