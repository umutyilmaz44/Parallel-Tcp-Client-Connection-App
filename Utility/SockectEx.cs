using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
    static class SocketExtensions
    {
        public static bool IsConnected(this Socket socket)
        {
            try
            {
                // FIONREAD is also available as the "Available" property.
                int FIONREAD = 0x4004667F;
                byte[] outValue = BitConverter.GetBytes(0);
                // Check how many bytes have been received.
                socket.IOControl(FIONREAD, null, outValue);

                bool pollSelectRead = socket.Poll(100, SelectMode.SelectRead);

                bool connection = !(pollSelectRead && socket.Available == 0);
                int sendResult = 0;
                if (connection)
                {
                    sendResult = socket.Send(new byte[1] { 0x00 });
                }

                Console.WriteLine("connection: " + connection + " / pollSelectRead: " + pollSelectRead + " / socket.Available: " + socket.Available + " / sendResult: " + sendResult);
                return connection;
            }
            catch (SocketException) { return false; }
        }
    }
}
