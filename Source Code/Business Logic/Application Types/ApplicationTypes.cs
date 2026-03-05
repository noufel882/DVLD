using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAccess.ApplicationTypesAccess;

namespace Business_Logic.Application_Types
{
    public class ApplicationType
    {
       public enum enMode { Add = 0 , Update = 1 }
        private enMode _mode {  get; set; } 
        public int ID { get; set; }
        public string Title { get; set; }
        public float Fees { get; set; }

        public ApplicationType()
        {
            this._mode = enMode.Add;
            this.ID = -1;
            this.Title = "";
            this.Fees = 0;
        }

        private ApplicationType(int id, string title, float fees)
        {
            this._mode = enMode.Update;
            this.ID = id;
            this.Title = title;
            this.Fees = fees;
        }
    
        public static ApplicationType Find(int id)
        {
            string title = "";
            float fees = 0;

            if (ApplicationTypesAccess.Find(id, ref title, ref fees))
            {
                return new ApplicationType(id, title, fees);
            }

            return null;
        }
       
        public static DataTable GetAllApplicationTypes()
        {
            return ApplicationTypesAccess.GetTypesList();
        }

        private bool Add()
        {
            this.ID = ApplicationTypesAccess.AddNewApplicationType(this.Title, this.Fees);
            return ID != -1;
        }

        private bool Update()
        {
            return ApplicationTypesAccess.UpdateApplicationTypeInfo(this.ID, this.Title, this.Fees);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.Add:
                    if (Add())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return Update();
                    
            }

            return false;
        }

    }
}
