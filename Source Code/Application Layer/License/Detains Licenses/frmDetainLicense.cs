using Application_Layer.Global_Classes;
using Application_Layer.License.International_Driver_Licenses;
using Business_Logic.Application_Types;
using Business_Logic.BaseApplicationClass;
using BusinessLayer.DetainedLicenses;
using System;
using System.Windows.Forms;

namespace Application_Layer.License.Detains_Licenses
{
    public partial class frmDetainLicense : Form
    {

        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            _EnableDetainButton(false);
            _EnableSearchBar(true);

            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblCreatedByUser.Text = Global.CurrentUser.UserName;

        }

        private void _EnableDetainButton(bool Enable)
        {
            btnDetain.Enabled = Enable;
            llblShowLicenseHestory.Enabled = Enable;
        }

        private void _EnableSearchBar(bool Enable)
        {
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = Enable;
            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _EnableDetainButton(false);
            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo == null)
            {
                return;
            }

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("This license is already Detained" , "Not Allowed" ,MessageBoxButtons.OK ,MessageBoxIcon.Stop);
                return;
            }

            if(!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("This license is not active , you can not detain it", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            lblLicenseID.Text = obj.ToString();
            _EnableDetainButton(true);

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

        private void txtFineFeesAmount_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtFineFeesAmount.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFeesAmount, "This field is require , please fill it.");
                return;
            }

            if(!Utils.Validation.IsNumber(txtFineFeesAmount.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError( txtFineFeesAmount,"Invalid Input !");
                return;
            }

            errorProvider1.SetError(txtFineFeesAmount, null);
        }
       
        private void txtFineFeesAmount_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) );
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("There is field(s) empty or include invalid value , please go to red icon to see the issue ", "Warning", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            var DialogResult = MessageBox.Show("Are you sure ?" , "Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question , MessageBoxDefaultButton.Button2);

            if(DialogResult == DialogResult.No)
            {
                return;
            }

            float FineFees = Convert.ToSingle(txtFineFeesAmount.Text);
            int CreatedByUser = Global.CurrentUser.UserID;

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Detain(CreatedByUser, FineFees) )
            {
                MessageBox.Show("Error : an unexpected issue occured during this operation , call you adminstaror to fix issue ", "Saving issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int DetainID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainInfo.DetainID;
            MessageBox.Show($"Detain successfully with ID = {DetainID}", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblDetainID.Text = DetainID.ToString();
            _EnableDetainButton(false);
            llblShowLicenseHestory.Enabled = true;
            llblShowLicenseInfo.Enabled = true;

            return;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
