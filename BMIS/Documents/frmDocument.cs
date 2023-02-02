using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
namespace BMIS
{
    public partial class frmDocument : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        public string _refno;
        string _captain;
        public string _name;
        public string _since;
        public string _business;
        public string _purpose;

        public frmDocument()
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmDocument_Load(object sender, EventArgs e)
        {

            
        }

        public void LoadBrgyBusinessClearance()
        {
            try
            {
                dataGridView1.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("select * from tblDocument where type like 'BARANGAY BUSINESS CLEARANCE' and gdate = cast(getdate() as date)",cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr["id"].ToString(), dr["refno"].ToString(), dr["details1"].ToString(), dr["details2"].ToString(), dr["details3"].ToString(), DateTime.Parse(dr["gdate"].ToString()).ToShortDateString(), dr["user"].ToString()); 
                }
                cn.Close();
                dr.Close();
                dataGridView1.ClearSelection();
            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadBrgyClearance()
        {
            try
            {
                dataGridView2.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("select * from tblDocument where type like 'BARANGAY CLEARANCE' and gdate = cast(getdate() as date)", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView2.Rows.Add(dr["id"].ToString(), dr["refno"].ToString(), dr["details2"].ToString(), dr["details1"].ToString(), DateTime.Parse(dr["gdate"].ToString()).ToShortDateString(), dr["user"].ToString());
                }
                cn.Close();
                dr.Close();
                dataGridView2.ClearSelection();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadBuildingClearance()
        {
            try
            {
                dataGridView3.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("select * from tblDocument where type like 'BUILDING CLEARANCE' and gdate = cast(getdate() as date)", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView3.Rows.Add(dr["id"].ToString(), dr["refno"].ToString(), dr["details2"].ToString(), dr["details1"].ToString(), DateTime.Parse(dr["gdate"].ToString()).ToShortDateString(), dr["user"].ToString());
                }
                cn.Close();
                dr.Close();
                dataGridView3.ClearSelection();
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

        private void button2_Click(object sender, EventArgs e)
        {
            frmPrintBusiness f = new frmPrintBusiness(this);
            f.LoadRefNo();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmPrintBrgyClearance f = new frmPrintBrgyClearance(this);
            f.LoadRefNo();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmBuildingPermit f = new frmBuildingPermit(this);
            f.LoadRefNo();
            f.ShowDialog();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dataGridView1.Columns[e.ColumnIndex].Name;
                if (colName == "btnPrint1")
                {
                    frmReport f = new frmReport();
                    f.GenerateBusinessReport(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    f.ShowDialog();
                }
            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dataGridView2.Columns[e.ColumnIndex].Name;
                if (colName == "btnPrint2")
                {
                    frmReport f = new frmReport();
                    f.GenerateClearancReport(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString(), dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString(), dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString());
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dataGridView3.Columns[e.ColumnIndex].Name;
                if (colName == "btnPrint3")
                {
                    frmReport f = new frmReport();
                    f.GenerateBuildingReport(dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString(), dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString(), dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString());
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

}
