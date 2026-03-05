using System;
using System.Data;
using System.Data.SqlClient;
using DbAccess.Settings;
using Utils;

namespace DbAccess.LicenseClassAccess
{
    public class LicenseClassAccess
    {
        /// <summary>
        /// Retrieves all license classes from the database.
        /// </summary>
        public static DataTable GetAllClasses()
        {
            DataTable dt = new DataTable();
            string query = $@"SELECT * FROM {AccessSettings.LicenseClassTableName}";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

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
                Log.LogException(ex, nameof(GetAllClasses), nameof(LicenseClassAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        /// <summary>
        /// Searches for a license class by its ID using float for fees.
        /// </summary>
        public static bool GetLicenseClassInfoByID(int LicenseClassID,
            ref string ClassName, ref string ClassDescription,
            ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;
            string query = $@"SELECT * FROM {AccessSettings.LicenseClassTableName} WHERE LicenseClassID = @ID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LicenseClassID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];

                 
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetLicenseClassInfoByID), nameof(LicenseClassAccess), AccessSettings.ErrorLogFile);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        /// <summary>
        /// Searches for a license class by its Name using float for fees.
        /// </summary>
        public static bool GetLicenseClassInfoByClassName(string ClassName,
            ref int LicenseClassID, ref string ClassDescription,
            ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;
            string query = $@"SELECT * FROM {AccessSettings.LicenseClassTableName} WHERE ClassName = @ClassName";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];


                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetLicenseClassInfoByClassName), nameof(LicenseClassAccess), AccessSettings.ErrorLogFile);
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