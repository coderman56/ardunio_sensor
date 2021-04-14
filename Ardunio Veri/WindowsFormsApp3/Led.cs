using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace WindowsFormsApp3
{
    public partial class Led : Form
    {
        public Led()
        {
            InitializeComponent();
        }
        GraphPane myPaneLed = new GraphPane();
        PointPairList listPointLed = new PointPairList();
        LineItem myCurveSicaklik;
        GraphPane myPaneLed2 = new GraphPane();
        PointPairList listPoinLed2 = new PointPairList();
        LineItem myCurveSicaklik2;
        GraphPane myPaneLed3 = new GraphPane();
        PointPairList listPointLedk3 = new PointPairList();
        LineItem myCurveSicaklik3;
        double zaman = 0;
        int deger = 0;

        private void Led_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < System.IO.Ports.SerialPort.GetPortNames().Length; i++)
            {
                comboBox1.Items.Add(System.IO.Ports.SerialPort.GetPortNames()[i]);
            }
            GrafikHazirla();
            button4.Enabled = false;
        }

        private void GrafikHazirla()
        {
            myPaneLed = zedGraphControl1.GraphPane;
            myPaneLed.Title.Text = "Red Grafiği";
            myPaneLed.XAxis.Title.Text = "t (s)";
            myPaneLed.YAxis.Title.Text = "Kod ";
            myPaneLed.YAxis.Scale.Min = 0;
            myPaneLed.YAxis.Scale.Max = 250;
            myCurveSicaklik = myPaneLed.AddCurve(null, listPointLed, Color.Red, SymbolType.None);
            myCurveSicaklik.Line.Width = 3;

            myPaneLed2 = zedGraphControl2.GraphPane;
            myPaneLed2.Title.Text = "Green Grafiği";
            myPaneLed2.XAxis.Title.Text = "t (s)";
            myPaneLed2.YAxis.Title.Text = "Kod ";
            myPaneLed2.YAxis.Scale.Min = 0;
            myPaneLed2.YAxis.Scale.Max = 250;
            myCurveSicaklik2 = myPaneLed2.AddCurve(null, listPointLed, Color.Red, SymbolType.None);
            myCurveSicaklik2.Line.Width = 3;

            myPaneLed3 = zedGraphControl3.GraphPane;
            myPaneLed3.Title.Text = "Blue Grafiği";
            myPaneLed3.XAxis.Title.Text = "t (s)";
            myPaneLed3.YAxis.Title.Text = "Kod ";
            myPaneLed3.YAxis.Scale.Min = 0;
            myPaneLed3.YAxis.Scale.Max = 250;
            myCurveSicaklik3 = myPaneLed3.AddCurve(null, listPointLed, Color.Red, SymbolType.None);
            myCurveSicaklik3.Line.Width = 3;


        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                serialPort1.DiscardInBuffer();
                serialPort1.Write("0");
                if (serialPort1.IsOpen)
                    serialPort1.Close();
                button3.Enabled = true;
                button4.Enabled = false;

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
                button3.Enabled = false;
                button4.Enabled = true;
                string[] data = serialPort1.ReadLine().Split('*');
                System.Threading.Thread.Sleep(500);
                zaman += 0.05;
                listPointLed.Add(new PointPair(zaman, Convert.ToDouble(data[0].ToString())));
                myPaneLed.XAxis.Scale.Max = zaman;
                myPaneLed.AxisChange();
                zedGraphControl1.Refresh();
                label1.Text = "RED Kodu: " + data[0];
                listPoinLed2.Add(new PointPair(zaman, Convert.ToDouble(data[1].ToString())));
                myPaneLed2.XAxis.Scale.Max = zaman;
                myPaneLed2.AxisChange();
                zedGraphControl2.Refresh();
                label2.Text = "GREEN Kodu: " + data[1];
                listPointLedk3.Add(new PointPair(zaman, Convert.ToDouble(data[2].ToString())));
                myPaneLed3.XAxis.Scale.Max = zaman;
                myPaneLed3.AxisChange();
                zedGraphControl3.Refresh();
                label3.Text = "BLUE Kodu: " + data[2];
                textBox1.Text += DateTime.Now.ToString() + "        " + data[0].ToString() + " " + data[1].ToString() + " " + data[2].ToString() + "\n";
                string filelocation = @"C:\";                                 
                string filename = "LedDegerleri.txt";                                                             
                System.IO.File.WriteAllText(filelocation + filename, "Zaman\t\t\tDeğer(R,G,B)\n" + textBox1.Text);
            }
            catch
            {
            }         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                if (!serialPort1.IsOpen)
                    serialPort1.Open();
             
                button3.Enabled = false;
                button4.Enabled = true;
                serialPort1.Write("2");


            }
            catch
            {

            }
        }

        private void Led_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void Led_FormClosing(object sender, FormClosingEventArgs e)
        {

                Application.Exit();

          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

 

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.DiscardInBuffer();

                if (serialPort1.IsOpen)
                    serialPort1.Close();


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
