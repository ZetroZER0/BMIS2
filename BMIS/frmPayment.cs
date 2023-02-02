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
namespace BMIS
{
    public partial class frmPayment : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        Random rnd;
        frmPaymentList f;
        public frmPayment(frmPaymentList f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
            this.f = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Clear()
        {
            GetRefrenceNo();
            txtAmount.Clear();
            txtName.Clear();
            cboType.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to save this payment?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("insert into tblpayment (refno, name, type, amount, sdate, suser)values(@refno, @name, @type, @amount, @sdate, @suser)", cn);
                    cm.Parameters.AddWithValue("@refno", lblRefNo.Text);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@type", cboType.Text);
                    cm.Parameters.AddWithValue("@amount", double.Parse(txtAmount.Text));
                    cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cm.Parameters.AddWithValue("@suser", var._user);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully saved!", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.LoadRecords();
                    Clear();
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GetRefrenceNo();
            }
        }
        public void GetRefrenceNo()
        {
            rnd = new Random();
            lblRefNo.Text = DateTime.Now.ToString("yyyyMMdd-") + rnd.Next(11111, 99999);
        } 
    }
}
