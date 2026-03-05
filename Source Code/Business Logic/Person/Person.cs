using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccess.People;




namespace Business_Logic
{
    public class Person
    {
       
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName  { get { return $"{FirstName} {SecondName} {ThirdName} {LastName}"; } }        
        public DateTime DateOfBirth { get; set; }
        public enGender Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }
        public Country CountryInfo { get; set; }
        public enMode Mode { get; set; }


        public enum enGender { Male = 0 , Female = 1 }
        public enum enMode { Add = 0, Update = 1 }


        public Person()
        {
            Mode = enMode.Add;
            PersonID = -1;
            NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            Gender = 0;
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = -1;
            ImagePath = "";
            CountryInfo = new Country();
        }

        private Person(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, byte Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            Mode = enMode.Update;
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = (enGender)Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            CountryInfo = Country.Find(NationalityCountryID);
        }

        /// <summary> Deletes a person from the database using the PersonID.</summary>
        /// <returns> true if the record was deleted successfully;  false if the delete operation failed or the record was not found. </returns>
        public static bool Delete(int ID)
        {
            return PersonAccess.DeletePerson(ID);
        }

        /// <summary> Finds a person by PersonID. </summary>        
        /// <returns>A Person object if found; null if the person does not exist. </returns>
        public static Person Find(int ID)
       {
           
            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gender = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = -1;
            string ImagePath = "";
            if (PersonAccess.FindPersonByID(ID , ref NationalNo ,ref FirstName, ref SecondName, ref ThirdName,ref LastName,ref DateOfBirth,ref Gender,ref Address,ref Phone,ref Email,ref NationalityCountryID,ref ImagePath))
            {
                return new Person(ID ,NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gender,Address,Phone,Email,NationalityCountryID,ImagePath);
            }

            return null;
       }

        /// <summary> Finds a person by National number. </summary>        
        /// <returns>A Person object if found; null if the person does not exist. </returns>
        public static Person Find(string NationalNo)
        {

            int PersonID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gender = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = -1;
            string ImagePath = "";
            if (PersonAccess.FindPersonByNationalNo(NationalNo ,ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new Person(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            }

            return null;
        }

        /// <summary>Checks whether a person exists using the NationalNo.</summary>
        /// <returns>true if the person exists;  false if the person does not exist. </returns>
        public static bool  IsExist(string NationalNo)
        {
            return PersonAccess.IsPersonExistsByNationalNo(NationalNo);
        }

        /// <summary>Checks whether a person exists using the Person.</summary>
        /// <returns>true if the person exists;  false if the person does not exist. </returns>
        public static bool IsExist(int ID)
        {
            return PersonAccess.IsPersonExistsByID(ID);
        }

        /// <summary>Retrieves the list of people from the database.</summary>
        /// <returns> A DataTable containing all people records. </returns>
        public static DataTable PeopleList()
        {
            return PersonAccess.PeopleList(); 
        }

        private bool Add()
        {
            this.PersonID = PersonAccess.AddPerson(NationalNo, FirstName,SecondName,ThirdName, LastName,  DateOfBirth, (byte)Gender,  Address, Phone,  Email, NationalityCountryID,  ImagePath);
            return PersonID != -1;
        }

        private bool Update()
        {
            return PersonAccess.UpdatePerson(PersonID,NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, (byte)Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
        }

        public bool Save()
        {
            bool SaveComplete = false;
            switch(Mode)
            {
                case enMode.Add:
                    SaveComplete = Add();
                    break;

                case enMode.Update:
                    SaveComplete = Update();
                    break;

            }
            return SaveComplete;
        }


    }
}
