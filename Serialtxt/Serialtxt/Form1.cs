using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;

namespace Serialtxt
{
    public partial class Form1 : Form
    {
        string line;

        ///Stop Watch
        ///
        Stopwatch stopWatch = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] strPortNames = SerialPort.GetPortNames();
            foreach (string n in strPortNames)
            {
                COM.Items.Add(n);
            }
             COM.SelectedIndex = 0;
       
            
        }

        private void OPEN_Click(object sender, EventArgs e)
        {
            
            serialPort1.PortName = COM.SelectedItem + "";
            serialPort1.Open();
            stopWatch.Start(); // Stop watch
            if (serialPort1.IsOpen)
            {
                OPEN.Enabled = false;
                CLOSE.Enabled = true;
                textBox.ReadOnly = false;
            }

        }

        int i = 0;
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            line = serialPort1.ReadLine; /// txt
            textBox.AppendText(line);
            textBox.AppendText("\n");
            Console.WriteLine(i++);

            //txt///
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@PATH.Text+".txt", true))
            {
                file.WriteLine(line + '\t'+ 
                stopWatch.Elapsed.Milliseconds.ToString());
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void CLOSE_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                OPEN.Enabled = true;
                CLOSE.Enabled = false;
                textBox.ReadOnly = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }
    }
}
