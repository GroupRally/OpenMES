using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Collections;
using xiApi.NET;
using MES.Core;
using MES.Utility;
using MES.Core.Parameter;

namespace MES.Acquirer
{
    public class XIAcquirer : Acquirer
    {
        private xiCam camera;

        public void SetDeviceInstance(object deviceInstance) 
        {
            this.camera = deviceInstance as xiCam;
        }

        public override void Open()
        {
            //base.Open();

            this.camera = new xiCam();

            foreach (int deviceID in ModuleConfiguration.Default_ActiveDevices)
            {
                this.camera.OpenDevice(deviceID);

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
            }
        } 

        public override void Start()
        {
            //base.Start();   

            this.camera.StartAcquisition();
        }

        public override DataItem Acquire()
        {
            //return base.Acquire();

            DataItem returnValue = new DataItem()
            {
                CreationTime = DateTime.Now,
                DeviceID = String.Format("{0}-{1}", this.camera.GetParamString(xiApi.NET.PRM.DEVICE_NAME), this.camera.GetParamString(xiApi.NET.PRM.DEVICE_SN)),
                DataParameters = new List<DataParameter>(new DataParameter[] 
                { 
                    new DataParameter(){ Name = "CameraFrameRate", Value = this.camera.GetParamString(PRM.FRAMERATE)} ,
                    new DataParameter(){ Name = "Exposure", Value = ModuleConfiguration.Default_CameraParameters[xiApi.NET.PRM.EXPOSURE].ToString() } ,
                    new DataParameter(){ Name = "Gain", Value = ModuleConfiguration.Default_CameraParameters[xiApi.NET.PRM.GAIN].ToString() } ,
                    new DataParameter(){ Name = "ImageDataFormat", Value = ModuleConfiguration.Default_CameraParameters[xiApi.NET.PRM.IMAGE_DATA_FORMAT].ToString() } ,
                    new DataParameter(){ Name = "ImageOutputFormat", Value = ModuleConfiguration.Default_ImageOuputFormat.ToString() } //, 
                    //new DataParameter(){ Name = "BarCodeScanningTimes", Value = ModuleConfiguration.Default_BarCodeScanningTimes.ToString()},
                    //new DataParameter(){ Name = "ImageRotationDegree", Value = ModuleConfiguration.Default_ImageRotationDegree.ToString()},
                    //new DataParameter(){ Name = "FailureRetryTimes", Value = ModuleConfiguration.Default_FailureRetryTimes.ToString()}
                })
            };

            Bitmap bitmap = null;

            this.camera.GetImage(out bitmap, ModuleConfiguration.Default_Timeout);

            //if (ModuleConfiguration.IsExtractingBarCodes)
            //{
            //    ArrayList codesRead = null;

            //    this.getBarCode(bitmap, out bitmap, out codesRead);

            //    if ((codesRead != null) && (codesRead.Count > 0))
            //    {
            //        string barcodes = "";

            //        for (int i = 0; i < codesRead.Count; i++)
            //        {
            //            if (codesRead[i] != null)
            //            {
            //                barcodes += codesRead[i].ToString();
            //            }

            //            if (i != (codesRead.Count - 1))
            //            {
            //                barcodes += ",";
            //            }
            //        }

            //        //returnValue.DataParameters = new List<DataParameter>(new DataParameter[] { new DataParameter() { Name = "BarCodes", Value = barcodes } });
                    
            //        returnValue.DataParameters.Add(new DataParameter() { Name = "BarCodes", Value = barcodes });
            //    }
            //}

            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, this.getGDIPlusImageFormat(ModuleConfiguration.Default_ImageOuputFormat));

                returnValue.DataBytes = stream.GetBuffer();

                returnValue.Size = stream.Length;
            }

            return returnValue;
        }

        public override void Stop()
        {
            //base.Stop();

            this.camera.StopAcquisition();
        }

        public override void Close()
        {
            //base.Close();

            int deviceCount = this.camera.GetNumberDevices();

            for (int i = 0; i < deviceCount; i++)
            {
                this.camera.CloseDevice();
            }
        }

        public override byte[] Test(out object[] OutputData)
        {
            //return base.Test(out OutputData);

            byte[] returnValue = null;

            OutputData = null;

            this.camera = new xiCam();

            this.camera.OpenDevice(ModuleConfiguration.Default_ActiveDevices[0]);

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

            //string barcodes = "";

            this.camera.GetImage(out bitmap, ModuleConfiguration.Default_Timeout);

            //if (ModuleConfiguration.IsExtractingBarCodes)
            //{
            //    ArrayList barcodeArray = null;

            //    this.getBarCode(bitmap, out bitmap, out barcodeArray);

            //    //OutputData = (barcodeArray != null) ? barcodeArray.ToArray() : null;

            //    if ((barcodeArray != null) && (barcodeArray.Count > 0))
            //    {
            //        for (int i = 0; i < barcodeArray.Count; i++)
            //        {
            //            if (barcodeArray[i] != null)
            //            {
            //                barcodes += barcodeArray[i].ToString();
            //            }

            //            if (i != (barcodeArray.Count - 1))
            //            {
            //                barcodes += ",";
            //            }
            //        }
            //    }
            //}

            if (bitmap != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, this.getGDIPlusImageFormat(ModuleConfiguration.Default_ImageOuputFormat));

                    returnValue = stream.GetBuffer();
                }
            }

            this.camera.StopAcquisition();

            //this.camera.CloseDevice();

            OutputData = new object[] 
            {
                new DataPair()
                {
                     Identifier = new DataIdentifier()
                     {
                          DataUniqueID = String.Format("Test-{0}", Utility.CommonUtility.GetMillisecondsOfCurrentDateTime(null)) //Guid.NewGuid().ToString()
                     },

                     Items = new List<DataItem>(new DataItem[]
                     {
                         new DataItem()
                         {
                            CreationTime = DateTime.Now,
                            DataBytes = returnValue,
                            Size = returnValue.Length,
                            DeviceID = String.Format("{0}-{1}", this.camera.GetParamString(xiApi.NET.PRM.DEVICE_NAME), this.camera.GetParamString(xiApi.NET.PRM.DEVICE_SN)),
                            DataParameters = new List<DataParameter>(new DataParameter[]
                            {
                                new DataParameter(){ Name = "CameraFrameRate", Value = this.camera.GetParamString(PRM.FRAMERATE)} ,
                                new DataParameter(){ Name = "Exposure", Value = ModuleConfiguration.Default_CameraParameters[xiApi.NET.PRM.EXPOSURE].ToString() } ,
                                new DataParameter(){ Name = "Gain", Value = ModuleConfiguration.Default_CameraParameters[xiApi.NET.PRM.GAIN].ToString() } ,
                                new DataParameter(){ Name = "ImageDataFormat", Value = ModuleConfiguration.Default_CameraParameters[xiApi.NET.PRM.IMAGE_DATA_FORMAT].ToString() } ,
                                new DataParameter(){ Name = "ImageOutputFormat", Value = ModuleConfiguration.Default_ImageOuputFormat.ToString() } //, 
                                //new DataParameter(){ Name = "BarCodeScanningTimes", Value = ModuleConfiguration.Default_BarCodeScanningTimes.ToString()},
                                //new DataParameter(){ Name = "ImageRotationDegree", Value = ModuleConfiguration.Default_ImageRotationDegree.ToString()},
                                //new DataParameter(){ Name = "FailureRetryTimes", Value = ModuleConfiguration.Default_FailureRetryTimes.ToString()},
                                //new DataParameter(){ Name = "BarCodes", Value = barcodes }
                            })
                         }
                     })
                }
            };

            this.camera.CloseDevice();

            return returnValue;
        }

        public override object[] Get()
        {
            //return base.Get();

            return new object[] 
            {
                new CameraParameter()
                {
                    AEAGLevel = this.camera.GetParamInt(PRM.AEAG_LEVEL),
                    AvailableBandWidth = this.camera.GetParamInt(PRM.AVAILABLE_BANDWIDTH),
                    BufferPolicy = this.camera.GetParamInt(PRM.BUFFER_POLICY),
                    DeviceInstancePath = this.camera.GetParamString(PRM.DEVICE_INSTANCE_PATH),
                    DeviceName = this.camera.GetParamString(PRM.DEVICE_NAME),
                    DeviceSN = this.camera.GetParamString(PRM.DEVICE_SN),
                    DeviceType = this.camera.GetParamString(PRM.DEVICE_TYPE),
                    DownSampling = this.camera.GetParamInt(PRM.DOWNSAMPLING),
                    DownSamplingType = this.camera.GetParamInt(PRM.DOWNSAMPLING_TYPE),
                    EnableAEAG = (this.camera.GetParamInt(PRM.AEAG) == 1),
                    EnableAutoBandWidthCalculation = (this.camera.GetParamInt(PRM.AUTO_BANDWIDTH_CALCULATION) == 1),
                    EnableBPC = (this.camera.GetParamInt(PRM.BPC) == 1),
                    Exposure = this.camera.GetParamInt(PRM.EXPOSURE),
                    ExposureMax = this.camera.GetParamInt(PRM.EXPOSURE_MAX),
                    ExposureMin = this.camera.GetParamInt(PRM.EXPOSURE_MIN),
                    ExposurePriority = this.camera.GetParamFloat(PRM.EXP_PRIORITY),
                    FrameRate = this.camera.GetParamFloat(PRM.FRAMERATE),
                    Gain = this.camera.GetParamFloat(PRM.GAIN),
                    GainMax = this.camera.GetParamFloat(PRM.GAIN_MAX),
                    GainMin = this.camera.GetParamFloat(PRM.GAIN_MIN),
                    GammaC = this.camera.GetParamFloat(PRM.GAMMAC),
                    GammaY = this.camera.GetParamFloat(PRM.GAMMAY),
                    ImageDataFormat = (ImageDataFormat)(this.camera.GetParamInt(PRM.IMAGE_DATA_FORMAT)),
                    LimitBandWidth = this.camera.GetParamInt(PRM.LIMIT_BANDWIDTH),
                    OuputImageWidth = this.camera.GetParamInt(PRM.WIDTH),
                    OutputImageHeight = this.camera.GetParamInt(PRM.HEIGHT),
                    Sharpness = this.camera.GetParamFloat(PRM.SHARPNESS)
                }
            };
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

        //private string getBarCode(Bitmap bitmap, out Bitmap resultImage, out ArrayList resultBarCode)
        //{
        //    string returnValue = String.Empty;

        //    //byte[] imageBytes = new byte[1024];

        //    //this.camera.GetImage(imageBytes, ModuleConfiguration.Default_TimeredBarCodeCameraTimeout);

        //    //MemoryStream stream = new MemoryStream(imageBytes);

        //    //Bitmap bitmap = null;// = new Bitmap(stream);

        //    //this.camera.GetImage(out bitmap, ModuleConfiguration.Default_Timeout);

        //    float imageRotationDegree = ModuleConfiguration.Default_ImageRotationDegree;

        //    if (imageRotationDegree != 0)
        //    {
        //        Dictionary<string, object> rotationParams = ModuleConfiguration.Default_ImageRotationParameters;

        //        if ((rotationParams != null) && (rotationParams.Count > 0))
        //        {
        //            int rotationAtPercentageOfImageWidth = (int)(rotationParams["RotationAtPercentageOfImageWidth"]);
        //            int rotationAtPercentageOfImageHeight = (int)(rotationParams["RotationAtPercentageOfImageHeight"]);
        //            bool isNoClip = (bool)(rotationParams["IsNoClip"]);

        //            bitmap = BarcodeUtility.RotateImage(bitmap, (bitmap.Width * (rotationAtPercentageOfImageWidth / 100)), (bitmap.Height * (rotationAtPercentageOfImageHeight / 100)), imageRotationDegree, isNoClip);
        //        }
        //        else
        //        {
        //            bitmap = BarcodeUtility.RotateImage(bitmap, imageRotationDegree);
        //        }
        //    }
            
        //    if (ModuleConfiguration.IsCheckingSkewAngle)
        //    {
        //        DeSkewUtility deSkewUtility = new DeSkewUtility();

        //        deSkewUtility.New(bitmap);

        //        double skewAngle = -1.0 * deSkewUtility.GetSkewAngle();

        //        bitmap = BarcodeUtility.RotateImage(bitmap, ((float)(skewAngle)));
        //    }

        //    ArrayList codesRead = new ArrayList();

        //    BarcodeUtility.FullScanPage(ref codesRead, bitmap, ModuleConfiguration.Default_BarCodeScanningTimes);

        //    if (codesRead.Count > 0)
        //    {
        //        returnValue = codesRead[0].ToString();
        //    }

        //    resultImage = bitmap;

        //    resultBarCode = codesRead;

        //    return returnValue;
        //}
    }
}
