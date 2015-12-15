using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Printing;
using System.Web.Security;
using System.IO;
using MES.Security;
using MES.Core.Parameter;
using MES.Utility;
using ZXing;

public partial class PrinterConf : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!this.User.Identity.IsAuthenticated)
        {
            FormsAuthentication.RedirectToLoginPage();
        }

        if (!Roles.IsUserInRole(RoleManager.SystemRole_SupperUser))
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            this.setControls();
            this.bindDataValues();
        }
    }

    private void setControls() 
    {
        this.DropDownListPrtiners.DataSource = PrinterSettings.InstalledPrinters;
        this.DropDownListPrtiners.DataBind();

        this.DropDownListBarcodeTypes.DataSource = new string[] 
        {
            BarcodeType.AZTEC.ToString(),
            BarcodeType.CODABAR.ToString(),
            BarcodeType.CODE_128.ToString(),
            BarcodeType.CODE_39.ToString(),
            BarcodeType.CODE_93.ToString(),
            BarcodeType.DATA_MATRIX.ToString(),
            BarcodeType.EAN_13.ToString(),
            BarcodeType.EAN_8.ToString(),
            BarcodeType.IMB.ToString(),
            BarcodeType.ITF.ToString(),
            BarcodeType.MAXICODE.ToString(),
            BarcodeType.MSI.ToString(),
            BarcodeType.PDF_417.ToString(),
            BarcodeType.PLESSEY.ToString(),
            BarcodeType.QR_CODE.ToString(),
            BarcodeType.RSS_14.ToString(),
            BarcodeType.RSS_EXPANDED.ToString(),
            BarcodeType.UPC_A.ToString(),
            //BarcodeType.UPC_E.ToString(),
            //BarcodeType.UPC_EAN_EXTENSION.ToString()
        };

        this.DropDownListBarcodeTypes.DataBind();
    }

    private void bindDataValues() 
    {
        string configFilePath = Server.MapPath("printer-config.xml");

        if (System.IO.File.Exists(configFilePath))
        {
            string configXml = "<BarcodePrintingParameter/>";

            MES.Core.Parameter.BarcodePrintingParameter printingParam = null;
            
            using (System.IO.FileStream stream = new System.IO.FileStream(configFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                {
                    configXml = reader.ReadToEnd();
                }
            }
            
            printingParam = new MES.Utility.XmlUtility().XmlDeserialize(configXml, typeof(MES.Core.Parameter.BarcodePrintingParameter), new Type[] { typeof(MES.Core.Parameter.BarcodeType) }, "utf-8") as MES.Core.Parameter.BarcodePrintingParameter;

            if (printingParam != null)
	        {
                this.DropDownListPrtiners.SelectedValue = printingParam.PrinterName;
                this.DropDownListBarcodeTypes.SelectedValue = printingParam.BarcodeType.ToString();
                this.TextBoxBarcodeImageHeight.Text = printingParam.BarcodeImageHeight.ToString();
                this.TextBoxBarcodeImageWidth.Text = printingParam.BarcodeImageWidth.ToString();
                this.TextBoxBarcodeXPosition.Text = printingParam.BarcodeXPosition.ToString();
                this.TextBoxBarcodeYPosition.Text = printingParam.BarcodeYPosition.ToString();
                this.TextBoxPaperWidth.Text = printingParam.BarcodeLabelPaperWidth.ToString();
                this.TextBoxPaperHeight.Text = printingParam.BarcodeLabelPaperHeight.ToString();
                this.CheckBoxIsPrintingBarcodeCaption.Checked = printingParam.IsPrintingCaption;
	        }
           
        }
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        BarcodePrintingParameter printingParam = new BarcodePrintingParameter()
        {
            PrinterName = this.DropDownListPrtiners.SelectedValue,
            IsPrintingCaption = this.CheckBoxIsPrintingBarcodeCaption.Checked,
            BarcodeType = ((BarcodeType)(Enum.Parse(typeof(BarcodeType), this.DropDownListBarcodeTypes.SelectedValue, true))),
            BarcodeXPosition = int.Parse(this.TextBoxBarcodeXPosition.Text),
            BarcodeYPosition = int.Parse(this.TextBoxBarcodeYPosition.Text),
            BarcodeImageWidth = int.Parse(this.TextBoxBarcodeImageWidth.Text),
            BarcodeImageHeight = int.Parse(this.TextBoxBarcodeImageHeight.Text),
            BarcodeLabelPaperWidth = int.Parse(this.TextBoxPaperWidth.Text),
            BarcodeLabelPaperHeight = int.Parse(this.TextBoxPaperHeight.Text)
        };

        XmlUtility xmlUtil = new XmlUtility();
        string xml = xmlUtil.XmlSerialize(printingParam, null, "utf-8");

        string configFilePath = Server.MapPath("printer-config.xml");

        using (FileStream stream = new FileStream(configFilePath, FileMode.Create, FileAccess.Write, FileShare.Write))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(xml);
            }
        }

        MES.Processor.ModuleConfiguration.Default_BarcodeFontName = printingParam.BarcodeFontName;
        MES.Processor.ModuleConfiguration.Default_BarcodeFontSize = printingParam.BarcodeFontSize;
        MES.Processor.ModuleConfiguration.Default_BarcodeImageHeight = printingParam.BarcodeImageHeight;
        MES.Processor.ModuleConfiguration.Default_BarcodeImageWidth = printingParam.BarcodeImageWidth;
        MES.Processor.ModuleConfiguration.Default_BarcodeType = printingParam.BarcodeType;
        MES.Processor.ModuleConfiguration.Default_BarcodeXPosition = printingParam.BarcodeXPosition;
        MES.Processor.ModuleConfiguration.Default_BarcodeYPosition = printingParam.BarcodeYPosition;
        MES.Processor.ModuleConfiguration.Default_IsPrintingBarcodeCaption= printingParam.IsPrintingCaption;
        MES.Processor.ModuleConfiguration.Default_PrinterName = printingParam.PrinterName;
        MES.Processor.ModuleConfiguration.Default_CaptionFontName = printingParam.CaptionFontName;
        MES.Processor.ModuleConfiguration.Default_CaptionFontSize = printingParam.CaptionFontSize;
        MES.Processor.ModuleConfiguration.Default_CaptionXPosition = printingParam.CaptionXPosition;
        MES.Processor.ModuleConfiguration.Default_CaptionYPosition = printingParam.CaptionYPosition;

        string script = "window.alert('Barcode printer settings are saved successfully!')";
        this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), Guid.NewGuid().ToString(), script, true);
    }
    protected void ButtonTest_Click(object sender, EventArgs e)
    {
        string printData = this.TextBoxSample.Text;

        if (String.IsNullOrEmpty(printData))
        {
            string script = "window.alert('Sample text should NOT be null!')";
            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), Guid.NewGuid().ToString(), script, true);
            return;
        }

        PrintDocument printDoc = new PrintDocument();
        printDoc.DocumentName = Guid.NewGuid().ToString();
        printDoc.PrinterSettings = new PrinterSettings() { PrinterName = this.DropDownListPrtiners.SelectedValue };

        if (printDoc.DefaultPageSettings.PaperSize.Kind == PaperKind.Custom)
        {
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("MES-Barcode-Label", int.Parse(this.TextBoxPaperWidth.Text), int.Parse(this.TextBoxPaperHeight.Text));
        }

        printDoc.PrintPage += (s, ev) =>
        {
            BarcodeWriter barcodeWriter = new BarcodeWriter();

            barcodeWriter.Format = ((BarcodeFormat)(Enum.Parse(typeof(BarcodeFormat), this.DropDownListBarcodeTypes.SelectedValue, true)));
            barcodeWriter.Options.PureBarcode = !this.CheckBoxIsPrintingBarcodeCaption.Checked;
            barcodeWriter.Options.Width = int.Parse(this.TextBoxBarcodeImageWidth.Text);
            barcodeWriter.Options.Height = int.Parse(this.TextBoxBarcodeImageHeight.Text);

            Bitmap bitmap = barcodeWriter.Write(printData);

            ev.Graphics.DrawImage(bitmap, int.Parse(this.TextBoxBarcodeXPosition.Text), int.Parse(this.TextBoxBarcodeYPosition.Text), int.Parse(this.TextBoxBarcodeImageWidth.Text), int.Parse(this.TextBoxBarcodeImageHeight.Text));
        };

        printDoc.Print();
    }
}