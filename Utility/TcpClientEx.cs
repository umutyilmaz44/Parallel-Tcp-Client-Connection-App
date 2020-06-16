using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ParallelTcpClientConnectionApp.Utility
{
    public class TcpClientEx: TcpClient
    {
        int rowIndex;
        System.Timers.Timer tmr;

        public delegate void OnDisconnectEventHandler(object sender, OnDisconnectEventArgs e);
        public event OnDisconnectEventHandler OnDisconnect;

        public TcpClientEx(int rowIndex):base()
        {
            this.rowIndex = rowIndex;            
            tmr = new System.Timers.Timer(TimeSpan.FromSeconds(15).TotalMilliseconds);
            tmr.Elapsed += Tmr_Elapsed;            
        }

        public async Task ConnectAsyncEx(string hostname, int port)
        {
            this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            await base.ConnectAsync(hostname, port);
            this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            tmr.Start();            
        }

        public void Close()
        {
            base.Close();
            tmr.Stop();
        }

        public void Dispose()
        {
            base.Dispose();
            tmr.Dispose();
        }
        

        private void Tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(!this.Client.IsConnected())
            {
                Console.WriteLine("Tmr_Elapsed called!...(Disconnected)");

                if (OnDisconnect != null)
                    OnDisconnect(this, new OnDisconnectEventArgs(this.rowIndex));
            }
            else
            {
                Console.WriteLine("Tmr_Elapsed called!...(Connected)");
            }


        }
    }

    public class OnDisconnectEventArgs : EventArgs
    {
        public int RowIndex { get; private set; }

        public OnDisconnectEventArgs(int rowIndex)
        {
            RowIndex = rowIndex;
        }
    }
}
