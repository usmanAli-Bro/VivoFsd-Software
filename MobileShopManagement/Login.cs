using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace MobileShopManagement
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(isvalidated()){

                try
                {

                    bool IsUserCorrect, IsPasswordCorrect;
                    Getislogincorrect(out IsUserCorrect, out  IsPasswordCorrect);
                    if(IsUserCorrect && IsPasswordCorrect){
                        this.Hide();
                        Main m = new Main();
                        m.ShowDialog();
                    }
                    else
                    {
                        if(!IsUserCorrect){
                            MessageBox.Show("User Not Valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            usernametextBox.Clear();
                            passwordtextBox.Clear();
                            usernametextBox.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Password Not Valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            usernametextBox.Clear();
                            passwordtextBox.Clear();
                            usernametextBox.Focus();
                        }
                    }
                }catch(AccessViolationException ex){

                    MessageBox.Show("Error" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

               
            }
          
        }

        private void Getislogincorrect(out bool IsUserCorrect, out bool IsPasswordCorrect)
        {
            //new NotImplementedException();
            //conn c = new conn();
            string con = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            using(SqlConnection cn = new SqlConnection(con)){
                using (SqlCommand cmd = new SqlCommand("USD_CHECKLOGIN",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    
                    // output variable
                    cmd.Parameters.Add("@IsUserCorrect", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@IsPasswordCorrect", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    //input paramiter
                    cmd.Parameters.AddWithValue("@UserName", usernametextBox.Text);
                    cmd.Parameters.AddWithValue("@Password", passwordtextBox.Text);
                    cmd.ExecuteNonQuery();
                    IsUserCorrect = (bool)cmd.Parameters["@IsUserCorrect"].Value;
                    IsPasswordCorrect = (bool)cmd.Parameters["@IsPasswordCorrect"].Value;
                }
            }
           
        }
    
        
        private bool isvalidated()
        {
            if(usernametextBox.Text.Trim() == string.Empty){
                usernametextBox.Clear();
                MessageBox.Show("User Name or Email is Required !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                usernametextBox.Focus();
                return false;
            }
            if(passwordtextBox.Text.Trim() == string.Empty){
                MessageBox.Show("Password Is Required !","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                passwordtextBox.Clear();
                passwordtextBox.Focus();
                return false;
            }
            return true;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main m = new Main();
            m.ShowDialog();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //usernametextBox.Enabled = false;
            usernametextBox.Text = "";
            usernametextBox.Focus();
            passwordtextBox.Focus();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           /// Application.Exit();
        }

        private void passwordtextBox_Enter(object sender, EventArgs e)
        {
          


            
        }
    }
}
