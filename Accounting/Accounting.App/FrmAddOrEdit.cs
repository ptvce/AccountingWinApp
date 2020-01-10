using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;

namespace Accounting.App
{
    public partial class FrmAddOrEdit : Form
    {
        public int customerId = 0;
        UnitOfWork db = new UnitOfWork();
        public FrmAddOrEdit()
        {
            InitializeComponent();
        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pcCustomer.ImageLocation = openFile.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);
                string path = Application.StartupPath + "/Images/";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                pcCustomer.Image.Save(path + imageName);
                Customers customer = new Customers()
                {
                    Address = txtAddress.Text,
                    Email = txtEmail.Text,
                    FullName = txtName.Text,
                    Mobile = txtMobile.Text,
                    CustomerImage = imageName
                };
                if (customerId == 0)
                    db.CustomerRepository.Insert(customer);
                else
                {
                    customer.CustomerID = customerId;
                    db.CustomerRepository.Update(customer);
                }
                db.Save();
                DialogResult = DialogResult.OK;
            }
        }

        private void FrmAddOrEdit_Load(object sender, EventArgs e)
        {
            if (customerId != 0)
            {
                this.Text = "Edit a person";
                var customer = db.CustomerRepository.GetCustomerById(customerId);
                txtAddress.Text = customer.Address;
                txtEmail.Text = customer.Email;
                txtMobile.Text = customer.Mobile;
                txtName.Text = customer.FullName;
                pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + customer.CustomerImage;
            }
        }
    }
}
