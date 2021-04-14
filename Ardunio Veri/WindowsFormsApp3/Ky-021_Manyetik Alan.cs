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
    public partial class Ky_021_Manyetik_Alan : Form
    {
        public Ky_021_Manyetik_Alan()
        {
            InitializeComponent();
        }
        GraphPane myPaneManyetik = new GraphPane();
        PointPairList listPointManyetik = new PointPairList();
        LineItem myCurveManyetik;
        double zaman = 0;


        private void GrafikHazirla()
        {
            myPaneManyetik = zedGraphControl1.GraphPane;
            myPaneManyetik.Title.Text = "Manyetik Alan-Zaman Grafiği";
            myPaneManyetik.XAxis.Title.Text = "t (s)";
            myPaneManyetik.YAxis.Title.Text = "Manyetik Alan ";
            myPaneManyetik.YAxis.Scale.Min = 0;
            myPaneManyetik.YAxis.Scale.Max = 2;
            myCurveManyetik = myPaneManyetik.AddCurve(null, listPointManyetik, Color.Red, SymbolType.None);
            myCurveManyetik.Line.Width = 3;
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;

                int income = Convert.ToInt16(serialPort1.ReadLine());

                label1.Text = income.ToString();
                System.Threading.Thread.Sleep(100);
                zaman += 0.05;
                listPointManyetik.Add(new PointPair(zaman, Convert.ToDouble(income.ToString())));
                myPaneManyetik.XAxis.Scale.Max = zaman;
                myPaneManyetik.AxisChange();
                zedGraphControl1.Refresh();

                textBox1.Text += DateTime.Now.ToString() + "        " + income.ToString() + "\n";
                string filelocation = @"C:\";                                   
                string filename = "ManyetikAlan.txt";                                                               
                System.IO.File.WriteAllText(filelocation + filename, "Zaman\t\t\tDeğer\n" + textBox1.Text);

            }
            catch (Exception ex)
            {

            }
        }

        private void Ky_025_Manyetik_Alan_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < System.IO.Ports.SerialPort.GetPortNames().Length; i++)
            {
                comboBox1.Items.Add(System.IO.Ports.SerialPort.GetPortNames()[i]);
            }
            GrafikHazirla();
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                if (!serialPort1.IsOpen)
                    serialPort1.Open();

                button1.Enabled = false;
                button2.Enabled = true;
                serialPort1.Write("a"); 


            }
            catch
            {

            }
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

        private void Ky_025_Manyetik_Alan_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void Ky_025_Manyetik_Alan_FormClosing(object sender, FormClosingEventArgs e)
        {
           

                Application.Exit();

        }

        private void sıcaklıkToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Anasayfa a = new Anasayfa();
            a.Show();
            this.Hide();


        }
    }
}
