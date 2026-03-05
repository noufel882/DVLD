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

namespace Application_Layer.Users.Controls
{
    public partial class ctrlUserCard : UserControl
    {
        User _user;
        int _UserId = -1;

        public int UserID
        {
            get { return _UserId; }
        }

        public User SelectedUser
        {
            get {return _user; }
        }

        public ctrlUserCard()
        {
            InitializeComponent();   
            _user = new User();
        }

        private void FillUserDataInForm()
        {
            _UserId = UserID;
            ctrlPersonCard1.LoadData(_user.PersonID);
            lblIsActrive.Text = (_user.IsActive) ? "Yes" : "No";
            lblUserID.Text = _user.UserID.ToString();
            lblUserName.Text = _user.UserName.ToString();
        }

        public void LoadUserInfo(int UserId)
        {
            _user = User.FindByUserID(UserId);

            if(_user == null)
            {
                MessageBox.Show("This user that you looking for is no longer exist","Error" , MessageBoxButtons.OK,MessageBoxIcon.Stop);
                SetDefaultValues();
                return;
            }
            FillUserDataInForm();
        }
       
        public void LoadUserInfo(string UserName)
        {
            _user = User.FindByUserName(UserName);

            if (_user == null)
            {
                MessageBox.Show("This user that you looking for is no longer exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                SetDefaultValues();
                return;
            }
            FillUserDataInForm();
        }
        
        public void LoadUserInfoByPersonID(int PersonID)
        {
            _user = User.FindByPeronID(PersonID);

            if (_user == null)
            {
                MessageBox.Show("This user that you looking for is no longer exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                SetDefaultValues();
                return;
            }
            FillUserDataInForm();
        }

        private void SetDefaultValues()
        {
            ctrlPersonCard1.SetDefaultValues();
            lblIsActrive.Text = "[????]";
            lblUserID.Text = "[????]";
            lblUserName.Text = "[????]";
        }

        private void ctrlUserCard_Load(object sender, EventArgs e)
        {
            SetDefaultValues();
        }
    }
}
