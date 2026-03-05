using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Forms;
using Application_Layer.Global_Classes;
using Business_Logic;

namespace Application_Layer.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            string UserName = "", Password = "";

            if (Global.GetStoredCredential(ref UserName, ref Password))
            {
                txtUsername.Text = UserName;
                txtPassword.Text = Password;
                chbRememberMe.Checked = true;
            }
            else
                chbRememberMe.Checked = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EnterTheSystem()
        {
            frmMain main = new frmMain(this);
            this.Hide();
            main.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {       
            User user = User.FindByUserNameAndPassword(txtUsername.Text ,txtPassword.Text);

            if (user != null)
            {
                if (!user.IsActive)
                {
                    MessageBox.Show("This acount is unactive acount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                else
                {
                    if (chbRememberMe.Checked)
                    {
                        Global.SaveUsernameAndPassword(user.UserName , user.Password);
                    }
                    else
                    {
                        Global.SaveUsernameAndPassword("","");
                    }
                    Global.CurrentUser = user;
                    EnterTheSystem();
                }
            }

            else
            {
                MessageBox.Show("Password or username are wrong !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;

            }


        }
    }
}
