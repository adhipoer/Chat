using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace ChatApps
{
    public partial class Form4 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;
        public Form4(NetworkStream server)
        {
            
            InitializeComponent();
            serverStream = server;
            //textBox1.Text = otherForm1.TextBox1.Text;
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("alluser");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            
        }

        private Form2 otherForm1;
        
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
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("private" + " " + otherForm1.TextBox1.Text + " " + "pur" + " " + textBox1.Text);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("logout" + " " + "\n");
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
