using System.Data;
using Business.Tests;
using Business_Logic.Test_Types;
using DbAccess.TestDataAccess;

namespace BusinessLayer.Tests
{
    public class Tests
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public TestAppointment AppointmentInfo { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public Tests()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private Tests(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.AppointmentInfo = TestAppointment.Find(TestAppointmentID) ;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        private bool _AddNewTest()
        {
            this.TestID = TestAccess.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {
            return TestAccess.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public static Tests Find(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;

            if (TestAccess.GetTestByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
                return new Tests(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            else
                return null;
        }

        public static Tests GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID, TestType.enTestTypes TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;

           
            if (TestAccess.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID, LicenseClassID, (int)TestTypeID, ref TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new Tests(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTest();
            }

            return false;
        }
    
        public static DataTable GetAllTests()
        {
            return TestAccess.GetAllTests();
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return TestAccess.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }



    }
}