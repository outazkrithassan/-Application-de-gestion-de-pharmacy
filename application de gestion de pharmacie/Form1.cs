using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace application_de_gestion_de_pharmacie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int pdd = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            pdd += 1;
            guna2CircleProgressBar1.Value = pdd;
            label3.Text = pdd + "%";
            if(guna2CircleProgressBar1.Value==100)
            {
                guna2CircleProgressBar1.Value = 0;
                timer1.Stop();
                Connexion Mycon = new Connexion();
                Mycon.Show();
                this.Hide();
            }
        }

        private void ProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
