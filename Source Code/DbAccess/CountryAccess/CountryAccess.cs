using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using DbAccess.Settings;
using Utils;
namespace DbAccess.Country
{
    public class CountryAccess
    {
/// <summary>
/// Retrieves all countries from database
/// </summary>
/// <returns>returns a full DataTable containing CountryId and CountryName , 
/// otherwise returns an empty DataTable if no records are founds</returns>
        public static DataTable GetCountriesList()
        {
            DataTable dt = new DataTable();
            string query = $@"SELECT * FROM {AccessSettings.CountryTableName}";
            
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
            catch(Exception ex)
            {
                Log.LogException(ex, nameof(GetCountriesList), nameof(DbAccess.Country.CountryAccess), AccessSettings.ErrorLogFile);
                throw;
            }
            finally
            {
                connection.Close();
            }
            

            return dt;
        }


/// <summary>Searches for a country by its ID.</summary>
/// <returns>Returns <c>true</c> if a country with the specified ID is found;otherwise, returns <c>false</c>.</returns>
        public static bool FindCountryByCountryID(int CountryID,ref string CountryName)
        {
            bool found = false;
            string query = $@"SELECT * FROM {AccessSettings.CountryTableName} WHERE CountryID = @ID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", CountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    CountryName = Convert.ToString(reader["CountryName"]);
                    found = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(FindCountryByCountryID), nameof(DbAccess.Country.CountryAccess), AccessSettings.ErrorLogFile);
                throw;
            }
            finally
            {
                connection.Close();
            }

            return found;
        }


/// <summary>Searches for a country by its Name.</summary>
/// <returns>Returns <c>true</c> if a country with the specified Name is found;otherwise, returns <c>false</c>.</returns>
        public static bool FindCountryByCountryName(string CountryName ,ref int CountryID)
        {
            CountryID = -1;
            string query = $@"
            SELECT * FROM {AccessSettings.CountryTableName} WHERE CountryName = @CountryName";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read()) 
                {
                    CountryID = Convert.ToInt32(reader["CountryID"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(FindCountryByCountryName), nameof(DbAccess.Country.CountryAccess), AccessSettings.ErrorLogFile);
                throw;
            }
            finally
            {
                connection.Close();
            }

            return (CountryID != -1);
        }

               
    }
}
