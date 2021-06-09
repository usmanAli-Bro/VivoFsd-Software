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
using System.Text.RegularExpressions;

namespace MobileShopManagement
{
    public partial class purchase : Form
    {
        public purchase()
        {
            InitializeComponent();
        }
        float nettotal = 0;
        Regex rg = new Regex(@"[0-9]+$");
        public int idval { get; set; }
        public bool isupdated { get; set; }
        string con = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        // bool isupdated = false;
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool found = false;
           // int totall float=0;
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (ProductidtextBox.Text.ToString() == row.Cells[1].Value.ToString())
                    {
                        row.Cells[3].Value = Convert.ToSingle(Convert.ToInt16(qtytextBox.Text.ToString()) + Convert.ToInt16(row.Cells[3].Value.ToString()));
                        nettotal += Convert.ToSingle(row.Cells[4].Value);
                        row.Cells[5].Value = Convert.ToSingle(Convert.ToInt16(row.Cells[3].Value.ToString()) * Convert.ToInt16(row.Cells[4].Value.ToString()));
                       // totall = Convert.ToSingle(row.Cells[5].Value);
                        
                        gttotallabel.Text = nettotal.ToString();
                        cleargrid();
                        found = true;
                    }

                }
               
            }
            if (!found)
            {

                dataGridView1.Rows.Add(CODEtextBox.Text, ProductidtextBox.Text, pnametextBox.Text, qtytextBox.Text, pricetextBox.Text, totallabel.Text);
                nettotal += Convert.ToSingle(totallabel.Text);
                gttotallabel.Text = nettotal.ToString();
                cleargrid();
            }

        }

        private void cleargrid()
        {
            ProductidtextBox.Clear();
            pnametextBox.Clear();
            pricetextBox.Clear();
            qtytextBox.Clear();

            ProductidtextBox.Focus();
        }
        private void purchase_Load(object sender, EventArgs e)
        {
            disable();
        }

        private void disable()
        {
            CODEtextBox.Enabled = false;
            SUPPLIERIDtextBox.Enabled = false;
            SUPPLIERNAMEtextBox.Enabled = false;
            richTextBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            ProductidtextBox.Enabled = false;
            pnametextBox.Enabled = false;
            qtytextBox.Enabled = false;
            pricetextBox.Enabled = false;
            //  totaltextBox.Enabled = false;
            //TOTALPAYtextBox.Enabled = false;
            //TOTALDUStextBox.Enabled = false;
            //grandtextBox.Enabled = false;

        }
        private string constring = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;

        private void button13_Click(object sender, EventArgs e)
        {
            button12.Enabled = true;
            SUPPLIERIDtextBox.Clear();
            SUPPLIERNAMEtextBox.Clear();
            dataGridView1.Rows.Clear();
            //MessageBox.Show("Data has been Saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            gttotallabel.Text = "0.00";
          //  newinvoice();
            SUPPLIERIDtextBox.Focus();
            newinvoice();
        }

        private void newinvoice()
        {
            Enable();
            using (SqlConnection c = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT [dbo].[PUR_NEXTVAL]()", c))
                {
                    cmd.CommandType = CommandType.Text;
                    c.Open();
                    object obj = cmd.ExecuteScalar();
                    this.CODEtextBox.Text = obj.ToString();
                    c.Close();
                }
            }
            SUPPLIERIDtextBox.Focus();
        }

        private void Enable()
        {
            CODEtextBox.Enabled = true;
            SUPPLIERIDtextBox.Enabled = true;
            SUPPLIERNAMEtextBox.Enabled = true;
            richTextBox1.Enabled = true;
            dateTimePicker1.Enabled = true;
            ProductidtextBox.Enabled = true;
            pnametextBox.Enabled = true;
            qtytextBox.Enabled = true;
            pricetextBox.Enabled = true;
            // totaltextBox.Enabled = true;
            //TOTALPAYtextBox.Enabled = true;
            //TOTALDUStextBox.Enabled = true;
            //grandtextBox.Enabled = true;
        }

        private DataTable getinfobyID(int idval)
        {
            string constrng = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection(constrng))
            {
                using (SqlCommand cmd = new SqlCommand("GET_VENDOR", cnn))
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
        private void button6_Click(object sender, EventArgs e)
        {
            using (VendorLOV lv = new VendorLOV())
            {
                if (lv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.idval = lv.idval;
                    this.isupdated = lv.isupdated;
                    //this.label14.Text = lv.idval.ToString();
                    //this.label15.Text = this.idval.ToString();
                }
            }

            if (isupdated)
            {

                DataTable getdatabyID = getinfobyID(this.idval);
                DataRow row = getdatabyID.Rows[0];
                SUPPLIERIDtextBox.Text = row["CODE"].ToString();
                SUPPLIERNAMEtextBox.Text = row["NAME"].ToString();


            }


        }

        private DataTable getdatabyid(int id)
        {

            DataTable dt = new DataTable();
            string str = ConfigurationManager.ConnectionStrings["mob"].ConnectionString;
            using (SqlConnection con = new SqlConnection(str))
            {
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
        private void button7_Click(object sender, EventArgs e)
        {
            // get product
            using (ProductLOV lv = new ProductLOV())
            {
                if (lv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //this.idtextBox.Text = lv.SelectedText;
                    this.ID = lv.SelectedText;
                    // this.idtextBox.Text = lv.SelectedText.ToString();
                    this.isupdated = lv.updt;
                }
            }


            //
            if (isupdated)
            {
                // id = Convert.ToInt16(idtextBox.Text);
                DataTable getdata = getdatabyid(this.id);
                DataRow row = getdata.Rows[0];
                // barcodetextBox.Text = row["BARCODE"].ToString();
                ProductidtextBox.Text = row["CODE"].ToString();
                pnametextBox.Text = row["NAME"].ToString();
                pricetextBox.Text = row["PRICE"].ToString();
                qtytextBox.Focus();

            }



        }

        private void pricetextBox_TextChanged(object sender, EventArgs e)
        {
            //long total = Convert.ToInt16(qtytextBox.Text) * Convert.ToInt16(pricetextBox.Text);
            //totaltextBox.Text = total.ToString();
        }

        private void totaltextBox_TextChanged(object sender, EventArgs e)
        {
            //total price
            //if(totaltextBox.Text != "")
            //{
            //    if(rg.Match(qtytextBox.Text).Success){
            //        float price, qty, total;
            //        price = Convert.ToSingle(pricetextBox.Text);
            //        qty = Convert.ToSingle(qtytextBox.Text);
            //       // total = Convert.ToSingle(totaltextBox.Text);
            //        total = price * qty;
            //       // totaltextBox.Text = total.ToString();
            //    }
            //}
        }

        private void qtytextBox_TextChanged(object sender, EventArgs e)
        {
            //total price
            if (qtytextBox.Text != "")
            {
                if (rg.Match(qtytextBox.Text).Success)
                {
                    float price, qty, total;
                    price = Convert.ToSingle(pricetextBox.Text);
                    qty = Convert.ToSingle(qtytextBox.Text);

                    total = price * qty;
                    // totaltextBox.Text = total.ToString("###.#");
                    totallabel.Text = total.ToString("###########.##");
                }
                else { qtytextBox.SelectAll(); }
            }
            else { totallabel.Text = "0.00"; }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            //total price
            if (totallabel.Text != "")
            {
                if (rg.Match(qtytextBox.Text).Success)
                {
                    float price, qty, total;
                    price = Convert.ToSingle(pricetextBox.Text);
                    qty = Convert.ToSingle(qtytextBox.Text);
                    // total = Convert.ToSingle(totaltextBox.Text);
                    total = price * qty;
                    // totaltextBox.Text = total.ToString("###########.##");
                }
                else
                {
                    qtytextBox.SelectAll();
                }
            }
            else
            {
                totallabel.Text = "0.00";
            }
        }

        private void SUPPLIERIDtextBox_Enter(object sender, EventArgs e)
        {



        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (e.ColumnIndex == 6)
                {
                    nettotal -= Convert.ToSingle(row.Cells["Total"].Value.ToString());
                    gttotallabel.Text = nettotal.ToString();
                    dataGridView1.Rows.Remove(row);
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // SAVE DATA DB


            // SAVING IN PURCHASE MASTER 
            {
                using (SqlConnection c = new SqlConnection(con))
                {
                    string cmds = "INSERT INTO PUR_MST(INV,DATES,VID,NAME,DESCRIPTION,TOTAL) VALUES (@inv,@date,@vid,@NAME,@desc,@TOTAL)";

                    using (SqlCommand cmd = new SqlCommand(cmds, c))
                    {
                        cmd.Parameters.AddWithValue("@inv", CODEtextBox.Text);
                        cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@vid", SUPPLIERIDtextBox.Text);
                        cmd.Parameters.AddWithValue("@NAME", SUPPLIERNAMEtextBox.Text);
                        cmd.Parameters.AddWithValue("@desc", richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@TOTAL", gttotallabel.Text);
                        c.Open();
                        cmd.ExecuteNonQuery();
                       
                       // MessageBox.Show("Data has been added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
                //string cmd = "INSERT INTO PUR_MST(INV,DATES,VID,DESCRIPTION) VALUES (" + CODEtextBox.Text + "," + dateTimePicker1.Value + "," + SUPPLIERIDtextBox.Text + "," + richTextBox1.Text + ")";
                //SqlDataAdapter ad = new SqlDataAdapter(cmd, constring);
                //DataSet ds = new DataSet();
                //ad.Fill(ds);
                //}
                // SAVING IN PURCHASE DETAIL 
                {
                    try 
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            using (SqlConnection c = new SqlConnection(con))
                            {
                                string cmds = "INSERT INTO PUR_DTL(INV,ITEMCODE,QTY,PRICE,TOTAL,NAME) VALUES (@inv,@ITEMCODE,@QTY,@PRICE,@TOTAL,@NAME)";

                                using (SqlCommand cmd = new SqlCommand(cmds, c))
                                {
                                    cmd.Parameters.AddWithValue("@inv", dataGridView1.Rows[i].Cells["invoice"].Value);
                                    cmd.Parameters.AddWithValue("@ITEMCODE", dataGridView1.Rows[i].Cells["CODE"].Value);
                                    cmd.Parameters.AddWithValue("@QTY", dataGridView1.Rows[i].Cells["QTY"].Value);
                                    cmd.Parameters.AddWithValue("@PRICE", dataGridView1.Rows[i].Cells["Price"].Value);
                                    cmd.Parameters.AddWithValue("@TOTAL", dataGridView1.Rows[i].Cells["Total"].Value);
                                    cmd.Parameters.AddWithValue("NAME", dataGridView1.Rows[i].Cells["NAME"].Value.ToString());
                                    c.Open();
                                    cmd.ExecuteNonQuery();
                                   // c.Close();
                                   // MessageBox.Show("Data has been added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                            }
                           
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                   
                }
            // update stock 
                {

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string cmdstring = "UPDATE ITEM SET QTY=QTY+" + dataGridView1.Rows[i].Cells["QTY"].Value + " WHERE CODE=" + dataGridView1.Rows[i].Cells["CODE"].Value + " ";
                        using(SqlConnection c = new SqlConnection(con))
                        {
                            using (SqlCommand cmd = new SqlCommand(cmdstring, c))
                            {
                               // cmd.Parameters.AddWithValue("@CODE", dataGridView1.Rows[i].Cells["CODE"].Value);
                                cmd.Parameters.AddWithValue("@QTY", dataGridView1.Rows[i].Cells["QTY"].Value);
                                c.Open();
                                cmd.ExecuteNonQuery();
                                c.Close();
                            }
                        }
                        
                       
                    }

                }
            //CLEAING
                {
                    SUPPLIERIDtextBox.Clear();
                    SUPPLIERNAMEtextBox.Clear();
                    dataGridView1.Rows.Clear();
                    MessageBox.Show("Data has been Saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gttotallabel.Text = "0.00";
                    newinvoice();
                    SUPPLIERIDtextBox.Focus();
                }


            }

        private void button14_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                button12.Enabled = false;

                // reload data PUR_MAST
                {
                    string cmdstring = "SELECT * FROM PUR_MST WHERE INV=" + invtextBox.Text + "";
                    SqlDataAdapter da = new SqlDataAdapter(cmdstring, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    CODEtextBox.Text = ds.Tables[0].Rows[0]["INV"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["DATES"].ToString());
                    SUPPLIERIDtextBox.Text = ds.Tables[0].Rows[0]["VID"].ToString();
                    SUPPLIERNAMEtextBox.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                    richTextBox1.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                    gttotallabel.Text = ds.Tables[0].Rows[0]["TOTAL"].ToString();
                }


                // reload data PUR_DTL
                {
                    string cmdstring = "SELECT * FROM PUR_DTL WHERE INV=" + invtextBox.Text + "";
                    SqlDataAdapter da = new SqlDataAdapter(cmdstring, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    // CODEtextBox.Text = ds.Tables[0].Rows[0]["INV"].ToString();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(ds.Tables[0].Rows[i]["INV"].ToString(), ds.Tables[0].Rows[i]["ITEMCODE"].ToString(), ds.Tables[0].Rows[i]["NAME"].ToString(), ds.Tables[0].Rows[i]["QTY"].ToString(), ds.Tables[0].Rows[i]["PRICE"].ToString(), ds.Tables[0].Rows[i]["TOTAL"].ToString());
                        //dataGridView1.Rows.Add(CODEtextBox.Text, ProductidtextBox.Text, pnametextBox.Text, qtytextBox.Text, pricetextBox.Text, totallabel.Text);
                    }
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Invoice Is Not Correct", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
           

        }

        private void button11_Click(object sender, EventArgs e)
        {
            // update stock 
            {

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string cmdstring = "UPDATE ITEM SET QTY= QTY - " + dataGridView1.Rows[i].Cells["QTY"].Value.ToString() + " WHERE CODE=" + dataGridView1.Rows[i].Cells["CODE"].Value.ToString() + " ";
                    using (SqlConnection c = new SqlConnection(con))
                    {
                        using (SqlCommand cmd = new SqlCommand(cmdstring, c))
                        {
                            // cmd.Parameters.AddWithValue("@CODE", dataGridView1.Rows[i].Cells["CODE"].Value);
                            //cmd.Parameters.AddWithValue("@QTY", dataGridView1.Rows[i].Cells["QTY"].Value);
                            c.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("working" + dataGridView1.Rows[i].Cells["CODE"].Value.ToString() + "", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            c.Close();
                          
                        }
                    }


                }

            }

            // DELETE FROM  PUR_DTL
            {
                string cmdstring = "DELETE  FROM PUR_DTL WHERE INV=" + CODEtextBox.Text + "";
                SqlDataAdapter da = new SqlDataAdapter(cmdstring, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
               
            }

            // DELETE FROM  PUR_MST
            {
                string cmdstring = "DELETE  FROM PUR_MST WHERE INV=" + CODEtextBox.Text + "";
                SqlDataAdapter da = new SqlDataAdapter(cmdstring, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

            }

          

            SUPPLIERIDtextBox.Clear();
            SUPPLIERNAMEtextBox.Clear();
            dataGridView1.Rows.Clear();
           // MessageBox.Show("Data has been Saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            gttotallabel.Text = "0.00";
            MessageBox.Show("Data has been Deleted", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SUPPLIERIDtextBox.Focus();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ProductidtextBox_TextChanged(object sender, EventArgs e)
        {

        }



        }
    }
