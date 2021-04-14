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
    public partial class Ky_027_Light_Cup : Form
    {
        public Ky_027_Light_Cup()
        {
            InitializeComponent();
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
                serialPort1.Write("6"); 


            }
            catch
            {

            }
        }
        GraphPane myPaneLight = new GraphPane();
        PointPairList listPointLight = new PointPairList();
        LineItem myCurveLight;
        double zaman = 0;


        private void GrafikHazirla()
        {
            myPaneLight = zedGraphControl1.GraphPane;
            myPaneLight.Title.Text = "Hareket-Zaman Grafiği";
            myPaneLight.XAxis.Title.Text = "t (s)";
            myPaneLight.YAxis.Title.Text = "Çıkış Hareket ";
            myPaneLight.YAxis.Scale.Min = 0;
            myPaneLight.YAxis.Scale.Max = 4;
            myCurveLight = myPaneLight.AddCurve(null, listPointLight, Color.Red, SymbolType.None);
            myCurveLight.Line.Width = 3;
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;

                int income = Convert.ToInt16(serialPort1.ReadLine());

                
                System.Threading.Thread.Sleep(100);
                zaman += 0.05;
                listPointLight.Add(new PointPair(zaman, Convert.ToDouble(income.ToString())));
                myPaneLight.XAxis.Scale.Max = zaman;
                myPaneLight.AxisChange();
                zedGraphControl1.Refresh();
                if (income == 0)
                {
                    label1.Text = "Yukarı Hareket Etti";
                }
                else
                    label1.Text = "Aşagıya Hareket Etti";
                textBox1.Text += DateTime.Now.ToString() + "        " + income.ToString() + "\n";
                string filelocation = @"C:\";                                 
                string filename = "EğimeGöreLed.txt";                                                               
                System.IO.File.WriteAllText(filelocation + filename, "Zaman\t\t\tDeğer\n" + textBox1.Text);

            }
            catch (Exception ex)
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

        private void Ky_027_Light_Cup_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < System.IO.Ports.SerialPort.GetPortNames().Length; i++)
            {
                comboBox1.Items.Add(System.IO.Ports.SerialPort.GetPortNames()[i]);
            }
            GrafikHazirla();
            button2.Enabled = false;

        }

        private void Ky_027_Light_Cup_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void Ky_027_Light_Cup_FormClosing(object sender, FormClosingEventArgs e)
        {

                Environment.Exit(0);

        }

        private void sıcaklıkToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Anasayfa a = new Anasayfa();
            a.Show();
            this.Hide();


        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
