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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
      //  private DataTable dt = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
           // dataGridView1.DataSource = getdata();
        }

        

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product frm = new Product();
            frm.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category ctr = new Category();
            ctr.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Brand brd = new Brand();
            brd.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Product frm = new Product();
            frm.ShowDialog();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Vendor v = new Vendor();
            v.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vendor v = new Vendor();
            v.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            customer c = new customer();
            c.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            customer c = new customer();
            c.ShowDialog();
        }

        private void toolStripSeparator2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CONTACT c = new CONTACT();
            c.ShowDialog();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            user u = new user();
            u.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            COMPANY c = new COMPANY();
            c.ShowDialog();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            purchase p = new purchase();
            p.ShowDialog();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            POSS p = new POSS();
            this.Hide();
            p.ShowDialog(); ;
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            Stock s = new Stock();
            s.ShowDialog();
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            POS p = new POS();
            p.ShowDialog();
        }

        private void billingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            billing b = new billing();
            b.ShowDialog();
        }

        private void voucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            voucher v = new voucher();
            v.ShowDialog();
        }
    }
}
