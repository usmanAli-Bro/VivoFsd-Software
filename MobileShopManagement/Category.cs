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
    public partial class Category : Form
    {
        string con = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
        public Category()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Category_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = getdata();
            brandcomboBox.DataSource = getbrand();
            brandcomboBox.DisplayMember = "NAME";
            brandcomboBox.ValueMember = "CODE";
        }

        private DataTable dta = new DataTable();
        private DataTable getdata()
        {
           // DataTable dt = new DataTable();
            dta.Rows.Clear();
            using(SqlConnection cnn = new SqlConnection(con)){
                using (SqlCommand cmd = new SqlCommand("CATSELECT", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cnn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dta.Load(dr);

                }
            }
            return dta;
        }

        
        private DataTable getbrand()
        {
            DataTable dt = new DataTable();
           
            using(SqlConnection cn = new SqlConnection(con)){
                using (SqlCommand cmd = new SqlCommand("BRANDCOMBO",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {

             if(isvalidated()){
                 using (SqlConnection c = new SqlConnection(con))
                 {
                     using (SqlCommand cmd = new SqlCommand("INSERT INTO CATE VALUES(@NAME,@BRANDID);", c))
                     {
                         cmd.CommandType = CommandType.Text;
                         cmd.Parameters.AddWithValue("@NAME", NAMEtextBox.Text);
                         cmd.Parameters.AddWithValue("@BRANDID", brandcomboBox.SelectedValue);
                         c.Open();
                         cmd.ExecuteNonQuery();
                         c.Close();
                         dataGridView1.DataSource = getdata();
                         MessageBox.Show("Data has been added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         NAMEtextBox.Clear();
                         NAMEtextBox.Focus();
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
        int ID;
        int brandid;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NAMEtextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            ID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            brandcomboBox.SelectedValue = dataGridView1.SelectedRows[0].Cells[3].Value;
           
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //
            DataView dv = dta.DefaultView;
            dv.RowFilter = "Category LIKE '%" + textBox4.Text + "%'";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dta.DefaultView;
            dv.RowFilter = "Brand LIKE '%" + textBox3.Text + "%'";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isvalidated())
            {
                using (SqlConnection c = new SqlConnection(con))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM CATE WHERE ID="+ID+"", c))
                    {
                        cmd.CommandType = CommandType.Text;
                     
                        c.Open();
                        cmd.ExecuteNonQuery();
                        c.Close();
                        dataGridView1.DataSource = getdata();
                       
                        MessageBox.Show("Data has been Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NAMEtextBox.Clear();
                        NAMEtextBox.Focus();
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //update
            if (isvalidated())
            {
                using (SqlConnection c = new SqlConnection(con))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE CATE SET NAME=@NAME,BRANDID=@BRANDID WHERE ID="+ID+";", c))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@NAME", NAMEtextBox.Text);
                        cmd.Parameters.AddWithValue("@BRANDID", brandcomboBox.SelectedValue);
                        c.Open();
                        cmd.ExecuteNonQuery();
                        c.Close();
                        dataGridView1.DataSource = getdata();
                        MessageBox.Show("Data has been Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NAMEtextBox.Clear();
                        NAMEtextBox.Focus();
                    }
                }
            }
        }
    }
}
