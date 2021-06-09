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
using System.Text.RegularExpressions;

namespace MobileShopManagement
{
    public partial class POSS : Form
    {
        public POSS()
        {
            InitializeComponent();
        }
        float nettoal = 0;
        Regex rg = new Regex(@"[0-9]+$");
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool found = false;
            if(dataGridView1.Rows.Count > 0)
            {
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if(codetextBox.Text.ToString()==row.Cells[0].Value.ToString())
                    {
                        row.Cells[3].Value = Convert.ToSingle(Convert.ToSingle(row.Cells[3].Value.ToString()) + Convert.ToSingle(qtytextBox.Text.ToString()));
                        nettoal += Convert.ToSingle(totaltextBox.Text.ToString());
                        row.Cells[5].Value = Convert.ToSingle(Convert.ToSingle(row.Cells[3].Value.ToString())*Convert.ToSingle(row.Cells[2].Value.ToString()));
                        NettotalrichTextBox6.Text = nettoal.ToString();
                        found = true;
                    }
                } 
                
            }
            if (!found)
            {
                dataGridView1.Rows.Add(codetextBox.Text,itemnametextBox.Text, pricetextBox.Text, qtytextBox.Text, "0.00", totaltextBox.Text);
                nettoal += Convert.ToSingle(totaltextBox.Text.ToString());
                NettotalrichTextBox6.Text = nettoal.ToString();
            }
        }

        private void POSS_Load(object sender, EventArgs e)
        {
           
            invoice();
            barcodetextBox.Focus();
           
        }

        private DataTable invoice()
        {
            string constring = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("select [dbo].[POS_NEXT_INVOICE]()",con))
                {
                    con.Open();
                    object obj = cmd.ExecuteScalar();
                    invoicelabel.Text = obj.ToString();
                    con.Close();
                }
            }
            return dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Main m = new Main();
            m.Show();
            this.Close();
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void totaltextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void qtytextBox_TextChanged(object sender, EventArgs e)
        {
            //totaltextBox.Text = Convert.ToString( Convert.ToSingle(pricetextBox.Text) * Convert.ToSingle(pricetextBox.Text));
            if(qtytextBox.Text.Trim()!=string.Empty)
            {
                if(rg.Match(qtytextBox.Text).Success)
                {
                    float qty = Convert.ToSingle(qtytextBox.Text);
                    float price = Convert.ToSingle(pricetextBox.Text);
                    float total = Convert.ToSingle(qty * price);
                    totaltextBox.Text = total.ToString("###########.##");
                }
                else { qtytextBox.SelectAll(); }
            }
            else { totaltextBox.Text = "0.00"; }
        }

        private void itemnametextBox_TextChanged(object sender, EventArgs e)
        {
            //ProductLOV lv = new ProductLOV();
            //lv.ShowDialog();
        }

        private void qtytextBox_Validated(object sender, EventArgs e)
        {
            totaltextBox.Text = Convert.ToString(Convert.ToSingle(pricetextBox.Text) * Convert.ToSingle(pricetextBox.Text));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if(e.RowIndex !=-1 && e.ColumnIndex !=-1){
                if(e.ColumnIndex == 6){
                    nettoal -= Convert.ToSingle(row.Cells[5].Value.ToString());
                    NettotalrichTextBox6.Text = nettoal.ToString();
                    dataGridView1.Rows.Remove(row);
                }
            }
            
        }
    }
}
