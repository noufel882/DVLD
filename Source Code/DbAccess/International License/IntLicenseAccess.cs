using DbAccess.Settings;
using System;
using System.Data;
using System.Data.SqlClient;
using Utils;

namespace DbAccess.LicenseAccess
{
    public class InternationalLicenseAccess
    {
        public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID,ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID,ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;
            string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetInternationalLicenseInfoByID), nameof(InternationalLicenseAccess));
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT InternationalLicenseID,ApplicationID , DriverID ,IssuedUsingLocalLicenseID, IssueDate ,ExpirationDate , IsActive FROM InternationalLicenses
ORDER BY IsActive , ExpirationDate DESC";

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
                Log.LogException(ex, nameof(GetAllInternationalLicenses), nameof(InternationalLicenseAccess));
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetInternationalDrivingLicenses(int DriverID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            string query = @" 
                          SELECT InternationalLicenseID,ApplicationID , DriverID ,IssuedUsingLocalLicenseID, IssueDate ,ExpirationDate , IsActive FROM InternationalLicenses
                           
                           WHERE InternationalLicenses.DriverID= @DriverID
                           
                           ORDER BY ExpirationDate DESC
                            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetInternationalDrivingLicenses), nameof(LicenseAccess));

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int InternationalLicenseID = -1;
            string query = @"
                UPDATE InternationalLicenses
                SET IsActive = 0
                WHERE DriverID = @DriverID
                INSERT INTO InternationalLicenses
                (ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID)
                VALUES
                (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID);
                SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int value))
                {
                    InternationalLicenseID = value;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(AddNewInternationalLicense), nameof(InternationalLicenseAccess));
            }
            finally
            {
                connection.Close();
            }

            return InternationalLicenseID;
        }

        public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID,
            int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int rowsAffected = 0;
            string query = @"
                UPDATE InternationalLicenses
                SET ApplicationID = @ApplicationID,
                    DriverID = @DriverID,
                    IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID,
                    IssueDate = @IssueDate,
                    ExpirationDate = @ExpirationDate,
                    IsActive = @IsActive,
                    CreatedByUserID = @CreatedByUserID
                WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(UpdateInternationalLicense), nameof(InternationalLicenseAccess));
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;
            string query = @"SELECT InternationalLicenseID FROM InternationalLicenses WHERE DriverID = @DriverID AND GetDate() BETWEEN IssueDate AND  ExpirationDate 
                            ORDER BY  ExpirationDate DESC
                            ";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int value))
                {
                    InternationalLicenseID = value;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetActiveInternationalLicenseIDByDriverID), nameof(InternationalLicenseAccess));
            }
            finally
            {
                connection.Close();
            }

            return InternationalLicenseID;
        }

        public static bool DeactivateInternationalLicense(int InternationalLicenseID)
        {
            int rowsAffected = 0;
            string query = @"UPDATE InternationalLicenses 
                    SET IsActive = 0 
                    WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(DeactivateInternationalLicense), nameof(InternationalLicenseAccess));
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }


    }
}