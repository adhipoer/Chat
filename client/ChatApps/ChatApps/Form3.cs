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
using System.Security.Cryptography;

namespace ChatApps
{
    public partial class Form3 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clientSocket.Connect("10.151.36.206", 1233);
            serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("register" + " " + textBox1.Text + " " + sha256(textBox2.Text + textBox1.Text.Length + textBox1.Text));
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            byte[] inStream = new Byte[256];
            // String to store the response ASCII representation.
            String responseData = String.Empty;
            // Read the first batch of the TcpServer response bytes.
            int bytes = serverStream.Read(inStream, 0, inStream.Length);
            responseData = System.Text.Encoding.ASCII.GetString(inStream, 0, bytes);
            MessageBox.Show(responseData);
            this.Close();
            Form2 form2 = new Form2();
            form2.Show();

        }
        static string sha256(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(password), 0, Encoding.ASCII.GetByteCount(password));
            foreach (byte bit in crypto)
            {
                hash += bit.ToString("x2");
            }
            return hash;
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
