using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using ZedGraph;


using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        GraphPane myPaneSicaklik = new GraphPane();
        PointPairList listPointSicaklik = new PointPairList();
        LineItem myCurveSicaklik;
        double zaman = 0;
        public Form1()
        {
            InitializeComponent();

        }
  

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < System.IO.Ports.SerialPort.GetPortNames().Length; i++)
            {
                comboBox1.Items.Add(System.IO.Ports.SerialPort.GetPortNames()[i]);
            }
            GrafikHazirla();
            textBox1.Visible = false;
            button2.Enabled = false;
        }

       
        private void GrafikHazirla()
        {
            myPaneSicaklik = zedGraphControl1.GraphPane;
            myPaneSicaklik.Title.Text = "Sıcaklık - Zaman Grafiği";
            myPaneSicaklik.XAxis.Title.Text = "t (s)";
            myPaneSicaklik.YAxis.Title.Text = "Çıkış Sıcaklık ";
            myPaneSicaklik.YAxis.Scale.Min = 0;
            myPaneSicaklik.YAxis.Scale.Max = 150;
            myCurveSicaklik = myPaneSicaklik.AddCurve(null, listPointSicaklik, Color.Red, SymbolType.None);
            myCurveSicaklik.Line.Width = 3;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                serialPort1.PortName = comboBox1.Text;
                if (!serialPort1.IsOpen)
                    serialPort1.Open();

                serialPort1.Write("1");
                button1.Enabled = false;
                button2.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Seri Porta Bağlı !!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
              
                serialPort1.DiscardInBuffer();
                if (serialPort1.IsOpen)
                    serialPort1.Close();
                button2.Enabled = false;
                button1.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Seri Port Kapalı !!");
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;

                serialPort1.Write("1");
                int receiveddata = Convert.ToInt16(serialPort1.ReadLine());
                receiveddata = ((receiveddata * 5000) / 1023) / 10;

                label1.Text = receiveddata.ToString() + "*C";

                System.Threading.Thread.Sleep(200);
                zaman += 0.05;
                listPointSicaklik.Add(new PointPair(zaman, Convert.ToDouble(receiveddata.ToString())));
                myPaneSicaklik.XAxis.Scale.Max = zaman;
                myPaneSicaklik.AxisChange();
                zedGraphControl1.Refresh();

                textBox1.Text += DateTime.Now.ToString() + "        " + receiveddata.ToString() + "\n";
                string filelocation = @"C:\";
                string filename = "data.txt";
                System.IO.File.WriteAllText(filelocation + filename, "Zaman\t\t\tDeğer\n" + textBox1.Text);
            }
            catch (Exception)
            {

               
            }
           

        }


       
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
                Application.Exit();

            
        }

  

        private void sıcaklıkToolStripMenuItem2_Click_2(object sender, EventArgs e)
        {
            try
            {

                serialPort1.DiscardInBuffer();
                if (serialPort1.IsOpen)
                    serialPort1.Close();
                button2.Enabled = false;
                button1.Enabled = true;
            }
            catch
            {
                
            }
            Anasayfa a = new Anasayfa();
            a.Show();
            this.Hide();
        }
    }
}
