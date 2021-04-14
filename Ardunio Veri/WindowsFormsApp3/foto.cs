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
    public partial class foto : Form
    {
        public foto()
        {
            InitializeComponent();
        }
       
        GraphPane myPaneısık = new GraphPane();
        PointPairList listPointısık = new PointPairList();
        LineItem myCurveısık;
        double zaman = 0;
 

        private void button1_Click(object sender, EventArgs e)
        {
    
            
            try
            {
                serialPort1.PortName = comboBox1.Text;
                if (!serialPort1.IsOpen)
                    serialPort1.Open();
                serialPort1.Write("4");
                button1.Enabled = false;
                button2.Enabled = true;


            }
            catch
            {

            }
        }
        private void GrafikHazirla()
        {
            myPaneısık = zedGraphControl1.GraphPane;
            myPaneısık.Title.Text = "Işık Şiddeti-Zaman Grafiği";
            myPaneısık.XAxis.Title.Text = "t (s)";
            myPaneısık.YAxis.Title.Text = "Işık şiddeti ";
            myPaneısık.YAxis.Scale.Min = 0;
            myPaneısık.YAxis.Scale.Max = 500;
            myCurveısık = myPaneısık.AddCurve(null, listPointısık, Color.Red, SymbolType.None);
            myCurveısık.Line.Width = 4;
        }

        private void foto_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < System.IO.Ports.SerialPort.GetPortNames().Length; i++)
            {
                comboBox1.Items.Add(System.IO.Ports.SerialPort.GetPortNames()[i]);
            }
            GrafikHazirla();
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

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            serialPort1.Write("4"); 
            
            int income = Convert.ToInt16(serialPort1.ReadLine()); 
            serialPort1.DiscardInBuffer();
            System.Threading.Thread.Sleep(200);
            label1.Text = income.ToString();
            
            zaman += 0.05;
            listPointısık.Add(new PointPair(zaman, Convert.ToDouble(income.ToString())));
            myPaneısık.XAxis.Scale.Max = zaman;
            myPaneısık.AxisChange();
            zedGraphControl1.Refresh();
            textBox1.Text += DateTime.Now.ToString() + "        " + income.ToString() + "\n";
            string filelocation = @"C:\";                                   
            string filename = "Işık.txt";                                                               
            System.IO.File.WriteAllText(filelocation + filename, "Zaman\t\t\tDeğer\n" + textBox1.Text);

        }
        

        private void foto_FormClosed(object sender, FormClosedEventArgs e)
        {
         
        }

        private void foto_FormClosing(object sender, FormClosingEventArgs e)
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
