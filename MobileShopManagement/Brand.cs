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
    public partial class Brand : Form
    {
        string con = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
        public Brand()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Brand_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = getdata();
           
        }

        private object getdata()
        {
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            using (SqlConnection cn = new SqlConnection(con))
            {
                using (SqlCommand cmd = new SqlCommand("select * from brand", cn))
                {
                    
                    cn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    cn.Close();
                   
                }
            }
            return dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(isvalidated()){
                using(SqlConnection c = new SqlConnection(con)){
                    using(SqlCommand cmd = new SqlCommand("insert into brand values(@name)",c)){
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@name", brandtextBox.Text);
                        c.Open();
                        cmd.ExecuteNonQuery();
                        c.Close();
                        MessageBox.Show("Data has been added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        brandtextBox.Clear();
                        brandtextBox.Focus();
                        dataGridView1.DataSource = getdata();
                    }
                }
            }
        }

        private bool isvalidated()
        {
            if(brandtextBox.Text.Trim()==string.Empty){
                MessageBox.Show("Pleas Fill All The Fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                brandtextBox.Clear();
                brandtextBox.Focus();
                return false;
                

            }
            else
            {

                return true;
            }
        
        }
        int id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            brandtextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using(SqlConnection cnn = new SqlConnection(con)){
                using(SqlCommand cmd = new SqlCommand("delete from brand where code="+id+"",cnn)){
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data has been Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    brandtextBox.Clear();
                    brandtextBox.Focus();
                    dataGridView1.DataSource = getdata();
                    cnn.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using(SqlConnection cn = new SqlConnection(con)){
                using(SqlCommand cmd = new SqlCommand("UPDATE BRAND SET NAME=@NAME",cn)){
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@name", brandtextBox.Text);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Data Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    brandtextBox.Clear();
                    brandtextBox.Focus();
                    dataGridView1.DataSource = getdata();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            brandtextBox.Clear();
            brandtextBox.Focus();
        }

      

    }
    
}
