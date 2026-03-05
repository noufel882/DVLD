using System;
using System.Data;
using System.Data.SqlClient;
using DbAccess.Settings;
using Utils;

namespace DbAccess.ApplicationsAccess
{
    public class ApplicationsAccess
    {
        public static DataTable GetApplicationsList()
        {
            DataTable dt = new DataTable();
            string query = $"SELECT * FROM {AccessSettings.ApplicationsTableName}";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetApplicationsList), nameof(ApplicationsAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,byte ApplicationStatus, DateTime LastStatusDate,float PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;
            string query = $@"
                INSERT INTO {AccessSettings.ApplicationsTableName}
                (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
                VALUES
                (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
                SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int value))
                {
                    ApplicationID = value;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(AddNewApplication), nameof(ApplicationsAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,int ApplicationTypeID, byte ApplicationStatus,DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int rowsAffected = 0;
            string query = $@"
                UPDATE {AccessSettings.ApplicationsTableName}
                SET ApplicantPersonID = @ApplicantPersonID,
                    ApplicationDate = @ApplicationDate,
                    ApplicationTypeID = @ApplicationTypeID,
                    ApplicationStatus = @ApplicationStatus,
                    LastStatusDate = @LastStatusDate,
                    PaidFees = @PaidFees,
                    CreatedByUserID = @CreatedByUserID
                WHERE ApplicationID = @ApplicationID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(UpdateApplication), nameof(ApplicationsAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }

        public static bool GetApplicationInfoByID(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate,ref int ApplicationTypeID, ref byte ApplicationStatus,ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;
            string query = $"SELECT * FROM {AccessSettings.ApplicationsTableName} WHERE ApplicationID = @ApplicationID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = Convert.ToByte(reader["ApplicationStatus"]);
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetApplicationInfoByID), nameof(ApplicationsAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetApplicationInfoByPersonID(int ApplicantPersonID, ref int ApplicationID, ref DateTime ApplicationDate,ref int ApplicationTypeID, ref byte ApplicationStatus,ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;
            string query = $"SELECT * FROM {AccessSettings.ApplicationsTableName} WHERE ApplicantPersonID = @ApplicantPersonID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = Convert.ToByte(reader["ApplicationStatus"]);
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetApplicationInfoByPersonID), nameof(ApplicationsAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;
            string query = $"DELETE FROM {AccessSettings.ApplicationsTableName} WHERE ApplicationID = @ApplicationID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(DeleteApplication), nameof(ApplicationsAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }

        public static bool DoesPersonHasActiveApps(int ApplicationID)
        {
            bool isExist = false;
            string query = $@"SELECT FOUND = 1 FROM {AccessSettings.ApplicationsTableName} WHERE ApplicationID = @Id";
            SqlConnection connection = new SqlConnection( AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue("@Id" , ApplicationID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null )
                {
                   isExist= true;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(DoesPersonHasActiveApps), nameof(ApplicationsAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return isExist;
        }

        public static bool DoesPersonHasActiveApps(int ApplicantPersonID, byte ApplicationTypeID)
        {
            return GetActiveAppID(ApplicantPersonID , ApplicationTypeID) != -1;
        }

        public static int GetActiveAppID(int PersonID , byte ApplicationTypeID)
        {
            int ApplicationID = -1;
            string query = $@"
                                select ApplicationID from {AccessSettings.ApplicationsTableName}
                                where ApplicantPersonID = @ID and ApplicationTypeID = @Type and ApplicationStatus = 1
                            ";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID",PersonID);
            command.Parameters.AddWithValue("Type",ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int value))
                {
                    ApplicationID = value;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetActiveAppID), nameof(ApplicationsAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;
        }

        public static bool UpdateApplicationStatus(int ApplicationID, byte NewStatus)
        {
            int rowsAffected = 0;
            string query = $@"
                    UPDATE {AccessSettings.ApplicationsTableName}
                    SET 
                        [ApplicationStatus] = @NewStatus,
                        [LastStatusDate] = GETDATE()
                    WHERE ApplicationID = @ApplicationID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NewStatus", NewStatus);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(UpdateApplicationStatus), nameof(ApplicationsAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }

        public static int GetActiveAppIDForLicenseClass(int PersonID, byte ApplicationTypeID,int LicenseClass)
        {
            int ApplicationID = -1;

            string AppsTable =  AccessSettings.ApplicationsTableName ;
            string LDLApps =  AccessSettings.LocalDrivingLicenseApplicationsTableName ;

            string query = $@"
                                SELECT ActiveAppID =Apps.ApplicationID FROM {AppsTable} AS Apps
                                    INNER JOIN {LDLApps} AS LDLApps
                                    ON Apps.ApplicationID  = LDLApps.ApplicationID
                               WHERE
                                        Apps.ApplicantPersonID = @ID 
                                    AND Apps.ApplicationTypeID = @Type 
                                    AND LDLApps.LicenseClassID = @Class 
                                    AND Apps.ApplicationStatus = 1
                            ";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", PersonID);
            command.Parameters.AddWithValue("@Type", ApplicationTypeID);
            command.Parameters.AddWithValue("@Class", LicenseClass);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int value))
                {
                    ApplicationID = value;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetActiveAppIDForLicenseClass), nameof(ApplicationsAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            string query = $"SELECT Found=1 FROM {AccessSettings.ApplicationsTableName} WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(IsApplicationExist), nameof(ApplicationsAccess), AccessSettings.ErrorLogFile);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


    }
}