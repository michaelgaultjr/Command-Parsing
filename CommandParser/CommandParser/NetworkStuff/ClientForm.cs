using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
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
            string myip = GetIPAddress();
            client.Connect(ip, port);
            client.WriteLine("DATA " + Username + " " + myip);
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
        //get ip function
        static string GetIPAddress()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }
    }
}
