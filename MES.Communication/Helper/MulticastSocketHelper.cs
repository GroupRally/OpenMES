using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MES.Communication.Helper
{
    class MulticastSocketHelper :IHelper
    {
        private Socket socket;

        private IPEndPoint remoteIPEndPoint;

        private IPEndPoint localIPEndPoint;

        public IDictionary<string, object> Parameters { get; set; }

        public void Initialize(string localAddress, string remoteAddress, int localPort, int remotePort, int timeToLive) 
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress localIP = String.IsNullOrEmpty(localAddress) ? IPAddress.Any : IPAddress.Parse(localAddress);

            //localIP = localIP.MapToIPv4();

            IPAddress remoteIP = String.IsNullOrEmpty(remoteAddress) ? IPAddress.Any : IPAddress.Parse(remoteAddress);

            //remoteIP = remoteIP.MapToIPv4();

            this.localIPEndPoint = new IPEndPoint(localIP, localPort);

            this.remoteIPEndPoint = new IPEndPoint(remoteIP, remotePort);

            this.socket.Bind(this.localIPEndPoint);

            MulticastOption multicastOption = new MulticastOption(remoteIP);//new MulticastOption(remoteIP, localIP);

            this.socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastOption);

            if (timeToLive > 0)
            {
                this.socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, timeToLive);
            }

            //this.socket.Connect(remoteIPEndPoint);
        }

        public int Send(byte[] data) 
        {
           //return this.socket.SendTo(data, SocketFlags.Multicast, this.remoteIPEndPoint);

            return this.socket.SendTo(data, this.remoteIPEndPoint);
        }

        public byte[] Receive(out int bytesReceived) 
        {
            byte[] data = new byte[1024];

            EndPoint endPoint = ((EndPoint)(this.localIPEndPoint));//((EndPoint)(this.remoteIPEndPoint));

            bytesReceived = this.socket.ReceiveFrom(data, ref endPoint);//this.socket.ReceiveFrom(data, SocketFlags.Multicast, ref endPoint);

            return data;
        }

        public void Close() 
        {
            this.socket.Close();
        }
    }
}
