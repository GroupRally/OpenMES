using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Communication.Helper
{
    interface IHelper
    {
        void Initialize(string localAddress, string remoteAddress, int localPort, int remotePort, int timeToLive);

        int Send(byte[] data);

        byte[] Receive(out int bytesReceived);

        void Close();

        IDictionary<string, object> Parameters { get; set; }
    }
}
