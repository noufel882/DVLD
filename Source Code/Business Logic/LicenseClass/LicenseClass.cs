using System;
using System.Data;
using DbAccess.LicenseClassAccess;

namespace Business_Logic
{
    public class LicenseClass
    {
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }

        public LicenseClass()
        {
            this.LicenseClassID = -1;
            this.ClassName = string.Empty;
            this.ClassDescription = string.Empty;
            this.MinimumAllowedAge = 18;
            this.DefaultValidityLength = 10;
            this.ClassFees = 0;
        }

       
        private LicenseClass(int LicenseClassID, string ClassName, string ClassDescription,
            byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
        }

        /// <summary>
        /// Retrieves all license classes as a DataTable for presentation purposes.
        /// </summary>
        /// <returns>A DataTable containing all license classes.</returns>
        public static DataTable GetAllLicenseClasses()
        {
            return LicenseClassAccess.GetAllClasses();
        }

        /// <summary>
        /// Finds a license class by its identifier.
        /// </summary>       
        /// <returns>A LicenseClass object if found; otherwise, returns null.</returns>
        public static LicenseClass Find(int LicenseClassID)
        {
            string ClassName = "", ClassDescription = "";
            byte MinimumAllowedAge = 0, DefaultValidityLength = 0;
            float ClassFees = 0;

            if (LicenseClassAccess.GetLicenseClassInfoByID(LicenseClassID,
                ref ClassName, ref ClassDescription,
                ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new LicenseClass(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Finds a license class by its name.
        /// </summary>       
        /// <returns>A LicenseClass object if found; otherwise, returns null.</returns>
        public static LicenseClass Find(string ClassName)
        {
            int LicenseClassID = -1;
            string ClassDescription = "";
            byte MinimumAllowedAge = 0, DefaultValidityLength = 0;
            float ClassFees = 0;

            if (LicenseClassAccess.GetLicenseClassInfoByClassName(ClassName,
                ref LicenseClassID, ref ClassDescription,
                ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new LicenseClass(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }
    }
}