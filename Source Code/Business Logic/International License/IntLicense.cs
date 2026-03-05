using Business_Logic;
using Business_Logic.Application_Types;
using Business_Logic.BaseApplicationClass;
using Business_Logic.Drivers;
using Business_Logic.Licenses;
using DbAccess.LicenseAccess;
using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLayer.LicenseBusiness
{
    public class InternationalLicense : BaseApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        public Drivers DriverInfo { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public InternationalLicense()
        {
            base.ApplicationTypeID = (int)BaseApplication.enApplicationType.NewInternationalLicense;

            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;

            Mode = enMode.AddNew;
        }

        private InternationalLicense(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID,
             int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive)
        {
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)BaseApplication.enApplicationType.NewInternationalLicense;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;


            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = Drivers.FindByDriverID(this.DriverID);
            Mode = enMode.Update;
        }

        public static InternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1; int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true; int CreatedByUserID = 1;

            if (InternationalLicenseAccess.GetInternationalLicenseInfoByID(InternationalLicenseID, ref ApplicationID, ref DriverID,
                ref IssuedUsingLocalLicenseID,
            ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                BaseApplication Application = BaseApplication.FindBaseApplication(ApplicationID);


                return new InternationalLicense(Application.ApplicationID,Application.ApplicantPersonID,Application.ApplicationDate,(enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID,InternationalLicenseID, DriverID, IssuedUsingLocalLicenseID,IssueDate, ExpirationDate, IsActive);

            }

            else
                return null;

        }

        private bool _AddNew()
        {

            this.InternationalLicenseID = InternationalLicenseAccess.AddNewInternationalLicense(
                this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
                this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);


            return (InternationalLicenseID != -1);
        }

        private bool _Update()
        {
            return InternationalLicenseAccess.UpdateInternationalLicense(
                this.InternationalLicenseID, this.ApplicationID, this.DriverID,
                this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate,
                this.IsActive, this.CreatedByUserID);
        }

        public bool Save()
        {
            base.Mode =(BaseApplication.enMode)Mode;
            if(!base.Save()) return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _Update();
            }

            return false;
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return InternationalLicenseAccess.GetAllInternationalLicenses();
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return InternationalLicenseAccess.GetInternationalDrivingLicenses(DriverID);
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return InternationalLicenseAccess.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }

        public static bool Deactivate(int InternationalLicenseID)
        {
            return InternationalLicenseAccess.DeactivateInternationalLicense(InternationalLicenseID);
        }
    }
}