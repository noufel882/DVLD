using DbAccess.Settings;
using System;
using System.Data;
using System.Data.SqlClient;
using Utils;

namespace DbAccess.DriverAccess
{
    public class DriverAccess
    {
        public static bool GetDriverInfoByID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            
            string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetDriverInfoByID), nameof(DriverAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetDriverInfoByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID ";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetDriverInfoByPersonID), nameof(DriverAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Drivers_View order by FullName";

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
                Log.LogException(ex, nameof(GetAllDrivers), nameof(DriverAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int DriverID = -1;
            string query = @"INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
                             VALUES (@PersonID, @CreatedByUserID, @CreatedDate);
                             SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int value))
                {
                    DriverID = value;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(AddNewDriver), nameof(DriverAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return DriverID;
        }

        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int rowsAffected = 0;
            string query = @"UPDATE Drivers
                             SET PersonID = @PersonID,
                                 CreatedByUserID = @CreatedByUserID,
                                 CreatedDate = @CreatedDate
                             WHERE DriverID = @DriverID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(UpdateDriver), nameof(DriverAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }


    }
}