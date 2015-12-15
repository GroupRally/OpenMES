using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using MES.Core;
using MES.Utility;
using ZXing;

namespace MES.Processor
{
    public class Processor : IProcessor
    {
        public MES.Core.Result Process(DataPair Pair)
        {
            throw new NotImplementedException();
        }

        public DataPair Compute(DataPair Pair)
        {
            DataPair returnValue = new DataPair()
            {
                Identifier = Pair.Identifier,
                Items = new List<DataItem>()
            };

            if (Pair.Items != null)
            {
                foreach (DataItem item in Pair.Items)
                {
                    returnValue.Items.Add(new DataItem() 
                    {
                          DataBytes = item.DataBytes,
                          DataParameters = new List<DataParameter>(item.DataParameters.ToArray()),
                          DeviceID = item.DeviceID,
                          LocationID = item.LocationID
                    });
                }
            }

            Bitmap bitmap = null;
            ArrayList codesRead = null;

            using(MemoryStream stream = new MemoryStream(returnValue.Items[0].DataBytes))
            {
                bitmap = Bitmap.FromStream(stream) as Bitmap;
            }

            this.getBarCode(bitmap, out bitmap, out codesRead);

            byte[] buffer = null;

            using (MemoryStream stream = new MemoryStream())
            {

                MES.Core.Parameter.ImageOutputFormat imageOutputFormat = (MES.Core.Parameter.ImageOutputFormat)(Enum.Parse(typeof(MES.Core.Parameter.ImageOutputFormat), Pair.Items[0].GetParameterValue("ImageOutputFormat")));

                System.Drawing.Imaging.ImageFormat imageFormat = this.getGDIPlusImageFormat(imageOutputFormat);

                bitmap.Save(stream, imageFormat);

                buffer = stream.GetBuffer();
            }

            returnValue.Items[0].DataBytes = buffer;

            returnValue.Items[0].Size = buffer.Length;

            returnValue.Items[0].CreationTime = DateTime.Now;

            returnValue.Items[0].Description = "Barcodes_Computed";

            if ((codesRead != null) && (codesRead.Count > 0))
            {
                string barcodes = "";

                for (int i = 0; i < codesRead.Count; i++)
                {
                    if (codesRead[i] != null)
                    {
                        barcodes += codesRead[i].ToString();
                    }

                    if (i != (codesRead.Count - 1))
                    {
                        barcodes += ",";
                    }
                }
                
                returnValue.Items[0].DataParameters.Add(new DataParameter() 
                { 
                    Name = "BarCodes", 
                    Value = barcodes 
                });
            }

            returnValue.Items[0].DataParameters.AddRange(new DataParameter[]
            {  
                new DataParameter(){ Name = "BarCodeScanningTimes", Value = ModuleConfiguration.Default_BarCodeScanningTimes.ToString()},
                new DataParameter(){ Name = "ImageRotationDegree", Value = ModuleConfiguration.Default_ImageRotationDegree.ToString()},
                new DataParameter(){ Name = "FailureRetryTimes", Value = ModuleConfiguration.Default_FailureRetryTimes.ToString()}
            });

            return returnValue;
        }


        private string getBarCode(Bitmap bitmap, out Bitmap resultImage, out ArrayList resultBarCode)
        {
            string returnValue = String.Empty;

            float imageRotationDegree = ModuleConfiguration.Default_ImageRotationDegree;

            if (imageRotationDegree != 0)
            {
                Dictionary<string, object> rotationParams = ModuleConfiguration.Default_ImageRotationParameters;

                if ((rotationParams != null) && (rotationParams.Count > 0))
                {
                    int rotationAtPercentageOfImageWidth = (int)(rotationParams["RotationAtPercentageOfImageWidth"]);
                    int rotationAtPercentageOfImageHeight = (int)(rotationParams["RotationAtPercentageOfImageHeight"]);
                    bool isNoClip = (bool)(rotationParams["IsNoClip"]);

                    bitmap = BarcodeUtility.RotateImage(bitmap, (bitmap.Width * (rotationAtPercentageOfImageWidth / 100)), (bitmap.Height * (rotationAtPercentageOfImageHeight / 100)), imageRotationDegree, isNoClip);
                }
                else
                {
                    bitmap = BarcodeUtility.RotateImage(bitmap, imageRotationDegree);
                }
            }

            if (ModuleConfiguration.IsCheckingSkewAngle)
            {
                DeSkewUtility deSkewUtility = new DeSkewUtility();

                deSkewUtility.New(bitmap);

                double skewAngle = -1.0 * deSkewUtility.GetSkewAngle();

                bitmap = BarcodeUtility.RotateImage(bitmap, ((float)(skewAngle)));
            }

            ArrayList codesRead = new ArrayList();

            BarcodeUtility.FullScanPage(ref codesRead, bitmap, ModuleConfiguration.Default_BarCodeScanningTimes);

            if (codesRead.Count > 0)
            {
                returnValue = codesRead[0].ToString();
            }

            resultImage = bitmap;

            resultBarCode = codesRead;

            return returnValue;
        }

        private System.Drawing.Imaging.ImageFormat getGDIPlusImageFormat(MES.Core.Parameter.ImageOutputFormat imageFormat)
        {
            switch (imageFormat)
            {
                case MES.Core.Parameter.ImageOutputFormat.BMP:
                    {
                        return System.Drawing.Imaging.ImageFormat.Bmp;
                        //break;
                    }
                case MES.Core.Parameter.ImageOutputFormat.JPEG:
                    {
                        return System.Drawing.Imaging.ImageFormat.Jpeg;
                        //break;
                    }
                case MES.Core.Parameter.ImageOutputFormat.PNG:
                    {
                        return System.Drawing.Imaging.ImageFormat.Png;
                        //break;
                    }
                case MES.Core.Parameter.ImageOutputFormat.GIF:
                    {
                        return System.Drawing.Imaging.ImageFormat.Gif;
                        //break;
                    }
                default:
                    {
                        return System.Drawing.Imaging.ImageFormat.Bmp;
                        //break;
                    }
            }
        }


        public DataPair Print(DataPair Pair, string DocumentName, string DataKeyName)
        {
            PrintDocument printDocument = new PrintDocument();

            printDocument.DocumentName = DocumentName;

            string printData = Pair.Items[0].GetParameterValue(DataKeyName);

            printDocument.PrinterSettings = new PrinterSettings() 
            {  
                PrinterName = ModuleConfiguration.Default_PrinterName
            };

            if (printDocument.DefaultPageSettings.PaperSize.Kind == PaperKind.Custom)
            {
                printDocument.DefaultPageSettings.PaperSize = new PaperSize("MES-Barcode-Label", ModuleConfiguration.Default_BarcodeLabelPaperWidth, ModuleConfiguration.Default_BarcodeLabelPaperHeight);
            }

            printDocument.PrintPage += (sender, e) =>
            {
            //    if (ModuleConfiguration.Default_IsPrintingBarcodeLabel)
            //    {
            //        e.Graphics.DrawString(printData, new Font(ModuleConfiguration.Default_LabelFontName, ModuleConfiguration.Default_LabelFontSize), Brushes.Black, ModuleConfiguration.Default_LabelXPosition, ModuleConfiguration.Default_LabelYPosition);
            //    }

            //    e.Graphics.DrawString(printData, new Font(ModuleConfiguration.Default_BarcodeFontName, ModuleConfiguration.Default_BarcodeFontSize), Brushes.Black, ModuleConfiguration.Default_BarcodeXPosition, ModuleConfiguration.Default_BarcodeYPosition);
                
                BarcodeWriter barcodeWriter = new BarcodeWriter();

                barcodeWriter.Options.PureBarcode = (!ModuleConfiguration.Default_IsPrintingBarcodeCaption);
                barcodeWriter.Options.Height = ModuleConfiguration.Default_BarcodeImageHeight;
                barcodeWriter.Options.Width = ModuleConfiguration.Default_BarcodeImageWidth;

                barcodeWriter.Format = ((BarcodeFormat)(ModuleConfiguration.Default_BarcodeType));

                Bitmap bitmap = barcodeWriter.Write(printData);

                e.Graphics.DrawImage(bitmap, ModuleConfiguration.Default_BarcodeXPosition, ModuleConfiguration.Default_BarcodeYPosition, ModuleConfiguration.Default_BarcodeImageWidth, ModuleConfiguration.Default_BarcodeImageHeight);

                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                    Pair.Items[0].DataBytes = stream.GetBuffer();
                }

                Pair.Items[0].DataParameters.AddRange(new DataParameter[] 
                {
                    new DataParameter(){ Name = "DocumentName", Value = DocumentName},
                    new DataParameter(){ Name = "DocumentData", Value = printData},
                    new DataParameter(){ Name = "PrinterName", Value = ModuleConfiguration.Default_PrinterName},
                    new DataParameter(){ Name = "BarcodeImageWidth", Value =  ModuleConfiguration.Default_BarcodeImageWidth.ToString()},
                    new DataParameter(){ Name = "BarcodeImageHeight", Value =  ModuleConfiguration.Default_BarcodeImageHeight.ToString()},
                    new DataParameter(){ Name = "BarcodeXPosition", Value =  ModuleConfiguration.Default_BarcodeXPosition.ToString()},
                    new DataParameter(){ Name = "BarcodeYPosition", Value =  ModuleConfiguration.Default_BarcodeYPosition.ToString()},
                    new DataParameter(){ Name = "IsPrintingBarcodeCaption", Value =  ModuleConfiguration.Default_IsPrintingBarcodeCaption.ToString()},
                    new DataParameter(){ Name = "BarcodeLabelPaperWidth", Value =  ModuleConfiguration.Default_BarcodeLabelPaperWidth.ToString()},
                    new DataParameter(){ Name = "BarcodeLabelPaperHeight", Value =  ModuleConfiguration.Default_BarcodeLabelPaperHeight.ToString()},
                });
            };

            printDocument.Print();

            return Pair;
        }
    }
}
