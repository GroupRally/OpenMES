using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace UnitTestMulticastSend
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("224.100.0.1"), 9050);

            byte[] data = Encoding.ASCII.GetBytes("This is a test message");
            server.SendTo(data, iep);

            Console.WriteLine("Please enter your message:");

            string message = Console.ReadLine();

            while (message != "exit")
            {
                data = Encoding.ASCII.GetBytes(message);
                server.SendTo(data, iep);

                Console.WriteLine("Please enter your message:");

                message = Console.ReadLine();
            }

            server.Close();
        }

        static void NewSend() 
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9051);
            IPEndPoint iep2 = new IPEndPoint(IPAddress.Parse("224.100.0.1"), 9050);
            server.Bind(iep);

            byte[] data = Encoding.ASCII.GetBytes("This is a test message");

            server.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPAddress.Parse("224.100.0.1")));
            server.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 50);

            server.SendTo(data, iep2);
            server.Close();
        }

        static void UdpSend() 
        {
            UdpClient sock = new UdpClient();
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("224.100.0.1"), 9050);
            byte[] data = Encoding.ASCII.GetBytes("This is a test message");
            sock.Send(data, data.Length, iep);
            sock.Close();
        }
    }
}
