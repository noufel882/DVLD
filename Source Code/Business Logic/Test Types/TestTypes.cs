using System;
using System.Data;
using DbAccess.TestTypesAccess; 

namespace Business_Logic.Test_Types
{
    public class TestType
    {
        public enum enTestTypes { VisionTest = 1 , WrittenTest =2, StreetTest = 3}
        public enum enMode { AddNew = 0, Update = 1 };

        public enTestTypes ID { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Fees { get; set; }
        public enMode Mode = enMode.AddNew;

        private TestType(enTestTypes id, string title, string description, float fees)
        {
            this.ID = id;
            this.Title = title;
            this.Description = description;
            this.Fees = fees;
            Mode = enMode.Update;
        }
  
        public TestType()
        {
            this.ID = enTestTypes.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;
            Mode = enMode.AddNew;
        }
        
        public static DataTable GetAllTestTypes()
        {
            return TestTypesAccess.GetTestTypesList();
        }
     
        public static TestType Find(enTestTypes id)
        {
            string title = "", description = "";
            float fees = 0;

            if (TestTypesAccess.Find((int)id, ref title, ref description, ref fees))
            {
                return new TestType(id, title, description, fees);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNew()
        {
            this.ID = (enTestTypes)TestTypesAccess.AddNewTestType(this.Title, this.Description, this.Fees);
            return Title != "";
        }

        private bool _Update()
        {
            return TestTypesAccess.UpdateTestTypeInfo((int)this.ID, this.Title, this.Description, this.Fees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _Update();
            }
            return false;
        }


    }
}