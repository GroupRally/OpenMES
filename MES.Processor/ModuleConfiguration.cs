using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Core.Parameter;

namespace MES.Processor
{
    public class ModuleConfiguration
    {
        public static bool IsCheckingSkewAngle = false;

        public static bool IsExtractingBarCodes = false;

        public static int Default_BarCodeScanningTimes = 75;

        public static int Default_FailureRetryTimes = 5;

        public static float Default_ImageRotationDegree = 0;

        public static Dictionary<String, object> Default_ImageRotationParameters = new Dictionary<string, object>() 
        {
            {"RotationAtPercentageOfImageWidth", 50},
            {"RotationAtPercentageOfImageHeight", 50},
            {"IsNoClip", true}
        };

        public static bool Default_IsPrintingBarcodeCaption = true;
        public static string Default_PrinterName = "";
        public static string Default_BarcodeFontName = "";
        public static int Default_BarcodeFontSize = 16;
        public static int Default_BarcodeXPosition = 1;
        public static int Default_BarcodeYPosition = 18;
        public static string Default_CaptionFontName = "Arial";
        public static int Default_CaptionFontSize = 8;
        public static int Default_CaptionXPosition = 1;
        public static int Default_CaptionYPosition = 1;
        public static int Default_BarcodeImageHeight = 35;
        public static int Default_BarcodeImageWidth = 60;
        public static BarcodeType Default_BarcodeType = BarcodeType.CODE_128;

        public static int Default_BarcodeLabelPaperWidth = 98;
        public static int Default_BarcodeLabelPaperHeight = 59;

    }
}
