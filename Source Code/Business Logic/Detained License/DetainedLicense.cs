using System;
using System.Data;
using Business_Logic;
using DbAccess.DetainedLicensesAccess;

namespace BusinessLayer.DetainedLicenses
{
    public class DetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public User CreatedByUserInfo { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public User ReleasedByUserInfo { get; set; }
        public int ReleaseApplicationID { get; set; }

        public DetainedLicense()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MinValue;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;

            Mode = enMode.AddNew;
        }

        private DetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,float FineFees, int CreatedByUserID, bool IsReleased,DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;

            this.CreatedByUserInfo = User.FindByUserID(CreatedByUserID);
            this.ReleasedByUserInfo = User.FindByUserID(ReleasedByUserID);

            Mode = enMode.Update;
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = DetainedLicensesAccess.AddNewDetainedLicense(
                this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);

            return (this.DetainID != -1);
        }

        private bool _UpdateDetainedLicense()
        {
            return DetainedLicensesAccess.UpdateDetainedLicense(this.DetainID , this.LicenseID ,this.DetainDate ,this.FineFees ,this.CreatedByUserID);
        }

        public static DetainedLicense FindByDetainID(int DetainID)
        {
            int LicenseID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.MinValue;
            float FineFees = 0;
            bool IsReleased = false;

            if (DetainedLicensesAccess.GetDetainedLicenseInfoByID(DetainID, ref LicenseID, ref DetainDate,
                ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate,
                ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new DetainedLicense(DetainID, LicenseID, DetainDate, FineFees,
                    CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static DetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.MinValue;
            float FineFees = 0;
            bool IsReleased = false;

            if (DetainedLicensesAccess.GetDetainedLicenseInfoByLicenseID(LicenseID , ref DetainID, ref DetainDate,
                ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate,
                ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new DetainedLicense(DetainID, LicenseID, DetainDate, FineFees,
                    CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return DetainedLicensesAccess.GetDetainedLicensesList();
        }

        public bool Release(int ReleasedByUserID,int ReleaseApplicationID)
        {
            return DetainedLicensesAccess.ReleaseDetainedLicense(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return DetainedLicensesAccess.IsLicenseDetained(LicenseID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDetainedLicense();
            }

            return false;
        }

    }
}