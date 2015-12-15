using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MES.Core.Parameter
{
    public class CameraParameter : ParameterBase
    {
        private int exposure = 20000;
        private int exposureMax = 200000;
        private int exposureMin = 2;
        private float gain = 18;
        private float gainMax = 100;
        private float gainMin = 1;
        private bool enableAEAG = false;
        private int aeagLevel = 40;
        private ImageDataFormat imageDataFormat = ImageDataFormat.MONO8;
        private ImageOutputFormat imageOuputFormat = ImageOutputFormat.BMP;
        private int deviceTimeout = 6000;

        private int downSampling = 1;
        private int downSamplingType = 0;
        private int outputImageWidth = 1280;
        private int outputImageHeight = 1024;
        //private int sensorDataBitDepth;
        //private int outputDataBitDepth;

        private int outputImageOffsetX = 0;
        private int outputImageOffsetY = 0;

        private bool enableAutoBandWidthCalculation = true;
        private bool enableBPC = false;
        private int bufferPolicy = 0;
        private int limitBandWidth = 5120;
        private float gammaY = 1.0F;
        private float gammaC = 0F;
        private float sharpness = 0F;
        private float exposurePriority = 0.8F;


        /// <summary>
        /// Exposure time in microseconds to be set on camera  
        /// </summary>
        [DefaultValue(20000)]
        public int Exposure { get { return this.exposure; } set { this.exposure = value; } }

        /// <summary>
        /// The longest possible exposure to be set on camera
        /// </summary>
        public int ExposureMax { get { return this.exposureMax; } set { this.exposureMax = value; } }

        /// <summary>
        /// The shortest possible exposure to be set on camera
        /// </summary>
        public int ExposureMin { get { return this.exposureMin; } set { this.exposureMin = value; } }

        /// <summary>
        /// Camera gain in dB
        /// </summary>
        [DefaultValue(18)]
        public float Gain { get { return this.gain; } set { this.gain = value; } }

        /// <summary>
        /// Highest possible camera gain in dB
        /// </summary>
        public float GainMax { get { return this.gainMax; } set { this.gainMax = value; } }

        /// <summary>
        /// Lowest possible camera gain in dB
        /// </summary>
        public float GainMin { get { return this.gainMin; } set { this.gainMin = value; } }

        /// <summary>
        /// Whether to start automatic exposure/gain
        /// </summary>
        [DefaultValue(false)]
        public bool EnableAEAG { get { return this.enableAEAG; } set { this.enableAEAG = value; } }

        /// <summary>
        /// Average intensity of output signal AEAG should achieve(in)
        /// </summary>
        public int AEAGLevel { get { return this.aeagLevel; } set { this.aeagLevel = value; } }

        /// <summary>
        /// Format of image output from camera
        /// </summary>
        [DefaultValue(ImageDataFormat.MONO8)]
        public ImageDataFormat ImageDataFormat { get { return this.imageDataFormat; } set { this.imageDataFormat = value; } }

        /// <summary>
        /// Format of image persisted
        /// </summary>
        [DefaultValue(ImageOutputFormat.BMP)]
        public ImageOutputFormat ImageOutputFormat { get { return this.imageOuputFormat; } set { this.imageOuputFormat = value; } }

        /// <summary>
        /// Time interval required to wait for the image (in milliseconds)
        /// </summary>
        [DefaultValue(6000)]
        public int DeviceTimeout { get { return this.deviceTimeout; } set { this.deviceTimeout = value; } }

        /// <summary>
        /// Image resolution by binning or skipping
        /// </summary>
        [DefaultValue(1)]
        public int DownSampling { get { return this.downSampling; } set { this.downSampling = value; } }

        /// <summary>
        /// Downsampling type, switching between binning and skipping
        /// </summary>
        [DefaultValue(0)]
        public int DownSamplingType { get { return this.downSamplingType; } set { this.downSamplingType = value; } }

        /// <summary>
        /// Width of the image provided by the camera (in pixels). Type int. Must be divisible by 4, otherwise exception is raised
        /// </summary>
        [DefaultValue(1280)]
        public int OuputImageWidth { get { return this.outputImageWidth; } set { this.outputImageWidth = value; } }

        /// <summary>
        /// Height of the image provided by the camera (in pixels). Type int. Must be even, otherwise exception is raised
        /// </summary>
        [DefaultValue(1024)]
        public int OutputImageHeight { get { return this.outputImageHeight; } set { this.outputImageHeight = value; } }

        /// <summary>
        /// Horizontal offset from the origin to the area of interest (in pixels). Type int. Must be even, otherwise exception is raised
        /// </summary>
        [DefaultValue(0)]
        public int OuputImageOffsetX { get { return this.outputImageOffsetX; } set { this.outputImageOffsetX = value; } }

        /// <summary>
        /// Vertical offset from the origin to the area of interest (in pixels). Type int. Must be even, otherwise exception is raised
        /// </summary>
        [DefaultValue(0)]
        public int OutputImageOffsetY { get { return this.outputImageOffsetY; } set { this.outputImageOffsetY = value; } }

        //public int SensorDataBitDepth { get { return this.sensorDataBitDepth; } set { this.sensorDataBitDepth = value; } }

        //public int OutputDataBitDepth { get { return this.outputDataBitDepth; } set { this.outputDataBitDepth = value; } }

        /// <summary>
        /// Frames per second
        /// </summary>
        [ReadOnly(true)]
        public float FrameRate { get; set; }

        /// <summary>
        /// Camera model name
        /// </summary>
        [ReadOnly(true)]
        public string DeviceName { get; set; }

        /// <summary>
        /// Device serial number in decimal format. Type string, int, float
        /// </summary>
        [ReadOnly(true)]
        public string DeviceSN { get; set; }

        /// <summary>
        /// Device type (1394, USB2.0, CURRERA…..). Type string
        /// </summary>
        [ReadOnly(true)]
        public string DeviceType { get; set; }

        /// <summary>
        /// Device system instance path
        /// </summary>
        [ReadOnly(true)]
        public string DeviceInstancePath { get; set; }

        /// <summary>
        /// Available interface bandwidth(int Megabits). Type int
        /// </summary>
        [ReadOnly(true)]
        public int AvailableBandWidth { get; set; }

        /// <summary>
        /// Enable/disable automatic bus speed adjusting on device init. Type int
        /// </summary>
        [DefaultValue(true)]
        public bool EnableAutoBandWidthCalculation { get { return this.enableAutoBandWidthCalculation; } set { this.enableAutoBandWidthCalculation = value; } }

        /// <summary>
        /// Enable correction of bad pixels. By default 0(disabled). Type integer
        /// </summary>
        [DefaultValue(false)]
        public bool EnableBPC { get { return this.enableBPC; } set { this.enableBPC = value; } }

        /// <summary>
        /// Data move policy, 
        /// can be safe, data will be copied to user/app buffer, 
        /// or unsafe, user will get internally allocated buffer without data copy
        /// </summary>
        [DefaultValue(0)]
        public int BufferPolicy { get { return this.bufferPolicy; } set { this.bufferPolicy = value; } }

        /// <summary>
        /// Bandwidth(datarate)(in Megabits)
        /// </summary>
        [DefaultValue(5120)]
        public int LimitBandWidth { get { return this.limitBandWidth; } set { this.limitBandWidth = value; } }

        /// <summary>
        /// Luminosity gamma. By default 1.0. Type float
        /// </summary>
        [DefaultValue(1.0F)]
        public float GammaY { get { return this.gammaY; } set { this.gammaY = value; } }

        /// <summary>
        /// Chromaticity gamma. By default 0. Type float
        /// </summary>
        [DefaultValue(0F)]
        public float GammaC { get { return this.gammaC; } set { this.gammaC = value; } }

        /// <summary>
        /// Sharpness strenght. By default 0. Type float
        /// </summary>
        [DefaultValue(0F)]
        public float Sharpness { get { return this.sharpness; } set { this.sharpness = value; } }

        /// <summary>
        /// Exposure priority (0.5 - exposure 50%, gain 50%). By default 0.8. Type float
        /// </summary>
        [DefaultValue(0.8F)]
        public float ExposurePriority { get { return this.exposurePriority; } set { this.exposurePriority = value; } }

        public override string ToString()
        {
            //return base.ToString();

            return "Camera";
        }
    }

    public enum ImageDataFormat 
    {
        // Summary:
        //     Monochrome image with 16 bits per pixel.
        MONO16 = 1,
        //
        // Summary:
        //     Monochrome image with 8 bits per pixel.
        MONO8 = 0,
        //
        // Summary:
        //     16 bits per pixel raw data from sensor.
        RAW16 = 6,
        //
        // Summary:
        //     8 bits per pixel raw data from sensor.
        RAW8 = 5,
        //
        // Summary:
        //     RGB image data format image with 8 bits per pixel.
        RGB24 = 2,
        //
        // Summary:
        //     RGBA image data format image with 8 bits per pixel, Alpha values not valid.
        RGB32 = 3,
        //
        // Summary:
        //     RGB planar image data format image with 8 bits per pixel.
        RGBPLANAR = 4
    }

    public enum ImageOutputFormat 
    {
        BMP = 0,
        JPEG = 1,
        PNG = 2,
        GIF = 3
    }
}
