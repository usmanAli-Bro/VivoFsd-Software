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
    public partial class COMPANY : Form
    {
        public COMPANY()
        {
            InitializeComponent();
        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void COMPANY_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = getdata();
            disable();
        }

        private DataTable dt = new DataTable();
        private object getdata()
        {
            dt.Rows.Clear();
            string constring = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("select * from COMPANY", con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);

                    con.Close();
                }
            }
            return dt;
        }

        private int id;
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = Convert.ToInt16(dataGridView1.SelectedRows[0].Cells[0].Value);
            CODETEXTBOX.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            nametextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            addresstextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            contacttextBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            emailetextBox.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            TINtextBox.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            STNOtextBox.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            CINtextBox.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            // SAVE
            //string fname =  + ".jpg";
            //string folder = @"D:\IMG\CUSTOMER";
            //string path = System.IO.Path.Combine(folder, fname);
            //Image a = mypictureBox.Image;
            //string constrng = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;

            if (isvalid())
            {


                string constrng = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constrng))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERTCOMPANY", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CODE", CODETEXTBOX.Text);
                        cmd.Parameters.AddWithValue("@NAME", nametextBox.Text);

                        cmd.Parameters.AddWithValue("@ADDRESS", addresstextBox.Text);
                        cmd.Parameters.AddWithValue("@CONTACT", contacttextBox.Text);
                        cmd.Parameters.AddWithValue("@EMAIL", emailetextBox.Text);
                        cmd.Parameters.AddWithValue("@TIN", TINtextBox.Text);
                        cmd.Parameters.AddWithValue("@STNO", STNOtextBox.Text);
                        cmd.Parameters.AddWithValue("@CIN", CINtextBox.Text);

                        //cmd.Parameters.AddWithValue("@PIC", path);



                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data has been Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //a.Save(path);

                        enable();
                        clearr();
                        disable();
                        dataGridView1.DataSource = getdata();
                        //mypictureBox.Image.Save(@"\\D:\\IMG"+barcodetextBox+".jpg");

                    }
                }

            }
        }

        private bool isvalid()
        {
            if (nametextBox.Text.Trim() == string.Empty || contacttextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Pleas Fill All The Fields!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void disable()
        {
            CODETEXTBOX.Enabled = false;
            nametextBox.Enabled = false;
            addresstextBox.Enabled = false;
            contacttextBox.Enabled = false;
            emailetextBox.Enabled = false;
            TINtextBox.Enabled = false;
            STNOtextBox.Enabled = false;
            CINtextBox.Enabled = false;
        }

        private void enable()
        {
            CODETEXTBOX.Enabled = true;
            nametextBox.Enabled = true;
            addresstextBox.Enabled = true;
            contacttextBox.Enabled = true;
            emailetextBox.Enabled = true;
            TINtextBox.Enabled = true;
            STNOtextBox.Enabled = true;
            CINtextBox.Enabled = true;
        }

        private void clearr()
        {
            CODETEXTBOX.Clear();
            nametextBox.Clear();
            addresstextBox.Clear();
            contacttextBox.Clear();
            emailetextBox.Clear();
            TINtextBox.Clear();
            STNOtextBox.Clear();
            CINtextBox.Clear();
        }

        private void newbutton_Click(object sender, EventArgs e)
        {

            enable();
            clearr();
            string constrng = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constrng))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT [dbo].[NEXTCOMP]()", con))
                {

                    // cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    object obj = cmd.ExecuteScalar();
                    CODETEXTBOX.Text = obj.ToString();
                    con.Close();


                }
            }
            nametextBox.Focus();
        }

        private void dataGridView1_RowDividerDoubleClick(object sender, DataGridViewRowDividerDoubleClickEventArgs e)
        {

        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            //enable();
            if (isvalid())
            {


                string constrng = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constrng))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATECOMPANY", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CODE", CODETEXTBOX.Text);
                        cmd.Parameters.AddWithValue("@NAME", nametextBox.Text);

                        cmd.Parameters.AddWithValue("@ADDRESS", addresstextBox.Text);
                        cmd.Parameters.AddWithValue("@CONTACT", contacttextBox.Text);
                        cmd.Parameters.AddWithValue("@EMAIL", emailetextBox.Text);
                        cmd.Parameters.AddWithValue("@TIN", TINtextBox.Text);
                        cmd.Parameters.AddWithValue("@STNO", STNOtextBox.Text);
                        cmd.Parameters.AddWithValue("@CIN", CINtextBox.Text);

                        //cmd.Parameters.AddWithValue("@PIC", path);



                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data has been Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //a.Save(path);

                        enable();
                        clearr();
                        disable();
                        dataGridView1.DataSource = getdata();
                        //mypictureBox.Image.Save(@"\\D:\\IMG"+barcodetextBox+".jpg");

                    }
                }

            }
        }

        private void getdatabutton_Click(object sender, EventArgs e)
        {
            enable();
        }

        private void deletebutton_Click(object sender, EventArgs e)
        {
            //delete
            if (isvalid())
            {


                string constrng = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constrng))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM COMPANY WHERE CODE=" + CODETEXTBOX.Text + "", con))
                    {

                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@CODE", CODETEXTBOX.Text);


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data has been Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //a.Save(path);

                        enable();
                        clearr();
                        disable();
                        dataGridView1.DataSource = getdata();
                        //mypictureBox.Image.Save(@"\\D:\\IMG"+barcodetextBox+".jpg");

                    }
                }
            }


        }
    }
}
