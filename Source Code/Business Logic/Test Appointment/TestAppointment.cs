using System;
using System.Data;
using Business_Logic.BaseApplicationClass;
using Business_Logic.Local_Driving_License_Application;
using Business_Logic.Test_Types;
using DbAccess.Tests;

namespace Business.Tests
{
    public class TestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public TestType.enTestTypes TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public BaseApplication RetakeAppInfo {  get; set; }

        public int TestID
        {
            get { return _GetTestID(); }
        }

        public TestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = TestType.enTestTypes.VisionTest;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;

            Mode = enMode.AddNew;
        }

        private TestAppointment(int TestAppointmentID, TestType.enTestTypes TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;           
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeAppInfo = BaseApplication.FindBaseApplication(RetakeTestApplicationID);
            Mode = enMode.Update;
        }

        public static TestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1, LocalDrivingLicenseApplicationID = -1, CreatedByUserID = -1, RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            bool IsLocked = false;

            if (TestAppointmentAccess.GetTestAppointmentByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                return new TestAppointment(TestAppointmentID, (TestType.enTestTypes)TestTypeID, LocalDrivingLicenseApplicationID,
                    AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }
            else
                return null;
        }

        public static TestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, TestType.enTestTypes TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            if (TestAppointmentAccess.GetLastTestAppointment(LocalDrivingLicenseApplicationID, ref TestAppointmentID,
               (int) TestTypeID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {

                return new TestAppointment(TestAppointmentID, (TestType.enTestTypes)TestTypeID, LocalDrivingLicenseApplicationID,
                    AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }
            else
            {
                return null;
            }
        }

        private bool _Add()
        {
            this.TestAppointmentID = TestAppointmentAccess.AddTestAppointment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }

        private bool _Update()
        {
            return TestAppointmentAccess.UpdateTestAppointment(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_Add())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _Update();
            }

            return false;
        }

        public  DataTable GetApplicationTestAppointments(TestType.enTestTypes TestTypeID)
        {
            return TestAppointmentAccess.GetAllTestAppointmentsPerTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static DataTable GetApplicationTestAppointments(int LocalDrivingLicenseAppID, TestType.enTestTypes TestTypeID)
        {
            return TestAppointmentAccess.GetAllTestAppointmentsPerTestType(LocalDrivingLicenseAppID, (int)TestTypeID);
        }

        private int _GetTestID()
        {
            return TestAppointmentAccess.GetTestID(this.TestAppointmentID);
        }


    }
}