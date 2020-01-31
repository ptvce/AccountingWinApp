using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmNewAccounting : Form
    {
        UnitOfWork db;
        public int AccountID = 0;
        public frmNewAccounting()
        {
            InitializeComponent();
        }

        private void frmNewAccounting_Load(object sender, EventArgs e)
        {
            db = new UnitOfWork();
            dgvCustomer.AutoGenerateColumns = false;
            dgvCustomer.DataSource = db.CustomerRepository.GetNameCustomers();
            if (AccountID != 0)
            {
                this.Text = "Edit Transaction";
                var account = db.AccountingRepository.GetById(AccountID);
                txtAmount.Text = account.Amount.ToString();
                txtDescription.Text = account.Description;
                txtName.Text = db.CustomerRepository.GetCustomerNameByID(account.CustomerID);
               
                if (account.TypeID == 1)
                    rbReceive.Checked = true;
                else
                    rbPayment.Checked = true;
            }
            db.Dispose();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            db = new UnitOfWork();
            dgvCustomer.AutoGenerateColumns = false;
            dgvCustomer.DataSource = db.CustomerRepository.GetNameCustomers(txtFilter.Text);
            db.Dispose();
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomer.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            db = new UnitOfWork();
            if (BaseValidator.IsFormValid(this.components))
            {
                if (rbReceive.Checked || rbPayment.Checked)
                {
                    DataLayer.Accounting accounting = new DataLayer.Accounting() {
                        Amount = int.Parse(txtAmount.Value.ToString()),
                        TypeID = (rbReceive.Checked) ? 1 : 2,
                        DateTime = DateTime.Now,
                        Description = txtDescription.Text,
                        CustomerID = db.CustomerRepository.GetCustomerIDByName(txtName.Text),
                    };

                    if (AccountID == 0)
                       db.AccountingRepository.Insert(accounting);
                    else
                    {
                        accounting.ID = AccountID;
                        db.AccountingRepository.Update(accounting);
                    }
                    db.Save();
                    db.Dispose();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Select Transaction Type");
                }    
            }
        }
    }
}
