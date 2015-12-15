using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Communication.Helper;

namespace MES.Communication
{
    public class CommunicationManager
    {
        public string LocalAddress { get; set; }

        public string RemoteAddress { get; set; }

        public int LocalPort { get; set; }

        public int RemotePort { get; set; }

        public int TimeToLive { get; set; }

        public string PipeName { get; set; }

        private IHelper helper;

        public void Start()
        {
            this.helper = new MulticastSocketHelper();

            this.helper.Initialize(this.LocalAddress, this.RemoteAddress, this.LocalPort, this.RemotePort, this.TimeToLive);
        }

        public void Start(int communicationType) 
        {
            switch (communicationType)
            {
                case 0:
                    {
                        this.helper = new UnicastSocketHelper();
                        break;
                    }
                case 1:
                    {
                        this.helper = new MulticastSocketHelper();
                        break;
                    }
                case 2:
                    {
                        this.helper = new NamedPipeHelper();
                        this.helper.Parameters["PipeName"] = this.PipeName;
                        break;
                    }
                default:
                    {
                        this.helper = new MulticastSocketHelper();
                        break;
                    }
            }

            this.helper.Initialize(this.LocalAddress, this.RemoteAddress, this.LocalPort, this.RemotePort, this.TimeToLive);
        }

        public int Send(byte[] data) 
        {
           return this.helper.Send(data);
        }

        public byte[] Receive(out int bytesReceived) 
        {
            return this.helper.Receive(out bytesReceived);
        }

        public void Close() 
        {
            this.helper.Close();
        }
    }
}
