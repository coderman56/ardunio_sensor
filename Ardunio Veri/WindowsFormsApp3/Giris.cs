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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }
        int hata = 0;
        public string giren ;
        private void button1_Click(object sender, EventArgs e)
        {
            if (hata < 3)
            {
                if (textBox1.Text == "admin" && textBox2.Text == "12345")
                {
                    giren = textBox1.Text;
                    MessageBox.Show("Giriş Başarılı");
                    Anasayfa a = new Anasayfa();
                    this.Hide();
                    a.Show();
                    hata = 0;
                   
                }
                else
                {
                    MessageBox.Show("Hatalı Giriş");
                    hata++;
                }
            }
            else
            {
                MessageBox.Show("3 defadan fazla giriş yaptınız");
                Application.Exit();

                Environment.Exit(0);

            }
        }

        private void Giris_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            Application.Exit();
        }

        private void Giris_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Programdan Çıkılıyor");
        }
    }
}
