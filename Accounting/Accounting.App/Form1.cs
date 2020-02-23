using Accounting.Business;
using Accounting.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            FrmCustomer frmCustomer = new FrmCustomer();
            frmCustomer.ShowDialog();
        }

        private void btNewAccount_Click(object sender, EventArgs e)
        {
            frmNewAccounting frmNewAccounting = new frmNewAccounting();
            frmNewAccounting.ShowDialog();
        }

        private void btnPaymentReport_Click(object sender, EventArgs e)
        {
            FrmReport frmReport = new FrmReport();
            frmReport.TypeID = 2;
            frmReport.ShowDialog();
        }

        private void btnReceiveReport_Click(object sender, EventArgs e)
        {
            FrmReport frmReport = new FrmReport();
            frmReport.TypeID = 1;
            frmReport.ShowDialog();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogin frmLogin = new FrmLogin();
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                lblDate.Text = DateTime.Now.ToShortDateString();
                lblTime.Text = DateTime.Now.ToString("HH:mm:ss");

                GetMonthReport();
            }
            else
                Application.Exit();
        }

        private void GetMonthReport()
        {
            ReportViewModel rp = Account.getReportForMonth();

            lblPay.Text = rp.Pay.ToString("#,0");
            lblReceive.Text = rp.Receive.ToString("#,0");
            lblRemain.Text = rp.Remain.ToString("#,0");
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void ChangePass_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.IsEdit = true;
            frmLogin.ShowDialog();
        }
    }
}
