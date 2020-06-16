using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ParallelTcpClientConnectionApp.Utility
{
    public class ClientRowInfo
    {
        public int rowIndex;
        public int port;
        public string ip;
        public bool PortAvailabel;
        public bool IpAvailabel;
        public TcpClientEx client;

        public ClientRowInfo(int rowIndex, string Ip, string portText, TcpClientEx client)
        {
            IPAddress ipAddress;
            this.rowIndex = rowIndex;
            this.ip = Ip;
            PortAvailabel = Int32.TryParse(portText, out port);
            IpAvailabel = IPAddress.TryParse(Ip, out ipAddress);
            this.client = client;
        }
    }
}
