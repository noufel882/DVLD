using System;
using System.Data;
using System.Windows.Forms;
using Application_Layer.Global_Classes;
using Business_Logic;
using Business_Logic.BaseApplicationClass;
using Business_Logic.Application_Types;
using Business_Logic.Local_Driving_License_Application;
using Business_Logic.Licenses;

namespace Application_Layer.Applications.Local_Driving_License
{
    public partial class frmAddEditLocalDrivingApplication : Form
    {
        enum enMode { AddNew , Update}

        enMode Mode = enMode.AddNew;

        int LDLAppID = -1;
        int _SelectedPerson;

        LDLApplication LocalDriveLicenseApp ;

        public frmAddEditLocalDrivingApplication()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
            LocalDriveLicenseApp = new LDLApplication();
        }

        public frmAddEditLocalDrivingApplication(int AppID)
        {
            InitializeComponent();
            Mode = enMode.Update;
            this.LDLAppID = AppID;
        }

        private void SetTitle()
        {
            lblTitle.Text = this.Text = (Mode == enMode.AddNew)? "New Local driving License Application" : "Update Local driving License Application info";
        }

        private void UnlockApplicationTab(bool Unlock)
        {
            tabAppliccationInfo.Enabled = Unlock;
            btnSave.Enabled = Unlock;           
        }

        private void UnlockFilter(bool Unlock)
        {
            ctrlPersonCardWithFilter1.FilterEnabled = Unlock;
        }

        private void GetLicenseClassesList()
        {
            DataTable LicenseClassesList = LicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in LicenseClassesList.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }

            cbLicenseClass.SelectedIndex = 2;
        }

        private void SetDefaultValues()
        {

            if(Mode == enMode.AddNew)
            {
                ctrlPersonCardWithFilter1.SetDefaultValues();
                lblApplicationDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                lblApplicationFees.Text = ApplicationType.Find((int)BaseApplication.enApplicationType.NewDrivingLicense).Fees.ToString("F2");
                lblCreatedByUser.Text = Global.CurrentUser.UserName;
                UnlockFilter(true);
                UnlockApplicationTab(false);
            }

            else
            {
                UnlockFilter(false);
                UnlockApplicationTab(true);
            }

            SetTitle();
            tabControlOptions.SelectedTab = tabPersonInfo;
            GetLicenseClassesList();

        }

        private void frmAddEditLocalDrivingApplication_Load(object sender,EventArgs e)
        {
            SetDefaultValues();

            if (Mode == enMode.Update)
            {
                LoadData(LDLAppID);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (Mode == enMode.AddNew)
            {

                if (_SelectedPerson == -1)
                {
                    MessageBox.Show("Please select person first !","Warinig",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    ctrlPersonCardWithFilter1.FilterFocus();
                    return;
                }
            }
                UnlockApplicationTab(true);
                tabControlOptions.SelectedTab = tabAppliccationInfo;
        }

        private void FillAppData()
        {
            ctrlPersonCardWithFilter1.LoadPersonInfo(LocalDriveLicenseApp.ApplicantPersonID);
            lblID.Text = LocalDriveLicenseApp.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = LocalDriveLicenseApp.ApplicationDate.ToString("dd/MM/yyyy");
            lblCreatedByUser.Text = User.FindByUserID(LocalDriveLicenseApp.CreatedByUserID).UserName;
            lblApplicationFees.Text = LocalDriveLicenseApp.PaidFees.ToString();
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(LicenseClass.Find(LocalDriveLicenseApp.LicenseClassID).ClassName);
        }

        private void LoadData(int ID)
        {
            LocalDriveLicenseApp = LDLApplication.FindByLocalDrivingAppID(ID);

            if( LocalDriveLicenseApp == null )
            {
                MessageBox.Show($"The local driving license application with ID = {ID} is not exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            FillAppData();

        }

        private void SetApplicationToSave()
        {

            LocalDriveLicenseApp.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            LocalDriveLicenseApp.ApplicationDate = DateTime.Now.Date;
            LocalDriveLicenseApp.ApplicationTypeID = 1;
            LocalDriveLicenseApp.ApplicationStatus = BaseApplication.enApplicationStatus.New;
            LocalDriveLicenseApp.PaidFees = float.Parse(lblApplicationFees.Text); 
            LocalDriveLicenseApp.CreatedByUserID = Global.CurrentUser.UserID;
            LocalDriveLicenseApp.LicenseClassID = LicenseClass.Find(cbLicenseClass.Text).LicenseClassID; 
            LocalDriveLicenseApp.LastStatusDate = DateTime.Now.Date;
                     
        }

        private bool SaveData()
        {
            SetApplicationToSave();

            if (LocalDriveLicenseApp.Save())
            {
                Mode = enMode.Update;
                return true;
            }

            return false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            int LicenseClassID = LicenseClass.Find(cbLicenseClass.Text).LicenseClassID;

            bool IsHaveActiveApp = 
            (
                (BaseApplication.GetActiveApplicationIDForLicenseClass(ctrlPersonCardWithFilter1.PersonID,BaseApplication.enApplicationType.NewDrivingLicense ,LicenseClassID) != -1) 
            );

            if (IsHaveActiveApp)
            {
                MessageBox.Show("This person already has active local driving license application.","Waring",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            
            
            if (Licenses.IsLicenseExistByPersonID(_SelectedPerson, LicenseClassID))
            {
                MessageBox.Show("This person already has active local driving license .", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (SaveData())
            {
                SetTitle();
                lblID.Text = LocalDriveLicenseApp.LocalDrivingLicenseApplicationID.ToString();
                MessageBox.Show("Data saved successfully .", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else
            {
                MessageBox.Show("Operation failed due to an unexpected error occurd while Saving this application informations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPerson = obj;
        }

        private void frmAddEditLocalDrivingApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

      
       
    }
}
