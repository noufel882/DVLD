using System;
using System.Windows.Forms;
using Application_Layer.Global_Classes;
using Business_Logic;
using static Utils.Cryptography.Hashing;

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

            var Username = txtUsername.Text.Trim();
            var HashedPassword = ComputeHash_SHA256(txtPassword.Text.Trim());

            User user = User.FindByUserNameAndPassword(Username, HashedPassword);

            if (user == null)
            {
                MessageBox.Show("Password or username are wrong !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;

            }

            if (!user.IsActive)
            {
                MessageBox.Show("This acount is unactive acount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (!chbRememberMe.Checked)
            {
                Global.SaveUsernameAndPassword("", "");
            }


            Global.SaveUsernameAndPassword(user.UserName, txtPassword.Text.Trim());//save loggin information in registry
            Global.CurrentUser = user;
            EnterTheSystem();

        }
    



        
    }
}
