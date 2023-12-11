using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace KPP3_2_1
{
    public partial class Form1 : Form
    {
        private TcpListener tcpListener;
        private Thread listenerThread;
        private Label label1, label2;
        private Panel panel1;

        public Form1()
        {
            InitializeComponent();
            InitializeServer();
        }

        private void InitializeServer()
        {
            tcpListener = new TcpListener(IPAddress.Any, 12345);
            listenerThread = new Thread(new ThreadStart(ListenForClients));
            listenerThread.Start();
        }

        private void ListenForClients()
        {
            tcpListener.Start();

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj;
            NetworkStream clientStream = tcpClient.GetStream();
            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                    break;

                string data = Encoding.ASCII.GetString(message, 0, bytesRead);
                ProcessData(data);
            }

            tcpClient.Close();
        }

        private void ProcessData(string data)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ProcessData(data)));
            }
            else
            {
                switch (data)
                {
                    case "Black":
                        label1.ForeColor = Color.Black;
                        label2.ForeColor = Color.Black;
                        break;

                    case "Red":
                        label1.ForeColor = Color.Red;
                        label2.ForeColor = Color.Red;
                        break;

                    case "Blue":
                        label1.ForeColor = Color.Blue;
                        label2.ForeColor = Color.Blue;
                        break;

                    case "HideLabel1":
                        label1.Visible = false;
                        break;

                    case "ShowLabel1":
                        label1.Visible = true;
                        break;

                    case "HidePanel1":
                        panel1.Visible = false;
                        break;

                    case "ShowPanel1":
                        panel1.Visible = true;
                        break;

                }
            }
        }
    }
}