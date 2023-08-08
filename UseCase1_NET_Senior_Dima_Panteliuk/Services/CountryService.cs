using System.Collections.Generic;
using System.Linq;
using UseCase1.Models;

namespace UseCase1.Services {
    public interface ICountryService
    {
        IEnumerable<Country> FilterCountriesByName(List<Country> countries, string searchString);
    }

    public class CountryService : ICountryService
    {
        public IEnumerable<Country> FilterCountriesByName(List<Country> countries, string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return countries;

            return countries.Where(country => country.Name.ToLower().Contains(searchString.ToLower()));
        }
    }
}