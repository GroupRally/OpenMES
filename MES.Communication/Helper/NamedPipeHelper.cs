using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;

namespace MES.Communication.Helper
{
    public class NamedPipeHelper : IHelper
    {
        public NamedPipeHelper() 
        {
            this.Parameters = new Dictionary<string, object>();
        }

        public IDictionary<string, object> Parameters { get; set; }

        private string pipeName;

        private NamedPipeServerStream namedPipeServerStream;

        private NamedPipeClientStream namePipeClientStream;

        private string localAddr;

        private string remoteAddr;

        private int pipeTimeout;

        public void Initialize(string localAddress, string remoteAddress, int localPort, int remotePort, int timeToLive) 
        {
            this.pipeName = this.Parameters["PipeName"].ToString();

            this.localAddr = localAddress;
            this.remoteAddr = remoteAddress;
            this.pipeTimeout = timeToLive;
        }

        public int Send(byte[] data) 
        {
            if (data == null || data.Length < 0)
            {
                return -1;
            }

            using (this.namePipeClientStream = new NamedPipeClientStream(this.remoteAddr, this.pipeName, PipeDirection.Out))
            {
                this.namePipeClientStream.Connect(this.pipeTimeout);
                this.namePipeClientStream.Write(data, 0, data.Length);
                this.namePipeClientStream.Flush();
            }

            return data.Length; 
        }

        public byte[] Receive(out int bytesReceived) 
        {
            bytesReceived = -1;

            byte[] bytes = null;

            string stringRead = null;

            using (this.namedPipeServerStream = new NamedPipeServerStream(this.pipeName, PipeDirection.In))
            {
               this.namedPipeServerStream.WaitForConnection();

               //bytesReceived = this.namedPipeServerStream.Read(bytes, 0, (int)(this.namedPipeServerStream.Length));

               using (StreamReader reader = new StreamReader(this.namedPipeServerStream, Encoding.ASCII))
               {
                   stringRead = reader.ReadToEnd();
               }
            }

            if (!String.IsNullOrEmpty(stringRead))
            {
                bytes = Encoding.ASCII.GetBytes(stringRead);
            }
            
            if (bytes != null)
            {
                bytesReceived = bytes.Length;
            }

            return bytes; 
        }

        public void Close() 
        {
            if (this.namedPipeServerStream != null)
            {
                if (this.namedPipeServerStream.IsConnected)
                {
                    this.namedPipeServerStream.Disconnect();
                }

                //this.namedPipeServerStream.Dispose();
            }

            //if (this.namePipeClientStream != null)
            //{
            //    this.namePipeClientStream.Dispose();
            //}
        }
    }
}
