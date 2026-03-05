using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccess.Settings;
using Utils;

namespace DbAccess.UserAccess
{

    /*
      user fiels :           
            UserID	    int	
            PersonID	int	
            UserName	nvarchar(20)	
            Password	nvarchar(20)	
            IsActive	bit	
     */
    public class UserAccess
    {
        /// <summary> Retrieves all users from the users table in the database. </summary>
        /// <returns> Returns a full "DataTable" containing all user records., else an empty "DataTable" .</returns>
        public static DataTable GetUsersList()
        {
            DataTable List = new DataTable();

            string query = $@"
                                SELECT UserID ,     
                                       {AccessSettings.UsersTableName}.PersonID ,  
                                       FullName = FirstName +' '+ SecondName +' '+ IsNull(ThirdName ,'')+' '+LastName,
                                       UserName,
                                       IsActive
                                        FROM {AccessSettings.UsersTableName} 
                                Inner join {AccessSettings.PeopleTableName} On  {AccessSettings.UsersTableName}.PersonID = {AccessSettings.PeopleTableName}.PersonID                   
                            ";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);


            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    List.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex ,nameof(GetUsersList) ,nameof(DbAccess.UserAccess.UserAccess) , AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }


            return List;
        }

        /// <summary> Deletes a user from the users table based on the provided user ID. </summary>
        /// <returns> Returns true if at least one record was deleted; otherwise, false. </returns>
        public static bool DeleteUser(int UserID)
        {
            int RowsAffected = 0;
            string query = $"Delete  FROM {AccessSettings.UsersTableName} WHERE UserID = @UserID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);
            
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID" , UserID);

            try
            {
                connection.Open();      
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(DeleteUser), nameof(DbAccess.UserAccess.UserAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }

        /// <summary> Inserts a new user into the users table and returns the generated user ID. </summary>
        /// <returns> Inserts a new user into the users table and returns the generated user ID. </returns>
        public static int AddNewUser( int PersonID , string UserName , string Password ,  bool IsActive)
        {
            int User_ID = -1;

            string query =             
            $@"
            INSERT INTO {AccessSettings.UsersTableName}
                 ([PersonID]
                ,[UserName]
                ,[Password]
                ,[IsActive])
            VALUES
                (@PersonID
                ,@UserName
                ,@Password
                ,@IsActive
                )
            SELECT SCOPE_IDENTITY()
            ";
            

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID" , PersonID);
            command.Parameters.AddWithValue("@UserName" , UserName);
            command.Parameters.AddWithValue("@Password" , Password);
            command.Parameters.AddWithValue("@IsActive" , IsActive);
           
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null )
                {
                    User_ID = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(AddNewUser), nameof(DbAccess.UserAccess.UserAccess), AccessSettings.ErrorLogFile);
                
            }
            finally
            {
                connection.Close();
            }

            return User_ID;
        }

        /// <summary> Updates an existing user's information </summary>
        /// <returns> Returns true if at least one record was updated; otherwise, false.  </returns>
        public static bool UpdateUserInfo(int UserID,int PersonID,string UserName, string Password, bool IsActive)
        {
            int RowsAffected = 0;
            string query = 
                $@"
                UPDATE {AccessSettings.UsersTableName}
                  SET [PersonID] = @PersonID
                     ,[UserName] = @UserName
                     ,[Password] = @Password
                     ,[IsActive] = @IsActive
                  WHERE UserID = @UserID
                ";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(UpdateUserInfo), nameof(DbAccess.UserAccess.UserAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }

        /// <summary> Retrieves a user's information by their UserID. </summary>
        /// <returns> Returns true if a user with the specified UserID exists; otherwise, false. </returns>
        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName, ref string Password,  ref bool IsActive)
        {
            bool IsFound = false;
            string query = $"SELECT * FROM {AccessSettings.UsersTableName} WHERE UserID = @UserID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    UserName = Convert.ToString(reader["UserName"]);
                    Password = Convert.ToString(reader["Password"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetUserInfoByUserID), nameof(DbAccess.UserAccess.UserAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }


            return IsFound;
        }


        /// <summary> Retrieves a user's information by their Person ID. </summary>
        /// <returns> Returns true if a user with the specified Person ID exists; otherwise, false. </returns>
        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            string query = $"SELECT * FROM {AccessSettings.UsersTableName} WHERE PersonID = @PersonID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    UserID = Convert.ToInt32(reader["UserID"]);
                    UserName = Convert.ToString(reader["UserName"]);
                    Password = Convert.ToString(reader["Password"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetUserInfoByPersonID), nameof(DbAccess.UserAccess.UserAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }


            return IsFound;
        }

        /// <summary> Retrieves a user's information by their username. </summary>
        /// <returns> Returns true if a user with the specified username exists; otherwise, false. </returns>
        public static bool GetUserInfoByUserName( string UserName, ref int UserID,ref int PersonID,  ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            string query = $"SELECT * FROM {AccessSettings.UsersTableName} WHERE UserName = @UserName";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    UserID = Convert.ToInt32(reader["UserID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);                   
                    Password = Convert.ToString(reader["Password"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetUserInfoByUserName), nameof(DbAccess.UserAccess.UserAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }


            return IsFound;
        }




        /// <summary> Retrieves a user's information by their Person ID. </summary>
        /// <returns> Returns true if a user with the specified Person ID exists; otherwise, false. </returns>
        public static bool GetUserInfoByUsernameAndPassword(string UserName, string Password,
         ref int UserID, ref int PersonID, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            string query = $"SELECT * FROM {AccessSettings.UsersTableName} WHERE Username = @Username and Password=@Password;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", UserName);
            command.Parameters.AddWithValue("@Password", Password);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                   
                    isFound = true;
                    UserID = Convert.ToInt32(reader["UserID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    UserName = Convert.ToString(reader["UserName"]);
                    Password = Convert.ToString(reader["Password"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }               
                reader.Close();


            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(GetUserInfoByUsernameAndPassword), nameof(DbAccess.UserAccess.UserAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        /// <summary> Checks if a user exists in the database by username. </summary>
        /// <returns>True if the user exists, otherwise false.</returns>
        public static bool IsUserExistsForUserName(string UserName)
        {
            bool IsExist = false;

            string query = $"SELECT FOUND=1 FROM {AccessSettings.UsersTableName} WHERE UserName = @UserName";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                IsExist = Reader.HasRows;

                Reader.Close();
            }
            catch(Exception ex)
            {
                Log.LogException(ex, nameof(IsUserExistsForUserName), nameof(DbAccess.UserAccess.UserAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return IsExist;
        }

        /// <summary> Checks if a user exists in the database by user id. </summary>
        /// <returns>True if the user exists, otherwise false.</returns>
        public static bool IsUserExistsForUserID(int UserID)
        {
            bool IsExist = false;

            string query = $"SELECT FOUND=1 FROM {AccessSettings.UsersTableName} WHERE UserID = @ID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@ID", UserID);
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();

                IsExist = Reader.HasRows;

                Reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(IsUserExistsForUserID), nameof(DbAccess.UserAccess.UserAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return IsExist;
        }

        /// <summary> Checks if a user exists in the database by person id. </summary>
        /// <returns>True if the user exists, otherwise false.</returns>
        public static bool IsUserExistsForPerson(int PersonID)
        {
            bool IsExist = false;

            string query = $"SELECT Found=1 FROM {AccessSettings.UsersTableName} WHERE PersonID = @ID";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.HasRows)
                    IsExist = true;

                Reader.Close();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(IsUserExistsForPerson), nameof(DbAccess.UserAccess.UserAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }

            return IsExist;
        }


        /// <summary> Change password . </summary>
        /// <returns>True if the operation succeeds, otherwise false.</returns
        public static bool ChangePassword(int UserID, string Password)
        {
            int RowsAffected = 0;
            string query =
                $@"
                UPDATE {AccessSettings.UsersTableName}
                  SET [Password] = @Password
                      
                  WHERE UserID = @UserID
                ";

            SqlConnection connection = new SqlConnection(AccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException(ex, nameof(ChangePassword), nameof(DbAccess.UserAccess.UserAccess), AccessSettings.ErrorLogFile);
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }


    }

}