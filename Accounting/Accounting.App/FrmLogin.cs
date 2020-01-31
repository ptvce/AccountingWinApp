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
    public partial class FrmLogin : Form
    {
        public bool IsEdit = false;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    if (IsEdit)
                    {
                        var login = db.LoginRepository.Get().First();
                        login.UserName = txtUserName.Text;
                        login.Password = txtPassword.Text;
                        db.LoginRepository.Update(login);
                        db.Save();
                        Application.Restart();
                    }
                    else
                    {
                        if (db.LoginRepository.Get(c => c.UserName == txtUserName.Text && c.Password == txtPassword.Text).Any())
                            DialogResult = DialogResult.OK;
                        else
                            MessageBox.Show("User not found");
                    }
                }
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                this.Text = "Change Password";
                btnLogin.Text = "Save";

                using (UnitOfWork db = new UnitOfWork())
                {
                    var user = db.LoginRepository.Get().First();
                    txtUserName.Text = user.UserName;
                    txtPassword.Text = user.Password;
                }
            }
        }
    }
}
