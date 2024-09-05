using System.Net.Sockets;
using System.Net;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab1_Lyachov_Alexandr_221_327
{
    public partial class Form1 : Form
    {
        private UdpClient udpServer;
        int port = 7777;

        public Form1()
        {
            InitializeComponent();

        }
        private void StartUdpServer()
        {
            udpServer = new UdpClient(port);
            udpServer.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            textBox3.Text += ("UDP Server started on port " + port);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
            byte[] receivedBytes = udpServer.EndReceive(ar, ref remoteEndPoint);
            string receivedData = Encoding.ASCII.GetString(receivedBytes);
            this.Invoke(new MethodInvoker(() =>
            {
                textBox3.Text += ("Received: " + receivedData + " from " + remoteEndPoint.Address);
            }));
            udpServer.BeginReceive(new AsyncCallback(ReceiveCallback), null);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            udpServer.Close();
            base.OnFormClosing(e);
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                StartUdpServer();
            }
            else
            {
                udpServer.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int data;
            if (textBox2.Text.Length > 0 && Int32.TryParse(textBox2.Text, out data) && textBox2.Text.Length < 5)
            {
                textBox2.BackColor = Color.White;
                port = data;
            }
            else
            {
                textBox2.BackColor = Color.Red;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
