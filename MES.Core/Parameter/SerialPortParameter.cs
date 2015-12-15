using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MES.Core.Parameter
{
    public class SerialPortParameter : ParameterBase
    {
        private int serialPortDataBits = 8;

        private int serialPortBaudRate = 9600;

        private string serialPortName = "COM1";

        private string serialPortStopBits = "One";

        private string serialPortHandShake = "None";

        private string serialPortParity = "None";

        private int serialPortReadTimeout = 500;

        private int serialPortWriteTimeout = 500;

        private int serialPortReadBufferSize = 4096;

        private int serialPortWriteBufferSize = 4096;

        private bool serialPortRtsEnable = true;

        private bool serialPortDtrEnable = true;

        private int serialPortEncodingCodePage = 20127;//(ASCIIEncoding).http://msdn.microsoft.com/en-us/library/dd317756(VS.85).aspx

        private SerialPortMode serialPortMode = SerialPortMode.Normal;

        private string[] serialPortExpectedDataValue = new string[]{ "7", "8", "9"};

        private string sequenceMask = "";

        private string sequenceSeparator = "|";

        private bool isReadingByLine = true;

        private bool isBroadcasting = true;
        
        /// <summary>
        /// The standard length of data bits per byte for serial communication
        /// </summary>
        public int SerialPortDataBits { get { return this.serialPortDataBits; } set { this.serialPortDataBits = value; } }

        /// <summary>
        /// Baud rate for serial communication
        /// </summary>
        public int SerialPortBaudRate { get { return this.serialPortBaudRate; } set { this.serialPortBaudRate = value; } }

        /// <summary>
        /// The name of the port for serial communications, including but not limited to all available COM ports
        /// </summary>
        public string SerialPortName { get { return this.serialPortName; } set { this.serialPortName = value; } }

        /// <summary>
        /// The standard number of stopbits per byte for serial communication
        /// </summary>
        public string SerialPortStopBits { get { return this.serialPortStopBits; } set { this.serialPortStopBits = value; } }

        /// <summary>
        /// The handshaking protocol for serial port transmission of data
        /// </summary>
        public string SerialPortHandShake { get { return this.serialPortHandShake; } set { this.serialPortHandShake = value; } }

        /// <summary>
        /// The parity-checking protocol
        /// </summary>
        public string SerialPortParity { get { return this.serialPortParity; } set { this.serialPortParity = value; } }

        /// <summary>
        /// The number of milliseconds before a time-out occurs when a read operation does not finish
        /// </summary>
        public int SerialPortReadTimeout { get { return this.serialPortReadTimeout; } set { this.serialPortReadTimeout = value; } }

        /// <summary>
        /// The number of milliseconds before a time-out occurs when a write operation does not finish
        /// </summary>
        public int SerialPortWriteTimeout { get { return this.serialPortWriteTimeout; } set { this.serialPortWriteTimeout = value; } }

        /// <summary>
        /// The size of the SerialPort input buffer
        /// </summary>
        public int SerialPortReadBufferSize { get { return this.serialPortReadBufferSize; } set { this.serialPortReadBufferSize = value; } }

        /// <summary>
        /// The size of the serial port output buffer
        /// </summary>
        public int SerialPortWriteBufferSize { get { return this.serialPortWriteBufferSize; } set { this.serialPortWriteBufferSize = value; } }

        /// <summary>
        /// A value indicating whether the Request to Send (RTS) signal is enabled during serial communication
        /// </summary>
        public bool SerialPortRtsEnable { get { return this.serialPortRtsEnable; } set { this.serialPortRtsEnable = value; } }

        /// <summary>
        /// A value that enables the Data Terminal Ready (DTR) signal during serial communication
        /// </summary>
        public bool SerialPortDtrEnable { get { return this.serialPortDtrEnable; } set { this.serialPortDtrEnable = value; } }

        /// <summary>
        /// The code page number corresponding to the byte encoding for pre- and post-transmission conversion of text
        /// </summary>
        public int SerialPortEncodingCodePage { get { return this.serialPortEncodingCodePage; } set { this.serialPortEncodingCodePage = value; } }//(ASCIIEncoding).http://msdn.microsoft.com/en-us/library/dd317756(VS.85).aspx

        /// <summary>
        /// The mode to determine how/when the signal device sends signal(s) out via serial communication
        /// </summary>
        public SerialPortMode SerialPortMode { get { return this.serialPortMode; } set { this.serialPortMode = value; } }

        /// <summary>
        /// The expecgted value range that trigers the signal devie to send signal(s) out
        /// </summary>
        public string[] SerialPortExpectedDataValue { get { return this.serialPortExpectedDataValue; } set { this.serialPortExpectedDataValue = value; } }

        /// <summary>
        /// 
        /// </summary>
        public string SequenceMask { get { return this.sequenceMask; } set { this.sequenceMask = value; } }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("|")]
        public string SequenceSeparator { get { return this.sequenceSeparator; } set { this.sequenceSeparator = value; } }

        [DefaultValue(true)]
        public bool IsReadingByLine { get { return this.isReadingByLine; } set { this.isReadingByLine = value; } }

        [DefaultValue(true)]
        public bool IsBroadcasting { get { return this.isBroadcasting; } set { this.isBroadcasting = value; } }

        public override string ToString()
        {
            //return base.ToString();

            return "Serial Port";
        }
    }

    public enum SerialPortMode
    {
        Normal = 0,
        TeachIn = 1,
        ReadOut = 2
    }
}
