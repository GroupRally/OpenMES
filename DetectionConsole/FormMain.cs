using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MES.Core;
using MES.Core.Parameter;
using MES.Detector;
using MES.Utility;
using MES.Persistency;

namespace DetectionConsole
{
    public partial class FormMain : Form
    {
        private SignalParameterSet parameters;
        private TimerDetector timerDetector;
        private SerialPortSignalDetector serailPortSignalDetector;

        private PersistencyManager persistencyManager;

        private List<DataIdentifier> dataIdentifiers;

        private void bindParameterProperties() 
        {
            this.parameters = new SignalParameterSet(true);

            this.comboBoxParameters.DataSource = this.parameters;

            this.comboBoxParameters.SelectedIndex = 1;

            this.comboBoxParameters.Refresh();
        }

        private void bindParameterProperties(SignalParameterSet paramSet)
        {
            this.parameters = paramSet;

            this.comboBoxParameters.DataSource = this.parameters;

            this.comboBoxParameters.SelectedIndex = 1;

            this.comboBoxParameters.Refresh();
        }

        private void setControls(bool isDoingTasks)
        {
            this.startToolStripMenuItem.Enabled = !isDoingTasks;
            this.startToolStripButton.Enabled = !isDoingTasks;

            this.stopToolStripMenuItem.Enabled = isDoingTasks;
            this.stopToolStripButton.Enabled = isDoingTasks;

            this.testToolStripButton.Enabled = !isDoingTasks;
            this.testToolStripMenuItem.Enabled = !isDoingTasks;

            this.newToolStripButton.Enabled = !isDoingTasks;
            this.newToolStripMenuItem.Enabled = !isDoingTasks;

            this.openToolStripButton.Enabled = !isDoingTasks;
            this.openToolStripMenuItem.Enabled = !isDoingTasks;

            this.toolStripComboBoxSignalSource.Enabled = !isDoingTasks;

            this.toolStripProgressBarCurrentProgress.Visible = isDoingTasks;
            this.toolStripStatusLabelCurrentStatus.Visible = isDoingTasks;
        }

        private void applyParameters()
        {
            NetworkParameter networkParameter = this.parameters.GetNetworkParameter();
            SerialPortParameter serialPortParameter = this.parameters.GetSerialPortParameter();
            TimerParameter timerParameter = this.parameters.GetTimerParameter();
            PersistencyParameter persistencyParameter = this.parameters.GetPersistencyParameter();

            ModuleConfiguration.Default_LocalAddress = networkParameter.LocalAddress;
            ModuleConfiguration.Default_LocalPort = networkParameter.LocalPort;
            ModuleConfiguration.Default_MulticastAddress = networkParameter.RemoteAddress; //networkParameter.MulticastAddress;
            ModuleConfiguration.Default_MulticastPort = networkParameter.RemotePort; //networkParameter.MulticastPort;
            ModuleConfiguration.Default_TimeToLive = networkParameter.TimeToLive;
            ModuleConfiguration.Default_CommunicationType = networkParameter.CommunicationType;
            ModuleConfiguration.Default_PipeName = networkParameter.PipeName;

            ModuleConfiguration.Default_TimerInterval = timerParameter.TimerInterval;

            ModuleConfiguration.Default_SerialPortBaudRate = serialPortParameter.SerialPortBaudRate;
            ModuleConfiguration.Default_SerialPortDataBits = serialPortParameter.SerialPortDataBits;
            ModuleConfiguration.Default_SerialPortEncodingCodePage = serialPortParameter.SerialPortEncodingCodePage;
            ModuleConfiguration.Default_SerialPortHandShake = serialPortParameter.SerialPortHandShake;
            ModuleConfiguration.Default_SerialPortName = serialPortParameter.SerialPortName;
            ModuleConfiguration.Default_SerialPortParity = serialPortParameter.SerialPortParity;
            ModuleConfiguration.Default_SerialPortReadBufferSize = serialPortParameter.SerialPortReadBufferSize;
            ModuleConfiguration.Default_SerialPortReadTimeout = serialPortParameter.SerialPortReadTimeout;
            ModuleConfiguration.Default_SerialPortRtsEnable = serialPortParameter.SerialPortRtsEnable;
            ModuleConfiguration.Default_SerialPortStopBits = serialPortParameter.SerialPortStopBits;
            ModuleConfiguration.Default_SerialPortWriteBufferSize = serialPortParameter.SerialPortWriteBufferSize;
            ModuleConfiguration.Default_SerialPortWriteTimeout = serialPortParameter.SerialPortWriteTimeout;
            ModuleConfiguration.Default_SerialPortDtrEnable = serialPortParameter.SerialPortDtrEnable;
            ModuleConfiguration.Default_SerialPortMode = serialPortParameter.SerialPortMode;
            ModuleConfiguration.Default_SerialPortExpectedDataValue = serialPortParameter.SerialPortExpectedDataValue;

            ModuleConfiguration.Default_SequenceMask = serialPortParameter.SequenceMask;
            ModuleConfiguration.Default_SequenceSeparator = serialPortParameter.SequenceSeparator;

            ModuleConfiguration.Default_SerialPortIsReadingByLine = serialPortParameter.IsReadingByLine;
            ModuleConfiguration.Default_SerialPortIsBroadcasting = serialPortParameter.IsBroadcasting;
        }

        private void startTimer() 
        {
            this.timerDetector = new TimerDetector();

            this.timerDetector.TimerCallBack += (object sender, TimerDetectorEventArgs e) => 
            {
                this.Invoke(new Action(() => 
                {
                    this.textBoxSignalMessage.Text += e.DataIdentifier.DataUniqueID;
                    this.textBoxSignalMessage.Text += " : ";
                    this.textBoxSignalMessage.Text += ((object[])(e.DataIdentifier.RawData))[0].ToString();
                    this.textBoxSignalMessage.Text += "\r\n";

                    this.textBoxSignalMessage.SelectionStart = this.textBoxSignalMessage.Text.Length;
                    this.textBoxSignalMessage.ScrollToCaret();
                    this.textBoxSignalMessage.Refresh();
                }));
            };

            this.timerDetector.Detect();
        }

        private void testTimer()
        {
            this.timerDetector = new TimerDetector();

            this.timerDetector.TimerCallBack += (object sender, TimerDetectorEventArgs e) =>
            {
                this.Invoke(new Action(() =>
                {
                    this.textBoxSignalMessage.Text += e.DataIdentifier.DataUniqueID;
                    this.textBoxSignalMessage.Text += " : ";
                    this.textBoxSignalMessage.Text += ((object[])(e.DataIdentifier.RawData))[0].ToString();
                    this.textBoxSignalMessage.Text += "\r\n";

                    this.textBoxSignalMessage.SelectionStart = this.textBoxSignalMessage.Text.Length;
                    this.textBoxSignalMessage.ScrollToCaret();
                    this.textBoxSignalMessage.Refresh();
                }));
            };

            object[] testOutput = null;

            this.timerDetector.Test(out testOutput);
        }

        private void stopTimer() 
        {
            if (this.timerDetector != null)
            {
                this.timerDetector.Stop();
            }
        }

        private void startSerialPortDetector() 
        {
            this.serailPortSignalDetector = new SerialPortSignalDetector();

            this.serailPortSignalDetector.SerialPortSignalCallBack += (object sender, SerialPortSignalDetectorEventArgs e) => 
            {
                this.Invoke(new Action(() => 
                {
                    if (this.persistencyManager == null)
                    {
                        this.persistencyManager = new PersistencyManager();
                    }

                    this.persistencyManager.Record
                    (
                        e.DataIdentifier.DataUniqueID, 
                        ((object[])(e.DataIdentifier.RawData))[0].ToString().Trim(),
                        ((int)(this.parameters.GetPersistencyParameter().PersistencyType)),
                        new Dictionary<string, object>() 
                        { 
                            {"DataStore", this.parameters.GetPersistencyParameter().DataStore},
                            {"SeriesID", this.parameters.GetBusinessParameter().SeriesID},
                            {"SeriesName", this.parameters.GetBusinessParameter().SeriesName},
                            {"ModelID", this.parameters.GetBusinessParameter().ModelID},
                            {"ModelName", this.parameters.GetBusinessParameter().ModelName},
                            {"DeviceID", this.parameters.GetBusinessParameter().DeviceID},
                            {"DeviceName", this.parameters.GetBusinessParameter().DeviceName},
                            {"OperatorID", this.parameters.GetBusinessParameter().OperatorID},
                            {"OperatorName", this.parameters.GetBusinessParameter().OperatorName},
                            {"StationID", this.parameters.GetBusinessParameter().StationID},
                            {"StationName", this.parameters.GetBusinessParameter().StationName},
                            {"LineID", this.parameters.GetBusinessParameter().LineID},
                            {"LineName", this.parameters.GetBusinessParameter().LineName},
                            {"BusinessID", this.parameters.GetBusinessParameter().BusinessID},
                            {"BusinessName", this.parameters.GetBusinessParameter().BusinessName},
                            {"OrderID", this.parameters.GetBusinessParameter().OrderID}
                        }
                    );

                    this.textBoxSignalMessage.Text += e.DataIdentifier.DataUniqueID;
                    this.textBoxSignalMessage.Text += " : ";
                    this.textBoxSignalMessage.Text += ((object[])(e.DataIdentifier.RawData))[0].ToString().Trim();
                    this.textBoxSignalMessage.Text += " : ";
                    this.textBoxSignalMessage.Text += CommonUtility.GetMillisecondsByDateTime((DateTime)(((object[])(e.DataIdentifier.RawData))[1]), null);
                    this.textBoxSignalMessage.Text += "\r\n";

                    this.textBoxSignalMessage.SelectionStart = this.textBoxSignalMessage.Text.Length;
                    this.textBoxSignalMessage.ScrollToCaret();
                    this.textBoxSignalMessage.Refresh();
                }));
            };

            this.serailPortSignalDetector.Detect();
        }

        private void testSerialPortDetector()
        {
            this.serailPortSignalDetector = new SerialPortSignalDetector();

            this.dataIdentifiers = new List<DataIdentifier>();

            this.serailPortSignalDetector.SerialPortSignalCallBack += (object sender, SerialPortSignalDetectorEventArgs e) =>
            {
                this.dataIdentifiers.Add(e.DataIdentifier);

                this.Invoke(new Action(() =>
                {
                    this.textBoxSignalMessage.Text += e.DataIdentifier.DataUniqueID;
                    this.textBoxSignalMessage.Text += " : ";
                    this.textBoxSignalMessage.Text += ((object[])(e.DataIdentifier.RawData))[0].ToString();
                    this.textBoxSignalMessage.Text += " : ";
                    this.textBoxSignalMessage.Text += CommonUtility.GetMillisecondsByDateTime((DateTime)(((object[])(e.DataIdentifier.RawData))[1]), null);
                    this.textBoxSignalMessage.Text += "\r\n";

                    this.textBoxSignalMessage.SelectionStart = this.textBoxSignalMessage.Text.Length;
                    this.textBoxSignalMessage.ScrollToCaret();
                    this.textBoxSignalMessage.Refresh();
                }));
            };

            object[] testOuput = null;

            this.serailPortSignalDetector.Test(out testOuput);
        }

        private void stopSerialPortDetector() 
        {
            if (this.serailPortSignalDetector != null)
            {
                this.serailPortSignalDetector.Stop();
            }
        }


        private void saveParameters(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    XmlUtility xmlUtility = new XmlUtility();

                    string parameterXml = xmlUtility.XmlSerialize(this.parameters, new Type[] { typeof(NetworkParameter), typeof(TimerParameter), typeof(SerialPortParameter), typeof(PersistencyParameter), typeof(BusinessParameter), typeof(PersistencyType), typeof(CommunicationType), typeof(List<ParameterBase>), typeof(ParameterBase) }, "utf-8");

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

        private SignalParameterSet loadParameters(string filePath)
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

            SignalParameterSet parameters = xmlUtility.XmlDeserialize(parameterXml, typeof(SignalParameterSet), new Type[] { typeof(NetworkParameter), typeof(TimerParameter), typeof(SerialPortParameter), typeof(PersistencyParameter), typeof(BusinessParameter), typeof(PersistencyType), typeof(CommunicationType), typeof(List<ParameterBase>), typeof(ParameterBase) }, "utf-8") as SignalParameterSet;

            return parameters;
        }

        public FormMain()
        {
            InitializeComponent();

            //this.bindParameterProperties();

            SignalParameterSet paras = null;

            string defaultParamFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MES"; //AppDomain.CurrentDomain.BaseDirectory + "\\parameters.xml";

            if (!Directory.Exists(defaultParamFilePath))
            {
                Directory.CreateDirectory(defaultParamFilePath);
            }

            defaultParamFilePath += "\\mes-parameters-detection.xml";

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

            this.toolStripComboBoxSignalSource.SelectedIndex = 0;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.propertyGridParameters.SelectedObject = this.parameters[this.comboBoxParameters.SelectedIndex];
            this.propertyGridParameters.Refresh();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.setControls(true);

            this.applyParameters();

            switch (this.toolStripComboBoxSignalSource.SelectedIndex)
            {
                case 0: 
                    {
                        Task.Run(new Action(()=>{this.testSerialPortDetector();}));
                        break;
                    }
                case 1: 
                    {
                        Task.Run(new Action(()=>{ this.testTimer();}));
                        break;
                    }
                default:
                    {
                        Task.Run(new Action(() => { this.testTimer(); }));
                        break;
                    }
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.setControls(true);

            this.applyParameters();

            switch (this.toolStripComboBoxSignalSource.SelectedIndex)
            {
                case 0:
                    {
                        Task.Run(new Action(() => { this.startSerialPortDetector(); }));
                        break;
                    }
                case 1:
                    {
                        Task.Run(new Action(() => { this.startTimer(); }));
                        break;
                    }
                default:
                    {
                        Task.Run(new Action(()=>{ this.startTimer();}));
                        break;
                    }
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (this.toolStripComboBoxSignalSource.SelectedIndex)
            {
                case 0:
                    {
                        Task.Run(new Action(()=>{ this.stopSerialPortDetector(); }));
                        break;
                    }
                case 1:
                    {
                        Task.Run(new Action(() => { this.stopTimer(); }));
                        break;
                    }
                default:
                    {
                        Task.Run(new Action(() => { this.stopTimer(); }));
                        break;
                    }
            }

            this.setControls(false);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.bindParameterProperties();
            this.textBoxSignalMessage.Text = "";
            this.pictureBoxSignalChart.Image = null;
            this.pictureBoxSignalChart.Refresh();
            this.tabControlSignalDetails.SelectedTab = this.tabPageSignalMessage;
            this.toolStripComboBoxSignalSource.SelectedIndex = 1;
            this.propertyGridParameters.Refresh();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
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

                //SignalParameterSet parameters = xmlUtility.XmlDeserialize(parameterXml, typeof(SignalParameterSet), new Type[] { typeof(NetworkParameter), typeof(TimerParameter), typeof(SerialPortParameter), typeof(PersistencyParameter), typeof(PersistencyType), typeof(CommunicationType), typeof(List<ParameterBase>), typeof(ParameterBase) }, "utf-8") as SignalParameterSet;

                SignalParameterSet parameters = this.loadParameters(this.openFileDialogOpenParameters.FileName);

                this.bindParameterProperties(parameters);

                this.pictureBoxSignalChart.Image = null;
                this.pictureBoxSignalChart.Refresh();
                this.textBoxSignalMessage.Text = "";

                this.toolStripComboBoxSignalSource.SelectedIndex = 1;

                this.tabControlSignalDetails.SelectedTab = this.tabPageSignalMessage;
                this.propertyGridParameters.Refresh();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialogSaveParameters.ShowDialog(this) == DialogResult.OK)
            {
                //using (FileStream stream = new FileStream(this.saveFileDialogSaveParameters.FileName, FileMode.Create, FileAccess.Write, FileShare.Write))
                //{
                //    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                //    {
                //        XmlUtility xmlUtility = new XmlUtility();

                //        string parameterXml = xmlUtility.XmlSerialize(this.parameters, new Type[] { typeof(NetworkParameter), typeof(TimerParameter), typeof(SerialPortParameter), typeof(PersistencyParameter), typeof(PersistencyType), typeof(CommunicationType), typeof(List<ParameterBase>), typeof(ParameterBase) }, "utf-8");

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

        private void toolStripComboBoxSignalSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.toolStripComboBoxSignalSource.SelectedIndex)
            {
                case 0: 
                    {
                        this.comboBoxParameters.SelectedIndex = 1;
                        break;
                    }
                case 1: 
                    {
                        this.comboBoxParameters.SelectedIndex = 2;
                        break; 
                    }
                default:
                    break;
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string defaultParamFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MES"; //AppDomain.CurrentDomain.BaseDirectory + "\\parameters.xml";

            if (!Directory.Exists(defaultParamFilePath))
            {
                Directory.CreateDirectory(defaultParamFilePath);
            }

            defaultParamFilePath += "\\mes-parameters-detection.xml";

            this.saveParameters(defaultParamFilePath);
        }
    }
}
