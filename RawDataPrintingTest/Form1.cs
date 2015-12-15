using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using ZXing;
//using MES.Core.Parameter;
//using MES.Utility;

namespace RawDataPrintingTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.printDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();

            if (this.printDialog1.ShowDialog(this) == DialogResult.OK)
            {
                //RawPrinterHelper.SendStringToPrinter(this.printDialog1.PrinterSettings.PrinterName, this.textBox2.Text);

                PrintDocument printDoc = new PrintDocument();
                printDoc.DocumentName = Guid.NewGuid().ToString();
                printDoc.PrinterSettings = this.printDialog1.PrinterSettings;

                if (printDoc.PrinterSettings.DefaultPageSettings.PaperSize.Kind == PaperKind.Custom)
                {
                    //printDoc.PrinterSettings.DefaultPageSettings.PaperSize.Height = int.Parse(this.textBoxY2.Text);
                    //printDoc.PrinterSettings.DefaultPageSettings.PaperSize.Width = int.Parse(this.textBoxX2.Text);
                    printDoc.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("Custom", int.Parse(this.textBoxX2.Text), int.Parse(this.textBoxY2.Text));
                }

                //printDoc.PrinterSettings.PaperSizes[0].Height = int.Parse(this.textBoxY2.Text);
                //printDoc.PrinterSettings.PaperSizes[0].Width =int.Parse(this.textBoxY2.Text);

                //printDoc.PrinterSettings = new PrinterSettings() 
                //{
                //    PrinterName = this.printDialog1.PrinterSettings.PrinterName,
                //};

                printDoc.PrintPage += (s, ev) => 
                {
                    //ev.Graphics.DrawString(this.textBox2.Text, new Font("Arial", 8), Brushes.Black, int.Parse(this.textBoxX1.Text), int.Parse(this.textBoxY1.Text));

                    //ev.Graphics.DrawString(this.textBox2.Text, new Font(this.comboBox1.SelectedItem.ToString(), int.Parse(this.textBox3.Text)), Brushes.Black, int.Parse(this.textBoxX2.Text), int.Parse(this.textBoxY2.Text));

                    BarcodeWriter barcodeWriter = new BarcodeWriter();

                    barcodeWriter.Format = BarcodeFormat.CODE_128;
                    barcodeWriter.Options.PureBarcode = false;
                    barcodeWriter.Options.Width = int.Parse(this.textBoxWidth.Text);
                    barcodeWriter.Options.Height = int.Parse(this.textBoxHeight.Text);
                    Bitmap bitmap = barcodeWriter.Write(this.textBox2.Text);

                    ev.Graphics.DrawImage(bitmap, int.Parse(this.textBoxX1.Text), int.Parse(this.textBoxY1.Text), int.Parse(this.textBoxWidth.Text), int.Parse(this.textBoxHeight.Text));

                    bitmap.Save(String.Format("test-{0}.png", DateTime.Now.ToString("yyyy-mm-dd-hh-mm-ss")));
                };

                printDoc.Print();
            }

            //BarcodeWriter barcodeWriter = new BarcodeWriter();

            //barcodeWriter.Format = BarcodeFormat.CODE_128;
            //barcodeWriter.Options.PureBarcode = false;//true;
            //Bitmap bitmap =  barcodeWriter.Write(this.textBox2.Text);

            //bitmap.Save(String.Format("test-{0}.png", DateTime.Now.ToString("yyyy-mm-dd-hh-mm-ss")));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.printDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();

                this.textBox1.Text = this.openFileDialog1.FileName;

                if (this.printDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    RawPrinterHelper.SendFileToPrinter(this.printDialog1.PrinterSettings.PrinterName, this.textBox1.Text);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> fontNames = new List<string>();

            foreach (var font in System.Drawing.FontFamily.Families)
            {
                fontNames.Add(font.Name);
            }

            this.comboBox1.DataSource = fontNames;

            //BarcodePrintingParameter param = new BarcodePrintingParameter()
            //{
            //    PrinterName = "",
            //    IsPrintingLabel = true,
            //    BarcodeType = BarcodeType.CODE_128,
            //    BarcodeXPosition = 1,
            //    BarcodeYPosition = 1,
            //    BarcodeImageWidth = 50,
            //    BarcodeImageHeight = 20
            //};

            //XmlUtility xmlUtil = new XmlUtility();
            //string xml = xmlUtil.XmlSerialize(param, null, "utf-8");

            //using (FileStream stream = new FileStream("printerparam.xml", FileMode.Create, FileAccess.Write, FileShare.Write)) 
            //{
            //    using (StreamWriter writer = new StreamWriter(stream))
            //    {
            //        writer.Write(xml);
            //    }
            //}

        }
    }
}
