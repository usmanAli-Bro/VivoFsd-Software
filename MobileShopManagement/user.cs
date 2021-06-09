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
    public partial class user : Form
    {
        public user()
        {
            InitializeComponent();
        }

        //private void disable()
        //{
        //    codetextBox.Enabled = false;
        //    nametextBox.Enabled = false;
        //    Male.Enabled = false;
        //    Female.Enabled = false;
        //    addrestextBox.Enabled = false;
        //    citytextBox.Enabled = false;
        //    pincodetextBox.Enabled = false;
        //    statetextBox.Enabled = false;
        //    remarkrichTextBox.Enabled = false;
        //    emailtextBox.Enabled = false;
        //    loadbutton.Enabled = false;
        //    removebutton.Enabled = false;
        //    phonetextBox.Enabled = false;
        //    savebutton.Enabled = false;
        //    updatebutton.Enabled = false;
        //    deletebutton.Enabled = false;
        //    //  getdatabutton.Enabled = false;
        //}
        //private void enable()
        //{
        //    codetextBox.Enabled = true;
        //    nametextBox.Enabled = true;
        //    Male.Enabled = true;
        //    Female.Enabled = true;
        //    addrestextBox.Enabled = true;
        //    citytextBox.Enabled = true;
        //    pincodetextBox.Enabled = true;
        //    statetextBox.Enabled = true;
        //    remarkrichTextBox.Enabled = true;
        //    emailtextBox.Enabled = true;
        //    loadbutton.Enabled = true;
        //    removebutton.Enabled = true;
        //    nametextBox.Focus();
        //    phonetextBox.Enabled = true;
        //    savebutton.Enabled = true;
        //    updatebutton.Enabled = true;
        //    deletebutton.Enabled = true;
        //    getdatabutton.Enabled = true;
        //}
        //private void clearr()
        //{
        //    codetextBox.Clear();
        //    nametextBox.Clear();
        //    Male.Checked = true;
        //    Female.Enabled = true;
        //    addrestextBox.Clear();
        //    citytextBox.Clear();
        //    pincodetextBox.Clear();
        //    statetextBox.Clear();
        //    remarkrichTextBox.Clear();
        //    emailtextBox.Clear();
        //    phonetextBox.Clear();

        //    nametextBox.Focus();
        //}
        private void user_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = getdata();
            comboBox1.DataSource = getrole();
            comboBox1.ValueMember = "Roleid";
            comboBox1.DisplayMember = "Desc";
        }

        private DataTable getrole()
        {
            string constring = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("GETROLES", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    con.Close();
                }
            }
            return dt;
        }
        private DataTable dt = new DataTable();
        private DataTable getdata()
        {

            string constring = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("GETUSER", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    con.Close();
                }
            }
            return dt;
        }

        string constrng = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
        private void exitbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newbutton_Click(object sender, EventArgs e)
        {
            string constrng = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constrng))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT [dbo].[NEXTUSERID]()", con))
                {
                    con.Open();
                    object obj = cmd.ExecuteScalar();
                    codetextBox.Text = obj.ToString();
                    nametextBox.Focus();
                    con.Close();
                }
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            //SAVE
            if (isvalid())
            {


                using (SqlConnection con = new SqlConnection(constrng))
                {
                    using (SqlCommand cmd = new SqlCommand("ADDUSERS", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CODE", codetextBox.Text);
                        cmd.Parameters.AddWithValue("@NAME", nametextBox.Text);
                        cmd.Parameters.AddWithValue("@GENDER", getgender());
                        cmd.Parameters.AddWithValue("@ADDRESS", addrestextBox.Text);
                        cmd.Parameters.AddWithValue("@ROLE",comboBox1.SelectedValue);
                        cmd.Parameters.AddWithValue("@PASS", PasswordtextBox.Text);
      

                      



                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data has been Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = getdata();
                        //a.Save(path);

                        //clearr();
                        //disable();
                        //mypictureBox.Image.Save(@"\\D:\\IMG"+barcodetextBox+".jpg");

                    }
                }

            }

        }

        private bool isvalid()
        {
            if (nametextBox.Text.Trim() == string.Empty|| PasswordtextBox.Text.Trim()==string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private int getgender()
        {
            if (Male.Checked)
            {
                return 1;
            }
            if (Female.Checked)
            {
                return 2;
            }
            return 0;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            codetextBox.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            nametextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            Male.Checked = ((dataGridView1.SelectedRows[0].Cells[2].Value) is DBNull) ? false : ((Convert.ToInt16(dataGridView1.SelectedRows[0].Cells[2].Value) == 1 ? true : false));
            Female.Checked = ((dataGridView1.SelectedRows[0].Cells[2].Value) is DBNull) ? false : ((Convert.ToInt16(dataGridView1.SelectedRows[0].Cells[2].Value) == 2 ? true : false));
            addrestextBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.SelectedValue = dataGridView1.SelectedRows[0].Cells[4].Value;
            PasswordtextBox.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            //update
            if (isvalid())
            {


                using (SqlConnection con = new SqlConnection(constrng))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATEUSERS", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CODE", codetextBox.Text);
                        cmd.Parameters.AddWithValue("@NAME", nametextBox.Text);
                        cmd.Parameters.AddWithValue("@GENDER", getgender());
                        cmd.Parameters.AddWithValue("@ADDRESS", addrestextBox.Text);
                        cmd.Parameters.AddWithValue("@ROLE", comboBox1.SelectedValue);
                        cmd.Parameters.AddWithValue("@PASS", PasswordtextBox.Text);






                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data has been Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = getdata();
                        //a.Save(path);

                        //clearr();
                        //disable();
                        //mypictureBox.Image.Save(@"\\D:\\IMG"+barcodetextBox+".jpg");

                    }
                }

            }
        }

        private void deletebutton_Click(object sender, EventArgs e)
        {
            //DELETE
            if (isvalid())
            {


                using (SqlConnection con = new SqlConnection(constrng))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[User] WHERE CODE="+codetextBox.Text+"", con))
                    {

                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@CODE", codetextBox.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data has been Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = getdata();
                        //a.Save(path);

                        //clearr();
                        //disable();
                        //mypictureBox.Image.Save(@"\\D:\\IMG"+barcodetextBox+".jpg");

                    }
                }

            }
        }
    }
}
