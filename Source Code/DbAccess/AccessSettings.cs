using System.Configuration;

namespace DbAccess.Settings
{
    internal class AccessSettings
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        public static readonly string ErrorLogFile = @"C:\DVLD\Log\Errors\Data_Access_Errors.txt";
   
        public static readonly string PeopleTableName = "People";
        public static readonly string CountryTableName = "Countries";
        public static readonly string UsersTableName = "Users";
        public static readonly string ApplicationTypesTableName = "ApplicationTypes";
        public static readonly string TestTypesTableName = "TestTypes";
        public static readonly string LicenseClassTableName = "LicenseClasses";
        public static readonly string ApplicationsTableName = "Applications";
        public static readonly string TestAppointmentTableName = "TestAppointments";
        public static readonly string LocalDrivingLicenseApplicationsTableName = "LocalDrivingLicenseApplications";
        public static readonly string LicensesTableName = "Licenses";
     

        
    }
}