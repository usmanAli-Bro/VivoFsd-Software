using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobileShopManagement
{
    public partial class customerLOV : Form
    {
        public int idval { get; set; }
        public bool isupdated { get; set; }
        public customerLOV()
        {
            InitializeComponent();
        }

       
        private void customerLOV_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = getdata();
        }
        private DataTable dt = new DataTable();
        private DataTable getdata()
        {

            string constring = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("CUSTOMERLIST", con))
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "NAME LIKE'%" + textBox3.Text + "%'";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.idval = Convert.ToInt16(dataGridView1.SelectedRows[0].Cells[0].Value);
            this.isupdated = true;
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
