using System;
using System.Data;
using System.Data.SqlClient;
using DbAccess.Settings;
using Utils;

namespace DbAccess.Tests
{
    public class TestAppointmentAccess
    {
        public static bool GetTestAppointmentByID(int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;
            string query = $"SELECT * FROM {AccessSettings.TestAppointmentTableName} WHERE TestAppointmentID = @TestAppointmentID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = Convert.ToDateTime( reader["AppointmentDate"]);
                    PaidFees = Convert.ToSingle( reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = Convert.ToBoolean( reader["IsLocked"]);

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                Log.LogException(ex, nameof(GetTestAppointmentByID), nameof(TestAppointmentAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool GetLastTestAppointment(int LocalDrivingLicenseApplicationID, ref int TestAppointmentID, int TestTypeID, ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;
            string query = $@"
                                SELECT Top 1 * FROM {AccessSettings.TestAppointmentTableName} WHERE
                                (TestTypeID = @Type) and (LocalDrivingLicenseApplicationID = @ID)
                                
                                ORDER BY TestAppointMentID DESC
                             ";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Type", TestTypeID);
            command.Parameters.AddWithValue("@ID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (float)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                Log.LogException(ex, nameof(GetTestAppointmentByID), nameof(TestAppointmentAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();
            string query = $@"SELECT * FROM TestAppointments_View ORDER BY TestAppointmentID DESC";

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
                Log.LogException(ex, nameof(GetAllTestAppointments), nameof(TestAppointmentAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable GetAllTestAppointmentsPerTestType(int LocalDrivingLicenseAppID, int TestType)
        {
            DataTable dt = new DataTable();
            string query = $@"
                           SELECT TestAppointmentID , AppointmentDate , PaidFees , IsLocked FROM {AccessSettings.TestAppointmentTableName}
                           WHERE
                                   LocalDrivingLicenseApplicationID = @ID AND  TestTypeID = @Type
                           ORDER BY TestAppointmentID DESC
                            ";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@Type", TestType);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetAllTestAppointmentsPerTestType), nameof(TestAppointmentAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static int AddTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, int RetakeTestApplicationID)
        {
            int AppointmentID = -1;
            string query = $@"
                INSERT INTO {AccessSettings.TestAppointmentTableName}
                (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID)
                VALUES
                (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, 0, @RetakeTestApplicationID);
                
                SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (RetakeTestApplicationID < 0)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int value))
                    AppointmentID = value;
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(AddTestAppointment), nameof(TestAppointmentAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }
            return AppointmentID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int rowsAffected = 0;
            string query = $@"
               UPDATE {AccessSettings.TestAppointmentTableName}
                SET TestTypeID = @TestTypeID,
                    LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                    AppointmentDate = @AppointmentDate,
                    PaidFees = @PaidFees,
                    CreatedByUserID = @CreatedByUserID,
                    IsLocked = @IsLocked,
                    RetakeTestApplicationID = @RetakeTestApplicationID
                WHERE TestAppointmentID = @TestAppointmentID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            if (RetakeTestApplicationID == -1)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(UpdateTestAppointment), nameof(TestAppointmentAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }
            return rowsAffected > 0;
        }

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }

            catch (Exception ex)
            {
                Log.LogException(ex , nameof(GetTestID) , nameof(TestAppointmentAccess) ,AccessSettings.ErrorLogFile);

            }

            finally
            {
                connection.Close();
            }


            return TestID;

        }




    }
}

