using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayVideo_Form
{
    public partial class VideoForm : Form
    {
        Simpl
        

        public VideoForm()
        {
            InitializeComponent();
        }

        

        private void VideoForm_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            //server.Delimiter = 0x13; //enter
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                txtStatus.Text += e.MessageString;
                e.ReplyLine(string.Format("You said: {0}", e.MessageString));
                
            });
        }




        //public static System.Net.IPAddress Parse(string ipString);

        private void button3_Click(object sender, EventArgs e)
        {
            txtStatus.Text += "Server starting...";
            //System.Net.IPAddress ip = new System.Net.IPAddress Parse(string ipString);
            //Console.WriteLine(long.Parse("127"));
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(txtHost.Text);
            server.Start(ip, Convert.ToInt32(txtPort.Text));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
                server.Stop();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
