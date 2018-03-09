using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;

namespace CommandParser
{
    public partial class ClientForm : Form
    {
        public static string Username { get; set; }
        public static string Ip { get; set; }
        public static int Port { get; set; }
        public ClientForm()
        {
            InitializeComponent();
        }

        SimpleTcpClient client;
        private void ClientForm_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
            connect(Ip, Port);
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            outputText.Invoke((MethodInvoker)delegate ()
            {
                outputText.Text += e.MessageString + Environment.NewLine;
            });
        }

        public void connect(string ip, int port = 8080)
        {
            client.Connect(ip, port);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        void SendMessage()
        {
                client.WriteLine($"{Username}> {inputText.Text}");
                inputText.Text = "";
        }

        private void inputText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
            }
        }
    }
}
