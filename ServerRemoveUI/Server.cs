using SimpleTcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerRemoveUI
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        SimpleTcpServer server;

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer("127.0.0.1:9000");
            server.Events.ClientConnected += Events_ClientConnected;
            server.Events.ClientDisconnected += Events_ClientDisconnected;
            server.Events.DataReceived += Events_DataReceived;
            server.Start();
            axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(axWindowsMediaPlayer1_PlayStateChange);
            //ListeningKey(sender, new KeyEventArgs);
            //CreateThumbnail("C:/Users/Windows 10/Downloads/wetransfer-d9d477/Aki Fire Dragon.mp4");
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
        }

        private void Events_ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                lstClientIP.Items.Remove(e.IpPort);
            });
        }

        private void Events_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                lstClientIP.Items.Add(e.IpPort);
            });
        }

        //private void CreateThumbnail(string imageFile)
        //{
        //   string dir = new FileInfo(imageFile).DirectoryName;
        //    string thmFilePath = Path.Combine(dir, "thumbnail.jpeg");

        //    System.Drawing.Image image = System.Drawing.Image.FromFile(imageFile);
        //    var thumbImage = image.GetThumbnailImage(64, 64, new Image.GetThumbnailImageAbort(() => false), IntPtr.Zero);
        //    thumbImage.Save(thmFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
        //}



        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (server.IsListening)
            {
                var allItems = lstClientIP.Items.OfType<String>().ToList();

                foreach (var items in allItems)
                {
                    server.Send(items.ToString(), "play");
                }
            }
            axWindowsMediaPlayer1.URL = "C:/Users/Windows 10/Downloads/wetransfer-d9d477/Aki Fire Dragon.mp4";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = "C:/Users/Windows 10/Downloads/wetransfer-d9d477/Aki Fire Dragon.mp4";
        }

        void axWindowsMediaPlayer1_PlayStateChange(object sender,AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 1) // Stopped
                pictureBox1.Visible = true;
        }

        private void ListeningKey(object sender, KeyPressEventArgs e)
        { 
            if (e.KeyChar == 32)
            {
                if (server.IsListening)
                {
                    var allItems = lstClientIP.Items.OfType<String>().ToList();

                    foreach (var items in allItems)
                    {
                        server.Send(items.ToString(), "play");
                    }
                }
                axWindowsMediaPlayer1.URL = "C:/Users/Windows 10/Downloads/wetransfer-d9d477/Aki Fire Dragon.mp4";
                axWindowsMediaPlayer1.Ctlcontrols.play();
                pictureBox1.Visible = false;
            }

            if (e.KeyChar == 13)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                TopMost = true;
            }
            if (e.KeyChar == 46)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                TopMost = false;
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
