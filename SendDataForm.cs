using ParallelTcpClientConnectionApp.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParallelTcpClientConnectionApp
{
    public partial class SendDataForm : Form
    {
        ClientRowInfo clientRowInfo;

        public SendDataForm(ClientRowInfo clientRowInfo)
        {
            InitializeComponent();

            this.clientRowInfo = clientRowInfo;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSendData_Click(object sender, EventArgs e)
        {
            btnClose.Enabled = false;
            btnSendData.Enabled = false;
            string data = txtData.Text;
            await Task.Factory.StartNew(() =>
            {
                Parallel.Invoke(() =>
                {
                    SendDataAsync(this.clientRowInfo, data);
                });
                
            });
            btnClose.Enabled = true;
            btnSendData.Enabled = true;
        }

        private void SendDataAsync(ClientRowInfo clientRowInfo, string dataText)
        {
            lblResult.Invoke((Action)(() =>
            {
                lblResult.Text = "";
            }));
            try
            {
                bool connected = clientRowInfo.client.Client.IsConnected();
                if (clientRowInfo.client != null && clientRowInfo.client.Connected)
                {
                    string result = "";
                    byte[] byteData = Encoding.UTF8.GetBytes(dataText);
                    NetworkStream networkStream = clientRowInfo.client.GetStream();
                    {
                        var writer = new StreamWriter(networkStream);
                        {
                            var reader = new StreamReader(networkStream, Encoding.UTF8);
                            {
                                networkStream.Write(byteData, 0, byteData.Length);
                                //result = reader.ReadToEnd();
                            }
                        }
                    }

                    lblResult.Invoke((Action)(() =>
                    {
                        lblResult.ForeColor = Color.DarkGreen;
                        lblResult.Text = result;
                    }));
                }
                else
                {
                    string result = "Client not connected!";
                    lblResult.Invoke((Action)(() =>
                    {
                        lblResult.ForeColor = Color.Red;
                        lblResult.Text = result;
                    }));
                }
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                lblResult.Invoke((Action)(() =>
                {
                    lblResult.ForeColor = Color.Red;
                    lblResult.Text = result;
                }));
            }
            finally
            {
                if (clientRowInfo.client != null && !clientRowInfo.client.Connected)
                {
                    btnSendData.Enabled = false;
                }
            }
        }
    }
}
