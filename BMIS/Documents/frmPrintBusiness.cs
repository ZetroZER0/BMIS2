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
    public partial class frmPrintBusiness : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        frmDocument f;
        public frmPrintBusiness(frmDocument f)
        {
            InitializeComponent();
            this.f = f;
            cn = new SqlConnection(dbconstring.connection);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    //validation for empty entry
                    if(MessageBox.Show("Do you want to save this record?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("update tblpayment set status = 'Complete' where refno like '" + cboRefNo.Text + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();

                        cn.Open();
                        cm = new SqlCommand("insert into tblDocument (refno, type, details1, details2, details3, gdate, [user])values(@refno, @type, @details1, @details2, @details3, @gdate, @user)", cn);
                        cm.Parameters.AddWithValue("@refno", cboRefNo.Text);
                        cm.Parameters.AddWithValue("@type", "BARANGAY BUSINESS CLEARANCE");
                        cm.Parameters.AddWithValue("@details1", txtEstablishment.Text);
                        cm.Parameters.AddWithValue("@details2", txtOwner.Text);
                        cm.Parameters.AddWithValue("@details3", txtSince.Text);
                        cm.Parameters.AddWithValue("@gdate", DateTime.Now);
                        cm.Parameters.AddWithValue("@user", var._user);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        //f._refno = cboRefNo.Text;
                        //f._since = txtSince.text;
                        //f._business =  txtEstablishment.Text;

                        MessageBox.Show("Record has been saved!", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //f.GenerateBusinessReport();
                        f.LoadBrgyBusinessClearance();
                        this.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    cn.Close();
                    MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void LoadRefNo()
        {
            try
            {
                cboRefNo.Items.Clear();
                cn.Open();
                cm = new SqlCommand("select refno from tblPayment where type like 'BARANGAY BUSINESS CLEARANCE' and status like 'Pending' order by id desc", cn);
                dr = cm.ExecuteReader();
                while (dr.Read()) 
                {
                    cboRefNo.Items.Add(dr[0].ToString());
                }
                dr.Close();
                cn.Close();
            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboRefNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("select * from tblPayment where refno =@refno", cn);
                cm.Parameters.AddWithValue("@refno", cboRefNo.Text);
                dr.Read(); 
                if (dr.HasRows)
                {
                    txtOwner.Text = dr["name"].ToString();
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
