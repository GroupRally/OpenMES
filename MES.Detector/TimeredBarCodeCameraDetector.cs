using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Collections;
using xiApi.NET;
using MES.Core;
using MES.Utility;

namespace MES.Detector
{
    public class TimeredBarCodeCameraDetector : TimerDetector
    {
        private xiCam camera;

        public event NotifyCallBack NotifyCallBack;

        public object GetDeviceInstance()
        {
            return this.camera;
        }

        public override void Detect()
        {
            this.camera = new xiCam();

            this.camera.OpenDevice(ModuleConfiguration.Default_TimeredBarCodeCameraDeviceID);

            if (ModuleConfiguration.Default_CameraParameters != null)
            {
                foreach (string paramName in ModuleConfiguration.Default_CameraParameters.Keys)
                {
                    object value = ModuleConfiguration.Default_CameraParameters[paramName];

                    if (value is float)
                    {
                        this.camera.SetParam(paramName, ((float)(ModuleConfiguration.Default_CameraParameters[paramName])));
                    }
                    else
                    {
                        this.camera.SetParam(paramName, ((int)(ModuleConfiguration.Default_CameraParameters[paramName])));
                    }
                }
            }

            this.camera.StartAcquisition();

            base.TimerCallBack += TimeredBarCodeCameraDetector_TimerCallBack;

            base.Detect();
        }

        void TimeredBarCodeCameraDetector_TimerCallBack(object sender, TimerDetectorEventArgs e)
        {
            Bitmap rawImage = null;
            ArrayList rawBarCode = null;

            DataIdentifier Identifier = e.DataIdentifier;

            string barcode = this.getBarCode(out rawImage, out rawBarCode);

            while (String.IsNullOrEmpty(barcode))
            {
                for (int i = 0; i < ModuleConfiguration.Default_FailureRetryTimes; i++)
                {
                    barcode = this.getBarCode(out rawImage, out rawBarCode);

                    if (i == (ModuleConfiguration.Default_FailureRetryTimes - 1))
                    {
                        barcode = "SignalError";
                    }
                }
            }

            Identifier.DataUniqueID = barcode;

            //Identifier.RawData = new object[] { rawImage, rawBarCode };

            using (MemoryStream stream = new MemoryStream()) 
            {
                rawImage.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

                Identifier.RawData = new object[] { stream.GetBuffer(), rawBarCode };
            }

            if (this.NotifyCallBack != null)
            {
                this.NotifyCallBack(Identifier);
            }

            //base.Notify(Identifier);
        }

        public override void Stop()
        {
            base.Stop();

            if (this.camera != null)
            {
                this.camera.StopAcquisition();
                this.camera.CloseDevice();
            }
        }

        public override byte[] Test(out object[] OutputData) 
        {
            byte[] returnValue = null;

            this.camera = new xiCam();

            this.camera.OpenDevice(ModuleConfiguration.Default_TimeredBarCodeCameraDeviceID);

            if (ModuleConfiguration.Default_CameraParameters != null)
            {
                foreach (string paramName in ModuleConfiguration.Default_CameraParameters.Keys)
                {
                    object value = ModuleConfiguration.Default_CameraParameters[paramName];

                    if (value is float)
                    {
                        this.camera.SetParam(paramName, ((float)(ModuleConfiguration.Default_CameraParameters[paramName])));
                    }
                    else
                    {
                        this.camera.SetParam(paramName, ((int)(ModuleConfiguration.Default_CameraParameters[paramName])));
                    }
                }
            }

            this.camera.StartAcquisition();

            Bitmap bitmap = null;

            ArrayList barcodeArray = null;

            this.getBarCode(out bitmap, out barcodeArray);

            OutputData = (barcodeArray != null) ? barcodeArray.ToArray() : null;

            if (bitmap != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

                    returnValue = stream.GetBuffer();
                }
            }

            this.camera.StopAcquisition();

            this.camera.CloseDevice();

            return returnValue;
        }

        private string getBarCode(out Bitmap rawImage, out ArrayList rawBarCode) 
        {
            string returnValue = String.Empty;

            //byte[] imageBytes = new byte[1024];

            //this.camera.GetImage(imageBytes, ModuleConfiguration.Default_TimeredBarCodeCameraTimeout);

            //MemoryStream stream = new MemoryStream(imageBytes);

            Bitmap bitmap = null;// = new Bitmap(stream);

            this.camera.GetImage(out bitmap, ModuleConfiguration.Default_TimeredBarCodeCameraTimeout);

            float imageRotationDegree = ModuleConfiguration.Default_ImageRotationDegree;

            if (imageRotationDegree != 0)
            {
               Dictionary<string, object> rotationParams = ModuleConfiguration.Defautl_ImageRotationParameters;

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

            ArrayList codesRead = new ArrayList();

            BarcodeUtility.FullScanPage(ref codesRead, bitmap, ModuleConfiguration.Default_TimeredBarCodeCameraScanningTimes);

            if (codesRead.Count > 0)
            {
                returnValue = codesRead[0].ToString();
            }

            rawImage = bitmap;

            rawBarCode = codesRead;

            return returnValue;
        }
    }

    public delegate void NotifyCallBack(DataIdentifier Identifier);
}