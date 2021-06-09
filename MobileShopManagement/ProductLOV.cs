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
   // public delegate void datasendhandler(string msg);
    
    public partial class ProductLOV : Form
    {
      
        
        public int SelectedText { get; set; }
        public bool updt { get; set; }
        //public event datasendhandler datasend;
        public ProductLOV()
        {
            InitializeComponent();
        }

        //public string mytext
        //{
        //    get { return mytext;}
        //}
       
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProductLOV_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = getdata();
           // this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private DataTable dt = new DataTable();
    private DataTable getdata()
    {
       
        string con = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
        using (SqlConnection cn = new SqlConnection(con))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT CODE,NAME,PRICE,QTY,VAT,DESCRIPTION FROM ITEM", cn))
            {
              //  cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                
                dt.Load(rd);
                cn.Close();
             }
            }
        return dt;

        }

    private void textBox3_TextChanged(object sender, EventArgs e)
    {
        DataView namv = dt.DefaultView;
        namv.RowFilter = "NAME LIKE '%"+textBox3.Text+"%'";
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        DataView brandv = dt.DefaultView;
        brandv.RowFilter = "Brand LIKE '%"+textBox1.Text+"%'";
    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {
        DataView catv = dt.DefaultView;
        catv.RowFilter = "Category LIKE '%" + textBox2.Text + "%'";
    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {

        GETLOV();
    }

    private void GETLOV()
    {
        // lov
        this.DialogResult = System.Windows.Forms.DialogResult.OK;
        SelectedText = Convert.ToChar(dataGridView1.SelectedRows[0].Cells[0].Value);
        updt = true;
        this.Hide();
    }

    private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        GETLOV();
    }




      
    }
}
