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

namespace ClientRemoveUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpClient client;

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient("127.0.0.1:9000");
            client.Events.Connected += Events_Connected;
            client.Events.DataReceived += Events_DataReceived;
            client.Events.Disconnected += Events_Disconnected;
            axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(axWindowsMediaPlayer1_PlayStateChange);
            client.Connect();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
        }
        private void Events_Disconnected(object sender, EventArgs e)
        {

        }

        private void Events_Connected(object sender, EventArgs e)
        {

        }

        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                Console.WriteLine(e.Data);
                if (Encoding.UTF8.GetString(e.Data) == "play")
                {
                    axWindowsMediaPlayer1.URL = "C:/Users/Windows 10/Downloads/wetransfer-d9d477/Aki Fire Dragon.mp4";
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    pictureBox1.Visible = false;
                }
            });
        }
        void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 1) // Stopped
                pictureBox1.Visible = true;
        }
    }
}
