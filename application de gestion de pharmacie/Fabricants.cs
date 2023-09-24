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
    public partial class Fabricants : Form
    {
        public Fabricants()
        {
            InitializeComponent();
            Afficher();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\OUTAZKRIT HASSAN\Documents\Mapharmacie.mdf;Integrated Security=True;Connect Timeout=30");
        private void Afficher()
        {
            Con.Open();
            string Req = "select * from FabricantTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Req,Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            FabricantDVG.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reinitialisation()
        {
            NomTb.Text = "";
            AdrTb.Text = "";
            DescTb.Text = "";
            TelTb.Text = "";
            Cle = 0;
        }
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (NomTb.Text == "" || AdrTb.Text == "" || DescTb.Text == "" || TelTb.Text == "")
            {
                MessageBox.Show("Completez les informations svp");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Req = "insert into FabricantTbl values('"+NomTb.Text+"','"+AdrTb.Text+"','"+DescTb.Text+"','"+TelTb.Text+"')";
                    SqlCommand cmd = new SqlCommand(Req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fabricant Ajouter Avec Succes");
                    Con.Close();
                    Afficher();
                    Reinitialisation();

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnReinitialiser_Click(object sender, EventArgs e)
        {
            Reinitialisation();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
        int Cle = 0;
        private void FabricantDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NomTb.Text = FabricantDVG.SelectedRows[0].Cells[1].Value.ToString();
            AdrTb.Text = FabricantDVG.SelectedRows[0].Cells[2].Value.ToString();
            DescTb.Text = FabricantDVG.SelectedRows[0].Cells[3].Value.ToString();
            TelTb.Text = FabricantDVG.SelectedRows[0].Cells[4].Value.ToString();
            if (NomTb.Text == "")
                Cle = 0;
            else
                Cle = Convert.ToInt32(FabricantDVG.SelectedRows[0].Cells[0].Value.ToString());

        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (Cle == 0)
            {
                MessageBox.Show("Selectionez Le Fabricant à Effacer");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Req = "delete from FabricantTbl where FabNum=" + Cle + "";
                    SqlCommand cmd = new SqlCommand(Req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fabricant Supprime Avec Succes");
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

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (NomTb.Text == "" || AdrTb.Text == "" || DescTb.Text == "" || TelTb.Text == "")
            {
                MessageBox.Show("Information Manquante");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Req = "Update FabricantTbl set FabNom='" + NomTb.Text + "',FabAd='" + AdrTb.Text + "' ,FabDescr='" + DescTb.Text + "' ,FabTel='" + TelTb.Text + "'  where FabNum=" + Cle + "";
                    SqlCommand cmd = new SqlCommand(Req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fabricant Modifier Avec Succes");
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

        private void label2_Click(object sender, EventArgs e)
        {
            Medicaments Med = new Medicaments();
            Med.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
