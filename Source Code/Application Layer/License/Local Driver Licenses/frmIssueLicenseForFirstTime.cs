using System;
using System.Windows.Forms;
using Application_Layer.Global_Classes;
using Business_Logic.Licenses;
using Business_Logic.Local_Driving_License_Application;

namespace Application_Layer.License
{
    public partial class frmIssueLicenseForFirstTime : Form
    {
        private int _LocalDrivngLicenseAppID = -1;
        private LDLApplication _LocalDrivngLicenseApp;

        public frmIssueLicenseForFirstTime(int LocalDrivngLicenseAppID)
        {
            InitializeComponent();
            this._LocalDrivngLicenseAppID = LocalDrivngLicenseAppID;
        }
                        
        private void frmIssueLicenseForFirstTime_Load(object sender, EventArgs e)
        {
            _LocalDrivngLicenseApp = LDLApplication.FindByLocalDrivingAppID(_LocalDrivngLicenseAppID);

            if( _LocalDrivngLicenseApp == null )
            {
                MessageBox.Show($"There no local drivng license application with ID = {_LocalDrivngLicenseAppID}","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if(!_LocalDrivngLicenseApp.PassedAllTest())
            {
                MessageBox.Show($"There's test(s) not passed yet ,Person should passed all tests first ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            int LicenseID = _LocalDrivngLicenseApp.GetActiveLicenseID();
            if(LicenseID != -1)
            {
                MessageBox.Show($"Person already has license on this license class ", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }


            ctrlApplocationInfoCard1._LoadLocalDrivingApplicationID(this._LocalDrivngLicenseAppID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int LicenseID = _LocalDrivngLicenseApp.IssueLicenseForFirstTime(txtNote.Text.Trim(), Global.CurrentUser.UserID);

            if (LicenseID != -1)
            {
                MessageBox.Show($"License issued successfully with ID = {LicenseID}","Issue License",MessageBoxButtons.OK,MessageBoxIcon.Information);
                btnIssue.Enabled = false;
                return;
            }

            else
                MessageBox.Show($"License not issued successfully ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
