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
    public partial class Medicaments : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\OUTAZKRIT HASSAN\Documents\Mapharmacie.mdf;Integrated Security=True;Connect Timeout=30");
        public Medicaments()
        {
            InitializeComponent();
            remlirFab();
            Afficher();
            Reinitialisation();
        }
        private void remlirFab()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select FabNum from FabricantTbl",Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("FabNum", typeof(int));
            dt.Load(Rdr);
            FabCombox.ValueMember = "FabNum";
            FabCombox.DataSource = dt;
        
            Con.Close();
        }
        private void Afficher()
        {
            Con.Open();
            string Req = "select * from MedicamentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Req, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MedicamentDVG.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reinitialisation()
        {
            MedNom.Text = "";
            MedPrix.Text = "";
            MedQte.Text = "";
            FabCombox.Text = "";
            Cle = 0;
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Fabricants Fab = new Fabricants();
            Fab.Show();
            this.Hide();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (MedNom.Text == "" || MedPrix.Text == "" || MedQte.Text == "" || FabCombox.SelectedIndex == -1)
            {
                MessageBox.Show("Completez les informations svp");
            }
            else
            {
                try 
                {
                    Con.Open();
                    string Req = "insert into MedicamentTbl values('" + MedNom.Text + "','" + MedPrix.Text + "','" + MedQte.Text + "','" + FabCombox.SelectedValue.ToString() + "','" + MedDate.Value.Date + "')";
                    SqlCommand cmd = new SqlCommand(Req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicament Ajouter Avec Succes");
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
        private void MedicamentDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MedNom.Text = MedicamentDVG.SelectedRows[0].Cells[1].Value.ToString();
            MedPrix.Text = MedicamentDVG.SelectedRows[0].Cells[2].Value.ToString();
            MedQte.Text = MedicamentDVG.SelectedRows[0].Cells[3].Value.ToString();
            FabCombox.SelectedValue = MedicamentDVG.SelectedRows[0].Cells[4].Value.ToString();
            MedDate.Text= MedicamentDVG.SelectedRows[0].Cells[5].Value.ToString();
            if (MedNom.Text == "")
                Cle = 0;
            else
                Cle = Convert.ToInt32(MedicamentDVG.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (Cle == 0)
            {
                MessageBox.Show("Selectionez Le Medicament à Effacer");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Req = "delete from MedicamentTbl where MedNum=" + Cle + "";
                    SqlCommand cmd = new SqlCommand(Req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicament Supprime Avec Succes");
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
            if (MedNom.Text == "" || MedPrix.Text == "" || MedQte.Text == "" || FabCombox.SelectedIndex == -1)
            {
                MessageBox.Show("Information Manquante");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Req = "Update MedicamentTbl set MedNom='" + MedNom.Text + "',MedPrix='" + MedPrix.Text + "' ,MedQte='" + MedQte.Text + "' ,MedFab='" + FabCombox.SelectedValue.ToString() + "',MedExp='" + MedDate.Value.Date + "'  where MedNum=" + Cle + "";
                    SqlCommand cmd = new SqlCommand(Req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicament Modifier Avec Succes");
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
    }
}
