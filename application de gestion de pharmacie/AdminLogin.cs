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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Password.Text=="")
            {
                MessageBox.Show("Entrer le mote de passe Administrateur");
            }
            else if(Password.Text=="Admin")
            {
                Agents Ag = new Agents();
                Ag.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Contactez l'administrateur");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Connexion Conx = new Connexion();
            Conx.Show();
            this.Hide();
        }
    }
}
