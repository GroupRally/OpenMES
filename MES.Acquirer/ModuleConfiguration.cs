using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Acquirer
{
    public class ModuleConfiguration
    {
        public static int Default_Timeout = 6000;

        public static int[] Default_ActiveDevices = new int[] {0 };

        public static Dictionary<string, object> Default_CameraParameters = new Dictionary<string, object>() 
        { 
            {xiApi.NET.PRM.AEAG, 1}, 
            {xiApi.NET.PRM.GAIN, 0}, 
            {xiApi.NET.PRM.IMAGE_DATA_FORMAT, xiApi.NET.IMG_FORMAT.RAW8}, 
            {xiApi.NET.PRM.EXPOSURE, 200}, 
            {xiApi.NET.PRM.EXPOSURE_MAX, 200},
            {xiApi.NET.PRM.GAIN_MAX, 16},
            {xiApi.NET.PRM.AEAG_LEVEL, 40},
            {xiApi.NET.PRM.DOWNSAMPLING_TYPE, xiApi.NET.DOWNSAMPLING_TYPE.SKIPPING},
            {xiApi.NET.PRM.DOWNSAMPLING, 1},
            {xiApi.NET.PRM.HEIGHT, 1024},
            {xiApi.NET.PRM.WIDTH, 1280},
            {xiApi.NET.PRM.SHARPNESS, 0F},
            {xiApi.NET.PRM.GAMMAY, 1.0F},
            {xiApi.NET.PRM.GAMMAC, 0F},
            {xiApi.NET.PRM.EXP_PRIORITY, 0.8F},
            {xiApi.NET.PRM.BPC, 0},
            {xiApi.NET.PRM.AUTO_BANDWIDTH_CALCULATION, 1},
            {xiApi.NET.PRM.BUFFER_POLICY, xiApi.NET.BUFF_POLICY.UNSAFE},
            {xiApi.NET.PRM.LIMIT_BANDWIDTH, 5120},
            {xiApi.NET.PRM.OFFSET_X, 0},
            {xiApi.NET.PRM.OFFSET_Y, 0}
        };

        //public static bool IsCheckingSkewAngle = false;

        //public static bool IsExtractingBarCodes = false;

        //public static int Default_BarCodeScanningTimes = 75;

        //public static int Default_FailureRetryTimes = 5;

        //public static float Default_ImageRotationDegree = 0;

        //public static Dictionary<String, object> Default_ImageRotationParameters = new Dictionary<string, object>() 
        //{
        //    {"RotationAtPercentageOfImageWidth", 50},
        //    {"RotationAtPercentageOfImageHeight", 50},
        //    {"IsNoClip", true}
        //};

        public static MES.Core.Parameter.ImageOutputFormat Default_ImageOuputFormat = Core.Parameter.ImageOutputFormat.BMP;
    }
}
