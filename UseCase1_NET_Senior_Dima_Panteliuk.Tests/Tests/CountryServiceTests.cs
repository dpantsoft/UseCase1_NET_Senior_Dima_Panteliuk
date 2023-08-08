using System.Collections.Generic;
using UseCase1.Models;
using UseCase1.Services;
using Xunit;

namespace UseCase1.Tests
{
    public class CountryServiceTests
    {
        private readonly ICountryService _countryService;

        public CountryServiceTests()
        {
            _countryService = new CountryService();
        }

        [Fact]
        public void FilterCountriesByName_ValidSearch_ReturnsFilteredCountries()
        {
            // Arrange
            var countries = new List<Country>
            {
                new Country { Name = "India" },
                new Country { Name = "Indonesia" },
                new Country { Name = "United States" }
            };

            // Act
            var result = _countryService.FilterCountriesByName(countries, "ind");

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void FilterCountriesByPopulation_ValidPopulation_ReturnsFilteredCountries()
        {
            // Arrange
            var countries = new List<Country>
            {
                new Country { Name = "India", Population = 1400000000 },
                new Country { Name = "Australia", Population = 25000000 }
            };

            // Act
            var result = _countryService.FilterCountriesByPopulation(countries, 500); // 500 million

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetCountriesByPage_ValidPage_ReturnsCountriesInPage()
        {
            // Arrange
            var countries = new List<Country>
            {
                new Country { Name = "India" },
                new Country { Name = "Australia" },
                new Country { Name = "Canada" },
                new Country { Name = "United States" }
            };

            // Act
            var result = _countryService.GetCountriesByPage(countries, 2, 2);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Theory]
        [InlineData("ascend", "Australia")]
        [InlineData("descend", "United States")]
        public void SortCountriesByName_ValidSortAttribute_ReturnsSortedCountries(string sortAttribute, string expectedFirstCountry)
        {
            // Arrange
            var countries = new List<Country>
            {
                new Country { Name = "India" },
                new Country { Name = "Australia" },
                new Country { Name = "Canada" },
                new Country { Name = "United States" }
            };

            // Act
            var result = _countryService.SortCountriesByName(countries, sortAttribute);

            // Assert
            Assert.Equal(expectedFirstCountry, result.First().Name);
        }

        [Fact]
        public void SortCountriesByName_InvalidSortAttribute_ThrowsArgumentException()
        {
            // Arrange
            var countries = new List<Country>
            {
                new Country { Name = "India" },
                new Country { Name = "Australia" }
            };

            // Assert
            Assert.Throws<ArgumentException>(() => _countryService.SortCountriesByName(countries, "invalid"));
        }
    }
}