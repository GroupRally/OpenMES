using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using MES.Core;
using MES.Core.Parameter;

namespace MES.Detector
{
    public class SerialPortSignalDetector : DetectorBase
    {
        private SerialPort serialPort;

        private bool isTesting = false;

        private string currentSequenceValue = "";

        public event SerialPortSignalDetectorCallBack SerialPortSignalCallBack;

        public override void Detect()
        {
            //base.Detect();

            this.serialPort = new SerialPort() 
            {
                PortName = ModuleConfiguration.Default_SerialPortName,
                BaudRate = ModuleConfiguration.Default_SerialPortBaudRate,
                DataBits = ModuleConfiguration.Default_SerialPortDataBits,
                StopBits = (StopBits)(Enum.Parse(typeof(StopBits), ModuleConfiguration.Default_SerialPortStopBits)),
                Parity = (Parity)(Enum.Parse(typeof(Parity), ModuleConfiguration.Default_SerialPortParity)),
                Handshake = (Handshake)(Enum.Parse(typeof(Handshake), ModuleConfiguration.Default_SerialPortHandShake)),
                ReadBufferSize = ModuleConfiguration.Default_SerialPortReadBufferSize,
                ReadTimeout = ModuleConfiguration.Default_SerialPortReadTimeout,
                WriteBufferSize = ModuleConfiguration.Default_SerialPortWriteBufferSize,
                WriteTimeout = ModuleConfiguration.Default_SerialPortWriteTimeout,
                RtsEnable = ModuleConfiguration.Default_SerialPortRtsEnable,
                DtrEnable = ModuleConfiguration.Default_SerialPortDtrEnable,
                Encoding = Encoding.GetEncoding(ModuleConfiguration.Default_SerialPortEncodingCodePage),
            };

            this.serialPort.DataReceived += serialPort_DataReceived;

            this.serialPort.Open();

            if (ModuleConfiguration.Default_SerialPortMode == SerialPortMode.TeachIn)
            {
                if ((ModuleConfiguration.Default_SerialPortExpectedDataValue != null) && (ModuleConfiguration.Default_SerialPortExpectedDataValue.Length > 0))
                {
                    string instruction = "";

                    for (int i = 0; i < ModuleConfiguration.Default_SerialPortExpectedDataValue.Length; i++)
                    {
                        instruction += ModuleConfiguration.Default_SerialPortExpectedDataValue[i];

                        if (i != (ModuleConfiguration.Default_SerialPortExpectedDataValue.Length - 1))
                        {
                            instruction += ",";
                        }
                    }

                    this.serialPort.WriteLine(instruction);
                }
                else if(!String.IsNullOrEmpty(ModuleConfiguration.Default_SequenceMask))
                {
                    this.serialPort.WriteLine(ModuleConfiguration.Default_SequenceMask);
                }
            }
        }

        public override void Stop()
        {
            base.Stop();

            if (this.serialPort.IsOpen) 
            {
                this.serialPort.Close();
            }

            if (this.isTesting)
            {
                this.isTesting = false;
            }
        }

        public override byte[] Test(out object[] OutputData)
        {
            //return base.Test(out OutputData);

            this.isTesting = true;

            this.serialPort = new SerialPort()
            {
                PortName = ModuleConfiguration.Default_SerialPortName,
                BaudRate = ModuleConfiguration.Default_SerialPortBaudRate,
                DataBits = ModuleConfiguration.Default_SerialPortDataBits,
                StopBits = (StopBits)(Enum.Parse(typeof(StopBits), ModuleConfiguration.Default_SerialPortStopBits)),
                Parity = (Parity)(Enum.Parse(typeof(Parity), ModuleConfiguration.Default_SerialPortParity)),
                Handshake = (Handshake)(Enum.Parse(typeof(Handshake), ModuleConfiguration.Default_SerialPortHandShake)),
                ReadBufferSize = ModuleConfiguration.Default_SerialPortReadBufferSize,
                ReadTimeout = ModuleConfiguration.Default_SerialPortReadTimeout,
                WriteBufferSize = ModuleConfiguration.Default_SerialPortWriteBufferSize,
                WriteTimeout = ModuleConfiguration.Default_SerialPortWriteTimeout,
                RtsEnable = ModuleConfiguration.Default_SerialPortRtsEnable,
                DtrEnable = ModuleConfiguration.Default_SerialPortDtrEnable,
                Encoding = Encoding.GetEncoding(ModuleConfiguration.Default_SerialPortEncodingCodePage)
            };

            this.serialPort.DataReceived += serialPort_DataReceived;

            this.serialPort.Open();

            if (ModuleConfiguration.Default_SerialPortMode == SerialPortMode.TeachIn)
            {
                string instruction = "";

                if ((ModuleConfiguration.Default_SerialPortExpectedDataValue != null) && (ModuleConfiguration.Default_SerialPortExpectedDataValue.Length > 0))
                {
                    for (int i = 0; i < ModuleConfiguration.Default_SerialPortExpectedDataValue.Length; i++)
                    {
                        instruction += ModuleConfiguration.Default_SerialPortExpectedDataValue[i];

                        if (i != (ModuleConfiguration.Default_SerialPortExpectedDataValue.Length - 1))
                        {
                            instruction += ",";
                        }
                    }
                }
                else if (!String.IsNullOrEmpty(ModuleConfiguration.Default_SequenceMask))
                {
                    instruction = ModuleConfiguration.Default_SequenceMask;
                }

                this.serialPort.WriteLine(instruction);
            }

            //OutputData = null;

            //return null;

            return base.Test(out OutputData);
        }

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)(sender);

            string data = ModuleConfiguration.Default_SerialPortIsReadingByLine ? port.ReadLine() : port.ReadExisting();  //port.ReadLine();

            if ((((ModuleConfiguration.Default_SerialPortMode == SerialPortMode.Normal) || (ModuleConfiguration.Default_SerialPortMode == SerialPortMode.TeachIn)) && (!String.IsNullOrEmpty(data))) || (ModuleConfiguration.Default_SerialPortExpectedDataValue.Contains(data)))
            {
                //string sequenceVal = String.IsNullOrEmpty(this.currentSequenceValue) ? data : this.currentSequenceValue + ModuleConfiguration.Default_SequenceSeparator + data;

                //if (String.IsNullOrEmpty(ModuleConfiguration.Default_SequenceMask) || Utility.CommonUtility.IsSequenceMask(sequenceVal, ModuleConfiguration.Default_SequenceSeparator, ModuleConfiguration.Default_SequenceMask))
                {
                    DataIdentifier dataIdentifier = new DataIdentifier()
                    {
                        DataUniqueID = Guid.NewGuid().ToString(),
                        RawData = new object[] { data, DateTime.Now }
                    };

                    if ((!this.isTesting) && (ModuleConfiguration.Default_SerialPortIsBroadcasting))
                    {
                        base.Notify(dataIdentifier);
                    }

                    if (this.SerialPortSignalCallBack != null)
                    {
                        this.SerialPortSignalCallBack(this, new SerialPortSignalDetectorEventArgs() { DataIdentifier = dataIdentifier });
                    }

                    this.currentSequenceValue = "";
                }
                //else if(ModuleConfiguration.Default_SequenceMask.Contains(sequenceVal))
                //{
                //    this.currentSequenceValue = sequenceVal;
                //}
            }
        }
    }

    public class SerialPortSignalDetectorEventArgs : EventArgs
    {
        public DataIdentifier DataIdentifier { get; set; }
    }

    public delegate void SerialPortSignalDetectorCallBack(object sender, SerialPortSignalDetectorEventArgs e);
}
