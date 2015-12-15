using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Core.Parameter;

namespace MES.Detector
{
    public static class  ModuleConfiguration
    {
        public static CommunicationType Default_CommunicationType = CommunicationType.MulticastSocket; 

        public static string Default_MulticastAddress = "224.100.0.1";

        public static int Default_MulticastPort = 9050;

        public static string Default_LocalAddress = "127.0.0.1";

        public static int Default_LocalPort = 9051;

        public static int Default_TimeToLive = 50;

        public static string Default_PipeName = "ImageAcquisitionPipe";

        public static int Default_InfraredDepth = 100;

        public static int Default_SerialPortDataBits = 8;

        public static int Default_SerialPortBaudRate = 9600;

        public static string Default_SerialPortName = "COM1";

        public static string Default_SerialPortStopBits = "One";

        public static string Default_SerialPortHandShake = "None";

        public static string Default_SerialPortParity = "None";

        public static int Default_SerialPortReadTimeout = 500;

        public static int Default_SerialPortWriteTimeout = 500;

        public static int Default_SerialPortReadBufferSize = 4096;

        public static int Default_SerialPortWriteBufferSize = 4096;

        public static bool Default_SerialPortRtsEnable = true;

        public static bool Default_SerialPortDtrEnable = true;

        public static int Default_SerialPortEncodingCodePage = 20127;//(ASCIIEncoding).http://msdn.microsoft.com/en-us/library/dd317756(VS.85).aspx

        public static SerialPortMode Default_SerialPortMode = SerialPortMode.Normal;

        public static string[] Default_SerialPortExpectedDataValue = new string[]{ "7", "8", "9"};

        public static bool Default_SerialPortIsReadingByLine = true;

        public static bool Default_SerialPortIsBroadcasting = true;

        public static string Default_SequenceMask = "";

        public static string Default_SequenceSeparator = "|";

        public static double Default_TimerInterval = 8000;

        public static int Default_TimeredBarCodeCameraDeviceID = 0;

        public static int Default_TimeredBarCodeCameraTimeout = 6000;

        public static int Default_TimeredBarCodeCameraScanningTimes = 75;

       public static Dictionary<string, object> Default_CameraParameters = new Dictionary<string, object>() 
        { 
            {xiApi.NET.PRM.AEAG, 1}, 
            {xiApi.NET.PRM.GAIN, 0}, 
            {xiApi.NET.PRM.IMAGE_DATA_FORMAT, xiApi.NET.IMG_FORMAT.RAW8}, 
            {xiApi.NET.PRM.EXPOSURE, 200},
            {xiApi.NET.PRM.EXPOSURE_MAX, 200},
            {xiApi.NET.PRM.GAIN_MAX, 16},
            {xiApi.NET.PRM.AEAG_LEVEL, 40}
        };

       public static int Default_FailureRetryTimes = 5;

       public static float Default_ImageRotationDegree = 0;

       public static Dictionary<String, object> Defautl_ImageRotationParameters = new Dictionary<string, object>() 
       {
           {"RotationAtPercentageOfImageWidth", 50},
           {"RotationAtPercentageOfImageHeight", 50},
           {"IsNoClip", true}
       }; 
    }
}
