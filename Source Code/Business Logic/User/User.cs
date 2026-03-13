using System.Data;
using DbAccess.UserAccess;
using static Utils.Cryptography.Hashing;

namespace Business_Logic
{
    public class User
    {
        public enum enMode { Add  , Update }

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; private set; }
        public bool IsActive { get; set; }        
        public Person PersonalDetails { get; set; }
        private enMode Mode { get; set; }
        
        public User()
        {
            Mode = enMode.Add;
            UserID = -1;
            PersonID = -1;
            UserName = string.Empty;
            Password = string.Empty;
            IsActive = false;
            
            PersonalDetails = null;
        }

        private User(int UserID , int PersonID , string UserName , string Password , bool IsActive)
        {
            Mode= enMode.Update;
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            this.PersonalDetails = Person.Find(PersonID);
        }

        /// <summary> Retrieves all users from the users table in the database. </summary>
        /// <returns> Returns a full "DataTable" containing all user records., else an empty "DataTable" .</returns>
        public static DataTable GetUsersList()
        {
            return UserAccess.GetUsersList();
        }

        /// <summary> Deletes a user from the users table based on the provided user ID. </summary>
        /// <returns> Returns true if at least one record was deleted; otherwise, false. </returns>
        public static bool Delete(int UserID)
        {
            return UserAccess.DeleteUser(UserID);
        }


        /// <summary> Inserts a new user into the users table and returns operations state (succeeds or failed). </summary>
        /// <returns> True if operation succeeds ; otherwise , false . </returns>  
        private bool Add()
        {
            this.UserID = UserAccess.AddNewUser(this.PersonID ,  this.UserName , this.Password, this.IsActive);
            return UserID != -1;
        }


        /// <summary> Updates an existing user's information </summary>
        /// <returns> Returns true if at least one record was updated; otherwise, false.  </returns>
        private bool Update()
        {
            return UserAccess.UpdateUserInfo(this.UserID , this.PersonID , this.UserName , this.IsActive);
        }


        /// <summary> Saves the current entity based on the specified mode. </summary>
        /// <returns> Returns true if the operation (Add or Update) succeeds; otherwise, false. </returns>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Update:
                    return Update();
                case enMode.Add:
                    if (Add())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
            }
            return false;
        }

        /// <summary> Finds a user by their UserID and returns a User object. </summary>
        /// <returns> Returns a full "User" object if the user is found; otherwise, null. </returns>
        public static User FindByUserID(int UserID)
        {
            int PersonID = -1;
            string  UserName = string.Empty;
            string Password = string.Empty;
            bool IsActive = false;

            if(UserAccess.GetUserInfoByUserID(UserID ,ref PersonID , ref UserName , ref Password , ref IsActive))
            {
                return new User(UserID ,PersonID ,UserName , Password , IsActive );
            }

            return null;
        }

        /// <summary> Finds a user by their Person ID and returns a User object. </summary>
        /// <returns> Returns a full "User" object if the user is found; otherwise, null. </returns>
        public static  User FindByPeronID(int PersonID)
        {
            int UserID = -1;
            string UserName = string.Empty;
            string Password = string.Empty;
            bool IsActive = false;

            if (UserAccess.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive))
            {
                return new User(UserID, PersonID, UserName, Password, IsActive);
            }

            return null;
        }

        /// <summary> Finds a user by their Username and returns a User object. </summary>
        /// <returns> Returns a full "User" object if the user is found; otherwise, null. </returns>
        public static User FindByUserName(string UserName)
        {
            int PersonID = -1;
            int UserID = -1;
            string Password = string.Empty;
            bool IsActive = false;

            if (UserAccess.GetUserInfoByUserName(UserName, ref UserID, ref PersonID, ref Password, ref IsActive))
            {
                return new User(UserID, PersonID, UserName, Password, IsActive);
            }

            return null;
        }

        /// <summary> Finds a user by their UserName and Password returns a User object. </summary>
        /// <returns> Returns a full "User" object if the user is found; otherwise, null. </returns>
        public static User FindByUserNameAndPassword(string UserName , string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;

            if (UserAccess.GetUserInfoByUsernameAndPassword(UserName,Password , ref UserID , ref PersonID , ref IsActive))
            {
                return new User(UserID, PersonID, UserName, Password, IsActive);
            }

            return null;
        }

        /// <summary> Checks if a user exists in the database by user id. </summary>
        /// <returns>True if the user exists, otherwise false.</returns>
        public static bool IsUserExists(int UserID)
        {
            return UserAccess.IsUserExistsForUserID(UserID);
        }

        /// <summary> Checks if a user exists in the database by username. </summary>
        /// <returns>True if the user exists, otherwise false.</returns>
        public static bool IsUserExists(string UserName)
        {
            return UserAccess.IsUserExistsForUserName(UserName);
        }

        /// <summary> Checks if a user exists in the database by person id. </summary>
        /// <returns>True if the user exists, otherwise false.</returns>
        public static bool IsUserExistsForPerson(int PersonID)
        {
            return UserAccess.IsUserExistsForPerson(PersonID);
        }

        /// <summary> Change password . </summary>
        /// <returns>True if the operation succeeds, otherwise false.</returns
        public static bool ChangePassword(int UserID, string NewPassword)
        {
            var HashedPassword =ComputeHash_SHA256(NewPassword);
            return UserAccess.ChangePassword(UserID, HashedPassword);
        }

        /// <summary> Change password . </summary>
        /// <returns>True if the operation succeeds, otherwise false.</returns
        public bool ChangePassword(string NewPassword)
        {
            var HashedPassword = ComputeHash_SHA256(NewPassword);
            if (UserAccess.ChangePassword(UserID, HashedPassword))
            {
                this.Password = HashedPassword;
                return true;
            }
            return false;
        }

    }
}
