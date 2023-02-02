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
using Microsoft.Reporting.WinForms;
namespace BMIS
{
    public partial class frmReport : Form
    {

        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        
        string _captain;
        


        public frmReport()
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
        }

        private void frmReport_Load(object sender, EventArgs e)
        {

            
        }

        public void PreviewBlotter(String sql)
        {
            try
            {
                ReportDataSource rptDs;
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\rptBlotter.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();


                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                da.SelectCommand = new SqlCommand(sql, cn);
                da.Fill(ds.Tables["dtBlotter"]);

                rptDs = new ReportDataSource("DataSet1", ds.Tables["dtBlotter"]);
                reportViewer1.LocalReport.DataSources.Add(rptDs);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                cn.Close();
            }catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void GenerateBusinessReport(string _since, string _name, string _business, string _refno)
        {
            try
            {
                GetCaptain();

                ReportDataSource rpdts;
                this.reportViewer1.RefreshReport();
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\rptBusiness.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                da.SelectCommand = new SqlCommand("select * from tblDocument where refno = '" + _refno + "'", cn);
                da.Fill(ds.Tables["dtBusiness"]);

                ReportParameter pCaptain = new ReportParameter("pCaptain", _captain);
                ReportParameter pSince= new ReportParameter("pSince", _since);
                ReportParameter pMonth = new ReportParameter("pMonth", DateTime.Now.ToString("MMMM"));
                ReportParameter pDay = new ReportParameter("pDay", DateTime.Now.Day.ToString());
                ReportParameter pYear = new ReportParameter("pYear", DateTime.Now.Year.ToString());
                ReportParameter pAddress = new ReportParameter("pAddress", var._address);
                ReportParameter pName = new ReportParameter("pName", _name);
                ReportParameter pBusiness = new ReportParameter("pBusiness", _business);


                reportViewer1.LocalReport.SetParameters(pMonth);
                reportViewer1.LocalReport.SetParameters(pDay);
                reportViewer1.LocalReport.SetParameters(pYear);
                reportViewer1.LocalReport.SetParameters(pAddress);
                reportViewer1.LocalReport.SetParameters(pCaptain);

                rpdts = new ReportDataSource("DataSet1", ds.Tables["dtBusiness"]);
                reportViewer1.LocalReport.DataSources.Add(rpdts);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                cn.Close();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void GenerateClearancReport(string _address, string _name, string _refno)
        {
            try
            {
                GetCaptain();

                ReportDataSource rpdts;
                this.reportViewer1.RefreshReport();
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\rptClearance.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                da.SelectCommand = new SqlCommand("select * from tblDocument where refno = '" + _refno + "'", cn);
                da.Fill(ds.Tables["dtBusiness"]);

                ReportParameter pMonth = new ReportParameter("pMonth", DateTime.Now.ToString("MMMM"));
                ReportParameter pDay = new ReportParameter("pDay", DateTime.Now.Day.ToString());
                ReportParameter pYear = new ReportParameter("pYear", DateTime.Now.Year.ToString());
                ReportParameter pAddress = new ReportParameter("pAddress", _address);
                ReportParameter pCaptain = new ReportParameter("pCaptain", _captain);
                ReportParameter pName = new ReportParameter("pName", _name);

                reportViewer1.LocalReport.SetParameters(pMonth);
                reportViewer1.LocalReport.SetParameters(pDay);
                reportViewer1.LocalReport.SetParameters(pYear);
                reportViewer1.LocalReport.SetParameters(pAddress);
                reportViewer1.LocalReport.SetParameters(pCaptain);

                rpdts = new ReportDataSource("DataSet1", ds.Tables["dtBusiness"]);
                reportViewer1.LocalReport.DataSources.Add(rpdts);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                cn.Close();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void GetCaptain()
        {
            cn.Open();
            cm = new SqlCommand("select * from tblofficial where position like 'Counsiler 1'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                _captain = dr["name"].ToString();
            }
            dr.Close();
            cn.Close();
        }


        public void GenerateBuildingReport(string _name, string _purpose, string _refno, string _address)
        {
            try
            {
                GetCaptain();

                ReportDataSource rpdts;
                this.reportViewer1.RefreshReport();
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\rptBuilding.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                da.SelectCommand = new SqlCommand("select * from tblDocument where refno = '" + _refno + "'", cn);
                da.Fill(ds.Tables["dtBusiness"]);

                ReportParameter pName = new ReportParameter("paddress", _name);
                ReportParameter pPurpose = new ReportParameter("pPurpose", _purpose);
                ReportParameter pMonth = new ReportParameter("pMonth", DateTime.Now.ToString("MMMM"));
                ReportParameter pDay = new ReportParameter("pDay", DateTime.Now.Day.ToString());
                ReportParameter pYear = new ReportParameter("pYear", DateTime.Now.Year.ToString());
                ReportParameter pAddress = new ReportParameter("pAddress", var._address);
                ReportParameter pCaptain = new ReportParameter("pCaptain", _captain);

                reportViewer1.LocalReport.SetParameters(pName);
                reportViewer1.LocalReport.SetParameters(pPurpose);
                reportViewer1.LocalReport.SetParameters(pMonth);
                reportViewer1.LocalReport.SetParameters(pDay);
                reportViewer1.LocalReport.SetParameters(pYear);
                reportViewer1.LocalReport.SetParameters(pCaptain);

                rpdts = new ReportDataSource("DataSet1", ds.Tables["dtBusiness"]);
                reportViewer1.LocalReport.DataSources.Add(rpdts);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                cn.Close();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        internal void GenerateBuildingReport(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
