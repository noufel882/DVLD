using System;
using System.Data;
using System.Data.SqlClient;
using DbAccess.Settings;
using Utils;

namespace DbAccess.TestTypesAccess
{
    public class TestTypesAccess
    {
        public static DataTable GetTestTypesList()
        {
            DataTable dt = new DataTable();
            string query = $@"SELECT * FROM {AccessSettings.TestTypesTableName}";

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
                Log.LogException(ex, nameof(GetTestTypesList), nameof(TestTypesAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool Find(int TestTypeID, ref string Title, ref string Description, ref float Fees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            string query = $@"SELECT * FROM {AccessSettings.TestTypesTableName} 
                              WHERE TestTypeID = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    Title = (string)reader["TestTypeTitle"];
                    Description = (string)reader["TestTypeDescription"];
                    Fees = Convert.ToSingle(reader["TestTypeFees"]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(Find), nameof(TestTypesAccess), AccessSettings.ErrorLogFile);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool UpdateTestTypeInfo(int TestTypeID, string Title, string Description, float Fees)
        {
            int rowsAffected = 0;

            string query = $@"UPDATE {AccessSettings.TestTypesTableName} 
                              SET TestTypeTitle = @Title, 
                                  TestTypeDescription = @Description,
                                  TestTypeFees = @Fees 
                              WHERE TestTypeID = @ID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", TestTypeID);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(UpdateTestTypeInfo), nameof(TestTypesAccess), AccessSettings.ErrorLogFile);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static int AddNewTestType(string Title, string Description, float Fees)
        {
            int insertedID = -1;

            string query = $@"INSERT INTO {AccessSettings.TestTypesTableName} 
                              (TestTypeTitle, TestTypeDescription, TestTypeFees)
                              VALUES (@Title, @Description, @Fees);
                              SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    insertedID = id;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(AddNewTestType), nameof(TestTypesAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return insertedID;
        }


    }
}