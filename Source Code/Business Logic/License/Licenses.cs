using Business_Logic.Application_Types;
using Business_Logic.BaseApplicationClass;
using BusinessLayer.DetainedLicenses;
using DbAccess.LicenseAccess;
using System;
using System.Data;


namespace Business_Logic.Licenses
{
    public class Licenses
    {
        
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public Drivers.Drivers DriverInfo { get; set; }
        public int LicenseClassID { get; set; }
        public LicenseClass licenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public string IssueReasonText
        {
            get { return _GetIssueReasonText(this.IssueReason); }
        }
        public int CreatedByUserID { get; set; }
        public DetainedLicense DetainInfo { get; set; }

        public bool IsDetained
        {
            get { return DetainedLicense.IsLicenseDetained(LicenseID); }
        }
        public Licenses()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }
      
        private Licenses(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees,
            bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;

            this.ApplicationID = ApplicationID;

            this.DriverID = DriverID;
            this.DriverInfo = Drivers.Drivers.FindByDriverID(this.DriverID);
            
            this.LicenseClassID = LicenseClassID;
            this.licenseClass = LicenseClass.Find(LicenseClassID);
            
            this.IssueDate = IssueDate;
            
            this.ExpirationDate = ExpirationDate;
            
            this.Notes = Notes;
            
            this.PaidFees = PaidFees;
            
            this.IsActive = IsActive;
            
            this.IssueReason = (enIssueReason)IssueReason;
            
            this.CreatedByUserID = CreatedByUserID;
            
            this.DetainInfo = DetainedLicense.FindByLicenseID(this.LicenseID);

            Mode = enMode.Update;
        }
     
        private bool _AddNewLicense()
        {
            this.LicenseID = DbAccess.LicenseAccess.LicenseAccess.AddNewLicense(
                this.ApplicationID, this.DriverID, this.LicenseClassID,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                (byte)this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            return DbAccess.LicenseAccess.LicenseAccess.UpdateLicense(
                this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClassID,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }
     
        public static Licenses Find(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 1;

            if (DbAccess.LicenseAccess.LicenseAccess.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new Licenses(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
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
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateLicense();
            }
            return false;
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClass)
        {
            return DbAccess.LicenseAccess.LicenseAccess.GetActiveLicenseIDByPersonID(PersonID, LicenseClass);
        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        public static DataTable GetAllLicenses()
        {
            return DbAccess.LicenseAccess.LicenseAccess.GetLicensesList();
        }

        public  DataTable GetDriverLicenses()
        {
            return LicenseAccess.GetDriverLicenses(this.DriverID);
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return LicenseAccess.GetDriverLicenses(DriverID);
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return DetainedLicense.GetAllDetainedLicenses();
        }

        public bool IsLicenseExpired()
        {
            return this.ExpirationDate.Date < DateTime.Now.Date;
        }

        public static bool DeactivateLicense(int LicenseID)
        {
            return DbAccess.LicenseAccess.LicenseAccess.DeactivateLicense(LicenseID);
        }

        public bool DeactivateLicense()
        {
            return DbAccess.LicenseAccess.LicenseAccess.DeactivateLicense(this.LicenseID);
        }

        private string _GetIssueReasonText(enIssueReason reason)
        {
            switch (reason)
            {
               
                case enIssueReason.FirstTime:          return "First Time";
                case enIssueReason.Renew:              return "Renew";
                case enIssueReason.DamagedReplacement: return "DamagedReplacement";
                case enIssueReason.LostReplacement:    return "LostReplacement";
                
                default: return "First Time";
            }
        }

        public Licenses RenewLicense(string Notes , int CreatedByUserID)
        {
            BaseApplication RenewLicenseApp = new BaseApplication();

            RenewLicenseApp.ApplicantPersonID = this.DriverInfo.PersonID;
            RenewLicenseApp.ApplicationDate = DateTime.Now;
            RenewLicenseApp.ApplicationTypeID = (int)BaseApplication.enApplicationType.RenewDrivingLicense ;
            RenewLicenseApp.ApplicationStatus = BaseApplication.enApplicationStatus.Completed;
            RenewLicenseApp.LastStatusDate = DateTime.Now;
            RenewLicenseApp.PaidFees = ApplicationType.Find((int)BaseApplication.enApplicationType.RenewDrivingLicense).Fees;
            RenewLicenseApp.CreatedByUserID = CreatedByUserID;

            if(!RenewLicenseApp.Save())
            {
                return null;
            }

            Licenses NewLicense = new Licenses();
            NewLicense.ApplicationID = RenewLicenseApp.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(LicenseClass.Find(this.LicenseClassID).DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = LicenseClass.Find(this.LicenseClassID).ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = enIssueReason.Renew;
            NewLicense.CreatedByUserID = CreatedByUserID;


            if (!NewLicense.Save())
            {
                RenewLicenseApp.Delete();
                return null;
            }


            DeactivateLicense();
            return NewLicense;
        }

        public Licenses ReplaceLicense(int CreatedByUserID , enIssueReason IssueReason)
        {
            if(IssueReason != enIssueReason.LostReplacement && IssueReason != enIssueReason.DamagedReplacement)
            {
                return null;
            }

            BaseApplication ReplaceLicenseApp = new BaseApplication();

            ReplaceLicenseApp.ApplicantPersonID = this.DriverInfo.PersonID;
            ReplaceLicenseApp.ApplicationDate = DateTime.Now;
            ReplaceLicenseApp.ApplicationStatus = BaseApplication.enApplicationStatus.Completed;
            ReplaceLicenseApp.LastStatusDate = DateTime.Now;
            ReplaceLicenseApp.CreatedByUserID = CreatedByUserID;

            if (IssueReason == enIssueReason.LostReplacement)
            {
                ReplaceLicenseApp.PaidFees = ApplicationType.Find((int)BaseApplication.enApplicationType.ReplaceLostDrivingLicense).Fees;
                ReplaceLicenseApp.ApplicationTypeID = (int)BaseApplication.enApplicationType.ReplaceLostDrivingLicense;
            }
            else
            {
                ReplaceLicenseApp.PaidFees = ApplicationType.Find((int)BaseApplication.enApplicationType.ReplaceDamagedDrivingLicense).Fees;
                ReplaceLicenseApp.ApplicationTypeID = (int)BaseApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            }

            if (!ReplaceLicenseApp.Save())
            {
                return null;
            }

            Licenses NewLicense = new Licenses();
            NewLicense.ApplicationID = ReplaceLicenseApp.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes =this.Notes;
            NewLicense.PaidFees =0;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;


            if (!NewLicense.Save())
            {
                ReplaceLicenseApp.Delete();
                return null;
            }


            DeactivateLicense();
            return NewLicense;
        }

        public bool Detain(int CreatedByUserID , float FineFees)
        {

            DetainedLicense detain = new DetainedLicense();

            detain.LicenseID = this.LicenseID;
            detain.DetainDate = DateTime.Now;
            detain.FineFees = FineFees;
            detain.CreatedByUserID = CreatedByUserID;
            detain.IsReleased = false;

            if(!detain.Save())
            {
                return false;
            }

            this.DetainInfo = detain;

            return true;
        }

        public bool Release(int CreatedByUserID , ref int AppID)
        {

            BaseApplication ReleaseLicenseApp = new BaseApplication();

            ReleaseLicenseApp.ApplicantPersonID = this.DriverInfo.PersonID;
            ReleaseLicenseApp.ApplicationDate = DateTime.Now;
            ReleaseLicenseApp.ApplicationTypeID = (int)BaseApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            ReleaseLicenseApp.ApplicationStatus = BaseApplication.enApplicationStatus.Completed;
            ReleaseLicenseApp.LastStatusDate = DateTime.Now;
            ReleaseLicenseApp.PaidFees = ApplicationType.Find((int)BaseApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees;
            ReleaseLicenseApp.CreatedByUserID = CreatedByUserID;

            if (!ReleaseLicenseApp.Save())
            {
                return false;
            }

            AppID = ReleaseLicenseApp.ApplicationID;
            this.DetainInfo.ReleaseApplicationID = ReleaseLicenseApp.ApplicationID;
            this.DetainInfo.ReleaseDate = DateTime.Now;
            this.DetainInfo.ReleasedByUserID = CreatedByUserID;

            if(!DetainInfo.Release(CreatedByUserID , ReleaseLicenseApp.ApplicationID))
            {
                AppID = -1;
                ReleaseLicenseApp.Delete();
                return false;
            }


            return true;

        }


    }
}