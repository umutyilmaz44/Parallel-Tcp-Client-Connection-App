using ParallelTcpClientConnectionApp.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParallelTcpClientConnectionApp
{
    public partial class MainForm : Form
    {        
        ConnectionStatus connectionStatus;
        ConcurrentBag<ClientRowInfo> connectionList = new ConcurrentBag<ClientRowInfo>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            connectionStatus = Utility.ConnectionStatus.None;
        }

        private void gvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowCount = gvList.AllowUserToAddRows ? gvList.Rows.Count - 1: gvList.Rows.Count;

            if (e.RowIndex < rowCount && e.ColumnIndex == 4 && (gvList.Rows[e.RowIndex].Cells["Send"] as DataGridViewDisableButtonCell).Enabled)
            {
                int port;
                IPAddress ipAddress;
                gvList.Invoke((Action)(() =>
                {
                    // Ip Address checking
                    if (!IPAddress.TryParse(gvList.Rows[e.RowIndex].Cells["Ip"].Value.ToString(), out ipAddress))
                        return;

                    // Port existing control
                    if (gvList.Rows[e.RowIndex].Cells["Port"].Value == null || string.IsNullOrEmpty(gvList.Rows[e.RowIndex].Cells["Port"].Value.ToString()))
                        return;
                    // Port available checking
                    if (!Int32.TryParse(gvList.Rows[e.RowIndex].Cells["Port"].Value.ToString(), out port))
                        return;
                    
                }));

                ClientRowInfo clientRowInfo = connectionList.Where(x => x.rowIndex == e.RowIndex).FirstOrDefault();
                if (clientRowInfo != null)
                {
                    SendDataForm sendDataForm = new SendDataForm(clientRowInfo);
                    sendDataForm.ShowDialog();

                    //string result = SendData(clientRowInfo);
                    //MessageBox.Show(result, "Result");
                }
            }
        }

        private async void btnConnection_Click(object sender, EventArgs e)
        {
            if (connectionStatus != Utility.ConnectionStatus.Connected)
            {
                string ip, port;
                IPAddress ipAddress;
                TcpClientEx tcpClient;
                connectionList = new ConcurrentBag<ClientRowInfo>();
                for (int i = 0; i < gvList.Rows.Count - 1; i++)
                {
                    gvList.Rows[i].Cells["Status"].Value = ParallelTcpClientConnectionApp.Properties.Resources.disconnected;

                    ip = gvList.Rows[i].Cells["Ip"].Value?.ToString();
                    port = gvList.Rows[i].Cells["Port"].Value?.ToString();

                    tcpClient = new TcpClientEx(i);
                    tcpClient.OnDisconnect += TcpClient_OnDisconnect;
                    tcpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                    connectionList.Add(new ClientRowInfo(i, ip, port, tcpClient));
                }

                connectionStatus = Utility.ConnectionStatus.Awaiting;
                btnConnection.Enabled = false;
                gvList.AllowUserToAddRows = false;
                gvList.AllowUserToDeleteRows = false;
                await Task.Factory.StartNew(() =>
                    Parallel.ForEach(connectionList, connectionInfo =>
                    {
                        Connect(connectionInfo);
                    })
                );

                connectionStatus = Utility.ConnectionStatus.Connected;
                btnConnection.Text = "Disconnect";
                btnConnection.Enabled = true;
            }
            else
            {
                connectionStatus = Utility.ConnectionStatus.Awaiting;
                btnConnection.Enabled = false;                

                await Task.Factory.StartNew(() =>
                    Parallel.ForEach(connectionList, connectionInfo =>
                    {
                        Disconnect(connectionInfo);
                    })
                );
                connectionStatus = Utility.ConnectionStatus.Disconnected;
                btnConnection.Text = "Connect";
                btnConnection.Enabled = true;
                gvList.AllowUserToAddRows = true;
                gvList.AllowUserToDeleteRows = true;
            }            
        }

        private void TcpClient_OnDisconnect(object sender, OnDisconnectEventArgs e)
        {
            //e.RowIndex
            ClientRowInfo clientRowInfo = connectionList.FirstOrDefault(x => x.rowIndex == e.RowIndex);
            if(clientRowInfo != null)
                Disconnect(clientRowInfo);
        }

        private void Connect(ClientRowInfo clientRowInfo)
        {
            bool isConnected = false;
            string result = "";
            Bitmap resultImage = null;
            if (clientRowInfo.client == null)
            {
                clientRowInfo.client = new TcpClientEx(clientRowInfo.rowIndex);
                clientRowInfo.client.OnDisconnect += TcpClient_OnDisconnect;
            }
            try
            {
                if (clientRowInfo.IpAvailabel && clientRowInfo.PortAvailabel && clientRowInfo.client.ConnectAsync(clientRowInfo.ip, clientRowInfo.port).Wait(5000))
                {
                    isConnected = true;
                    resultImage = ParallelTcpClientConnectionApp.Properties.Resources.port_open_32x32;
                }
                else
                {
                    result = "Connection failed";
                    resultImage = ParallelTcpClientConnectionApp.Properties.Resources.disconnected;
                }
            }
            catch (Exception ex)
            {
                result = "Connection error!";
                resultImage = ParallelTcpClientConnectionApp.Properties.Resources.disconnected;
            }
            finally
            {
                gvList.Invoke((Action)(() =>
                {
                    gvList.Rows[clientRowInfo.rowIndex].Cells["Status"].Value = resultImage;
                    gvList.Rows[clientRowInfo.rowIndex].Cells["Description"].Value = result + " / " + isConnected.ToString();
                    ((DataGridViewDisableButtonCell)gvList.Rows[clientRowInfo.rowIndex].Cells["Send"]).Enabled = isConnected;
                }));
            }
        }

        private void Disconnect(ClientRowInfo clientRowInfo)
        {
            bool isConnected = true;
            string result = "";
            Bitmap resultImage=null;
            try
            {
                if (clientRowInfo.client != null && clientRowInfo.client.Client != null)
                {
                    if (clientRowInfo.client.Connected)
                    {
                        clientRowInfo.client.Close();
                    }
                    clientRowInfo.client.Dispose();
                }
                isConnected = false;
                resultImage = ParallelTcpClientConnectionApp.Properties.Resources.disconnected;
            }
            catch (Exception ex)
            {
                result = "Disconnection error!";
                resultImage = ParallelTcpClientConnectionApp.Properties.Resources.disconnected;
            }
            finally
            {
                gvList.Invoke((Action)(() =>
                {
                    gvList.Rows[clientRowInfo.rowIndex].Cells["Status"].Value = resultImage;
                    gvList.Rows[clientRowInfo.rowIndex].Cells["Description"].Value = result;
                    ((DataGridViewDisableButtonCell)gvList.Rows[clientRowInfo.rowIndex].Cells["Send"]).Enabled = isConnected;
                }));
            }
        }

        private void gvList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < gvList.Rows.Count - 1; i++)
            {
                gvList.Rows[i].Cells["Send"].Value = "Send";
                ((DataGridViewDisableButtonCell)gvList.Rows[i].Cells["Send"]).Enabled = false;
            }
            ((DataGridViewDisableButtonCell)gvList.Rows[e.RowIndex].Cells["Send"]).Enabled = false;
        }
    }
}
