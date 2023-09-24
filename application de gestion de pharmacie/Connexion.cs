using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace application_de_gestion_de_pharmacie
{
    public partial class Connexion : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\OUTAZKRIT HASSAN\Documents\Mapharmacie.mdf;Integrated Security=True;Connect Timeout=30");

        public Connexion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(NonUtili.Text=="" ||Password.Text=="")
            {
                MessageBox.Show("Entre un Nom d'utilisateur et Mot de passe");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AgentTbl where AgNom='" + NonUtili.Text + "'and AgPass='" + Password.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    Medicaments Med = new Medicaments();
                    Med.Show();
                    this.Hide();
                    Con.Close();
                }
                else
                {
                    MessageBox.Show("Mote de Passe Incorrect");
                }
                Con.Close();

            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin Log = new AdminLogin();
            Log.Show();
            this.Hide();
        }
    }
}
