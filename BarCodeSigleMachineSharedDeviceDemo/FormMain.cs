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
using MES.Core;
using MES.Detector;
using MES.Utility;
using MES.Acquirer;

namespace BarCodeSigleMachineSharedDeviceDemo
{
    public partial class FormMain : Form
    {
        private Bitmap bitmap = null;

        private TimeredBarCodeCameraDetector detector = null;

        delegate void SetImageCallBack(DataIdentifier Identifier);

        delegate void SetBarCodeListCallBack(DataIdentifier Identifier);

        private XIAcquirer xiAcquirer = null;

        //private Thread detectingThread = null;

        private string currentSignalFile = "";

        public FormMain()
        {
            InitializeComponent();

            this.pictureBoxSignalPreview.Image = this.bitmap;

            this.comboBoxImageFormats.SelectedIndex = 0;
        }

        private bool processUserInputs(bool isTesting) 
        {
            double timerInterval = 0;
            int deviceTimeout = 0, scanningRepeats = 0, exposureTime_us = 200, rotationDegree = 0;
            float gain_db = 0;

            if (!isTesting)
            {
                if ((String.IsNullOrEmpty(this.textBoxTimerInterval.Text)) || (!double.TryParse(this.textBoxTimerInterval.Text, out timerInterval)))
                {
                    if (MessageBox.Show("The value of timer interval is invalid, use defualt value instead?", "Invalid Value", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        this.textBoxTimerInterval.Text = MES.Detector.ModuleConfiguration.Default_TimerInterval.ToString();

                        timerInterval = MES.Detector.ModuleConfiguration.Default_TimerInterval;
                    }
                    else
                    {
                        return false;
                    }
                }

                timerInterval = double.Parse(this.textBoxTimerInterval.Text);

                MES.Detector.ModuleConfiguration.Default_TimerInterval = timerInterval;
            }

            if ((String.IsNullOrEmpty(this.textBoxDeviceTimeout.Text)) || (!int.TryParse(this.textBoxDeviceTimeout.Text, out deviceTimeout)))
            {
                if (MessageBox.Show("The value of device timeout is invalid, use defualt value instead?", "Invalid Value", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    this.textBoxTimerInterval.Text = MES.Detector.ModuleConfiguration.Default_TimeredBarCodeCameraTimeout.ToString();

                    deviceTimeout = MES.Detector.ModuleConfiguration.Default_TimeredBarCodeCameraTimeout;
                }
                else
                {
                    return false;
                }
            }

            if ((String.IsNullOrEmpty(this.textBoxBarCodeScanningRepeats.Text)) || (!int.TryParse(this.textBoxBarCodeScanningRepeats.Text, out scanningRepeats)))
            {
                if (MessageBox.Show("The value of bar code scanning repeats is invalid, use defualt value instead?", "Invalid Value", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    this.textBoxBarCodeScanningRepeats.Text = MES.Detector.ModuleConfiguration.Default_TimeredBarCodeCameraScanningTimes.ToString();

                    scanningRepeats = MES.Detector.ModuleConfiguration.Default_TimeredBarCodeCameraScanningTimes;
                }
                else
                {
                    return false;
                }
            }

            if ((String.IsNullOrEmpty(this.textBoxExposureTime.Text)) || (!int.TryParse(this.textBoxExposureTime.Text, out exposureTime_us)))
            {
                if (MessageBox.Show("The value of exposure time is invalid, use defualt value instead?", "Invalid Value", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    exposureTime_us = (int)(MES.Detector.ModuleConfiguration.Default_CameraParameters["exposure"]);
                    this.textBoxExposureTime.Text = exposureTime_us.ToString();
                }
                else
                {
                    return false;
                }
            }

            if ((String.IsNullOrEmpty(this.textBoxGain.Text)) || (!float.TryParse(this.textBoxGain.Text, out gain_db)))
            {
                if (MessageBox.Show("The value of gain is invalid, use defualt value instead?", "Invalid Value", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    gain_db = (float)(MES.Detector.ModuleConfiguration.Default_CameraParameters["gain"]);
                    this.textBoxGain.Text = gain_db.ToString();
                }
                else
                {
                    return false;
                }
            }

            if ((String.IsNullOrEmpty(this.textBoxRotationDegree.Text)) || (!int.TryParse(this.textBoxExposureTime.Text, out rotationDegree)))
            {
                if (MessageBox.Show("The value of rotation degree is invalid, use defualt value instead?", "Invalid Value", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    exposureTime_us = (int)(MES.Detector.ModuleConfiguration.Default_ImageRotationDegree);
                    this.textBoxRotationDegree.Text = rotationDegree.ToString();
                }
                else
                {
                    return false;
                }
            }

            deviceTimeout = int.Parse(this.textBoxDeviceTimeout.Text);

            scanningRepeats = int.Parse(this.textBoxDeviceTimeout.Text);

            exposureTime_us = int.Parse(this.textBoxExposureTime.Text);

            gain_db = float.Parse(this.textBoxGain.Text);

            rotationDegree = int.Parse(textBoxRotationDegree.Text);

            MES.Detector.ModuleConfiguration.Default_TimeredBarCodeCameraTimeout = deviceTimeout;

            MES.Detector.ModuleConfiguration.Default_TimeredBarCodeCameraScanningTimes = scanningRepeats;

            System.Collections.Generic.Dictionary<string, object> cameraParams = new Dictionary<string, object>()
            {
                {"exposure", exposureTime_us},
                {"gain", gain_db},
                {"imgdataformat", this.comboBoxImageFormats.SelectedIndex}
            };

            MES.Detector.ModuleConfiguration.Default_CameraParameters = cameraParams;

            MES.Acquirer.ModuleConfiguration.Default_CameraParameters = cameraParams;

            MES.Detector.ModuleConfiguration.Default_ImageRotationDegree = rotationDegree;

            return true;
        }

        private void saveSignalData(DataIdentifier identifier) 
        {
            string signalDataStore = ConfigurationManager.AppSettings.Get("DefaultSignalDataStore");

            signalDataStore = signalDataStore.EndsWith("\\") ? signalDataStore.Substring(0, (signalDataStore.Length - 1)) : signalDataStore;

            if (!Directory.Exists(signalDataStore))
            {
                Directory.CreateDirectory(signalDataStore);
            }

            string signalDataDirectory = String.Format("{0}\\{1}", signalDataStore, identifier.DataUniqueID);

            if (!Directory.Exists(signalDataDirectory))
            {
                Directory.CreateDirectory(signalDataDirectory);
            }

            string filePath = String.Format("{0}\\{1}.bmp", signalDataDirectory,  DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write)) 
            {
                byte[] rawDataByte = ((object[])(identifier.RawData))[0] as byte[];

                fileStream.Write(rawDataByte, 0, rawDataByte.Length);
            }

            this.currentSignalFile = filePath;
        }

        private void saveDataPair(DataPair pair)
        {
            string imageStore = ConfigurationManager.AppSettings.Get("DefaultImageStore"); //this.textBoxDefaultImageStore.Text;

            imageStore = imageStore.EndsWith("\\") ? imageStore.Substring(0, (imageStore.Length - 1)) : imageStore;

            string imageDirectory = String.Format("{0}\\{1}", imageStore, pair.Identifier.DataUniqueID);

            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            string filePath;

            FileStream fileStream;

            foreach (DataItem item in pair.Items)
            {
                filePath = String.Format("{0}\\{1}.bmp", imageDirectory, item.CreationTime.ToString("yyyy-MM-dd-hh-mm-ss"));

                fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write);

                fileStream.Write(pair.Items[0].DataBytes, 0, pair.Items[0].DataBytes.Length);

                fileStream.Flush();

                fileStream.Close();

                //this.acquiredImagePaths.Add(filePath);
            }
        }

        private DataPair doAcquisition(DataIdentifier identifier)
        {
            if (this.xiAcquirer == null)
            {
                this.xiAcquirer = new XIAcquirer();

                this.xiAcquirer.SetDeviceInstance(this.detector.GetDeviceInstance());
            }

            DataItem dataItem = this.xiAcquirer.Acquire();

            DataPair pair = new DataPair()
            {
                Identifier = identifier,
                Items = new List<DataItem>() { dataItem }
            };

            return pair;
        }

        private void startDetecting() 
        {
            this.detector = new TimeredBarCodeCameraDetector();
            this.detector.NotifyCallBack += detector_CallBack;
            this.detector.Detect();
        }

        void detector_CallBack(DataIdentifier Identifier)
        {
            this.saveSignalData(Identifier);

            this.saveDataPair(this.doAcquisition(Identifier));

            //Thread thread = new Thread(this.threadProcSafe);

            //thread.Start(Identifier);

            this.Invoke(new Action(() => 
            { 
                this.SetControls(Identifier);

                //this.toolStripProgressBarCurrentProgress.Value = 80;
                //this.toolStripProgressBarCurrentProgress.PerformStep();

                this.toolStripStatusLabelCurrentStatus.Text = this.currentSignalFile;

                //this.toolStripProgressBarCurrentProgress.Value = 0;
                //this.toolStripProgressBarCurrentProgress.PerformStep();
            }));
        }

        // This method is executed on the worker thread and makes 
        // a thread-safe call on the controls. 
        private void threadProcSafe(object identifier)
        {
            this.SetControls(identifier as DataIdentifier);
        }

        private void setControls(bool isDoingTasks) 
        {
            this.startToolStripMenuItem.Enabled = !isDoingTasks;
            this.stopToolStripMenuItem.Enabled = isDoingTasks;
            this.toolStripButtonStart.Enabled = !isDoingTasks;
            this.toolStripButtonStop.Enabled = isDoingTasks;
            this.toolStripButtonTest.Enabled = !isDoingTasks;
            this.toolStripMenuItemTest.Enabled = !isDoingTasks;

            this.xiViewerToolStripMenuItem.Enabled = !isDoingTasks;
            this.frequencyDetectorToolStripMenuItem.Enabled = !isDoingTasks;
            this.barCodeReaderToolStripMenuItem.Enabled = !isDoingTasks;
            this.toolStripButtonxiViewer.Enabled = !isDoingTasks;
            this.toolStripButtonFrequencyDetector.Enabled = !isDoingTasks;
            this.toolStripButtonBarCodeReader.Enabled = !isDoingTasks;

            this.toolStripProgressBarCurrentProgress.Visible = isDoingTasks;
            this.toolStripStatusLabelCurrentStatus.Visible = isDoingTasks;
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


        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!this.processUserInputs(false))
            {
                return;
            }

            this.setControls(true);

            //this.toolStripProgressBarCurrentProgress.Value = 0;
            //this.toolStripProgressBarCurrentProgress.PerformStep();

            //this.detectingThread = new Thread(new ThreadStart(this.startDetecting));
            //this.detectingThread.Start();

            Action action = new Action(() => { this.startDetecting(); });

            Task.Run(action);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.detectingThread != null)
            //    {
            //        this.detectingThread.Abort();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            if (this.detector != null)
            {
                this.detector.Stop();
            }

            this.setControls(false);
        }

        private void xiViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string xiHome = Microsoft.Win32.Registry.LocalMachine.GetValue(@"SOFTWARE\XIMEA\API_SoftwarePackage\Path").ToString();

            //if (!String.IsNullOrEmpty(xiHome)) 
            //{
            //    string xiPath = xiHome + @"\Examples\Bin\xiApiViewer.exe";

            //    try
            //    {
            //        System.Diagnostics.Process.Start(xiPath);
            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
            //}

            string externalAppPath = ConfigurationManager.AppSettings.Get("DefaultExternalAppPath");

            System.Diagnostics.Process.Start(externalAppPath);
        }

        private void barCodeReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BarcodeReaderSample.frmBarcodeReaderSample formBarCodeReaderSample = new BarcodeReaderSample.frmBarcodeReaderSample();

            formBarCodeReaderSample.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.detector != null)
            {
                this.detector.Stop();
            }

            this.Close();
        }

        private void toolStripMenuItemTest_Click(object sender, EventArgs e)
        {
            if (!this.processUserInputs(true))
            {
                return;
            }

            this.setControls(true);

            //this.toolStripProgressBarCurrentProgress.Value = 0;
            //this.toolStripProgressBarCurrentProgress.PerformStep();

            Action action = new Action(() => 
            {
                this.detector = new TimeredBarCodeCameraDetector();

                object[] barcodes = null;

                byte[] result = this.detector.Test(out barcodes);

                using (MemoryStream stream = new MemoryStream(result))
                {
                    this.bitmap = Bitmap.FromStream(stream) as Bitmap;
                }

                this.Invoke(new Action( () =>
                {
                    //this.toolStripProgressBarCurrentProgress.Value = 80;
                    //this.toolStripProgressBarCurrentProgress.PerformStep();

                    this.listBoxBarCodesExtracted.DataSource = barcodes;
                    this.listBoxBarCodesExtracted.Refresh();

                    this.pictureBoxSignalPreview.Image = this.bitmap;
                    this.pictureBoxSignalPreview.Refresh();

                    this.setControls(false);
                }));
            });

            //Task task = new Task(action);
            //task.Start();

            Task.Run(action);
        }
    }
}
