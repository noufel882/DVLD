using Application_Layer.Application_Types;
using Application_Layer.Applications.International_Driver_License;
using Application_Layer.Applications.Local_Driving_License;
using Application_Layer.Applications.Release_License;
using Application_Layer.Applications.Renew_Driver_License;
using Application_Layer.Applications.Replacement_License;
using Application_Layer.Driver;
using Application_Layer.Global_Classes;
using Application_Layer.License.Detains_Licenses;
using Application_Layer.Login;
using Application_Layer.People;
using Application_Layer.Test_types;
using Application_Layer.Users;
using System;
using System.Windows.Forms;


namespace Application_Layer
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;
        public frmMain(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;
        }

        private void perToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeopleList managePeople = new frmPeopleList();
            managePeople.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserList userList = new frmUserList();
            userList.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserDetails userDetails = new frmShowUserDetails(Global.CurrentUser.UserID);
            userDetails.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword changePassword = new frmChangePassword(Global.CurrentUser.UserID);
            changePassword.ShowDialog();
        }

        private void BackToLoginScreen()
        {
            Global.CurrentUser = null;
            _frmLogin.Show();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackToLoginScreen();
            this.Close();
            
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            BackToLoginScreen();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApplicationTypesList frm = new frmApplicationTypesList();
            frm.ShowDialog();
        }

        private void manageTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestTypesList frm= new frmTestTypesList();
            frm.ShowDialog();
        }

        private void loacalDrivingApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseList frm = new frmLocalDrivingLicenseList();
            frm.ShowDialog();

        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditLocalDrivingApplication frm = new frmAddEditLocalDrivingApplication();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmDriversList driversList = new frmDriversList();
            driversList.ShowDialog();

        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseList frm = new frmLocalDrivingLicenseList();
            frm.ShowDialog();
        }

        private void internationalDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalDriveringLicenseApplication frm = new frmInternationalDriveringLicenseApplication();
            frm.ShowDialog();
        }

        private void internationalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewLicenseInterNationalApplication frm = new frmAddNewLicenseInterNationalApplication();
            frm.ShowDialog();

        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLicenseApplication frm = new frmRenewLicenseApplication();
            frm.ShowDialog();
        }

        private void replacementForListOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLicenseApplication frm = new frmReplaceLicenseApplication();
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainsLicenseList frm = new frmDetainsLicenseList();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frm = new frmReleaseLicense();
            frm.ShowDialog();
        }

        private void relseaseDestainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frm = new frmReleaseLicense();
            frm.ShowDialog();
        }
    }
}
