using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace ChatApps
{
    public partial class Form4 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;
        private Form2 otherForm2;

        public Form4(NetworkStream server, string read, string name)
        {
            
            InitializeComponent();
            serverStream = server;
            readData = read;
            textBox3.Text = "Selamat Datang " + name;
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("alluser");
            serverStream.Write(outStream, 0, outStream.Length);
            byte[] inStream = new Byte[256];
            // String to store the response ASCII representation.
            String responseData = String.Empty;
            // Read the first batch of the TcpServer response bytes.
            int bytes = serverStream.Read(inStream, 0, inStream.Length);
            responseData = System.Text.Encoding.ASCII.GetString(inStream, 0, bytes);

            var items = listView1.Items;
            foreach (var val in responseData)
            {
                items.Add(val.ToString());
            }
            serverStream.Flush();
            Thread thread = new Thread(getMessage);
            thread.Start();
            
        }
 
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        public ListView list
        {
            get
            {
                return listView1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String send = CodeFile1.StringToHexString("private" + " " + otherForm2.TextBox1.ToString() + " " + "pur" + " " + textBox1.Text);

            String packet = CodeFile1.Encrypt(send, CodeFile1.initKey, 16);

            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(packet);

            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("logout");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }
        private void getMessage()
        {
            while (true)
            {
                byte[] inStream = new Byte[256];
                // String to store the response ASCII representation.
                String responseData = String.Empty;
                // Read the first batch of the TcpServer response bytes.
                int bytes = serverStream.Read(inStream, 0, inStream.Length);
                responseData = System.Text.Encoding.ASCII.GetString(inStream, 0, bytes);
                serverStream.Read(inStream, 0, inStream.Length);
                responseData = System.Text.Encoding.ASCII.GetString(inStream, 0, bytes);
                readData = " " + responseData;
                msg();
            }
        }
        private void msg()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(msg));
            else
                textBox2.Text = textBox2.Text + Environment.NewLine + " >> " + readData;
        }


    }
}
