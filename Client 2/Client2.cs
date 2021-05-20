using SimpleTcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_2
{
    public partial class Client2 : Form
    {
        public Client2()
        {
            InitializeComponent();
        }

        SimpleTcpClient client;

        private void Client2_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient(txtIP.Text);
            client.Events.Connected += Events_Connected;
            client.Events.DataReceived += Events_DataReceived;
            client.Events.Disconnected += Events_Disconnected;
        }

        private void Events_Disconnected(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"Server disconnected.{Environment.NewLine}";
            });
            
        }

        private void Events_Connected(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"Server connected.{Environment.NewLine}";
            });
                
        }

        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                Console.WriteLine(e.Data);
                if (Encoding.UTF8.GetString(e.Data) == "play")
                {
                    axWindowsMediaPlayer2.URL = textBox1.Text;
                    axWindowsMediaPlayer2.Ctlcontrols.play();
                }
                txtInfo.Text += $"Server: {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";
            });
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer2.URL = textBox1.Text;
            axWindowsMediaPlayer2.Ctlcontrols.play();
        }

        private void axWindowsMediaPlayer2_Enter(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client.Connect();
                btnSend.Enabled = true;
                btnConnect.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (client.IsConnected)
            {
                if (!string.IsNullOrEmpty(txtInfo.Text))
                {
                    client.Send(txtMessage.Text);
                    txtInfo.Text += $"Me : {txtMessage.Text}{Environment.NewLine}";
                    txtMessage.Text = string.Empty;
                }
            }
        }

        private void txtIP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
