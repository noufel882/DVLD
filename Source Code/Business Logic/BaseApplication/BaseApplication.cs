using System;
using Business_Logic.Application_Types;
using DbAccess.ApplicationsAccess;

namespace Business_Logic.BaseApplicationClass
{
    public class BaseApplication
    {
        public enum enApplicationType
        {

            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public enum enMode { AddNew = 0, Update = 1 };

        public enMode Mode {  get; set; }

        public int ApplicationID { get; set; }

        public int ApplicationTypeID { get; set; }
        public ApplicationType ApplicationTypeInfo { get; set; }

        public enApplicationStatus ApplicationStatus { get; set; }
        public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Canceled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                }
                return "Unknown";
            }
        }
        public DateTime LastStatusDate { get; set; }
        public DateTime ApplicationDate { get; set; }

        public float PaidFees { get; set; }

        public int ApplicantPersonID { get; set; }
        public Person PersonInfo { get; set; }
        public string ApplicantFullName
        {
            get
            {
                return Person.Find(ApplicantPersonID).FullName;
            }
        }

        public int CreatedByUserID { get; set; }
        public User CreatedByUserInfo {  get; set; }
     
        public BaseApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New; 
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }
    
        private BaseApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
            float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PersonInfo = Person.Find(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = ApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus =ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = User.FindByUserID(CreatedByUserID);
            Mode = enMode.Update;
        }
    
        public static BaseApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1, ApplicationTypeID = -1, CreatedByUserID = -1;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            byte ApplicationStatus = 1;
            float PaidFees = 0;

            bool IsFound = ApplicationsAccess.GetApplicationInfoByID(
                ApplicationID, ref ApplicantPersonID, ref ApplicationDate,
                ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate,
                ref PaidFees, ref CreatedByUserID);

            if (IsFound)
                return new BaseApplication(ApplicationID, ApplicantPersonID, ApplicationDate,
                    ApplicationTypeID, (enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            else
                return null;
        }
     
        private bool _AddNew()
        {
            this.ApplicationID = ApplicationsAccess.AddNewApplication(
                this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
                (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _Update()
        {
            return ApplicationsAccess.UpdateApplication(
                this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus, this.LastStatusDate,
                this.PaidFees, this.CreatedByUserID);
        }

        public static bool Delete(int ApplicationID)
        {
            return ApplicationsAccess.DeleteApplication(ApplicationID);
        }

        public bool Delete()
        {
            return ApplicationsAccess.DeleteApplication(this.ApplicationID);
        }

        public bool Save()
        {
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

        public  bool Cancel()
        {
            return ApplicationsAccess.UpdateApplicationStatus(this.ApplicationID,(byte)enApplicationStatus.Cancelled);
        }

        public bool SetComplete()
        {
            return ApplicationsAccess.UpdateApplicationStatus(this.ApplicationID, (byte)enApplicationStatus.Completed);
        }

        public static bool IsExist(int ApplicationID)
        {
            return ApplicationsAccess.IsApplicationExist(ApplicationID);
        }

        public static bool UpdateStatus(int ApplicationID, byte NewStatus)
        {
            return ApplicationsAccess.UpdateApplicationStatus(ApplicationID, NewStatus);
        }

        public static bool DoesPersonHasActiveApps(int ApplicationID, byte ApplicationTypeID)
        {
            return ApplicationsAccess.DoesPersonHasActiveApps(ApplicationID , ApplicationTypeID);
        }

        public bool DoesPersonHasActiveApps(byte ApplicationTypeID)
        {
            return ApplicationsAccess.DoesPersonHasActiveApps(this.ApplicationID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, BaseApplication.enApplicationType ApplicationTypeID)
        {
            return ApplicationsAccess.GetActiveAppID(PersonID, (byte)ApplicationTypeID);
        }

        public int GetActiveApplicationID(BaseApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, BaseApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return ApplicationsAccess.GetActiveAppIDForLicenseClass(PersonID, (byte)ApplicationTypeID, LicenseClassID);
        }

    }
}