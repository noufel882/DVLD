using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DbAccess.Settings;
using Utils;

namespace DbAccess.People
{

    /*
    person Fields :
        PersonID	            int	               
        NationalNo	            nvarchar(20)	
        FirstName	            nvarchar(20)	
        SecondName	            nvarchar(20)	
        ThirdName	            nvarchar(20)	   nullable
        LastName	            nvarchar(20)	
        DateOfBirth	            datetime	
        Gender	                tinyint	
        Address	                nvarchar(500)	
        Phone	                nvarchar(20)	
        Email	                nvarchar(50)	   nullable
        NationalityCountryID	int	
        ImagePath	            nvarchar(250)	   nullable
		
     */
    public class PersonAccess
    {
        /// <summary> Adds a new person record to the People table and returns it identifier. </summary>
        /// <returns> Returns the newly created Person ID if insert succeeds, otherwise returns -1. </returns>
        public static int AddPerson(string NationalNo, string FirstName,  string SecondName, string ThirdName,string LastName,  DateTime DateOfBirth,  byte Gender,  string Address,string Phone,  string Email, int NationalityCountryID,  string ImagePath)
        {
            int PersonID = -1;
            string query =
            $@"
                INSERT INTO {AccessSettings.PeopleTableName}
                (
                 NationalNo,
                 FirstName,
                 SecondName,
                 ThirdName,
                 LastName,
                 DateOfBirth,
                 Gender,
                 Address,
                 Phone,
                 Email,
                 NationalityCountryID,
                 ImagePath
                )
                VALUES
                (
                 @NationalNo,
                 @FirstName,
                 @SecondName,
                 @ThirdName,
                 @LastName,
                 @DateOfBirth,
                 @Gender,
                 @Address,
                 @Phone,
                 @Email,
                 @NationalityCountryID,
                 @ImagePath
                );
                
                SELECT SCOPE_IDENTITY();
                
            ";
            
            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query,connection);


            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            //Nullable fields
            if (Email == "")
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Email",Email);
            }



            if(ThirdName == "")
            {
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }



            if (ImagePath == "")
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if(result != null  && int.TryParse(result.ToString() , out int value))
                    PersonID = value;
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(AddPerson), nameof(DbAccess.People.PersonAccess), AccessSettings.ErrorLogFile);
                
            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }


        /// <summary> Finds a person record by PersonID and fills the provided variables with the person's data. </summary>
        /// <returns>True if the person is found; otherwise, false.</returns>
        public static bool FindPersonByID(int PersonID,ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;
            string query =
            $@"
                SELECT * FROM  {AccessSettings.PeopleTableName} WHERE PersonID = @PersonID
            ";
            
            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    NationalNo = Convert.ToString(reader["NationalNo"]);
                    FirstName = Convert.ToString(reader["FirstName"]);
                    SecondName = Convert.ToString(reader["SecondName"]);
                    LastName = Convert.ToString(reader["LastName"]);
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gender = Convert.ToByte(reader["Gender"]);
                    Address = Convert.ToString(reader["Address"]);
                    Phone = Convert.ToString(reader["Phone"]);
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);

                    //Nullable fields
                    ThirdName = Convert.ToString(reader["ThirdName"]);
                    Email = Convert.ToString(reader["Email"]);
                    ImagePath = Convert.ToString(reader["ImagePath"]);
                    // I use 'Convert.ToString' to handle nullable columns because The string representation of  reader's colmun value,
                    // or Empty string if  reader's colmun value is null.

                    reader.Close();
                }
                
            }
            catch(Exception ex)
            {
                isFound=false;
                Log.LogException(ex, nameof(FindPersonByID), nameof(DbAccess.People.PersonAccess), AccessSettings.ErrorLogFile);
                
            }
            finally
            {
                connection.Close(); 
            }


            return isFound;
        }


        /// <summary> Finds a person record by Person NationalNo and fills the provided variables with the person's data. </summary>
        /// <returns>True if the person is found; otherwise, false.</returns>
        public static bool FindPersonByNationalNo(string NationalNo, ref int PersonID,  ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;
            string query =
            $@"
                SELECT * FROM  {AccessSettings.PeopleTableName} WHERE NationalNo = @NationalNo
            ";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    FirstName = Convert.ToString(reader["FirstName"]);
                    SecondName = Convert.ToString(reader["SecondName"]);
                    LastName = Convert.ToString(reader["LastName"]);
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gender = Convert.ToByte(reader["Gender"]);
                    Address = Convert.ToString(reader["Address"]);
                    Phone = Convert.ToString(reader["Phone"]);
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);

                    //Nullable fields
                    ThirdName = Convert.ToString(reader["ThirdName"]);
                    Email = Convert.ToString(reader["Email"]);
                    ImagePath = Convert.ToString(reader["ImagePath"]);
                    // I use Convert.ToString to handle nullable columns because it returns
                    // the string representation of the reader's column value,
                    // or an empty string if the column value is DBNull.Value.

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                isFound = false;
                Log.LogException(ex, nameof(FindPersonByNationalNo), nameof(DbAccess.People.PersonAccess), AccessSettings.ErrorLogFile);
                
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        /// <summary>Deletes a person record from the database.</summary>
        /// <returns>True if the delete operation succeeds; otherwise, false.</returns>
        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;
            string query = $"DELETE FROM {AccessSettings.PeopleTableName} WHERE PersonID = @PersonID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                Log.LogException(ex, nameof(DeletePerson), nameof(DbAccess.People.PersonAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }
            
            
            return rowsAffected > 0;
        }


        /// <summary> Updates an existing person's data in the People table using the PersonID. </summary>
        /// <returns> true if the update succeeds; otherwise, false. </returns>
        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName,string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, byte Gender, string Address, string Phone, string Email, int NationalityCountryID,  string ImagePath)
        {
            int RowsAffected = 0;
            string query = 
           $@"
              UPDATE {AccessSettings.PeopleTableName}
                SET [NationalNo] = @NationalNo
                   ,[FirstName] = @FirstName
                   ,[SecondName] = @SecondName
                   ,[ThirdName] = @ThirdName
                   ,[LastName] = @LastName
                   ,[DateOfBirth] = @DateOfBirth
                   ,[Gender] = @Gender
                   ,[Address] = @Address
                   ,[Phone] = @Phone
                   ,[Email] = @Email
                   ,[NationalityCountryID] = @NationalityCountryID
                   ,[ImagePath] = @ImagePath
                WHERE PersonID = @PersonID
            ";


            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            
            //Nullable fields
            if (Email == "")
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", Email);
            }



            if (ThirdName == "")
            {
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }



            if (ImagePath == "")
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(UpdatePerson),nameof(DbAccess.People.PersonAccess),AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return RowsAffected > 0;
        }


        /// <summary>Checks whether a person exists in the People table using the PersonID.</summary>
        /// <returns>true if the person exists; otherwise, false.</returns>
        public static bool IsPersonExistsByID(int PersonID)
        {
            bool IsPersonExists = false;
            string query =$@"SELECT FOUND = 1 FROM {AccessSettings.PeopleTableName} WHERE PersonID = @PersonID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null )
                    IsPersonExists = true;

            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(IsPersonExistsByID),nameof(DbAccess.People.PersonAccess),AccessSettings.ErrorLogFile);
                throw;
            }
            finally
            {
                connection.Close();
            }

            return IsPersonExists ;
        }


        /// <summary>Checks whether a person exists in the People table using the NationalNo.</summary>
        /// <returns> true if the person exists; otherwise, false. </returns>
        public static bool IsPersonExistsByNationalNo(string NationalNo)
        {
            bool IsFound = false;

            string query =$@"SELECT FOUND = 1 FROM {AccessSettings.PeopleTableName} WHERE NationalNo = @NationalNo";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(IsPersonExistsByNationalNo),nameof(DbAccess.People.PersonAccess),AccessSettings.ErrorLogFile);
                
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }


        /// <summary>Retrieves the list of people from the database. </summary>
        /// <returns> A DataTable containing all people records.</returns>
        public static DataTable PeopleList()
        {
            DataTable dt = new DataTable();
            string query =
            $@"SELECT [PersonID]
                    ,[NationalNo]
                    ,[FirstName]
                    ,[SecondName]
                    ,[ThirdName]
                    ,[LastName]
                    ,[DateOfBirth]
                    ,[Gender]
	                  ,( 
	                  CASE
	                		WHEN Gender = 0 THEN 'Male'
	                		ELSE 'Female'
	                  END
	                  
	                  ) AS GenderCaption
                    ,[Address]
                    ,[Phone]
                    ,[Email]
                    ,[NationalityCountryID]
	                  ,[CountryName]
                    ,[ImagePath]
                FROM [dbo].[{AccessSettings.PeopleTableName}]

                JOIN [dbo].[{AccessSettings.CountryTableName}]

                ON [{AccessSettings.PeopleTableName}].NationalityCountryID = [{AccessSettings.CountryTableName}].CountryID
             ";
            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand(query, connection);
            

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);

                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(IsPersonExistsByNationalNo), nameof(DbAccess.People.PersonAccess), AccessSettings.ErrorLogFile);
                
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


    }
}
