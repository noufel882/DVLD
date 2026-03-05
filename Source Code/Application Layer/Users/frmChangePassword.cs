using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application_Layer.People.Controls;
using Business_Logic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Application_Layer.Users
{
    public partial class frmChangePassword : Form
    {
        int _UserID = -1;
        User _user;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
        }

        private void SetDefaultValue()
        {
            txtConfirmPassword.Text = string.Empty;
            txtCurrentPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;
            txtCurrentPassword.Focus();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            SetDefaultValue();
            _user = User.FindByUserID(this._UserID);

            if( _user == null)
            {
                MessageBox.Show("This user that you looking for in no longer exist ." ,"Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlUserCard1.LoadUserInfo(_UserID);
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Please fill this Field .");
                return;
            }


            if(txtCurrentPassword.Text.Trim() != _user.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password is wrong !");
                return;
            }

            e.Cancel = false;
            errorProvider1.SetError(txtCurrentPassword, null);
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "Please fill this Field .");
                return;
            }

            e.Cancel = false;
            errorProvider1.SetError(txtNewPassword, null);

        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if(txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim() )
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Wrong password !");
                return;
            }

            e.Cancel = false;
            errorProvider1.SetError(txtConfirmPassword, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fields are empty , please go to red marks and fill those fields.", "Warning" , MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            if ( User.ChangePassword(_UserID , txtNewPassword.Text.Trim()) )
            {
                MessageBox.Show($"Change Password successfully for user that has ID = {_UserID}" , "Info" , MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show($"Operation failed due to an unspected error . ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
