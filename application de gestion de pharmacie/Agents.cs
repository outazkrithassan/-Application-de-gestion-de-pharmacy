using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace application_de_gestion_de_pharmacie
{
    public partial class Agents : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\OUTAZKRIT HASSAN\Documents\Mapharmacie.mdf;Integrated Security=True;Connect Timeout=30");
        public Agents()
        {
            InitializeComponent();
            Afficher();
            Reinitialisation();
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (Cle == 0)
            {
                MessageBox.Show("Selectionez Le Agent à Effacer");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Req = "delete from AgentTbl where AgNum=" + Cle + "";
                    SqlCommand cmd = new SqlCommand(Req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agnet Supprime Avec Succes");
                    Con.Close();
                    Afficher();
                    Reinitialisation();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
        private void Afficher()
        {
            Con.Open();
            string Req = "select * from AgentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Req, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AgentDVG.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reinitialisation()
        {
            AgNom.Text = "";
            AgTel.Text = "";
            AgPass.Text = "";
            AgGen.SelectedIndex = -1;
            //Cle = 0;
        }
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (AgNom.Text == "" || AgTel.Text == "" || AgGen.SelectedIndex == -1 || AgPass.Text == "")
            {
                MessageBox.Show("Completez les informations svp");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Req = "insert into AgentTbl values('" + AgNom.Text + "','" + AgDate.Value.Date+ "','" + AgTel.Text + "','" + AgGen.SelectedItem.ToString() + "','" + AgPass.Text + "')";
                    SqlCommand cmd = new SqlCommand(Req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Ajouter Avec Succes");
                    Con.Close();
                    Afficher();
                    Reinitialisation();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnReinitialiser_Click(object sender, EventArgs e)
        {
            Reinitialisation();
        }
        int Cle = 0;
        private void AgentDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AgNom.Text =AgentDVG.SelectedRows[0].Cells[1].Value.ToString();
            AgDate.Text = AgentDVG.SelectedRows[0].Cells[2].Value.ToString();
            AgTel.Text = AgentDVG.SelectedRows[0].Cells[3].Value.ToString();
            AgGen.SelectedItem = AgentDVG.SelectedRows[0].Cells[4].Value.ToString();
            AgPass.Text = AgentDVG.SelectedRows[0].Cells[5].Value.ToString();
            if (AgNom.Text == "")
                Cle = 0;
            else
                Cle = Convert.ToInt32(AgentDVG.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (AgNom.Text == "" || AgTel.Text == "" || AgGen.SelectedIndex == -1 || AgPass.Text == "")
            {
                MessageBox.Show("Information Manquante");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Req = "Update AgentTbl set AgNom='" + AgNom.Text + "',AgDDN='" + AgDate.Value.Date + "' ,AgTel='" + AgTel.Text + "' ,AgSex='" +AgGen.SelectedItem.ToString() + "',AgPass='" + AgPass.Text + "'  where AgNum=" + Cle + "";
                    SqlCommand cmd = new SqlCommand(Req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Modifier Avec Succes");
                    Con.Close();
                    Afficher();
                    Reinitialisation();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Connexion Conx = new Connexion();
            Conx.Show();
            this.Hide();
        }
    }
}
