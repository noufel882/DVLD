using Application_Layer.Properties;
using Business_Logic;
using Business_Logic.Licenses;
using BusinessLayer.LicenseBusiness;
using System.Windows.Forms;

namespace Application_Layer.License.International_Driver_Licenses.Controls
{
    public partial class ctrlShowInternationalDrivingLicense : UserControl
    {
        int _InternationalLicenseID = -1;
        InternationalLicense _InternationalLicense;

        public ctrlShowInternationalDrivingLicense()
        {
            InitializeComponent();
        }

        private void LoadPersonImage()
        {

            if (_InternationalLicense.DriverInfo.PersonalInfo.Gender == Person.enGender.Male)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
            else
            {
                pbPersonImage.Image = Resources.Female_512;
            }

            string imagePath = _InternationalLicense.DriverInfo.PersonalInfo.ImagePath;

            if (imagePath != "")
            {
                if (Utils.ImageUtils.IsImageExists(imagePath))
                {
                    pbPersonImage.Load(imagePath);

                }
                else
                {
                    MessageBox.Show($"Could not Find image : {imagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


        }

        private void _FillData()
        {
           
            lblPersonName.Text = _InternationalLicense.DriverInfo.PersonalInfo.FullName;
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonalInfo.NationalNo;
            lblGender.Text = (_InternationalLicense.DriverInfo.PersonalInfo.Gender == Person.enGender.Male) ? "Male" : "Female";
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToString("dd/MMM/yyyy");
            lblIsActive.Text = (_InternationalLicense.IsActive) ? "Yes" : "No";
            lblDateOfBirth.Text = _InternationalLicense.DriverInfo.PersonalInfo.DateOfBirth.ToString("dd/MMM/yyyy");
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExperationDate.Text = _InternationalLicense.ExpirationDate.ToString("dd/MMM/yyyy");
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblInternationalID.Text = _InternationalLicense.InternationalLicenseID.ToString();

            LoadPersonImage();

        }

        public void _Reset()
        {
            lblPersonName.Text = "[????]";
            lblInternationalID.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblIssueDate.Text = "dd/MMM/yyyy";
            lblApplicationID.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblDateOfBirth.Text = "dd/MMM/yyyy";
            lblDriverID.Text = "[????]";
            lblExperationDate.Text = "dd/MMM/yyyy";
           

            pbPersonImage.Image = Resources.Male_512;

        }

        public void LoadData(int InternationalLicenseID)
        {
            _InternationalLicense = InternationalLicense.Find(InternationalLicenseID);
            
            if (_InternationalLicense == null)
            {
                _Reset();
                MessageBox.Show($"There is no international license with ID = {InternationalLicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillData();
        }

    }
}