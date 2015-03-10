﻿using System;
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
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox1.Text + ";" + textBox2.Text + ";" + "LOGIN");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        public TextBox TextBox1
        {
            get
            {
                return textBox1;
            }
        }

        Form3 form3 = new Form3();
        private void button1_Click(object sender, EventArgs e)
        {
            form3.Show();
        }
    }
}
