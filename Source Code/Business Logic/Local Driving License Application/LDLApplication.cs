using System;
using System.Data;
using DbAccess.LDLApp_Access;
using Business_Logic.BaseApplicationClass;
using Business_Logic.Test_Types;
using BusinessLayer.Tests;
using Business_Logic.Application_Types;
using DbAccess.LicenseAccess;
using Business_Logic.Drivers;

namespace Business_Logic.Local_Driving_License_Application
{
    public class LDLApplication : BaseApplicationClass.BaseApplication
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;
        
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public LicenseClass LicenseClass { get; set; }

        public string PersonFullName
        {
            get
            {
                return Person.Find(ApplicantPersonID).FullName;
            }
        }

        public LDLApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            this._mode = enMode.AddNew;
        }      

        private LDLApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID, int LicenseClassID)

        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.PersonInfo = Person.Find(ApplicantPersonID);
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)ApplicationTypeID;
            base.ApplicationTypeInfo = ApplicationType.Find(ApplicationTypeID);
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.CreatedByUserID = CreatedByUserID;
            base.CreatedByUserInfo = User.FindByUserID(CreatedByUserID);
            base.PaidFees = PaidFees;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClass = LicenseClass.Find(LicenseClassID);
            this._mode = enMode.Update;
        }


        public static LDLApplication FindByLocalDrivingAppID(int ldlAppID)
        {
            int appID = -1;
            int licenseClassID = -1;

            bool IsFound = LDLAppAccess.GetLocalDrivingLicenseApplicationInfoByID(ldlAppID,ref appID, ref licenseClassID);

            if (IsFound)
            {
              BaseApplication BaseApp = BaseApplication.FindBaseApplication(appID);

                return new LDLApplication(
                    ldlAppID,
                    BaseApp.ApplicationID,            
                    BaseApp.ApplicantPersonID,
                    BaseApp.ApplicationDate,
                    BaseApp.ApplicationTypeID,
                    BaseApp.ApplicationStatus,
                    BaseApp.LastStatusDate,
                    BaseApp.PaidFees,
                    BaseApp.CreatedByUserID,
                    licenseClassID
                                      
                    );

            }
            return null;
        }

        public static LDLApplication FindByApplicationID(int appID)
        {

            int LDLappID = -1;
            int licenseClassID = -1;

            bool IsFound = LDLAppAccess.GetLocalDrivingLicenseApplicationInfoByBaseApplicationID(appID,ref LDLappID, ref licenseClassID);

            if (IsFound)
            {
                BaseApplication BaseApp = BaseApplication.FindBaseApplication(appID);


                return new LDLApplication(
                    LDLappID,
                    BaseApp.ApplicationID,
                    BaseApp.ApplicantPersonID,
                    BaseApp.ApplicationDate,
                    BaseApp.ApplicationTypeID,
                    BaseApp.ApplicationStatus,
                    BaseApp.LastStatusDate,
                    BaseApp.PaidFees,
                    BaseApp.CreatedByUserID,
                    licenseClassID

                    );

            }
            return null;
        }

        public bool Save()
        {
            base.Mode = (BaseApplicationClass.BaseApplication.enMode)_mode;

            if(!base.Save())
                return false;

            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _Update();
            }
            return false;
        }

        private bool _AddNew()
        {
            this.LocalDrivingLicenseApplicationID = LDLAppAccess.AddNewLocalDrivingLicenseApplication(
                this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _Update()
        {
            return LDLAppAccess.UpdateLocalDrivingLicenseApplication(
                this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }

        public bool Delete()
        {
           bool IsLocalDrivingLicenseApplicationDeleted =  LDLAppAccess.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);
           bool IsBaseDrivingLicenseApplicationDeleted;
           
           if (!IsLocalDrivingLicenseApplicationDeleted)
           {
               return false;
           }

           IsBaseDrivingLicenseApplicationDeleted = base.Delete();

            return IsLocalDrivingLicenseApplicationDeleted;
        }

        public static DataTable GetAllApplications()
        {
            return LDLAppAccess.GetAllLocalDrivingLicenseApplications();
        }


        public bool DoesPassTestType(TestType.enTestTypes TestTypeID)

        {
            return LDLAppAccess.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesPassPreviousTest(TestType.enTestTypes CurrentTestType)
        {

            switch (CurrentTestType)
            {
                case TestType.enTestTypes.VisionTest:
                    //in this case no required prvious test to pass.
                    return true;

                case TestType.enTestTypes.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.

                    return this.DoesPassTestType(TestType.enTestTypes.VisionTest);


                case TestType.enTestTypes.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    return this.DoesPassTestType(TestType.enTestTypes.WrittenTest);

                default:
                    return false;
            }
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, TestType.enTestTypes TestTypeID)

        {
            return LDLAppAccess.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(TestType.enTestTypes TestTypeID)
        {
            return LDLAppAccess.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(TestType.enTestTypes TestTypeID)
        {
            return LDLAppAccess.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, TestType.enTestTypes TestTypeID)

        {
            return LDLAppAccess.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, TestType.enTestTypes TestTypeID)

        {
            return LDLAppAccess.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public bool AttendedTest(TestType.enTestTypes TestTypeID)

        {
            return LDLAppAccess.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, TestType.enTestTypes  TestTypeID)

        {

            return LDLAppAccess.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(TestType.enTestTypes TestTypeID)

        {

            return LDLAppAccess.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsLicenseIssued()
        {
            return Licenses.Licenses.GetActiveLicenseIDByPersonID(this.ApplicantPersonID,this.LicenseClassID) != -1;
        }

        public byte GetPassedTest()
       {
            return Tests.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
       }

        public static byte GetPassedTest(int LocalDrivingLicenseApplicationID)
        {
            return Tests.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public int GetActiveLicenseID()
        {
            return LicenseAccess.GetActiveLicenseIDByPersonID(this.ApplicantPersonID ,this.LicenseClassID);
        }

        public static int GetActiveLicenseID(int PersonID ,int LicenseClass)
        {
            return LicenseAccess.GetActiveLicenseIDByPersonID(PersonID , LicenseClass);
        }


        public Tests GetLastTestPerTestType(TestType.enTestTypes type)
        {
            return Tests.GetLastTestByPersonAndTestTypeAndLicenseClass(this.ApplicantPersonID ,this.LicenseClassID , type);
        }

        public bool PassedAllTest()
        {
            return GetPassedTest() == 3;
        }

        public int IssueLicenseForFirstTime(string Note , int CreatedByUserId)
        {
            int DriverID = -1;

            Drivers.Drivers driver = Drivers.Drivers.FindByPersonID(this.ApplicantPersonID);
            if(driver == null)
            {
                driver = new Drivers.Drivers();

                driver.PersonID = this.ApplicantPersonID;
                driver.CreatedByUserID  = CreatedByUserId;

                if(driver.Save())
                {
                    DriverID = driver.DriverID;
                }

                else
                {
                    return -1;
                }

            }
            DriverID = driver.DriverID;

            Licenses.Licenses NewLicense = new Licenses.Licenses();

            NewLicense.ApplicationID = this.ApplicationID;
            NewLicense.DriverID = DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now.Date;
            NewLicense.ExpirationDate = DateTime.Now.Date.AddYears(this.LicenseClass.DefaultValidityLength);
            NewLicense.Notes = Note;
            NewLicense.PaidFees = this.LicenseClass.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = Licenses.Licenses.enIssueReason.FirstTime;
            NewLicense.CreatedByUserID = CreatedByUserId;


            if(NewLicense.Save())
            {
                this.SetComplete();

                return NewLicense.LicenseID;
            }


            return -1;
           
        }


    }
}