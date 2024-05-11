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

namespace MedicinskaLaboratorija_projekat
{
    public partial class Form2 : Form
    {
        int postoji = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id_pacijenta = Convert.ToInt32(textBox1.Text);
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-STK7GNM;Initial Catalog=Med_lab;Integrated Security=True");
                string upit = "SELECT * FROM pacijenti  WHERE id_pacijenta= " + id_pacijenta;
                SqlCommand cmd = new SqlCommand(upit, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                    MessageBox.Show("Pacijent sa datim id-jem ne postoji u bazi. Morate uneti novog pacijenta.", "Obavestenje!");
                else
                {
                    postoji = id_pacijenta;
                    textBox2.Text = dt.Rows[0][1].ToString() + ' ' + dt.Rows[0][2].ToString();
                    if (dt.Rows[0][3].ToString() == "zenski") radioButton2.Checked = true;
                    else radioButton1.Checked = true;
                    monthCalendar1.SetDate(Convert.ToDateTime(dt.Rows[0][4].ToString())); 
                    dataGridView1.DataSource = dt;

                    string upit1 = "SELECT * FROM kontakti  WHERE id_pacijenta= " + id_pacijenta;
                    SqlCommand cmd1 = new SqlCommand(upit1, con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    if (dt1.Rows.Count != 0)
                    {
                        textBox3.Text = dt1.Rows[0][0].ToString();
                        textBox4.Text = dt1.Rows[0][1].ToString();
                        dataGridView2.DataSource = dt1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id_pacijenta = Convert.ToInt32(textBox1.Text);
            if (id_pacijenta == postoji) MessageBox.Show("Pacijent sa datim id-jem vec postoji u bazi.", "Upozorenje!");
            else
            {
                string ime = textBox2.Text.Split(' ')[0];
                string prezime = textBox2.Text.Split(' ')[1];
                string pol;
                if (radioButton1.Checked) pol = "muski";
                else pol = "zenski";
                string datum_rodjenja = monthCalendar1.SelectionStart.ToString().Split(' ')[0];
                string mejl = textBox3.Text;
                string br_tel = textBox4.Text;
                try
                {
                    SqlConnection con = new SqlConnection("Data Source=DESKTOP-STK7GNM;Initial Catalog=Med_lab;Integrated Security=True");
                    string upit = "INSERT INTO pacijenti (ime, prezime, pol, datum_rodjenja, id_pacijenta) VALUES (@v1, @v2, @v3, @v4, @v5)";
                    SqlCommand cmd = new SqlCommand(upit, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@v1", ime);
                    cmd.Parameters.AddWithValue("@v2", prezime);
                    cmd.Parameters.AddWithValue("@v3", pol);
                    cmd.Parameters.AddWithValue("@v4", datum_rodjenja);
                    cmd.Parameters.AddWithValue("@v5", id_pacijenta);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    string upit1 = "SELECT * FROM pacijenti WHERE id_pacijenta= " + id_pacijenta;
                    SqlCommand cmd1 = new SqlCommand(upit1, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    if (mejl != null)
                    {
                        upit = "INSERT INTO kontakti (id_pacijenta, mejl, br_tel) VALUES (@v1, @v2, @v3)";
                        cmd = new SqlCommand(upit, con);
                        con.Open();
                        cmd.Parameters.AddWithValue("@v1", id_pacijenta);
                        cmd.Parameters.AddWithValue("@v2", mejl);
                        cmd.Parameters.AddWithValue("@v3", br_tel);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        string upit2 = "SELECT * FROM kontakti WHERE id_pacijenta= " + id_pacijenta;
                        SqlCommand cmd2 = new SqlCommand(upit2, con);
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        dataGridView2.DataSource = dt1;
                    }
                    MessageBox.Show("Pacijent je uspesno registrovan!", "Obavestenje!");   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            monthCalendar1.TodayDate = DateTime.Now;
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int id_pacijenta = Convert.ToInt32(textBox1.Text);

            string ime = textBox2.Text.Split(' ')[0];
            string prezime = textBox2.Text.Split(' ')[1];
            string pol;
            if (radioButton1.Checked) pol = "muski";
            else pol = "zenski";
            string datum_rodjenja = monthCalendar1.SelectionStart.ToString().Split(' ')[0];
            string mejl = textBox3.Text;
            string br_tel = textBox4.Text;
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-STK7GNM;Initial Catalog=Med_lab;Integrated Security=True");
                string upit = "UPDATE pacijenti SET ime = '" + ime + "', prezime = '" + prezime + "', pol= '" + pol + "', datum_rodjenja= '" + datum_rodjenja + "' WHERE id_pacijenta = " + id_pacijenta;
                SqlCommand cmd = new SqlCommand(upit, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (mejl != null)
                {
                    upit = "UPDATE kontakti SET mejl='" + mejl + "', br_tel='" + br_tel + "' WHERE id_pacijenta=" + id_pacijenta;
                    cmd = new SqlCommand(upit, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@v1", id_pacijenta);
                    cmd.Parameters.AddWithValue("@v2", mejl);
                    cmd.Parameters.AddWithValue("@v3", br_tel);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                string upit1 = "SELECT * FROM pacijenti WHERE id_pacijenta= " + id_pacijenta;
                SqlCommand cmd1 = new SqlCommand(upit1, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                string upit2 = "SELECT * FROM kontakti WHERE id_pacijenta= " + id_pacijenta;
                SqlCommand cmd2 = new SqlCommand(upit2, con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                dataGridView2.DataSource = dt1;
                MessageBox.Show("Informacije o pacijentu su uspesno izmenjene", "Obavestenje!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                monthCalendar1.TodayDate = DateTime.Now;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int id_pacijenta = Convert.ToInt32(textBox1.Text);
            string deleteChildQuery = "DELETE FROM kontakti WHERE id_pacijenta = @id_pacijenta";
            string deleteChildQuery1 = "DELETE FROM racun WHERE id_pacijenta = @id_pacijenta";
            string deleteChildQuery2 = "DELETE FROM rezultati WHERE id_pacijenta = @id_pacijenta";
            string deleteParentQuery = "DELETE FROM pacijenti WHERE id_pacijenta = @id_pacijenta";


            int parentIDToDelete = @id_pacijenta;

            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-STK7GNM;Initial Catalog=Med_lab;Integrated Security=True");
                SqlCommand cmd = new SqlCommand(deleteChildQuery, con);
                con.Open();
                cmd.Parameters.AddWithValue("@id_pacijenta", id_pacijenta);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand(deleteChildQuery1, con);
                cmd1.Parameters.AddWithValue("@id_pacijenta", id_pacijenta);
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand(deleteChildQuery2, con);
                cmd2.Parameters.AddWithValue("@id_pacijenta", id_pacijenta);
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand(deleteParentQuery, con);
                cmd3.Parameters.AddWithValue("@id_pacijenta", id_pacijenta);
                cmd3.ExecuteNonQuery();
                MessageBox.Show("Pacijent je uspesno izbrisan", "Obavestenje!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                monthCalendar1.TodayDate = DateTime.Now;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
