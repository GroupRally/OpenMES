using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using MES.Core;
using MES.Core.Parameter;
using MES.Acquirer;
using MES.Communication;
using MES.Utility;
using MES.Persistency;
using MES.Processor;
using MES.Correlator;

namespace AcquisitionConsole
{
    public partial class FormMain : Form
    {
        private CommunicationManager communicationManager;

        private PersistencyManager persistencyManager;

        private AcquisitionParameterSet parameters;

        private XIAcquirer xiAcquirer;

        private Processor processor;

        private Correlator correlator;

        private Bitmap bitmap;

        private string currentImageFile = "";

        private BindingSource bindingSource = null;

        private Dictionary<string, string> imageUrlMappings = new Dictionary<string,string>();

        private void bindParameterProperties()
        {
            this.parameters = new AcquisitionParameterSet(true);

            this.comboBoxParameters.DataSource = this.parameters;

            this.comboBoxParameters.SelectedIndex = 1;
        }

        private void bindParameterProperties(AcquisitionParameterSet paramSet)
        {
            this.parameters = paramSet;

            this.comboBoxParameters.DataSource = this.parameters;

            this.comboBoxParameters.SelectedIndex = 1;
        }

        private void setControls(bool isDoingTasks)
        {
            this.startToolStripMenuItem.Enabled = !isDoingTasks;
            this.stopToolStripMenuItem.Enabled = isDoingTasks;
            this.toolStripButtonStart.Enabled = !isDoingTasks;
            this.toolStripButtonStop.Enabled = isDoingTasks;
            this.toolStripButtonTest.Enabled = !isDoingTasks;
            this.testToolStripMenuItem.Enabled = !isDoingTasks;
            this.newToolStripButton.Enabled = !isDoingTasks;
            this.openToolStripButton.Enabled = !isDoingTasks;
            this.toolStripMenuItemNew.Enabled = !isDoingTasks;
            this.toolStripMenuItemOpen.Enabled = !isDoingTasks;

            this.xiViewerToolStripMenuItem.Enabled = !isDoingTasks;
            this.frequencyDetectorToolStripMenuItem.Enabled = !isDoingTasks;
            this.barcodeReaderToolStripMenuItem.Enabled = !isDoingTasks;
            this.xiViewerToolStripButton.Enabled = !isDoingTasks;
            this.frequencyDetectorToolStripButton.Enabled = !isDoingTasks;
            this.barcodeReaderToolStripButton.Enabled = !isDoingTasks;

            this.toolStripProgressBarCurrentProgress.Visible = isDoingTasks;
            //this.toolStripStatusLabelCurrentStatus.Visible = isDoingTasks;
        }

        private void applyParameters()
        {
            NetworkParameter networkParameter = this.parameters.GetNetworkParameter();
            CameraParameter cameraParameter = this.parameters.GetCameraParameter();
            BarcodeImagingParameter barcodeImagingParameter = this.parameters.GetBarcodeImagingParameter();
            PersistencyParameter persistencyParameter = this.parameters.GetPersistencyParameter();

            MES.Processor.ModuleConfiguration.Default_BarCodeScanningTimes = barcodeImagingParameter.ScanTimes;
            MES.Processor.ModuleConfiguration.Default_FailureRetryTimes = barcodeImagingParameter.FailureRetryTimes;
            MES.Processor.ModuleConfiguration.IsExtractingBarCodes = barcodeImagingParameter.IsExtractingBarCode;
            MES.Processor.ModuleConfiguration.IsCheckingSkewAngle = barcodeImagingParameter.IsCheckingSkewAngle;
            MES.Processor.ModuleConfiguration.Default_ImageRotationDegree = barcodeImagingParameter.ImageRotationDegree;
            MES.Processor.ModuleConfiguration.Default_ImageRotationParameters = new Dictionary<string, object>() 
            {
                {"RotationAtPercentageOfImageWidth", barcodeImagingParameter.RotationAtPercentageOfImageWidth},
                {"RotationAtPercentageOfImageHeight", barcodeImagingParameter.RotationAtPercentageOfImageHeight},
                {"IsNoClip", barcodeImagingParameter.IsNoClip}
            };

            MES.Acquirer.ModuleConfiguration.Default_Timeout = cameraParameter.DeviceTimeout;
            MES.Acquirer.ModuleConfiguration.Default_CameraParameters = new Dictionary<string, object>()
            {
                {"exposure", cameraParameter.Exposure},
                {"gain", cameraParameter.Gain},
                {"imgdataformat", cameraParameter.ImageDataFormat},
                {"downsampling_type", cameraParameter.DownSamplingType},
                {"downsampling", cameraParameter.DownSampling},
                {"height", cameraParameter.OutputImageHeight},
                {"width", cameraParameter.OuputImageWidth},
                {"sharpness", cameraParameter.Sharpness},
                {"gammaY", cameraParameter.GammaY},
                {"gammaC", cameraParameter.GammaC},
                {"exp_priority", cameraParameter.ExposurePriority},
                {"bpc", cameraParameter.EnableBPC ? 1 : 0},
                {"auto_bandwidth_calculation", cameraParameter.EnableAutoBandWidthCalculation ? 1 : 0},
                {"buffer_policy", cameraParameter.BufferPolicy},
                //{"limit_bandwidth", cameraParameter.LimitBandWidth}
                {"aeag", cameraParameter.EnableAEAG ? 1 : 0},
                {"offsetX", cameraParameter.OuputImageOffsetX},
                {"offsetY", cameraParameter.OutputImageOffsetY}
            };

            MES.Acquirer.ModuleConfiguration.Default_ImageOuputFormat = cameraParameter.ImageOutputFormat;
        }

        private void updateCammeraParameters() 
        {
            CameraParameter cameraParameter = this.parameters.GetCameraParameter();

            if (this.xiAcquirer == null)
            {
                this.xiAcquirer = new XIAcquirer();
            }

            this.xiAcquirer.Open();

            object[] parameters = this.xiAcquirer.Get();

            this.xiAcquirer.Close();

            if ((parameters != null) && (parameters.Length > 0))
            {
                CameraParameter newCammeraParam = parameters[0] as CameraParameter;

                if (newCammeraParam != null)
                {
                    cameraParameter.DeviceInstancePath = newCammeraParam.DeviceInstancePath;
                    cameraParameter.DeviceName = newCammeraParam.DeviceName;
                    cameraParameter.DeviceSN = newCammeraParam.DeviceSN;
                    cameraParameter.DeviceType = newCammeraParam.DeviceType;
                    cameraParameter.FrameRate = newCammeraParam.FrameRate;
                    cameraParameter.AvailableBandWidth = newCammeraParam.AvailableBandWidth;
                    cameraParameter.OuputImageOffsetX = newCammeraParam.OuputImageOffsetX;
                    cameraParameter.OutputImageOffsetY = newCammeraParam.OutputImageOffsetY;

                    this.propertyGridParameters.Refresh();
                }
            }
        }

        private void saveDataPair(DataPair pair)
        {
            if (this.persistencyManager == null)
            {
                this.persistencyManager = new PersistencyManager();
            }

            List<string> result = this.persistencyManager.Persist
            (
                pair, 
                ((int)(this.parameters.GetPersistencyParameter().PersistencyType)), 
                new Dictionary<string, object>() 
                { 
                    {"DataStore", this.parameters.GetPersistencyParameter().DataStore}, 
                    {"FileExtension", this.parameters.GetCameraParameter().ImageOutputFormat.ToString()} 
                }
            ) as List<string>;

            if (this.parameters.GetPersistencyParameter().PersistencyType == PersistencyType.FileSystem)
            {
                this.currentImageFile = result[1];

                this.imageUrlMappings.Add(this.currentImageFile, this.currentImageFile + ".info.xml");
            }
            else
            {
                string servicePoint = ConfigurationManager.AppSettings.Get("ServicePoint");
                string imageServiceUrl = ConfigurationManager.AppSettings.Get("ImageServiceUrl");
                string imageInfoUrl = ConfigurationManager.AppSettings.Get("ImageInfoUrl");

                imageServiceUrl = String.Format(imageServiceUrl, result[1]);
                imageInfoUrl = String.Format(imageInfoUrl, result[1]);

                if (!servicePoint.EndsWith("/"))
                {
                    servicePoint = servicePoint + "/";
                }

                if (imageServiceUrl.StartsWith("/"))
                {
                    imageServiceUrl = imageServiceUrl.Remove(0, 1);
                }

                if (imageInfoUrl.StartsWith("/"))
                {
                    imageInfoUrl = imageInfoUrl.Remove(0, 1);
                }

                this.currentImageFile = servicePoint + imageServiceUrl;

                this.imageUrlMappings.Add(this.currentImageFile, servicePoint + imageInfoUrl);
            }
        }

        private void starExternalImageViewer(string imageUrl) 
        {
            string photoViewerPath = ConfigurationManager.AppSettings.Get("DefaultExternalImageViewer"); //@"C:\Program Files\Windows Photo Viewer\PhotoViewer.dll";

            if (String.IsNullOrEmpty(photoViewerPath) || !File.Exists(photoViewerPath))
            {
                photoViewerPath = @"C:\Windows\system32\shimgvw.dll";
            }

            string arguments = String.Format("\"{0}\", ImageView_Fullscreen {1}", photoViewerPath, imageUrl);

            System.Diagnostics.ProcessStartInfo processStarInfo = new System.Diagnostics.ProcessStartInfo();

            processStarInfo.FileName = "rundll32.exe";
            processStarInfo.Arguments = arguments;
            processStarInfo.UseShellExecute = true;

            System.Diagnostics.Process.Start(processStarInfo);

            if (this.imageUrlMappings.ContainsKey(imageUrl))
            {
                System.Diagnostics.Process.Start("iexplore.exe", this.imageUrlMappings[imageUrl]);
            }
        }

        private void startListener()
        {
            NetworkParameter networkParameter = this.parameters.GetNetworkParameter();

            this.communicationManager = new CommunicationManager()
            {
                LocalAddress = networkParameter.LocalAddress, //ConfigurationManager.AppSettings.Get("LocalAddress"),
                RemoteAddress = networkParameter.RemoteAddress, //networkParameter.MulticastAddress, //ConfigurationManager.AppSettings.Get("RemoteAddress"),
                LocalPort = networkParameter.LocalPort, //int.Parse(ConfigurationManager.AppSettings.Get("LocalPort")),
                RemotePort = networkParameter.RemotePort, //networkParameter.MulticastPort, //int.Parse(ConfigurationManager.AppSettings.Get("RemotePort")),
                TimeToLive = networkParameter.TimeToLive, //int.Parse(ConfigurationManager.AppSettings.Get("TimeToLive"))
                PipeName = networkParameter.PipeName
            };

            //this.communicationManager.Start();

            this.communicationManager.Start((int)(networkParameter.CommunicationType));

            byte[] bytesReceived = null;

            int bytesCount = -1;

            while (true)
            {
                bytesReceived = this.communicationManager.Receive(out bytesCount);

                if ((bytesCount > 0) && (bytesReceived != null) && (bytesReceived.Length > 0))
                {
                    DataIdentifier identifier = CommonUtility.BinaryDeserialize(bytesReceived) as DataIdentifier; //JsonUtility.JsonDeserialize(bytesReceived, typeof(DataIdentifier), null, null) as DataIdentifier;

                    DataPair pair = this.doAcquisition(identifier);

                    pair = this.doComputation(pair);

                    pair = this.doCorrelation(pair.Identifier, pair.Items[0]);

                    this.saveDataPair(pair);

                    this.Invoke(new Action(() => 
                    {
                        using (MemoryStream stream = new MemoryStream(pair.Items[1].DataBytes))
                        {
                            this.bitmap = Bitmap.FromStream(stream) as Bitmap;
                            this.pictureBoxAcquiredImage.Image = this.bitmap;
                            this.pictureBoxAcquiredImage.Refresh();
                        }

                        if ((pair.Items[0].DataParameters != null) && (pair.Items[1].DataParameters.Count > 0))
                        {
                            //this.listBoxBarcodes.DataSource = pair.Items[0].DataParameters[0].Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                            string barcodes = pair.Items[1].GetParameterValue("BarCodes");

                            this.listBoxBarcodes.DataSource = !String.IsNullOrEmpty(barcodes) ? barcodes.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries) : null;
                        }
                        else
                        {
                            this.listBoxBarcodes.DataSource = null;
                        }
                        
                        this.listBoxBarcodes.Refresh();

                        this.tabControlMain.SelectedTab = this.tabPageBarcodes;

                        this.toolStripStatusLabelCurrentStatus.Text = this.currentImageFile;

                        if (this.bindingSource.Count == 20)
                        {
                            string imageUrlMappingKey = this.bindingSource[0].ToString();

                            if (this.imageUrlMappings.ContainsKey(imageUrlMappingKey))
                            {
                                this.imageUrlMappings.Remove(imageUrlMappingKey);
                            } 

                            this.bindingSource.RemoveAt(0);
                        }

                        this.bindingSource.Add(new DataParameter() { Name = this.currentImageFile, Value = this.currentImageFile });

                        this.dataGridViewRecentImages.Refresh();
                    }));
                }
            }
        }

        private void startAcquisition()
        {
            if (this.xiAcquirer == null)
            {
                this.xiAcquirer = new XIAcquirer();
            }

            this.xiAcquirer.Open();

            this.xiAcquirer.Start();
        }

        private DataPair doAcquisition(DataIdentifier identifier)
        {
            DataItem dataItem = this.xiAcquirer.Acquire();

            return this.doCorrelation(identifier, dataItem);
        }

        private DataPair doCorrelation(DataIdentifier identifier, DataItem item) 
        {
            if (this.correlator == null)
            {
                this.correlator = new Correlator();
            }

            return this.correlator.Correlate(identifier, item);
        }

        private DataPair doComputation(DataPair pair) 
        {
            if (this.processor == null)
            {
                this.processor = new Processor();
            }

            return this.processor.Compute(pair);
        }

        private void stopAcquisition()
        {
            if (this.xiAcquirer != null)
            {
                this.xiAcquirer.Stop();
                this.xiAcquirer.Close();
            }
        }

        private void saveParameters(string filePath) 
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    XmlUtility xmlUtility = new XmlUtility();

                    string parameterXml = xmlUtility.XmlSerialize(this.parameters, new Type[] { typeof(NetworkParameter), typeof(CameraParameter), typeof(BarcodeImagingParameter), typeof(PersistencyParameter), typeof(BusinessParameter), typeof(PersistencyType), typeof(CommunicationType), typeof(List<ParameterBase>), typeof(ParameterBase) }, "utf-8");

                    if ((parameterXml.Length - parameterXml.LastIndexOf(">")) > 1)
                    {
                        parameterXml = parameterXml.Remove(parameterXml.LastIndexOf(">") + 1);
                    }

                    if (parameterXml.StartsWith("<?xml version=\"1.0\"?>"))
                    {
                        parameterXml = parameterXml.Insert(parameterXml.IndexOf("?>"), " encoding=\"utf-8\"");
                    }

                    writer.Write(parameterXml);
                }
            }
        }

        private AcquisitionParameterSet loadParameters(string filePath) 
        {
            string parameterXml = "";

            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    parameterXml = reader.ReadToEnd();
                }
            }

            XmlUtility xmlUtility = new XmlUtility();

            AcquisitionParameterSet parameters = xmlUtility.XmlDeserialize(parameterXml, typeof(AcquisitionParameterSet), new Type[] { typeof(NetworkParameter), typeof(CameraParameter), typeof(BarcodeImagingParameter), typeof(PersistencyParameter), typeof(BusinessParameter), typeof(PersistencyType), typeof(CommunicationType), typeof(List<ParameterBase>), typeof(ParameterBase) }, "utf-8") as AcquisitionParameterSet;

            return parameters;
        }

        public FormMain()
        {
            InitializeComponent();

            AcquisitionParameterSet paras = null;

            string defaultParamFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MES"; //AppDomain.CurrentDomain.BaseDirectory + "\\parameters.xml";

            if (!Directory.Exists(defaultParamFilePath))
            {
                Directory.CreateDirectory(defaultParamFilePath);
            }

            defaultParamFilePath += "\\mes-parameters-acquisition.xml";

            if (File.Exists(defaultParamFilePath))
            {
                paras = this.loadParameters(defaultParamFilePath);  
            }

            if (paras != null)
            {
                this.bindParameterProperties(paras);
            }
            else
            {
                this.bindParameterProperties();
            }

            this.dataGridViewRecentImages.AutoGenerateColumns = false;

            this.bindingSource = new BindingSource();

            this.dataGridViewRecentImages.DataSource = this.bindingSource;
        }

        private void comboBoxParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.propertyGridParameters.SelectedObject = this.parameters[this.comboBoxParameters.SelectedIndex];
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.saveParameters("parameters.xml");

            this.Close();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.setControls(true);

            this.applyParameters();

            this.updateCammeraParameters();

            Action action = new Action(() => 
            {
                this.xiAcquirer = new XIAcquirer();

                object[] testOutput = null;

                byte[] result = this.xiAcquirer.Test(out testOutput);

                DataPair pair = testOutput[0] as DataPair;

                pair = this.doCorrelation(pair.Identifier, pair.Items[0]);

                pair = this.doComputation(pair);

                pair = this.doCorrelation(pair.Identifier, pair.Items[0]);

                using (MemoryStream stream = new MemoryStream(pair.Items[1].DataBytes))
                {
                    this.bitmap = Bitmap.FromStream(stream) as Bitmap;
                }

                if (this.persistencyManager == null)
                {
                    this.persistencyManager = new PersistencyManager();
                }

                List<string> persistencyResult = this.persistencyManager.Persist
                (
                    pair,
                    ((int)(this.parameters.GetPersistencyParameter().PersistencyType)),
                    new Dictionary<string, object>() 
                { 
                    {"DataStore", this.parameters.GetPersistencyParameter().DataStore}, 
                    {"FileExtension", this.parameters.GetCameraParameter().ImageOutputFormat.ToString()} 
                }
                ) as List<string>;

                if (this.parameters.GetPersistencyParameter().PersistencyType == PersistencyType.FileSystem)
                {
                    this.currentImageFile = persistencyResult[1];

                    this.imageUrlMappings.Add(this.currentImageFile, this.currentImageFile + ".info.xml");
                }
                else
                {
                    string servicePoint = ConfigurationManager.AppSettings.Get("ServicePoint");
                    string imageServiceUrl = ConfigurationManager.AppSettings.Get("ImageServiceUrl");
                    string imageInfoUrl = ConfigurationManager.AppSettings.Get("ImageInfoUrl");

                    imageServiceUrl = String.Format(imageServiceUrl, persistencyResult[1]);
                    imageInfoUrl = String.Format(imageInfoUrl, persistencyResult[1]);

                    if (!servicePoint.EndsWith("/"))
                    {
                        servicePoint = servicePoint + "/";
                    }

                    if (imageServiceUrl.StartsWith("/"))
                    {
                        imageServiceUrl = imageServiceUrl.Remove(0, 1);
                    }

                    if (imageInfoUrl.StartsWith("/"))
                    {
                        imageInfoUrl = imageInfoUrl.Remove(0, 1);
                    }

                    this.currentImageFile = servicePoint + imageServiceUrl;

                    this.imageUrlMappings.Add(this.currentImageFile, servicePoint + imageInfoUrl);
                }

                this.Invoke(new Action(() => 
                {
                    this.pictureBoxAcquiredImage.Image = this.bitmap;
                    this.listBoxBarcodes.DataSource = pair.Items[1].GetParameterValue("BarCodes").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries); //barcodes;

                    this.pictureBoxAcquiredImage.Refresh();
                    this.listBoxBarcodes.Refresh();

                    this.tabControlMain.SelectedTab = this.tabPageBarcodes;

                    this.toolStripStatusLabelCurrentStatus.Text = this.currentImageFile;

                    if (this.bindingSource.Count == 20)
                    {
                        string imageUrlMappingKey = this.bindingSource[0].ToString();

                        if (this.imageUrlMappings.ContainsKey(imageUrlMappingKey)) 
                        {
                            this.imageUrlMappings.Remove(imageUrlMappingKey);
                        } 

                        this.bindingSource.RemoveAt(0);
                    }

                    this.bindingSource.Add(new DataParameter() { Name = this.currentImageFile, Value = this.currentImageFile });

                    this.dataGridViewRecentImages.Refresh();

                    this.setControls(false);
                }));
            });

            Task.Run(action);
        }

        private void xiViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string externalAppPath = ConfigurationManager.AppSettings.Get("DefaultExternalAppPath");

            System.Diagnostics.Process.Start(externalAppPath);
        }

        private void toolStripMenuItemNew_Click(object sender, EventArgs e)
        {
            this.bindParameterProperties();

            this.pictureBoxAcquiredImage.Image = null;
            this.listBoxBarcodes.DataSource = null;

            this.pictureBoxAcquiredImage.Refresh();
            this.listBoxBarcodes.Refresh();

            this.tabControlMain.SelectedTab = this.tabPageParameters;
            this.propertyGridParameters.Refresh();
        }

        private void toolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialogSaveParameters.ShowDialog(this) == DialogResult.OK)
            {
                //using (FileStream stream = new FileStream(this.saveFileDialogSaveParameters.FileName, FileMode.Create, FileAccess.Write, FileShare.Write))
                //{
                //    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                //    {
                //        XmlUtility xmlUtility = new XmlUtility();

                //        string parameterXml = xmlUtility.XmlSerialize(this.parameters, new Type[] { typeof(NetworkParameter), typeof(CameraParameter), typeof(BarcodeImagingParameter), typeof(PersistencyParameter), typeof(PersistencyType), typeof(CommunicationType), typeof(List<ParameterBase>), typeof(ParameterBase) }, "utf-8");

                //        if ((parameterXml.Length - parameterXml.LastIndexOf(">")) > 1)
                //        {
                //            parameterXml = parameterXml.Remove(parameterXml.LastIndexOf(">") + 1);
                //        }

                //        if (parameterXml.StartsWith("<?xml version=\"1.0\"?>"))
                //        {
                //            parameterXml = parameterXml.Insert(parameterXml.IndexOf("?>"), " encoding=\"utf-8\"");
                //        }

                //        writer.Write(parameterXml);
                //    }
                //}

                this.saveParameters(this.saveFileDialogSaveParameters.FileName);

                MessageBox.Show(String.Format("Parameter successfully saved to: {0}.", this.saveFileDialogSaveParameters.FileName), "Operation Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            if (this.openFileDialogOpenParameters.ShowDialog(this) == DialogResult.OK)
            {
                //string parameterXml = "";

                //using (FileStream stream = new FileStream(this.openFileDialogOpenParameters.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                //{
                //    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                //    {
                //        parameterXml = reader.ReadToEnd();
                //    }
                //}

                //XmlUtility xmlUtility = new XmlUtility();

                //AcquisitionParameterSet parameters = xmlUtility.XmlDeserialize(parameterXml, typeof(AcquisitionParameterSet), new Type[] { typeof(NetworkParameter), typeof(CameraParameter), typeof(BarcodeImagingParameter), typeof(PersistencyParameter), typeof(PersistencyType), typeof(CommunicationType), typeof(List<ParameterBase>), typeof(ParameterBase)}, "utf-8") as AcquisitionParameterSet;

                AcquisitionParameterSet parameters = this.loadParameters(this.openFileDialogOpenParameters.FileName);

                this.bindParameterProperties(parameters);

                this.pictureBoxAcquiredImage.Image = null;
                this.listBoxBarcodes.DataSource = null;

                this.pictureBoxAcquiredImage.Refresh();
                this.listBoxBarcodes.Refresh();

                this.tabControlMain.SelectedTab = this.tabPageParameters;
                this.propertyGridParameters.Refresh();
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.setControls(true);

            this.applyParameters();

            this.updateCammeraParameters();

            this.startAcquisition();

            Task.Run(new Action(() => 
            {
                this.startListener();
            }));
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Task.Run(new Action(()=>
            //{
                this.stopAcquisition();

                if (this.communicationManager != null)
                {
                    this.communicationManager.Close();
                }
           // }));

            this.setControls(false);
        }

        private void pictureBoxAcquiredImage_DoubleClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.currentImageFile))
            {
                this.starExternalImageViewer(this.currentImageFile);
            }
        }

        private void dataGridViewRecentImages_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                string imageUrl = this.dataGridViewRecentImages.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (!String.IsNullOrEmpty(imageUrl))
                {
                    this.starExternalImageViewer(imageUrl);
                }
            }
        }

        private void barcodeReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BarcodeReaderSample.frmBarcodeReaderSample formBarcodeReaderSample = new BarcodeReaderSample.frmBarcodeReaderSample();

            formBarcodeReaderSample.Show();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string defaultParamFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MES"; //AppDomain.CurrentDomain.BaseDirectory + "\\parameters.xml";

            if (!Directory.Exists(defaultParamFilePath))
            {
                Directory.CreateDirectory(defaultParamFilePath);
            }

            defaultParamFilePath += "\\mes-parameters-acquisition.xml";

            this.saveParameters(defaultParamFilePath);
        }
    }
}
