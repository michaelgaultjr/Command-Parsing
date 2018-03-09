using System;
using System.Text;
using System.Windows.Forms;
using SimpleTCP;

namespace CommandParser
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        public static int Port { get; set; }

        SimpleTcpServer server;

        private void ServerForm_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x00;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
            startHost(Port);
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            serverConsole.Invoke((MethodInvoker)delegate ()
            {
                string msg = e.MessageString.Substring(0, (e.MessageString.Length - 1));
                serverConsole.Text += msg + Environment.NewLine;
                server.BroadcastLine(msg + Environment.NewLine);
            });
        }
        
        private void startHost(int _port)
        {
            serverConsole.Text += $"Server Started | Port [{_port}]" + Environment.NewLine;
            server.Start(_port);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
                server.Stop();
            Close();
        }
    }
}
