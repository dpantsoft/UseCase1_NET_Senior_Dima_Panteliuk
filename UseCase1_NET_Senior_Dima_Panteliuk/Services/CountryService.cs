using System.Collections.Generic;
using System.Linq;
using UseCase1.Models;

namespace UseCase1.Services {
    public interface ICountryService
    {
        IEnumerable<Country> FilterCountriesByName(List<Country> countries, string searchString);
        IEnumerable<Country> FilterCountriesByPopulation(List<Country> countries, int populationInMillions);
        IEnumerable<Country> GetCountriesByPage(List<Country> countries, int pageNumber, int pageSize);
        IEnumerable<Country> SortCountriesByName(List<Country> countries, string sortAttribute);
    }

    public class CountryService : ICountryService
    {
        public IEnumerable<Country> FilterCountriesByName(List<Country> countries, string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return countries;

            return countries.Where(country => country.Name.ToLower().Contains(searchString.ToLower()));
        }

        public IEnumerable<Country> FilterCountriesByPopulation(List<Country> countries, int populationInMillions)
        {
            if (populationInMillions <= 0)
                return countries;

            int population = populationInMillions * 1_000_000; // Convert population in millions to absolute population

            return countries.Where(country => country.Population < population);
        }

        public IEnumerable<Country> GetCountriesByPage(List<Country> countries, int pageNumber, int pageSize)
        {
            return countries.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Country> SortCountriesByName(List<Country> countries, string sortAttribute)
        {
            if (sortAttribute == "ascend")
            {
                return countries.OrderBy(country => country.Name);
            }
            else if (sortAttribute == "descend")
            {
                return countries.OrderByDescending(country => country.Name);
            }
            else
            {
                throw new ArgumentException("Invalid sort attribute provided. It should be either 'ascend' or 'descend'.");
            }
        }
    }
}