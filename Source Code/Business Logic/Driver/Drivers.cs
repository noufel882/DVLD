using Business_Logic.Licenses;
using DbAccess.DriverAccess;
using DbAccess.LicenseAccess;
using System;
using System.Data;

namespace Business_Logic.Drivers
{
    public class Drivers
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public Person PersonalInfo { get; set; }

        public Drivers()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;

            Mode = enMode.AddNew;
        }

        private Drivers(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonalInfo = Person.Find(PersonID);
            Mode = enMode.Update;
        }

        private bool _AddNewDriver()
        {
            this.DriverID = DriverAccess.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            return (this.DriverID != -1);
        }

        private bool _UpdateDriver()
        {
            return DriverAccess.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
        }

        public static Drivers FindByDriverID(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (DriverAccess.GetDriverInfoByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new Drivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }

        public static Drivers FindByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (DriverAccess.GetDriverInfoByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
            {
                return new Drivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
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
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateDriver();
            }
            return false;
        }

        public static DataTable GetAllDrivers()
        {
            return DriverAccess.GetAllDrivers();
        }

        public DataTable GetDriverLicenses()
        {
            return Licenses.Licenses.GetDriverLicenses(this.DriverID);
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return Licenses.Licenses.GetDriverLicenses(DriverID);
        }
        public DataTable GetInternationalDriverLicense()
        {
            return InternationalLicenseAccess.GetInternationalDrivingLicenses(this.DriverID);
        }

        public static DataTable GetInternationalDriverLicense(int DriverID)
        {
            return InternationalLicenseAccess.GetInternationalDrivingLicenses(DriverID);
        }

    }
}