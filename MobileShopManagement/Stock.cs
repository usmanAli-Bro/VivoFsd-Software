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
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
        }

        private DataTable dt = new DataTable();
        private void Stock_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = getdata();
        }
       
        private DataTable getdata()
        {

            string con = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(con))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT CODE,NAME,PRICE,QTY,VAT,DESCRIPTION FROM ITEM", cn))
                {
                   
                    cn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    dt.Load(rd);
                    cn.Close();
                }
            }
            return dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DataView namv = dt.DefaultView;
            namv.RowFilter = "NAME LIKE '%" + textBox3.Text + "%'";
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if(Convert.ToInt16( row.Cells["QTY"].Value) < 10){
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }
    }
}
