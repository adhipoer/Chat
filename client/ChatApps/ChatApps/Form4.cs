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
    public partial class Form4 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;
        public Form4()
        {
            InitializeComponent();
        }

        private Form2 otherForm1;

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(listView1.SelectedItems);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
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
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(otherForm1.TextBox1.Text + ";" + listView1.SelectedItems + ";" + textBox3.Text + ";" + "MESSAGE");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }
    }
}
