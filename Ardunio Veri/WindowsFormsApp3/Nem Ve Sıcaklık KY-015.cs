using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace WindowsFormsApp3
{
    public partial class Nem_Ve_Sıcaklık_KY_015 : Form
    {
        public Nem_Ve_Sıcaklık_KY_015()
        {
            InitializeComponent();
        }
        GraphPane myPaneSicaklik = new GraphPane();
        PointPairList listPointSicaklik = new PointPairList();
        LineItem myCurveSicaklik;
        double zaman = 0;
        GraphPane myPaneNem = new GraphPane();
        PointPairList listPointNem = new PointPairList();
        LineItem myCurveSicaklikk;
        
        private void GrafikHazirla()
        {
            myPaneSicaklik = zedGraphControl1.GraphPane;
            myPaneSicaklik.Title.Text = "Sıcaklık-Zaman Grafiği";
            myPaneSicaklik.XAxis.Title.Text = "t (s)";
            myPaneSicaklik.YAxis.Title.Text = "Çıkış Derece ";
            myPaneSicaklik.YAxis.Scale.Min = 0;
            myPaneSicaklik.YAxis.Scale.Max = 100;
            myCurveSicaklik = myPaneSicaklik.AddCurve(null, listPointSicaklik, Color.Red, SymbolType.None);
            myCurveSicaklik.Line.Width = 4;
            myPaneNem = zedGraphControl2.GraphPane;
            myPaneNem.Title.Text = "Nem-Zaman Grafiği";
            myPaneNem.XAxis.Title.Text = "t (s)";
            myPaneNem.YAxis.Title.Text = "Nem ";
            myPaneNem.YAxis.Scale.Min = 0;
            myPaneNem.YAxis.Scale.Max = 100;
            myCurveSicaklikk = myPaneNem.AddCurve(null, listPointNem, Color.Red, SymbolType.None);
            myCurveSicaklikk.Line.Width = 4;
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
                serialPort1.Write("8"); 


            }
            catch
            {

            }
        }

        private void Nem_Ve_Sıcaklık_KY_015_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < System.IO.Ports.SerialPort.GetPortNames().Length; i++)
            {
                comboBox1.Items.Add(System.IO.Ports.SerialPort.GetPortNames()[i]);
            }
            GrafikHazirla();
            button1.Enabled = true;
            button2.Enabled = false;
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

      

     


    
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                string[] data = serialPort1.ReadLine().Split('*');
                label1.Text = "Sıcaklık :" + data[1];
                label2.Text = "Nem :" + data[0];
                zaman += 0.05;
                listPointNem.Add(new PointPair(zaman, Convert.ToDouble(data[0].ToString())));
                listPointSicaklik.Add(new PointPair(zaman, Convert.ToDouble(data[1].ToString())));
                myPaneNem.XAxis.Scale.Max = zaman;
                myPaneSicaklik.XAxis.Scale.Max = zaman;
                myPaneNem.AxisChange();
                myPaneSicaklik.AxisChange();
                zedGraphControl1.Refresh();
                zedGraphControl2.Refresh();
                textBox1.Text += DateTime.Now.ToString() + "        " + data[0].ToString() + " " + data[1].ToString() + "\n";
                string filelocation = @"C:\";                                   
                string filename = "Nem-Sıcaklık.txt";                                                       
                System.IO.File.WriteAllText(filelocation + filename, "Zaman\t\t\tDeğer(Nem, Sıcaklık)\n" + textBox1.Text);
            }
            catch
            {

            }

        }

        private void Nem_Ve_Sıcaklık_KY_015_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }


        private void Nem_Ve_Sıcaklık_KY_015_FormClosing(object sender, FormClosingEventArgs e)
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
