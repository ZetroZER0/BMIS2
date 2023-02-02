using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMIS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int y = Screen.PrimaryScreen.Bounds.Height;
            int x = Screen.PrimaryScreen.Bounds.Width;
            this.Height = y - 40;
            this.Width = x;
            this.Left = 0;
            this.Top = 0;
        }

        private void btnDocument_Click(object sender, EventArgs e)
        {
            frmDocument f = new frmDocument();
            f.TopLevel = false;
            panel5.Controls.Add(f);
            f.LoadBrgyBusinessClearance();
            f.LoadBrgyClearance();
            f.LoadBuildingClearance();
            f.BringToFront();
            f.Show();
        }

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            frmMaintenance f = new frmMaintenance();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.LoadRecord();
            f.LoadPurok();
            f.BringToFront();
            f.Show();
        }

        private void btnResident_Click(object sender, EventArgs e)
        {
            frmResidentList f = new frmResidentList();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.BringToFront();
            f.Loadrecords();
            f.LoadVaccination();
            f.LoadHead();
            f.Show();

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            frmIssue f = new frmIssue();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.LoadRecords();
            f.BringToFront();
            f.Show();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
           frmPaymentList f = new frmPaymentList();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.LoadRecords();
            f.BringToFront();
            f.Show();
        }
    }
}
