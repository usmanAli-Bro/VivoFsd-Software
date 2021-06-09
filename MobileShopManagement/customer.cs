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
    public partial class customer : Form
    {
        public int idval { get; set; }
        public bool isupdated { get; set; }
        public customer()
        {
            InitializeComponent();
        }

       
        private void exitbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void disable()
        {
            codetextBox.Enabled = false;
            nametextBox.Enabled = false;
            Male.Enabled = false;
            Female.Enabled = false;
            addrestextBox.Enabled = false;
            citytextBox.Enabled = false;
            pincodetextBox.Enabled = false;
            statetextBox.Enabled = false;
            remarkrichTextBox.Enabled = false;
            emailtextBox.Enabled = false;
            loadbutton.Enabled = false;
            removebutton.Enabled = false;
            phonetextBox.Enabled = false;
            savebutton.Enabled = false;
            updatebutton.Enabled = false;
            deletebutton.Enabled = false;
          //  getdatabutton.Enabled = false;
        }
        private void enable()
        {
            codetextBox.Enabled = true;
            nametextBox.Enabled = true;
            Male.Enabled = true;
            Female.Enabled = true;
            addrestextBox.Enabled = true;
            citytextBox.Enabled = true;
            pincodetextBox.Enabled = true;
            statetextBox.Enabled = true;
            remarkrichTextBox.Enabled = true;
            emailtextBox.Enabled = true;
            loadbutton.Enabled = true;
            removebutton.Enabled = true;
            nametextBox.Focus();
            phonetextBox.Enabled = true;
            savebutton.Enabled = true;
            updatebutton.Enabled = true;
            deletebutton.Enabled = true;
            getdatabutton.Enabled = true;
        }
        private void clearr()
        {
            codetextBox.Clear();
            nametextBox.Clear();
            Male.Checked = true;
            Female.Enabled = true;
            addrestextBox.Clear();
            citytextBox.Clear();
            pincodetextBox.Clear();
            statetextBox.Clear();
            remarkrichTextBox.Clear();
            emailtextBox.Clear();
            phonetextBox.Clear();

            nametextBox.Focus();
        }
        string constrng = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
        private bool isvalid()
        {
            if (nametextBox.Text != string.Empty || addrestextBox.Text != string.Empty)
            {
                return true;
            }
            return false;
        }
        private void savebutton_Click(object sender, EventArgs e)
        {
            string fname = codetextBox.Text + ".jpg";
            string folder = @"D:\IMG\CUSTOMER";
            string path = System.IO.Path.Combine(folder, fname);
            Image a = mypictureBox.Image;

            if (isvalid())
            {


                using (SqlConnection con = new SqlConnection(constrng))
                {
                    using (SqlCommand cmd = new SqlCommand("ADDCUSTOMER", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NAME", nametextBox.Text);
                        cmd.Parameters.AddWithValue("@GENDER", getgender());
                        cmd.Parameters.AddWithValue("@ADDRESS", addrestextBox.Text);
                        cmd.Parameters.AddWithValue("@CITY", citytextBox.Text);
                        cmd.Parameters.AddWithValue("@STATE", statetextBox.Text);
                        cmd.Parameters.AddWithValue("@PINCODE", pincodetextBox.Text);
                        cmd.Parameters.AddWithValue("@EMAIL", emailtextBox.Text);
                        cmd.Parameters.AddWithValue("@REMARKS", remarkrichTextBox.Text);
                        cmd.Parameters.AddWithValue("@Phone ", phonetextBox.Text);

                        cmd.Parameters.AddWithValue("@PIC", path);
                       


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data has been Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        a.Save(path);

                        clearr();
                        disable();
                        //mypictureBox.Image.Save(@"\\D:\\IMG"+barcodetextBox+".jpg");

                    }
                }

            }
        }

        private int getgender()
        {
            if(Male.Checked){
                return 1;
            }
            if(Female.Checked){
                return 2;
            }
            return 0;
        }

        private void customer_Load(object sender, EventArgs e)
        {
            disable();
        }

        private void newbutton_Click(object sender, EventArgs e)
        {
            enable();
            string constrng = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constrng))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT [dbo].[NEXTCUSTMER]()", con))
                {
                    con.Open();
                    object obj = cmd.ExecuteScalar();
                    codetextBox.Text = obj.ToString();
                    con.Close();
                }
            }
        }

        private void loadbutton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    mypictureBox.Image = Image.FromFile(open.FileName);
                }
            }
        }

        //private bool isvalid()
        //{
        //    if (nametextBox.Text != string.Empty || addrestextBox.Text != string.Empty)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        private DataTable getinfobyID(int idval)
        {
            string constrng = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection(constrng))
            {
                using (SqlCommand cmd = new SqlCommand("GET_CUSTOMER", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cnn.Open();
                    cmd.Parameters.AddWithValue("@CODE", idval);
                    SqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    cnn.Close();
                }
            }
            return dt;
        }
        private void getdatabutton_Click(object sender, EventArgs e)
        {
            enable();
            using (customerLOV lv = new customerLOV())
            {
                if (lv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.idval = lv.idval;
                    this.isupdated = lv.isupdated;
                    this.label14.Text = lv.idval.ToString();
                    this.label15.Text = this.idval.ToString();
                }
            }

            if (isupdated)
            {

                DataTable getdatabyID = getinfobyID(this.idval);
                DataRow row = getdatabyID.Rows[0];
                codetextBox.Text = row["CODE"].ToString();
                nametextBox.Text = row["NAME"].ToString();
                addrestextBox.Text = row["ADDRESS"].ToString();
                phonetextBox.Text = row["Phone"].ToString();
                Male.Checked = (row["GENDER"] is DBNull) ? false : (Convert.ToInt16(row["GENDER"]) == 1 ? true : false);
                Female.Checked = (row["GENDER"] is DBNull) ? false : (Convert.ToInt16(row["GENDER"]) == 2 ? true : false);
                citytextBox.Text = row["CITY"].ToString();
                emailtextBox.Text = row["EMAIL"].ToString();
                statetextBox.Text = row["STATE"].ToString();
                pincodetextBox.Text = row["PINCODE"].ToString();
                remarkrichTextBox.Text = row["REMARKS"].ToString();
                mypictureBox.Image = Image.FromFile(row["PIC"].ToString());

            }
        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            string fname = codetextBox.Text + ".jpg";
            string folder = @"D:\IMG\CUSTOMER";
            string path = System.IO.Path.Combine(folder, fname);
            Image a = mypictureBox.Image;

            if (isvalid())
            {


                using (SqlConnection con = new SqlConnection(constrng))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATECUSTOMER", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CODE", codetextBox.Text);
                        cmd.Parameters.AddWithValue("@NAME", nametextBox.Text);
                        cmd.Parameters.AddWithValue("@GENDER", getgender());
                        cmd.Parameters.AddWithValue("@ADDRESS", addrestextBox.Text);
                        cmd.Parameters.AddWithValue("@CITY", citytextBox.Text);
                        cmd.Parameters.AddWithValue("@STATE", statetextBox.Text);
                        cmd.Parameters.AddWithValue("@PINCODE", pincodetextBox.Text);
                        cmd.Parameters.AddWithValue("@EMAIL", emailtextBox.Text);
                        cmd.Parameters.AddWithValue("@REMARKS", remarkrichTextBox.Text);
                        cmd.Parameters.AddWithValue("@Phone ", phonetextBox.Text);

                       // cmd.Parameters.AddWithValue("@PIC", path);



                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data has been Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        a.Save(path);

                        clearr();
                        disable();
                        //mypictureBox.Image.Save(@"\\D:\\IMG"+barcodetextBox+".jpg");

                    }
                }

            }
        }

        private void deletebutton_Click(object sender, EventArgs e)
        {
            //delete
            string fname = codetextBox.Text + ".jpg";
            string folder = @"D:\IMG\CUSTOMER";
            string path = System.IO.Path.Combine(folder, fname);
            Image a = mypictureBox.Image;

            if (isvalid())
            {


                using (SqlConnection con = new SqlConnection(constrng))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM CUSTOMER WHERE CODE="+codetextBox.Text+"", con))
                    {

                    

                        // cmd.Parameters.AddWithValue("@PIC", path);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data has been Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        a.Save(path);

                        clearr();
                        disable();
                        //mypictureBox.Image.Save(@"\\D:\\IMG"+barcodetextBox+".jpg");

                    }
                }

            }
        }
    }
}
