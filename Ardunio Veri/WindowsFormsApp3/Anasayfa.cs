using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }
    
        private void Anasayfa_Load(object sender, EventArgs e)
        {

            Giris a = new Giris();
            label1.Text = "Programa Hoşgeldiniz" + a.giren;
        }

        private void Anasayfa_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void sıcaklıkToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form1 a = new Form1();
            a.Show();
            this.Hide();
        }

        private void uzaklıkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uzaklık a = new Uzaklık();
            a.Show();
            this.Hide();
        }

        private void ledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Led a = new Led();
            a.Show();
            this.Hide();
        }

        private void fotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foto a = new foto();
            a.Show();
            this.Hide();
        }

        private void kY015ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Nem_Ve_Sıcaklık_KY_015 a = new Nem_Ve_Sıcaklık_KY_015();
            a.Show();
            this.Hide();
        }

        private void kY001ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sıcaklık_KY_001 a = new Sıcaklık_KY_001();
            a.Show();
            this.Hide();
        }

        private void kY040ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KY_040 a = new KY_040();
            a.Show();
            this.Hide();
        }

        private void kY025ManyetikAlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ky_021_Manyetik_Alan a = new Ky_021_Manyetik_Alan();
            a.Show();
            this.Hide();
        }

        private void kY027LightCupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ky_027_Light_Cup a = new Ky_027_Light_Cup();
            a.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Anasayfa_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
