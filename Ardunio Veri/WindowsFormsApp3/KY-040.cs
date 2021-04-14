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
    public partial class KY_040 : Form
    {
        public KY_040()
        {
            InitializeComponent();
        }
        GraphPane myPanePotasyo = new GraphPane();
        PointPairList listPointPotasyo = new PointPairList();
        LineItem myCurvePotasyo;
        double zaman = 0;
        private void GrafikHazirla()
        {
            myPanePotasyo = zedGraphControl1.GraphPane;
            myPanePotasyo.Title.Text = "PotasyoMetre Değeri-Zaman Grafiği";
            myPanePotasyo.XAxis.Title.Text = "t (s)";
            myPanePotasyo.YAxis.Title.Text = "Değer";
            myPanePotasyo.YAxis.Scale.Min = 0;
            myPanePotasyo.YAxis.Scale.Max = 100;
            myCurvePotasyo = myPanePotasyo.AddCurve(null, listPointPotasyo, Color.Red, SymbolType.None);
            myCurvePotasyo.Line.Width = 4;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
       

        

            try
            {
                serialPort1.PortName = comboBox1.Text;
                if (!serialPort1.IsOpen)
                    serialPort1.Open();

                serialPort1.Write("7");

                button2.Enabled = true;
                button1.Enabled = false;
            }
            catch
            {

            }
        }

        private void KY_040_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < System.IO.Ports.SerialPort.GetPortNames().Length; i++)
            {
                comboBox1.Items.Add(System.IO.Ports.SerialPort.GetPortNames()[i]);
            }
            GrafikHazirla();
            button2.Enabled = false;
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

          
            int income = Convert.ToInt16(serialPort1.ReadLine()); 

            label1.Text = income.ToString();
            serialPort1.DiscardInBuffer();
            System.Threading.Thread.Sleep(500);
            zaman += 0.05;
            listPointPotasyo.Add(new PointPair(zaman, Convert.ToDouble(income.ToString())));
            myPanePotasyo.XAxis.Scale.Max = zaman;
            myPanePotasyo.AxisChange();
            zedGraphControl1.Refresh();
          
            textBox1.Text += DateTime.Now.ToString() + "        " + income.ToString() + "\n";
            string filelocation = @"C:\";                              
            string filename = "PotasyoMetre.txt";                                         
            System.IO.File.WriteAllText(filelocation + filename, "Zaman\t\t\tDeğer\n" + textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.DiscardInBuffer();

                if (serialPort1.IsOpen)
                    serialPort1.Close();
                button1.Enabled = true;
                button2.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Seri Port Kapalı !!");
            }
        }

        private void KY_040_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void KY_040_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

       

        private void sıcaklıkToolStripMenuItem2_Click_1(object sender, EventArgs e)
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
