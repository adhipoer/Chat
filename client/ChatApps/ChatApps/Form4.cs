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
using System.Security.Cryptography;

namespace ChatApps
{
    public partial class Form4 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;
        //private Form2 otherForm2;

        /*public Form4(NetworkStream server, string read, string name)
        {
            
            InitializeComponent();
            serverStream = server;
            //readData = read;
            textBox3.Text = "Selamat Datang " + name;
            
            Thread thread = new Thread(() => getAllUser(serverStream));
            thread.Start();
        }*/

        public Form4()
        {

            InitializeComponent();
        }

        private void getAllUser(NetworkStream server)
        {
            serverStream = server;
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
            String send = CodeFile1.StringToHexString("private" + " " + username.Text + " " + textBox1.Text);

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
                //readData = " " + responseData;

                //String decs =  .Decrypt(responseData, Decryptions.initKey, 16);
                //String packets = Decryptions.HexStringToString(decs);

                //readData = " " + packets;

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

        private void label4_Click(object sender, EventArgs e)
        {

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


        private void button4_Click(object sender, EventArgs e)
        {
            clientSocket.Connect("10.151.36.206", 1234);
            serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("login " + username.Text + " " + sha256(pass.Text + Convert.ToString(username.Text.Length) + username.Text));
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new Byte[256];
            // String to store the response ASCII representation.
            String responseData = String.Empty;
            // Read the first batch of the TcpServer response bytes.
            int bytes = serverStream.Read(inStream, 0, inStream.Length);
            responseData = System.Text.Encoding.ASCII.GetString(inStream, 0, bytes);
            MessageBox.Show(responseData);
            //Form4 form4 = new Form4(serverStream, readData, textBox1.Text);
            Form4 form4 = new Form4();
            form4.Show();
        }
    }
}



//y = len - 1;
            //do
            //{
            //    x = Counter(baru4[y], out baru2[y]);
            //    Console.WriteLine("x:{0} y:{1}", baru4[y], baru2[y]);
            //    y--;
            //} while (x == 1 && y != -1);
            //baru3 = new String(baru4);
            //baru = new String(baru2);
            //Console.WriteLine(baru);
            //Console.WriteLine(baru3);
        //}