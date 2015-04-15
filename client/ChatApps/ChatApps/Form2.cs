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
    public partial class Form2 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clientSocket.Connect("10.151.36.55", 1234);
            serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("login " + textBox1.Text + " " + textBox2.Text);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            
            byte[] inStream = new Byte[256];
            // String to store the response ASCII representation.
            String responseData = String.Empty;
            // Read the first batch of the TcpServer response bytes.
            int bytes = serverStream.Read(inStream, 0, inStream.Length);
            responseData = System.Text.Encoding.ASCII.GetString(inStream, 0, bytes);
            
            if (responseData == "success")
            {
                //readData = " " + responseData;
                Form4 form4 = new Form4(serverStream, readData, textBox1.Text);
                form4.Show();
            }
            else if(responseData == "failed")
            {
                MessageBox.Show("anda sudah login");
                //readData = " " + responseData;
                Form4 form4 = new Form4(serverStream, readData, textBox1.Text);
                form4.Show();
            }
            else
                MessageBox.Show("anda siapa?");

        }

        public TextBox TextBox1
        {
            get
            {
                return textBox1;
            }
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();

            //ctr.cobaCoba();
            //CodeFile1.Encrypt();
        }
    }
}
