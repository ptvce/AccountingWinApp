using Accounting.DataLayer.Context;
using Accounting.Utilities;
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
    public partial class FrmReport : Form
    {
        public int TypeID = 0;
        UnitOfWork db = new UnitOfWork();
        public FrmReport()
        {
            InitializeComponent();
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
            if (TypeID == 1)
                this.Text = "Receive Report";
            else if (TypeID == 2)
                this.Text = "Payment Report";

            List<ListCustomerViewModel> lst = new List<ListCustomerViewModel>();
            lst.Add(new ListCustomerViewModel() {
                CustomerID = 0,
                FullName = "انتخاب کنید",
            });
            lst.AddRange(db.CustomerRepository.GetNameCustomers());
            cbCustomer.DataSource = lst;
            cbCustomer.ValueMember = "CustomerID";
            cbCustomer.DisplayMember = "FullName";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Filter();
        }

        void Filter()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                List<DataLayer.Accounting> result = new List<DataLayer.Accounting>();
                DateTime? startDate;
                DateTime? endDate;

                
                if ((int)cbCustomer.SelectedValue != 0)
                {
                    int customerId = int.Parse(cbCustomer.SelectedValue.ToString());
                    result.AddRange(db.AccountingRepository.Get(a => a.TypeID == TypeID && a.CustomerID == customerId));
                }
                else
                    result.AddRange(db.AccountingRepository.Get(a => a.TypeID == TypeID));

                if (txtFromDate.Text != "    /  /")
                {
                    startDate = Convert.ToDateTime(txtFromDate.Text);
                    startDate = DateConvertor.ToMiladi(startDate.Value);
                    result = result.Where(a => a.DateTime >= startDate.Value).ToList();
                }
                if (txtToDate.Text != "    /  /")
                {
                    endDate = Convert.ToDateTime(txtToDate.Text);
                    endDate = DateConvertor.ToMiladi(endDate.Value);
                    result = result.Where(a => a.DateTime <= endDate.Value).ToList();
                }

                dgViewReport.Rows.Clear();
                foreach (var item in result)
                {
                    string CustomerName = db.CustomerRepository.GetCustomerNameByID(item.CustomerID);
                    dgViewReport.Rows.Add(item.ID, CustomerName, item.Amount, item.DateTime.ToShamsi(), item.Description);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgViewReport.CurrentRow != null)
            {
                int id = int.Parse(dgViewReport.CurrentRow.Cells[0].Value.ToString());
                if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    db.AccountingRepository.Delete(id);
                    db.Save();
                    Filter();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgViewReport.CurrentRow != null)
            {
                int id = int.Parse(dgViewReport.CurrentRow.Cells[0].Value.ToString());
                frmNewAccounting frmNewAccounting = new frmNewAccounting();
                frmNewAccounting.AccountID = id;
                if (frmNewAccounting.ShowDialog() == DialogResult.OK)
                    Filter();
                
            }
        }
    }
}
