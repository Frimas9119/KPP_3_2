using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace KPP3._2
{
    public partial class Form1 : Form
    {
        private TcpClient tcpClient;
        private NetworkStream clientStream;

        public Form1()
        {
            InitializeComponent();
            InitializeClient();
        }

        private void InitializeClient()
        {
            tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 12345);
            clientStream = tcpClient.GetStream();
        }


        private void SendData(string data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SendData("Black");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SendData("Red");
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SendData("Blue");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                SendData("HideLabel1");
            }
            else
            {
                SendData("ShowLabel1");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                SendData("ShowPanel1");
            }
            else
            {
                SendData("HidePanel1");
            }
        }
    }
}
