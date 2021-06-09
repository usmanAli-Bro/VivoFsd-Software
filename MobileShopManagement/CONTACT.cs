using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MobileShopManagement
{
    public partial class CONTACT : Form
    {
        public CONTACT()
        {
            InitializeComponent();
        }
        private int id;
        string con = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
        private void button2_Click(object sender, EventArgs e)
        {
            if (isvalidated())
            {
                using (SqlConnection c = new SqlConnection(con))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO CONTACT (NAME,DISCRIP,PHONE)VALUES(@NAME,@DESCRIP,@PHON);", c))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@NAME", NAMEtextBox.Text);
                        cmd.Parameters.AddWithValue("@DESCRIP", remarkrichTextBox.Text);
                        cmd.Parameters.AddWithValue("@PHON", phonetextBox.Text);
                        c.Open();
                        cmd.ExecuteNonQuery();
                        c.Close();
                     
                        MessageBox.Show("Data has been added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NAMEtextBox.Clear();
                        phonetextBox.Clear();
                        remarkrichTextBox.Clear();
                        dataGridView1.Refresh();
                        dataGridView1.DataSource = getdata();
                    }
                }
            }
        }

        private bool isvalidated()
        {
            
            if (NAMEtextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Pleas Fill All The Fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NAMEtextBox.Clear();
                NAMEtextBox.Focus();
                return false;


            }
            else
            {

                return true;
            }

        
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void CONTACT_Load(object sender, EventArgs e)
        {
            NAMEtextBox.Focus();
            dataGridView1.DataSource = getdata();
        }

        private DataTable dt = new DataTable();
        private DataTable getdata()
        {

            string constring = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            dt.Rows.Clear();
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("select * from CONTACT", con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    con.Close();
                }
            }
            return dt;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id =Convert.ToInt16( dataGridView1.SelectedRows[0].Cells[0].Value);
            NAMEtextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            phonetextBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            remarkrichTextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // update
            if (isvalidated())
            {
                using (SqlConnection c = new SqlConnection(con))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE CONTACT SET NAME=@NAME,DISCRIP=@DESCRIP,PHONE=@PHON WHERE CODE="+id+"", c))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@NAME", NAMEtextBox.Text);
                        cmd.Parameters.AddWithValue("@DESCRIP", remarkrichTextBox.Text);
                        cmd.Parameters.AddWithValue("@PHON", phonetextBox.Text);
                        c.Open();
                        cmd.ExecuteNonQuery();
                        c.Close();

                        MessageBox.Show("Data has been Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NAMEtextBox.Clear();
                        NAMEtextBox.Focus();
                        dataGridView1.DataSource = getdata();
                    }
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "NAME LIKE'%" + textBox4.Text + "%'";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "PHONE LIKE'%" + textBox3.Text + "%'";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isvalidated())
            {
                using (SqlConnection c = new SqlConnection(con))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM CONTACT WHERE CODE="+id+"", c))
                    {
                        
                        c.Open();
                        cmd.ExecuteNonQuery();
                        c.Close();

                        MessageBox.Show("Data has been added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NAMEtextBox.Clear();
                        phonetextBox.Clear();
                        remarkrichTextBox.Clear();
                        dataGridView1.Refresh();
                        dataGridView1.DataSource = getdata();
                    }
                }
            }
        }
    }
}
