using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicinskaLaboratorija_projekat
{
    public partial class Form4 : Form
    {
        int[] spisak_analiza = new int[14];
        int iznos = 0;
        double[] rezultati_analiza = new double[14];
        int br_analiza = 0;
        Form5 form5 = new Form5();
    
        public Form4()
        {
            InitializeComponent();
            form5.label3.Text = "0";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-STK7GNM;Initial Catalog=Med_lab;Integrated Security=True");
            int id_pacijenta = Convert.ToInt16(textBox5.Text);            
            int id_biohemicara = Convert.ToInt16(textBox8.Text);
            string upit = "SELECT zanimanje FROM zaposleni WHERE id = " + id_biohemicara;
            SqlCommand cmd = new SqlCommand(upit, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows[0][0].ToString() != "Biohemicar")
            {
                MessageBox.Show("Ne postoji biohemicar sa datim id-jem! Ponovite unos.", "Upozorenje!");
                textBox8.Clear();
            }
            else
            {
                try
                {
                    string[] sa = textBox6.Text.Split(',');
                    br_analiza = sa.Length;
                    for (int i = 0; i < br_analiza; i++)
                    {
                        spisak_analiza[i] = Convert.ToInt16(sa[i]);
                        string upit1 = "SELECT cena FROM analize WHERE analize.id=" + spisak_analiza[i];
                        SqlCommand cmd1 = new SqlCommand(upit1, con);
                        con.Open();
                        object iznosi = cmd1.ExecuteScalar();
                        iznos += Convert.ToInt32(iznosi);
                        con.Close();
                    }
                    string upit2 = "INSERT INTO racun(id_pacijenta, spisak_analiza, placen, iznos) VALUES (@v1, @v2, @v3, @v4)";
                    SqlCommand cmd2 = new SqlCommand(upit2, con);
                    con.Open();
                    cmd2.Parameters.AddWithValue("@v1", id_pacijenta);
                    cmd2.Parameters.AddWithValue("@v2", textBox6.Text);
                    cmd2.Parameters.AddWithValue("@v3", 0);
                    cmd2.Parameters.AddWithValue("@v4", iznos);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                iznos = 0;
                try
                {
                    string[] ra = textBox7.Text.Split(',');
                    for (int i = 0; i < ra.Length; i++)
                    {
                        rezultati_analiza[i] = Double.Parse(ra[i]);
                        string upit3 = "INSERT INTO rezultati(id_pacijenta, id_analize, id_zaposlenog, rezultat) VALUES (@v1, @v2, @v3, @v4)";
                        SqlCommand cmd3 = new SqlCommand(upit3, con);
                        con.Open();
                        cmd3.Parameters.AddWithValue("@v1", id_pacijenta);
                        cmd3.Parameters.AddWithValue("@v2", spisak_analiza[i]);
                        cmd3.Parameters.AddWithValue("@v3", id_biohemicara);
                        cmd3.Parameters.AddWithValue("@v4", rezultati_analiza[i]);
                        cmd3.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                MessageBox.Show("Uspesno ste uneli podatke u tabele rezultati i racun.", "Obavestenje!");
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form5.Show();
            DataTable[] dt = new DataTable[14];
            DataTable dt1 = new DataTable();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-STK7GNM;Initial Catalog=Med_lab;Integrated Security=True");
            for (int i = 0; i < br_analiza; i++)
            {
                dt[i] = new DataTable();
                string upit = "SELECT ime_analize, cena FROM analize WHERE analize.id=" + spisak_analiza[i];
                SqlCommand cmd = new SqlCommand(upit, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt[i]);
            }
            for (int i = 0; i < br_analiza; i++)
            {
                dt1.Merge(dt[i]);
            }
            form5.dataGridView1.DataSource = dt1;
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-STK7GNM;Initial Catalog=Med_lab;Integrated Security=True");
            conn.Open();
            string upit1 = "SELECT iznos FROM racun WHERE id IN (SELECT MAX(id) FROM racun)";
            SqlCommand com = new SqlCommand(upit1, conn);
            SqlDataAdapter da1 = new SqlDataAdapter(com);
            DataTable dt2 = new DataTable();
            da1.Fill(dt2);
            form5.label3.Text = dt2.Rows[0][0].ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            DataTable[] dt = new DataTable[14];
            DataTable dt1 = new DataTable();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-STK7GNM;Initial Catalog=Med_lab;Integrated Security=True");
            for (int i = 0; i < br_analiza; i++)
            {
                dt[i] = new DataTable();
                string upit = "SELECT ime_analize 'Naziv analize', rezultat 'Rezultat', donja_granica, gornja_granica FROM rezultati JOIN analize ON(rezultati.id_analize = analize.id) JOIN referentne_vrednosti ON(analize.id_ref_vrednost = referentne_vrednosti.id) WHERE rezultati.id IN (SELECT (MAX(rezultati.id)-" + i + ") FROM rezultati)";
                SqlCommand cmd = new SqlCommand(upit, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt[i]);
            }
            for (int i = 0; i < br_analiza; i++)
            {
                dt1.Merge(dt[i]);
            }
            form6.dataGridView1.DataSource = dt1;
        }
    }
}
