using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Logic;


namespace Application_Layer.Users
{ 
    public partial class frmAddEditUsers : Form
    {
        enum enMode { add , update };
        enMode _mode;
        User _user ;
        int _UserID = -1;
        public frmAddEditUsers()
        {
            InitializeComponent();
          
            _mode = enMode.add;           
            _user = new User();
        }

        public frmAddEditUsers(int UserID)
        {
            InitializeComponent();
            _user = new User();
            _mode = enMode.update;
            this._UserID = UserID;
        }

        private void LoadData(int UserID)
        {
            _user = User.FindByUserID(UserID);

            if(_user == null)
            {
                MessageBox.Show("This user is no longer exists .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            LoginInfoTabEnabled(true);
            FillUserInfoInForm();
            ctrlPersonCardWithFilter1.LoadPersonInfo(_user.PersonID);
            
        }

        private void frmAddEditUsers_Load(object sender, EventArgs e)
        {
            SetFormTitle();
            SetDefaultValues();
            if(_mode == enMode.update)
            {
                LoadData(_UserID);
            }
        }

        private void GoToLoginInfoTab()
        {
            LoginInfoTabEnabled(true);
            tabControl1.SelectedIndex = 1;
        }

        private void SetFormTitle()
        {
            this.Text = lblTitle.Text = (_mode == enMode.add)?"Add new user ":"Update user info";
        }

        private void SetDefaultValues()
        {
            ctrlPersonCardWithFilter1.SetDefaultValues();
            txtConfirmPassword.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUserName.Text = string.Empty;

            bool IsUpdate = (_mode == enMode.update);

            chbIsActive.Checked = IsUpdate;
            LoginInfoTabEnabled(IsUpdate);
            ctrlPersonCardWithFilter1.FilterEnabled = !IsUpdate;
                   
        }

        private void FillUserInfoInForm()
        {
            lblUserID.Text = _user.UserID.ToString();
            txtPassword.Text = _user.Password;
            txtUserName.Text = _user.UserName;
            txtConfirmPassword.Text = _user.Password;
            chbIsActive.Checked = _user.IsActive;
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_mode == enMode.add)
            {
                int PersonID = ctrlPersonCardWithFilter1.PersonID;

                if (PersonID == -1)
                {
                    MessageBox.Show("You should select a person first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ctrlPersonCardWithFilter1.FilterFocus();
                    return;
                }


                if (User.IsUserExistsForPerson(PersonID))//cheack if the person with this id is already user in the system.
                {
                    MessageBox.Show("This person is already user in the system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                

            }
                GoToLoginInfoTab();
        }

        private void SetUserInfoToSave()
        {        
            _user.UserName = txtUserName.Text;
            _user.Password = txtPassword.Text;
            _user.IsActive = chbIsActive.Checked;
            _user.PersonID = ctrlPersonCardWithFilter1.PersonID;
        }

        private void LoginInfoTabEnabled(bool Enabled)
        {
            tabLoginInfo.Enabled = Enabled;
            btnSave.Enabled =Enabled;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("You left field(s) empty .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
              
            SetUserInfoToSave();

            if (_user.Save())
            {
                if (_mode == enMode.add)
                {
                    MessageBox.Show($"Add a new user successfully with ID ={_user.UserID}.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _mode = enMode.update;
                    this.Text = lblTitle.Text = "Update user info";
                    ctrlPersonCardWithFilter1.FilterEnabled = false;
                    LoginInfoTabEnabled(true);
                    lblUserID.Text = _user.UserID.ToString();
                }
                else
                {
                    MessageBox.Show("Update the User's information successfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Operation Failed Due to an unspected error occurd.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            string UserName = txtUserName.Text.Trim();
            if(UserName== "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName,"Is not allowed to let This field empty !");
                return;
            }

            bool IsNameRepeated =
            (
                _mode == enMode.add ||
                (_mode == enMode.update && UserName != _user.UserName.Trim())
            );


             if (IsNameRepeated && User.IsUserExists(UserName) )
             {
                 e.Cancel = true;
                 errorProvider1.SetError(txtUserName, "Another user in the system owner this username.");
                 return;
             }             

            
            e.Cancel = false;
            errorProvider1.SetError(txtUserName, "");


        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {

            if (txtPassword.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Is not allowed to let This field empty !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim() )
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Wrong Password !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void frmAddEditUsers_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}
