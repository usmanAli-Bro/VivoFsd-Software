using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace MobileShopManagement
{
   
    
    public partial class Product : Form
    {


        private int id;
        public int ID 
        {
            get { return id; }
            set { id = value; }
        }
        bool isupdated = false;
      
        private string constring = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
        public Product()
        {
            InitializeComponent();
           
        }
      

        private void Product_Load(object sender, EventArgs e)
        {
            //LOAD

          
          //  lv.datasend += datasend;
            // NEXTCODE
           
           
           //GET CATEGORY
            catcomboBox.DataSource = getcat();
            catcomboBox.ValueMember = "ID";
            catcomboBox.DisplayMember = "NAME";
            //GET BRAND
            brandcomboBox.DataSource = getbrand();
            brandcomboBox.DisplayMember = "NAME";
            brandcomboBox.ValueMember = "CODE";
        }

        private void datasend(string msg)
        {
            //
        }

        private DataTable getbrand()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM BRAND", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    con.Close();
                }
            }
            return dt;
        }

        private DataTable getcat()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM CATE", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    con.Close();
                }
            }
            return dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newbutton_Click(object sender, EventArgs e)
        {
            CEALRDATA();
            using (SqlConnection c = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("select [dbo].NEXTITEMID()", c))
                {
                    cmd.CommandType = CommandType.Text;
                    c.Open();
                    object obj = cmd.ExecuteScalar();
                    this.barcodetextBox.Text = obj.ToString();
                    c.Close();
                }
            }
        }

        private void CEALRDATA()
        {
            barcodetextBox.Clear();
            nametextBox.Clear();
            descrichTextBox.Clear();
            costtextBox.Clear();
            pricetextBox.Clear();
            vattextBo.Clear();
            discounttextBox.Clear();
            stocktextBox.Clear();
           
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            string fname = barcodetextBox.Text+".jpg";
            string folder = @"D:\\IMG";
            string path = System.IO.Path.Combine(folder,fname);
            Image a = mypictureBox.Image;
           
            if(isvalid()){
                
              
                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERTPRODUCT", con))
                    {
                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BARCODE", barcodetextBox.Text);
                        cmd.Parameters.AddWithValue("@NAME",nametextBox.Text);
                        cmd.Parameters.AddWithValue("@CAT", catcomboBox.SelectedValue);
                        cmd.Parameters.AddWithValue("@BRAND", brandcomboBox.SelectedValue);
                        cmd.Parameters.AddWithValue("@COST",costtextBox.Text);
                        cmd.Parameters.AddWithValue("@RICE", pricetextBox.Text);
                        cmd.Parameters.AddWithValue("@QTY", stocktextBox.Text);
                        cmd.Parameters.AddWithValue("@DESCRIPTION",descrichTextBox.Text);
                        cmd.Parameters.AddWithValue("@VAT",vattextBo.Text);
                        cmd.Parameters.AddWithValue("@PIC", path);
                        cmd.Parameters.AddWithValue("@DISCOUNT", discounttextBox.Text);
                       
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data has been Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        a.Save(path);
                        //mypictureBox.Image.Save(@"\\D:\\IMG"+barcodetextBox+".jpg");
                        CEALRDATA();
                    }
                }
               
            }
        }

        private bool isvalid()
        {
            if(nametextBox.Text.Trim()==string.Empty || descrichTextBox.Text.Trim()==string.Empty){
                MessageBox.Show("Pleas Fill All The Fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nametextBox.Clear();
                nametextBox.Focus();
                return false;
            }
            else
            {
                return true;
            }
            
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            OpenFileDialog open = new OpenFileDialog();
          //  open.Filter = "image File(*.jpg)|*.jpg|All Files(*.*)|*.*";
            open.ShowDialog();
            mypictureBox.Image = Image.FromFile(open.FileName);
           

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string folder = @"D:\\IMG";

            string path = System.IO.Path.Combine(folder, barcodetextBox.Text + ".jpg");
            Image a = mypictureBox.Image;
            a.Save(@"path");
           // a.Save(@"C:\\Users\\usman\\Desktop" + barcodetextBox.Text + ".jpg");
            mypictureBox.Image.Save(@"C:\\Users\\usman\\Desktop" + barcodetextBox.Text + ".jpg");

        }

        private void getdatabutton_Click(object sender, EventArgs e)
        {
            using( ProductLOV lv = new ProductLOV()){
                if( lv.ShowDialog()== System.Windows.Forms.DialogResult.OK){
                    //this.idtextBox.Text = lv.SelectedText;
                    this.ID = lv.SelectedText;
                   // this.idtextBox.Text = lv.SelectedText.ToString();
                    this.isupdated = lv.updt;
                }
            }

            
          //  int id = 0;// Convert.ToInt16(idtextBox.Text);
            if (isupdated)
            {
                // id = Convert.ToInt16(idtextBox.Text);
                DataTable getdata = getdatabyid(this.id);
                DataRow row = getdata.Rows[0];
               // barcodetextBox.Text = row["BARCODE"].ToString();
                barcodetextBox.Text = row["BARCODE"].ToString();
                nametextBox.Text = row["NAME"].ToString();
                catcomboBox.SelectedValue = row["CAT"];
                brandcomboBox.SelectedValue = row["BRAND"];
                descrichTextBox.Text = row["DESCRIPTION"].ToString();
                discounttextBox.Text = row["DISCOUNT"].ToString();
                costtextBox.Text = row["COST"].ToString();
                pricetextBox.Text = row["PRICE"].ToString();
                stocktextBox.Text = row["QTY"].ToString();
                vattextBo.Text = row["VAT"].ToString();
             
                mypictureBox.Image = Image.FromFile(row["PIC"].ToString());

            }
            
        }
        
        private DataTable getdatabyid(int id)
        {

            DataTable dt = new DataTable();
            string str = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            using(SqlConnection con = new SqlConnection(str)){
                using (SqlCommand cmd = new SqlCommand("select * from ITEM where CODE=" + id + "", con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    con.Close();
                   
                }
            }
            return dt;
        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            //UPDATEITEM
            if (isvalid())
            {
               

                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATEITEM", con))
                    {
                        string folder = @"D:\\IMG";

                        string path = System.IO.Path.Combine(folder, barcodetextBox.Text + ".jpg");
                        Image a = mypictureBox.Image;
                        a.Save(@"path");
                        a.Save(@"C:\\Users\\usman\\Desktop" + barcodetextBox.Text + ".jpg");
                        mypictureBox.Image.Save(@"C:\\Users\\usman\\Desktop" + barcodetextBox.Text + ".jpg");
                       // cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CODE", this.id);
                        cmd.Parameters.AddWithValue("@BARCODE", barcodetextBox.Text);
                        cmd.Parameters.AddWithValue("@NAME", nametextBox.Text);
                        cmd.Parameters.AddWithValue("@CAT", catcomboBox.SelectedValue);
                        cmd.Parameters.AddWithValue("@BRAND", brandcomboBox.SelectedValue);
                        cmd.Parameters.AddWithValue("@COST", costtextBox.Text);
                        cmd.Parameters.AddWithValue("@PRICE", pricetextBox.Text);
                        cmd.Parameters.AddWithValue("@QTY", stocktextBox.Text);
                        cmd.Parameters.AddWithValue("@DESCRIPTION", descrichTextBox.Text);
                   
                        cmd.Parameters.AddWithValue("@VAT", vattextBo.Text);
                        cmd.Parameters.AddWithValue("@PIC", path);
                        cmd.Parameters.AddWithValue("@DISCOUNT", discounttextBox.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data has been Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      
                        //mypictureBox.Image.Save(@"\\D:\\IMG"+barcodetextBox+".jpg");
                        CEALRDATA();
                    }
                }

            }
        }

        private void deletebutton_Click(object sender, EventArgs e)
        {
            if (isvalid())
            {


                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM ITEM WHERE CODE=@CODE", con))
                    {

                        // cmd.CommandType = CommandType.StoredProcedure;
                      

                        cmd.Parameters.AddWithValue("@CODE", this.id);
                      

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Data has been Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //mypictureBox.Image.Save(@"\\D:\\IMG"+barcodetextBox+".jpg");
                        CEALRDATA();
                    }
                }

            }
        }

       
    }
}
