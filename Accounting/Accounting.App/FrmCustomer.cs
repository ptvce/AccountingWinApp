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

namespace Accounting.App
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddOrEdit frmAddOrEdit = new FrmAddOrEdit();
            if (frmAddOrEdit.ShowDialog() == DialogResult.OK)
            {
                BindGrid();
            }
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        void BindGrid()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgCustomers.AutoGenerateColumns = false;
                dgCustomers.DataSource = db.CustomerRepository.GetAllCustomers();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
            txtFilter.Text = null;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
              dgCustomers.DataSource =  db.CustomerRepository.GetCustomerByFilter(txtFilter.Text);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgCustomers.CurrentRow != null)
            {
                string name = dgCustomers.CurrentRow.Cells[1].Value.ToString();
                if (MessageBox.Show($"Are you sure to delete {name} ?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (UnitOfWork db = new UnitOfWork())
                    {
                        int customerId = int.Parse(dgCustomers.CurrentRow.Cells[0].Value.ToString());
                        db.CustomerRepository.DeleteCustomer(customerId);
                        db.Save();
                        BindGrid();
                    }
                }
            }
            else
            {
                MessageBox.Show("رکورد مورد نظر را انتخاب کنید");
            }
        }
    }
}
