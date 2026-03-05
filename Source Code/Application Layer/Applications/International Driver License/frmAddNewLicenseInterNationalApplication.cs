using Application_Layer.Global_Classes;
using Application_Layer.License;
using Application_Layer.License.International_Driver_Licenses;
using Business_Logic.Application_Types;
using Business_Logic.BaseApplicationClass;
using BusinessLayer.LicenseBusiness;
using System;
using System.Windows.Forms;

namespace Application_Layer.Applications.International_Driver_License
{
    public partial class frmAddNewLicenseInterNationalApplication : Form
    {
        int _InternationalLicenseID = -1;

        public frmAddNewLicenseInterNationalApplication()
        {
            InitializeComponent();
        }

        private void _SetDefaultValues()
        {
            lblAppFees.Text = ApplicationType.Find((int)BaseApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblAppDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            lblExpirationsDate.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");
        }


        private void frmAddNewLicenseInterNationalApplication_Load(object sender, EventArgs e)
        {
            _SetDefaultValues();
            _EnableSearchBar(true);
            _EnableIssueButton(false);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _EnableIssueButton(bool Enable)
        {
            btnIssue.Enabled = Enable;
            llblShowLicenseHestory.Enabled = Enable;
        }

        private void _EnableSearchBar(bool Enable)
        {
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = Enable;
            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int LocalLicenseID)
        {            
            // 3 -> Ordinary license 
            if(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassID != 3)
            {
                ctrlDriverLicenseInfoWithFilter1.ResetDefaultValue();
                MessageBox.Show($"This license is not Class 3 , please select another one", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);               
                return;
            }

            int ActiveInternationalLicenseID = InternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);

            if(ActiveInternationalLicenseID != -1)
            {
                MessageBox.Show($"This person already have international license with ID = {ActiveInternationalLicenseID}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _InternationalLicenseID = ActiveInternationalLicenseID;
                llblShowLicenseInfo.Enabled = true;
                _EnableIssueButton(false);
                return;
            }

          
            lblLocalLicenseID.Text = LocalLicenseID.ToString();
            _EnableIssueButton(true);
            _EnableSearchBar(false);
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            var DialogResult = MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.No)
            {
                return;
            }

            InternationalLicense InternationalLicense = new InternationalLicense();

            InternationalLicense.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = BaseApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = ApplicationType.Find((int)BaseApplication.enApplicationType.NewInternationalLicense).Fees;

            InternationalLicense.DriverID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            InternationalLicense.CreatedByUserID = Global.CurrentUser.UserID;

            if (InternationalLicense.Save())
            {
                MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblInternationalAppID.Text = InternationalLicense.ApplicationID.ToString();
                _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
                lblInterLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
                _EnableIssueButton(false);
                _EnableSearchBar(false);
                llblShowLicenseInfo.Enabled = true;
                llblShowLicenseHestory.Enabled = true;
                return;
            }


            MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void llblShowLicenseHestory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(PersonID);
            frm.ShowDialog();


        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(this._InternationalLicenseID);
            frm.ShowDialog();
        }


    }
}
