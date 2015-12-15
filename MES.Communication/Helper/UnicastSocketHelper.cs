using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MES.Communication.Helper
{
    public class UnicastSocketHelper : IHelper
    {
        private Socket socket;

        private IPEndPoint remoteIPEndPoint;

        private IPEndPoint localIPEndPoint;

        public IDictionary<string, object> Parameters { get; set; }

        public void Initialize(string localAddress, string remoteAddress, int localPort, int remotePort, int timeToLive) 
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress localIP = String.IsNullOrEmpty(localAddress) ? IPAddress.Any : IPAddress.Parse(localAddress);

            IPAddress remoteIP = String.IsNullOrEmpty(remoteAddress) ? IPAddress.None : IPAddress.Parse(remoteAddress);

            this.localIPEndPoint = new IPEndPoint(localIP, localPort);

            this.remoteIPEndPoint = new IPEndPoint(remoteIP, remotePort);

            this.socket.Bind(this.localIPEndPoint);

            //this.socket.Connect(this.remoteIPEndPoint);
        }

        public int Send(byte[] data) 
        {
            return this.socket.SendTo(data, SocketFlags.None, this.remoteIPEndPoint);

            //return this.socket.Send(data, data.Length, SocketFlags.None);
        }

        public byte[] Receive(out int bytesReceived) 
        {
            byte[] data = new byte[1024];

            EndPoint endPoint = ((EndPoint)(this.remoteIPEndPoint));

            bytesReceived = this.socket.ReceiveFrom(data, SocketFlags.None, ref endPoint);

            //bytesReceived = this.socket.Receive(data, SocketFlags.None);

            return data;
        }

        public void Close() 
        {
            this.socket.Close();
        }

        public void SetParameter(string parameterName, object parameterValue)
        {
            System.Reflection.PropertyInfo propInfo = this.GetType().GetProperty(parameterName);

            if (propInfo != null)
            {
                propInfo.SetValue(this, parameterValue);
            }
        }
    }
}
