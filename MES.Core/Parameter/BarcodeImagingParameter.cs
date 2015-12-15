using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MES.Core.Parameter
{
    public class BarcodeImagingParameter : ParameterBase
    {
        private int scanTimes = 75;
        private int failureRetryTimes = 6;
        private int imageRotationDegree = 0;
        private int rotationAtPercentageOfImageWidth = 50;
        private int rotationAtPercentageOfImageHeight = 50;
        private bool isNoClicp = true;
        private bool isExtractingBarCode = false;
        private bool isCheckingSkewAngle = false;

        /// <summary>
        /// The value that determines how many times that a whole image is scaned through
        /// </summary>
        [DefaultValue(75)]
        public int ScanTimes { get { return this.scanTimes; } set { this.scanTimes = value; } }

        /// <summary>
        /// The max retry times when a failuer occurs during scanning
        /// </summary>
        [DefaultValue(6)]
        public int FailureRetryTimes { get { return this.failureRetryTimes; } set { this.failureRetryTimes = value; } }

        /// <summary>
        /// The degree that the image is rotated to
        /// </summary>
        [DefaultValue(0)]
        public int ImageRotationDegree { get { return this.imageRotationDegree; } set { this.imageRotationDegree = value; } }

        /// <summary>
        /// The percentage of the width of the image at which the rotation begins
        /// </summary>
        [DefaultValue(50)]
        public int RotationAtPercentageOfImageWidth { get { return this.rotationAtPercentageOfImageWidth; } set { this.rotationAtPercentageOfImageWidth = value; } }

        /// <summary>
        /// The percentage of the hieght of the image at which the rotation begins
        /// </summary>
        [DefaultValue(50)]
        public int RotationAtPercentageOfImageHeight { get { return this.rotationAtPercentageOfImageHeight; } set { this.rotationAtPercentageOfImageHeight = value; } }

        /// <summary>
        /// Whether to clip the image
        /// </summary>
        [DefaultValue(true)]
        public bool IsNoClip { get { return this.isNoClicp; } set { this.isNoClicp = value; } }

        /// <summary>
        /// Whether to extract bar code during image scan
        /// </summary>
        [DefaultValue(false)]
        public bool IsExtractingBarCode { get { return this.isExtractingBarCode; } set { this.isExtractingBarCode = value; } }

        /// <summary>
        /// Whether to check skew angle during image scan
        /// </summary>
        [DefaultValue(false)]
        public bool IsCheckingSkewAngle { get { return this.isCheckingSkewAngle; } set { this.isCheckingSkewAngle = value; } }

        public override string ToString()
        {
            //return base.ToString();

            return "Barcode Imaging";
        }
    }
}
