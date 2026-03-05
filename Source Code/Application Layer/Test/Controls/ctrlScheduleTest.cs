using Application_Layer.Global_Classes;
using Application_Layer.Properties;
using Business.Tests;
using Business_Logic.Application_Types;
using Business_Logic.BaseApplicationClass;
using Business_Logic.Local_Driving_License_Application;
using Business_Logic.Test_Types;
using System;
using System.Windows.Forms;


namespace Application_Layer.Test.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {


        public enum enMode { AddNew , Update}
        private enMode _mode;

        public enum enCreationMode { FirstTestSchedule , RestakeTestSchedule }
        private enCreationMode _creationMode;

        private TestType.enTestTypes _TestTypeID;

        private int _LocalDrivingApplicationID = -1;

        private LDLApplication _LocalDrivingApplication = null;

        private int _TestAppointmentID = -1;

        private TestAppointment _TestAppointment = null;

        public TestType.enTestTypes TestTypeID
        {
            get {return _TestTypeID; }

            set
            {
                _TestTypeID = value;
                switch (TestTypeID)
                {
                    case TestType.enTestTypes.VisionTest:
                        gbTestInfo.Text = "Vision Test";
                        pbTestImage.Image = Resources.Vision_512;
                        break;

                    case TestType.enTestTypes.StreetTest:
                        gbTestInfo.Text = "Street Test";
                        pbTestImage.Image = Resources.driving_test_512;
                        break;

                    case TestType.enTestTypes.WrittenTest:
                        gbTestInfo.Text = "Written Test";
                        pbTestImage.Image = Resources.Written_Test_512;
                        break;
                }

            }

        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }
        
        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = TestAppointment.Find(_TestAppointmentID);

            if( _TestAppointment == null )
            {
                MessageBox.Show("Error : Can't load appointment data !" , "Load" , MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblTestFees.Text = TestAppointment.Find(_TestAppointmentID).PaidFees.ToString();

            if (DateTime.Compare(_TestAppointment.AppointmentDate.Date  , DateTime.Now.Date) > 0)
                dtpTestDate.MinDate = DateTime.Now.Date;
            else
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate.Date;

            dtpTestDate.Value = _TestAppointment.AppointmentDate.Date;

            if(_TestAppointmentID == -1)
            {
                _UnlockRetakeTestGroupBox(false);
            }

            else
            {
                _UnlockRetakeTestGroupBox(true);
            }


            return true;

        }

        private void _SetNewTestAppointment()
        {
            lblTestFees.Text = TestType.Find(TestTypeID).Fees.ToString();
            dtpTestDate.MinDate = DateTime.Now;
            lblRetakeTestFess.Text = "0";

            _TestAppointment = new TestAppointment();
        }

        private bool _LoadLocalDrivingApplicationData(int LocalDrivingApplicationID , int TestAppointmentID)
        {
            _LocalDrivingApplicationID = LocalDrivingApplicationID;

            _TestAppointmentID = TestAppointmentID;

            _LocalDrivingApplication = LDLApplication.FindByLocalDrivingAppID(LocalDrivingApplicationID);

            if (_LocalDrivingApplication == null)
            {         
                return false;
            }

            lblLocalAppID.Text = _LocalDrivingApplication.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = _LocalDrivingApplication.LicenseClass.ClassName;
            lblPersonName.Text = _LocalDrivingApplication.ApplicantFullName;
            lblTestTrials.Text = _LocalDrivingApplication.TotalTrialsPerTest(this._TestTypeID).ToString() + "/3";

            return true;
        }

        public void _Load(int LocalDrivingApplicationID , int TestAppointmentID =-1)
        {
            if (TestAppointmentID == -1)
                _mode = enMode.AddNew;

            else
                _mode = enMode.Update;

            if(!_LoadLocalDrivingApplicationData(LocalDrivingApplicationID , TestAppointmentID))
            {
                MessageBox.Show($"There's no any local driving license with ID = {LocalDrivingApplicationID}" , "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                _SaveButtonEnable(false);
                return;
            }

            if(_LocalDrivingApplication.DoesAttendTestType(this.TestTypeID))
            {
                _creationMode = enCreationMode.RestakeTestSchedule;
            }

            else
            {
                _creationMode=enCreationMode.FirstTestSchedule;
            }



            if(_creationMode == enCreationMode.FirstTestSchedule)
            {
                _UnlockRetakeTestGroupBox(false);
            }

            else
            {
                _UnlockRetakeTestGroupBox(true);
            }

            
            
            if(_mode == enMode.AddNew)
            {
                _SetNewTestAppointment();
            }

            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }


            lblTotalFees.Text =( Convert.ToSingle(lblRetakeTestFess.Text) + Convert.ToSingle(lblTestFees.Text) ).ToString();

            if (!_HandleAllConstriants())
                return;
                
        }

        private bool _HandleAllConstriants()
        {
            if (!_HandleActiveTestAppointmentsConstraint())
                return false;

            if (!_HandleLockedTestAppointmentsConstraint())
                return false;

            if (!_HandlePrviousTestConstraint())
                return false;

            return true;
        }

        private bool _HandleActiveTestAppointmentsConstraint()
        {
            if(_mode == enMode.AddNew && _LocalDrivingApplication.IsThereAnActiveScheduledTest(this.TestTypeID))
            {
                _SetUserMessageLabel("Person Already have an active appointment for this test");
                _SaveButtonEnable(false);
                return false;
            }
            return true;
        }

        private bool _HandleLockedTestAppointmentsConstraint()
        {
            if (_TestAppointment.IsLocked)
            {
                _SetUserMessageLabel("Person already sat for the test, appointment loacked.");
                _SaveButtonEnable(false);
                return false;
            }
            return true;
        }

        private bool _HandlePrviousTestConstraint()
        {
            switch (this.TestTypeID)
            {
                case TestType.enTestTypes.VisionTest:
                    
                    _SetUserMessageLabel(null);
                    _SaveButtonEnable(true);
                    return true;


                case TestType.enTestTypes.WrittenTest:
                    if(!_LocalDrivingApplication.DoesPassPreviousTest(TestType.enTestTypes.VisionTest))
                    {
                        _SetUserMessageLabel("Cannot Sechule, The vison test should be passed first");
                        _SaveButtonEnable(false);
                        return false;
                    }
                    else
                    {
                        _SetUserMessageLabel(null);
                        _SaveButtonEnable(true);
                    }
                    return true;



                case TestType.enTestTypes.StreetTest:
                    if (!_LocalDrivingApplication.DoesPassPreviousTest(TestType.enTestTypes.WrittenTest))
                    {
                        _SetUserMessageLabel("Cannot Sechule, The written test should be passed first");
                        _SaveButtonEnable(false);
                        return false;
                    }
                    else
                    {
                        _SetUserMessageLabel(null);
                        _SaveButtonEnable(true);
                    }
                    return true;

            }
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingApplicationID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblTestFees.Text);
            _TestAppointment.CreatedByUserID = Global.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                MessageBox.Show("Test Scheduled successfully","Schedule test"  , MessageBoxButtons.OK, MessageBoxIcon.Question);
                _mode = enMode.Update;
                return;
            }

            else
            {
                MessageBox.Show("Data Is not Saved Successfully ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private bool _HandleRetakeApplication()
        {
            if( _mode == enMode.AddNew && _creationMode == enCreationMode.RestakeTestSchedule)
            {
                BaseApplication RetakeTest = new BaseApplication();

                RetakeTest.ApplicantPersonID = _LocalDrivingApplication.ApplicantPersonID;
                RetakeTest.ApplicationDate = DateTime.Now.Date;
                RetakeTest.ApplicationTypeID = (int) BaseApplication.enApplicationType.RetakeTest ;
                RetakeTest.ApplicationStatus = BaseApplication.enApplicationStatus.Completed;
                RetakeTest.LastStatusDate = DateTime.Now.Date;
                RetakeTest.PaidFees = ApplicationType.Find((int)BaseApplication.enApplicationType.RetakeTest).Fees;
                RetakeTest.CreatedByUserID = Global.CurrentUser.UserID;
                

                if(!RetakeTest.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = RetakeTest.ApplicationID;

            }


            return true;
        }

        private void _UnlockRetakeTestGroupBox(bool Unlock)
        {
            if (Unlock)
            {
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestID.Text = "N/A";
                lblRetakeTestFess.Text = ApplicationType.Find((int)BaseApplication.enApplicationType.RetakeTest).Fees.ToString();
            }
           
            else
            {
                lblTitle.Text = "Schedule Test";
                lblRetakeTestID.Text = "N/A";
                lblRetakeTestFess.Text = "0";
            }
            gbRetakeTestInfo.Enabled = Unlock;

        }

        private void _SaveButtonEnable(bool Enabled)
        {
            btnSave.Enabled = Enabled;
            dtpTestDate.Enabled =Enabled;
        }

        private void _SetUserMessageLabel(string Message)
        {
            if(string.IsNullOrEmpty(Message))
            {
                lblUserMessage.Visible =false;
                lblUserMessage.Text = "";
                return;
            }

            lblUserMessage.Text = Message;
            lblUserMessage.Visible = true;

        }

        private void lblTestTrials_Click(object sender, EventArgs e)
        {

        }
    }
}
