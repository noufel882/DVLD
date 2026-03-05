using Application_Layer.Global_Classes;
using Application_Layer.License;
using Business_Logic.Application_Types;
using Business_Logic.BaseApplicationClass;
using Business_Logic.Licenses;
using System;
using System.Windows.Forms;

namespace Application_Layer.Applications.Replacement_License
{
    public partial class frmReplaceLicenseApplication : Form
    {
        private int _ReplacementLicenseID = -1;
        public frmReplaceLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmReplaceLicenseApplication_Load(object sender, EventArgs e)
        {
            _SetDefaultValues();
        }

        private void _SetTitle()
        {
            lblTitle.Text = (rbDamagedLicense.Checked) ? "Replacement for Damaged License" : "Replacement for Lost License";
        }

        private void _SetDefaultValues()
        {
            _SetTitle();
            _EnableSearchBar(true);
            _EnableIssueReplacementButton(false);

            lblAppDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            lblAppFees.Text = ApplicationType.Find((int)BaseApplication.enApplicationType.ReplaceDamagedDrivingLicense).Fees.ToString();

        }

        private void _EnableIssueReplacementButton(bool Enable)
        {
            btnIssue.Enabled = Enable;
            llblShowLicenseHestory.Enabled = Enable;
        }

        private void _EnableSearchBar(bool Enable)
        {
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = Enable;
            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int OldLicenseID)
        {
            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo == null)
            {
                _EnableIssueReplacementButton(false);
                return;
            }


            lblOldLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.LicenseID.ToString();

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show($"The selected license is not active , you can not edit on it "
                    , "Not allowed"
                    , MessageBoxButtons.OK,
                    MessageBoxIcon.Hand);

                _EnableIssueReplacementButton(false);
                return;
            }


            _EnableIssueReplacementButton(true);
        }

        private void llblShowLicenseHestory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            frmPersonLicenseHistory history = new frmPersonLicenseHistory(PersonID);
            history.ShowDialog();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(_ReplacementLicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            var DialogResult = MessageBox.Show("Are you sure ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

            if (DialogResult == DialogResult.No)
            {
                return;
            }

            Licenses.enIssueReason IssueReason = (rbDamagedLicense.Checked) ? Licenses.enIssueReason.DamagedReplacement : Licenses.enIssueReason.LostReplacement;
            Licenses _ReplacedLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ReplaceLicense(Global.CurrentUser.UserID , IssueReason);

            //The License is not renewed
            if (_ReplacedLicense == null)
            {
                MessageBox.Show("Error : an unexpected error occured during this operation , call your adimstrator to fix it ", "Conection error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                _ReplacementLicenseID = _ReplacedLicense.LicenseID;
                MessageBox.Show($"Licene replaced successfully with ID = {_ReplacementLicenseID}", "Replace License", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _EnableSearchBar(false);
                _EnableIssueReplacementButton(false);
                pbReplacementFor.Enabled = false;
                llblShowLicenseInfo.Enabled = true;
                llblShowLicenseHestory.Enabled = true;
                lblReplacedLicenseID.Text = _ReplacedLicense.LicenseID.ToString();
                lblReplacementAppID.Text = _ReplacedLicense.ApplicationID.ToString();
            }


        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblAppFees.Text = ApplicationType.Find((int)BaseApplication.enApplicationType.ReplaceDamagedDrivingLicense).Fees.ToString();
            lblTitle.Text = "Replacement for Damaged License" ;
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblAppFees.Text = ApplicationType.Find((int)BaseApplication.enApplicationType.ReplaceLostDrivingLicense).Fees.ToString();
            lblTitle.Text ="Replacement for Lost License";
        }
    }
}