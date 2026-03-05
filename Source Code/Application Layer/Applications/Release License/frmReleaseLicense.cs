using Application_Layer.Global_Classes;
using Application_Layer.License;
using Business_Logic.Application_Types;
using Business_Logic.BaseApplicationClass;
using BusinessLayer.DetainedLicenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application_Layer.Applications.Release_License
{

    public partial class frmReleaseLicense : Form
    {
        int _LicenseID = -1;
        public frmReleaseLicense()
        {
            InitializeComponent();
        }

        public frmReleaseLicense(int LicenseID )
        {
            InitializeComponent();
            _LicenseID = LicenseID;
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_LicenseID);
            _EnableSearchBar(false);
        }

        private void frmReleaseLicense_Load(object sender, EventArgs e)
        {
         
        }

        private void _EnableReleaseButton(bool Enable)
        {
            btnRelease.Enabled = Enable;
            llblShowLicenseHestory.Enabled = Enable;
        }

        private void _EnableSearchBar(bool Enable)
        {
            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = Enable;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _EnableReleaseButton(false);
            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo == null)
            {
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("This license is not Detained , please choose another one ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("This license is not active , you can not detain it", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblApplicationFees.Text = ApplicationType.Find((int)BaseApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees.ToString();


            lblLicenseID.Text = obj.ToString();
            lblDetainID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainInfo.DetainID.ToString();
            lblFineFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();
            _EnableReleaseButton(true);
        }

        private void llblShowLicenseHestory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;
            frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {          
            var DialogResult = MessageBox.Show("Are you sure ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (DialogResult == DialogResult.No)
            {
                return;
            }

            int ReleasedID = -1;
            bool IsReleased = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Release(Global.CurrentUser.UserID , ref ReleasedID);

            if (!IsReleased)
            {
                MessageBox.Show("Error : an unexpected issue occured during this operation , call you adminstaror to fix issue ", "Saving issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            MessageBox.Show($"License Released successfully with Application ID = {ReleasedID}", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblReleaesApplicationID.Text = ReleasedID.ToString();
            _EnableReleaseButton(false);
            llblShowLicenseHestory.Enabled = true;
            llblShowLicenseInfo.Enabled = true;

            return;
        }


    }
}
