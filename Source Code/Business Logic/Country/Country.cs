using System.Data;
using DbAccess.Country;

namespace Business_Logic
{
    /// <summary>
    /// Represents a country entity in the Business Logic layer.
    /// </summary>
    public class Country
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        
        public Country()
        {

        }


        /// <summary>
        /// Initializes a new instance of the Country class with specified values.
        /// </summary>
        private Country(int CountryId, string CountryName)
        {
            this.CountryID = CountryId;
            this.CountryName = CountryName;
        }


        /// <summary>
        /// Retrieves all countries as a DataTable for presentation purposes.
        /// </summary>
        /// <returns>
        /// A DataTable containing all countries; returns an empty DataTable if no records exist.
        /// </returns>
        public static DataTable GetCountryList()
        {
            return CountryAccess.GetCountriesList();
        }


        /// <summary>
        /// Finds a country by its identifier.
        /// </summary>
        /// <param name="CountryId">The identifier of the country to search for.</param>
        /// <returns>
        /// A Country object if found; otherwise, returns null.
        /// </returns>
        public static Country Find(int CountryId)
        {
            string CountryName = string.Empty;
            if ( CountryAccess.FindCountryByCountryID(CountryId,ref CountryName) )
            {
                return new Country(CountryId,CountryName);
            }
            else
            {
                return null;
            }
                
        }

        /// <summary>
        /// Finds a country by its name.
        /// </summary>
        /// <param name="CountryName">The name of the country to search for.</param>
        /// <returns>
        /// A Country object if found; otherwise, returns null.
        /// </returns>
        public static Country Find(string CountryName)
        {
             int CountryId = -1;
            if (CountryAccess.FindCountryByCountryName(CountryName, ref CountryId))
            {
                return new Country(CountryId, CountryName);
            }
            else
            {
                return null;
            }

        }

    }
}
