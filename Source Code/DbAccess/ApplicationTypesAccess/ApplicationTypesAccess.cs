using System;
using System.Data;
using System.Data.SqlClient;
using DbAccess.Settings;
using Utils;

namespace DbAccess.ApplicationTypesAccess
{
    public class ApplicationTypesAccess
    {
        public static DataTable GetTypesList()
        {
            DataTable dt = new DataTable();
            string query = $@"SELECT * FROM {AccessSettings.ApplicationTypesTableName}";

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
                Log.LogException(ex, nameof(GetTypesList), nameof(ApplicationTypesAccess), AccessSettings.ErrorLogFile);
                throw;
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool UpdateApplicationTypeInfo(int TypeID, string Name, float Fees)
        {
            int rowsAffected = 0;

            string query = $@"UPDATE {AccessSettings.ApplicationTypesTableName} 
                              SET ApplicationTypeTitle = @Title, 
                                  ApplicationFees = @Fees 
                              WHERE ApplicationTypeID = @ID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Title", Name);
            command.Parameters.AddWithValue("@Fees", Fees);
            command.Parameters.AddWithValue("@ID", TypeID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(UpdateApplicationTypeInfo), nameof(ApplicationTypesAccess), AccessSettings.ErrorLogFile);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
    
        public static bool Find(int TypeID, ref string Name, ref float Fees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            string query = $@"SELECT * FROM {AccessSettings.ApplicationTypesTableName} 
                              WHERE ApplicationTypeID = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", TypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    Name = (string)reader["ApplicationTypeTitle"];
                    Fees = Convert.ToSingle(reader["ApplicationFees"]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(Find), nameof(ApplicationTypesAccess), AccessSettings.ErrorLogFile);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewApplicationType(string Title, float Fees)
        {
            int TargetID = -1;
           
            string query = $@"INSERT INTO {AccessSettings.ApplicationTypesTableName} (ApplicationTypeTitle, ApplicationFees)
                      VALUES (@Title, @Fees);
                      SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar(); 

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TargetID = insertedID;
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(AddNewApplicationType), nameof(ApplicationTypesAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return TargetID;
        }

    }
}