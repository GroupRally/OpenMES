using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace UnitTestMulticastReceive
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            Console.WriteLine("Ready to receive…");

            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
            EndPoint ep = (EndPoint)iep;

            sock.Bind(iep);
            sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPAddress.Parse("224.100.0.1")));

            while (true)
            {
                byte[] data = new byte[1024];
                int recv = sock.ReceiveFrom(data, ref ep);
                string stringData = Encoding.ASCII.GetString(data, 0, recv);

                Console.WriteLine("received: {0} from: {1}", stringData, ep.ToString());
            }
            
            //byte[] data = new byte[1024];
            //int recv = sock.ReceiveFrom(data, ref ep);
            //string stringData = Encoding.ASCII.GetString(data, 0, recv);

            //Console.WriteLine("received: {0} from: {1}", stringData, ep.ToString());

            //sock.Close();
        }

        static void UdpReceive() 
        {
            UdpClient sock = new UdpClient(9050);

            Console.WriteLine("Ready to receive…");

            sock.JoinMulticastGroup(IPAddress.Parse("224.100.0.1"), 50);

            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = sock.Receive(ref iep);
            string stringData = Encoding.ASCII.GetString(data, 0, data.Length);

            Console.WriteLine("received: {0} from: {1}", stringData, iep.ToString());

            sock.Close();
        }
    }
}
