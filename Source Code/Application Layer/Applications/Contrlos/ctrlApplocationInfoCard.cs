using Application_Layer.License;
using Application_Layer.People;
using Business_Logic.BaseApplicationClass;
using Business_Logic.Licenses;
using Business_Logic.Local_Driving_License_Application;
using System;
using System.Windows.Forms;

namespace Application_Layer.Applications
{
    public partial class ctrlApplocationInfoCard : UserControl
    {
       
        private int _LocalDrivingLicenseApplicationID = -1;

        private int _LicenseID = -1;

        public int LocalDrivingLicenseApplicationID
        {
            get {  return _LocalDrivingLicenseApplicationID;}
        }

        public ctrlApplocationInfoCard()
        {
            InitializeComponent();
        }

        private LDLApplication _LocalDrivingLicenseApplication ;

        private void FillData()
        {
            _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;

            _LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();

            llblShowLicense.Enabled = (_LicenseID != -1);

            lblApplicant.Text = _LocalDrivingLicenseApplication.ApplicantFullName;
            lblBaseAppID.Text = _LocalDrivingLicenseApplication.ApplicationID.ToString();
            lblCreatedByUser.Text = _LocalDrivingLicenseApplication.CreatedByUserInfo.UserName;
            lblDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToString("dd/MMM/yyyy");
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString("F2");
            lblLicenseClass.Text = _LocalDrivingLicenseApplication.LicenseClass.ClassName;
            lblLocalAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblPassedTests.Text = _LocalDrivingLicenseApplication.GetPassedTest().ToString() +"/3";
            lblStatus.Text = _LocalDrivingLicenseApplication.StatusText.ToString();
            lblStatusDate.Text = _LocalDrivingLicenseApplication.LastStatusDate.ToString("dd/MMM/yyyy");
            lblType.Text = _LocalDrivingLicenseApplication.ApplicationTypeInfo.Title;
            
        }
            
        public void _LoadLocalDrivingApplicationID(int LocalDrivingLicenseApplicationID)
        {
             _LocalDrivingLicenseApplication = LDLApplication.FindByLocalDrivingAppID(LocalDrivingLicenseApplicationID);
            
            if(_LocalDrivingLicenseApplication == null )
            {
                ResetApplicationInfo();
                MessageBox.Show($"Theres no Local Driving License Application with ID = {LocalDrivingLicenseApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            else
            {
                FillData();
               
            }
        }

        public void _LoadByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicenseApplication = LDLApplication.FindByApplicationID(ApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                ResetApplicationInfo();
                MessageBox.Show($"Theres no Application with ID = {ApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                FillData();
                
            }
        }

        private void llblPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEdit addEdit = new frmAddEdit(_LocalDrivingLicenseApplication.ApplicantPersonID);
            addEdit.ShowDialog();
            _Refresh();
        }
    
        private void _Refresh()
        {
            if(_LocalDrivingLicenseApplication != null) 
                _LoadLocalDrivingApplicationID(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
        }

        public void ResetApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            _LicenseID = -1;

            lblApplicant.Text = "???";
            lblBaseAppID.Text = "N/A";
            lblCreatedByUser.Text = "???";
            lblDate.Text = "???";
            lblFees.Text = "???";
            lblLicenseClass.Text = "???";
            lblLocalAppID.Text ="N/A";
            lblPassedTests.Text ="0/3";
            lblStatus.Text = "???";
            lblStatusDate.Text = "???";
            lblType.Text = "???";

        }

        private void llblShowLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(this._LocalDrivingLicenseApplication.GetActiveLicenseID());
            frm.ShowDialog();
        }
   
    
    
    }
}

