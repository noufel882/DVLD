using Application_Layer.Properties;
using Business_Logic;
using Business_Logic.Drivers;
using Business_Logic.Licenses;
using System;

using System.Windows.Forms;

namespace Application_Layer.License.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private int _LicenseID = -1;
        private Licenses _License;

        public int LicenseID
        {
            get {  return _LicenseID; }
        }

        public Licenses SelectedLicense
        {
            get { return _License; }
        }
              
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        private void LoadPersonImage()
        {

            if (_License.DriverInfo.PersonalInfo.Gender == Person.enGender.Male)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
            else
            {
                pbPersonImage.Image = Resources.Female_512;
            }

            string imagePath = _License.DriverInfo.PersonalInfo.ImagePath;

            if (imagePath != "")
            {
                if (Utils.ImageUtils.IsImageExists(imagePath))
                {
                    pbPersonImage.Load(imagePath);
                    
                }
                else
                {
                    MessageBox.Show($"Could not Find image : {imagePath}", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


        }

        private void _FillData()
        {
            lblLicenseClass.Text = _License.licenseClass.ClassName;
            lblPersonName.Text = _License.DriverInfo.PersonalInfo.FullName;
            lblLicenseID.Text = _LicenseID.ToString();
            lblNationalNo.Text = _License.DriverInfo.PersonalInfo.NationalNo;
            lblGender.Text = (_License.DriverInfo.PersonalInfo.Gender ==Person.enGender.Male)? "Male" : "Female";
            lblIssueDate.Text = _License.IssueDate.ToString("dd/MMM/yyyy");
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes;
            lblIsActive.Text = (_License.IsActive)? "Yes" : "No";
            lblDateOfBirth.Text = _License.DriverInfo.PersonalInfo.DateOfBirth.ToString("dd/MMM/yyyy");
            lblDriverID.Text = _License.DriverID.ToString();
            lblExperationDate.Text = _License.ExpirationDate.ToString("dd/MMM/yyyy");
            lblIsDetained.Text = (_License.IsDetained) ? "Yes" : "No";
            LoadPersonImage();
            
        }

        public void _Reset()
        {
            lblLicenseClass.Text = "[????]";
            lblPersonName.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblIssueDate.Text = "dd/MMM/yyyy";
            lblIssueReason.Text = "[????]";
            lblNotes.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblDateOfBirth.Text ="dd/MMM/yyyy";
            lblDriverID.Text = "[????]";
            lblExperationDate.Text ="dd/MMM/yyyy";
            lblIsDetained.Text = "[????]";

            pbPersonImage.Image = Resources.Male_512;

        }

        public void LoadData(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = Licenses.Find(LicenseID);

            if (_License == null)
            {
                _Reset();
                MessageBox.Show($"There is no license with ID = {LicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);               
                return;
            }
            _FillData();
        }

       
    }
}
