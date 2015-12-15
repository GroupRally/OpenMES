using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Configuration;
using ImageAcquisition.Core;
using ImageAcquisition.Detector;
using ImageAcquisition.Utility;

namespace DetectorDemo
{
    public partial class FormMain1 : Form
    {
        private Bitmap bitmap = null;

        private TimeredBarCodeCameraDetector detector = null;

        delegate void SetImageCallBack(DataIdentifier Identifier);

        delegate void SetBarCodeListCallBack(DataIdentifier Identifier);

        public FormMain1()
        {
            InitializeComponent();

            this.pictureBoxSignalPreview.Image = this.bitmap;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            double timerInterval = 0;
            int deviceTimeout = 0, scanningRepeats = 0;

            if ((String.IsNullOrEmpty(this.textBoxTimerInterval.Text)) || (!double.TryParse(this.textBoxTimerInterval.Text, out timerInterval)))
            {
                if (MessageBox.Show("The value of timer interval is invalid, use defualt value instead?", "Invalid Value", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) 
                {
                    this.textBoxTimerInterval.Text = ModuleConfiguration.Default_TimerInterval.ToString();

                    timerInterval = ModuleConfiguration.Default_TimerInterval;
                }
                else
                {
                    return;
                }
            }

            if ((String.IsNullOrEmpty(this.textBoxDeviceTimeout.Text)) || (!int.TryParse(this.textBoxDeviceTimeout.Text, out deviceTimeout)))
            {
                if (MessageBox.Show("The value of device timeout is invalid, use defualt value instead?", "Invalid Value", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    this.textBoxTimerInterval.Text = ModuleConfiguration.Default_TimeredBarCodeCameraTimeout.ToString();

                    deviceTimeout = ModuleConfiguration.Default_TimeredBarCodeCameraTimeout;
                }
                else
                {
                    return;
                }
            }

            if ((String.IsNullOrEmpty(this.textBoxBarCodeScanningRepeats.Text)) || (!int.TryParse(this.textBoxBarCodeScanningRepeats.Text, out deviceTimeout)))
            {
                if (MessageBox.Show("The value of bar code scanning repeats is invalid, use defualt value instead?", "Invalid Value", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    this.textBoxBarCodeScanningRepeats.Text = ModuleConfiguration.Default_TimeredBarCodeCameraScanningTimes.ToString();

                    scanningRepeats = ModuleConfiguration.Default_TimeredBarCodeCameraScanningTimes;
                }
                else
                {
                    return;
                }
            }

            timerInterval = double.Parse(this.textBoxTimerInterval.Text);

            deviceTimeout = int.Parse(this.textBoxDeviceTimeout.Text);

            scanningRepeats = int.Parse(this.textBoxDeviceTimeout.Text);

            ModuleConfiguration.Default_TimerInterval = timerInterval;

            ModuleConfiguration.Default_TimeredBarCodeCameraTimeout = deviceTimeout;

            ModuleConfiguration.Default_TimeredBarCodeCameraScanningTimes = scanningRepeats;

            Thread thread = new Thread(new ThreadStart(this.startDetecting));

            thread.Start();

            //this.startDetecting();
        }

        private void startDetecting() 
        {
            this.detector = new TimeredBarCodeCameraDetector();
            this.detector.NotifyCallBack += detector_CallBack;
            this.detector.Detect();
            //this.pictureBoxSignalPreview.Image = this.detector.DataIdentifier.RawData as Bitmap;

            //this.setDeviceInstance();
        }

        //private void setDeviceInstance() 
        //{
        //    object deviceInstance = this.detector.GetDeviceInstance();

        //    byte[] deviceInstanceBytes = CommonUtility.BinarySerialize(deviceInstance);

        //    string deviceInstanceStateLocation = ConfigurationManager.AppSettings.Get("DeviceInstanceStateLocation");

        //    deviceInstanceStateLocation = deviceInstanceStateLocation.EndsWith("\\") ? deviceInstanceStateLocation.Substring(0, (deviceInstanceStateLocation.Length - 1)) : deviceInstanceStateLocation;

        //    if (!Directory.Exists(deviceInstanceStateLocation))
        //    {
        //        Directory.CreateDirectory(deviceInstanceStateLocation);
        //    }

        //    string deviceInstanceStateResourceName = ConfigurationManager.AppSettings.Get("DefaultDeviceInstanceStateResourceName");

        //    string filePath = String.Format("{0}\\{1}", deviceInstanceStateLocation, deviceInstanceStateResourceName);

        //    using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
        //    {
        //        fileStream.Write(deviceInstanceBytes, 0, deviceInstanceBytes.Length);
        //    }
        //}

        void detector_CallBack(DataIdentifier Identifier)
        {
            //this.pictureBoxSignalPreview.Image = ((object[])(Identifier.RawData))[0] as Bitmap;
            //this.pictureBoxSignalPreview.Refresh();
            //this.listBoxBarCodesExtracted.DataSource = ((object[])(Identifier.RawData))[1];//new string[] { Identifier.DataUniqueID};
            //this.listBoxBarCodesExtracted.Refresh();

            Thread thread = new Thread(this.threadProcSafe);

            thread.Start(Identifier);
        }

        // This method is executed on the worker thread and makes 
        // a thread-safe call on the controls. 
        private void threadProcSafe(object identifier)
        {
            this.SetControls(identifier as DataIdentifier);
        }

        private void SetControls(DataIdentifier identifier) 
        {
            if (this.pictureBoxSignalPreview.InvokeRequired)
            {
                SetImageCallBack setImageCallBack = new SetImageCallBack(this.SetControls);

                this.Invoke(setImageCallBack, identifier);
            }
            else 
            {
                //this.pictureBoxSignalPreview.Image = ((object[])(identifier.RawData))[0] as Bitmap;

                using (MemoryStream stream = new MemoryStream((((object[])(identifier.RawData))[0]) as byte[]))
                {
                    this.pictureBoxSignalPreview.Image = System.Drawing.Bitmap.FromStream(stream);
                }

                this.pictureBoxSignalPreview.Refresh();
            }

            if (this.listBoxBarCodesExtracted.InvokeRequired)
            {
                SetBarCodeListCallBack setBarCodeListCallBack = new SetBarCodeListCallBack(this.SetControls);

                this.Invoke(setBarCodeListCallBack, identifier);
            }
            else
            {
                this.listBoxBarCodesExtracted.DataSource = ((object[])(identifier.RawData))[1];//new string[] { Identifier.DataUniqueID};
                this.listBoxBarCodesExtracted.Refresh();
            }
        }


        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (this.detector != null)
            {
                this.detector.Stop();
            }
        }
    }
}
