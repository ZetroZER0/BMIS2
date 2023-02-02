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
    
    public partial class frmBuildingPermit : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        frmDocument f;
        public frmBuildingPermit(frmDocument f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
            this.f = f;
        }

        public void LoadRefNo()
        {
            try
            {
                cboRefNo.Items.Clear();
                cn.Open();
                cm = new SqlCommand("select refno from tblPayment where type like 'BUILDING CLEARANCE' and status like 'Pending' order by id desc", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    cboRefNo.Items.Add(dr[0].ToString());
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboRefNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("select * from tblpayment where refno like '" + cboRefNo.Text + "'", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    txtName.Text = dr["name"].ToString();
                }
                cn.Close();
            }catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //validation for empty entry
                if (MessageBox.Show("Do you want to save this record?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("update tblpayment set status = 'Complete' where refno like '" + cboRefNo.Text + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    cm = new SqlCommand("insert into tblDocument(refno, type, details1, details2, gdate, [user])values(@refno, @type, @details1, @details2, @gdate, @user)", cn);
                    cm.Parameters.AddWithValue("@refno", cboRefNo.Text);
                    cm.Parameters.AddWithValue("@type", "BUILDING CLEARANCE");
                    cm.Parameters.AddWithValue("@details1", txtPurpose.Text);
                    cm.Parameters.AddWithValue("@details2", txtName.Text);
                    cm.Parameters.AddWithValue("@gdate", DateTime.Now);
                    cm.Parameters.AddWithValue("@user", var._user);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    f._name = txtName.Text;

                    //f._refno = cboRefNo.Text;
                    //f._purpose = txtPurpose.Text;
                    MessageBox.Show("Record has been saved!", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.LoadBuildingClearance();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmBuildingPermit_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
