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
    public partial class Form3 : Form
    {
        int postoji = 0;
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            if (id == postoji) MessageBox.Show("Zaposleni sa datim id-jem vec postoji u bazi.", "Upozorenje!");
            else
            {
                string ime = textBox2.Text.Split(' ')[0];
                string prezime = textBox2.Text.Split(' ')[1];
                string zanimanje = comboBox1.Text;
                int plata = Convert.ToInt32(textBox3.Text);
                string datum_zaposlenja = monthCalendar1.SelectionStart.ToString().Split(' ')[0];
                try
                {
                    SqlConnection con = new SqlConnection("Data Source=DESKTOP-STK7GNM;Initial Catalog=Med_lab;Integrated Security=True");
                    string upit = "INSERT INTO zaposleni (ime, prezime, zanimanje, plata, datum_zaposlenja) VALUES (@a2, @a3, @a4, @a5, @a6)";
                    SqlCommand cmd = new SqlCommand(upit, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@a2", ime);
                    cmd.Parameters.AddWithValue("@a3", prezime);
                    cmd.Parameters.AddWithValue("@a4", zanimanje);
                    cmd.Parameters.AddWithValue("@a5", plata);
                    cmd.Parameters.AddWithValue("@a6", datum_zaposlenja);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string upit1 = "SELECT * FROM zaposleni WHERE id IN (SELECT MAX(id) FROM zaposleni)";
                    SqlCommand cmd1 = new SqlCommand(upit1, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    MessageBox.Show("Zaposleni je uspesno unet!", "Obavestenje!");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    monthCalendar1.TodayDate = DateTime.Now;
                    dataGridView1.DataSource = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-STK7GNM;Initial Catalog=Med_lab;Integrated Security=True");
                string upit = "SELECT * FROM zaposleni  WHERE id= " + id;
                SqlCommand cmd = new SqlCommand(upit, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Zaposleni sa datim id-jem ne postoji u bazi. Morate uneti novog zaposlenog.", "Obavestenje!");
                    textBox1.Clear();
                    comboBox1.SelectedText = null;
                    textBox2.Clear();
                    textBox3.Clear();
                    monthCalendar1.TodayDate = DateTime.Now;
                    dataGridView1.DataSource = null;
                }
                    
                else
                {
                    postoji = id;
                    dataGridView1.DataSource = dt;
                    textBox2.Text = dt.Rows[0][1].ToString() + ' ' + dt.Rows[0][2].ToString();
                    comboBox1.Text = dt.Rows[0][3].ToString();
                    textBox3.Text = dt.Rows[0][4].ToString();
                    monthCalendar1.SetDate(Convert.ToDateTime(dt.Rows[0][5].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            string ime = textBox2.Text.Split(' ')[0];
            string prezime = textBox2.Text.Split(' ')[1];
            string zanimanje = comboBox1.Text;
            int plata = Convert.ToInt32(textBox3.Text);
            string datum_zaposlenja = monthCalendar1.SelectionStart.ToString().Split(' ')[0];
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-STK7GNM;Initial Catalog=Med_lab;Integrated Security=True");
                string upit = "UPDATE zaposleni SET ime = '" + ime + "', prezime = '" + prezime + "', datum_zaposlenja= '" + datum_zaposlenja + "', plata= '" + plata + "', zanimanje= '" + plata + "' WHERE id = " + id;
                SqlCommand cmd = new SqlCommand(upit, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                string upit1 = "SELECT * FROM zaposleni  WHERE id= " + id;
                SqlCommand cmd1 = new SqlCommand(upit1, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                MessageBox.Show("Informacije o zaposlenom su uspesno izmenjene", "Obavestenje!");
                textBox1.Clear();
                comboBox1.SelectedText = null;
                textBox2.Clear();
                textBox3.Clear();
                monthCalendar1.TodayDate = DateTime.Now;
                dataGridView1.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-STK7GNM;Initial Catalog=Med_lab;Integrated Security=True");

                string upit = "DELETE FROM zaposleni WHERE id = " + id;
                SqlCommand cmd = new SqlCommand(upit, con);
                con.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Zaposleni je uspesno izbrisan.", "Obavestenje!");
                textBox1.Clear();
                comboBox1.SelectedText = null;
                textBox2.Clear();
                textBox3.Clear();
                monthCalendar1.TodayDate = DateTime.Now;
                dataGridView1.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
