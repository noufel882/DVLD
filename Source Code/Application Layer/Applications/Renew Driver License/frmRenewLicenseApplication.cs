using Application_Layer.Applications.Local_Driving_License;
using Application_Layer.Global_Classes;
using Application_Layer.License;
using Application_Layer.License.International_Driver_Licenses;
using Business_Logic;
using Business_Logic.Application_Types;
using Business_Logic.BaseApplicationClass;
using Business_Logic.Licenses;
using System;
using System.Windows.Forms;

namespace Application_Layer.Applications.Renew_Driver_License
{
    public partial class frmRenewLicenseApplication : Form
    {
        private int _RenewedLicenseID = -1;
        private Licenses _RenewedLicense = null;

        public frmRenewLicenseApplication()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _SetDefaultValues()
        {
            lblCreatedByUser.Text = Global.CurrentUser.UserName;
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblAppDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblAppFees.Text = ApplicationType.Find((int)BaseApplication.enApplicationType.RenewDrivingLicense).Fees.ToString();

        }

        private void _EnableRenewButton(bool Enable)
        {
            btnRenew.Enabled = Enable;
            llblShowLicenseHestory.Enabled = Enable;
        }

        private void _EnableSearchBar(bool Enable)
        {
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = Enable;
            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
        }

        private void frmRenewLicenseApplication_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
            _SetDefaultValues();
            _EnableRenewButton(false);
            _EnableSearchBar(true);

        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int OldLicenseID)
        {
            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo == null)
            {
                _EnableRenewButton(false);
                return;
            }

            lblOldLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.LicenseID.ToString();


            lblLicenseFees.Text = LicenseClass.Find(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassID).ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblLicenseFees.Text) + Convert.ToSingle(lblAppFees.Text)).ToString();

            int DefaultValidityLength = LicenseClass.Find(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassID).DefaultValidityLength;
            lblExpirationsDate.Text = DateTime.Now.AddYears(DefaultValidityLength).ToString("dd/MMM/yyyy");


            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show($"The selected license is not expired yet , it will be expire on :{Environment.NewLine}{ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate:dd/MMM/yyyy} "
                    ,"Not allowed"
                    ,MessageBoxButtons.OK,
                    MessageBoxIcon.Hand);
                _EnableRenewButton(false);
                return;
            }


            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show($"The selected license is not active , you can not edit on it "
                    , "Not allowed"
                    , MessageBoxButtons.OK,
                    MessageBoxIcon.Hand);
                _EnableRenewButton(false);
                return;
            }

            _EnableRenewButton(true);
        }

        private void llblShowLicenseHestory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            frmPersonLicenseHistory history = new frmPersonLicenseHistory(PersonID);
            history.ShowDialog();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(_RenewedLicenseID);
            frm.ShowDialog();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            var DialogResult = MessageBox.Show("Are you sure ?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if(DialogResult == DialogResult.No)
            {
                return;
            }

            _RenewedLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(), Global.CurrentUser.UserID);

            //The License is not renewed
            if(_RenewedLicense == null )
            {
                MessageBox.Show("Error : an unexpected error occured during this operation , call your adimstrator to fix it ","Conection error",MessageBoxButtons.OK ,MessageBoxIcon.Stop);
                return;
            }
            else
            {
                _RenewedLicenseID = _RenewedLicense.LicenseID;
                MessageBox.Show($"Licene renewed successfully with ID = {_RenewedLicenseID}", "Renew License", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _EnableSearchBar(false);
                _EnableRenewButton(false);
                llblShowLicenseInfo.Enabled = true;
                llblShowLicenseHestory.Enabled = true;
                lblRenewdLicenseID.Text =  _RenewedLicense.LicenseID.ToString();
                lblRenewAppID.Text = _RenewedLicense.ApplicationID.ToString();
            }


        }


    }
}
