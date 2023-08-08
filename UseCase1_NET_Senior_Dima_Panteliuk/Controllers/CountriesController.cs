using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using UseCase1.Services; // Add the services namespace
using System.Collections.Generic;
using UseCase1.Models;

namespace UseCase1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ICountryService _countryService;  // 1. Inject the country service
        private readonly string _countriesApiUrl;
        private readonly JsonSerializerOptions _serializerOptions;

        public CountriesController(IHttpClientFactory clientFactory, IConfiguration configuration, ICountryService countryService)  // 2. Add the ICountryService in constructor
        {
            _clientFactory = clientFactory;
            _countryService = countryService;   // 3. Assign the country service
            _countriesApiUrl = configuration.GetValue<string>("CountriesApi:Url");

            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.Converters.Add(new CountryConverter());

        }

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries(
            [FromQuery] string? searchString,
            [FromQuery] int? populationLessThan = null,    // Filter for population
            [FromQuery] int pageNumber = 1,       // For pagination
            [FromQuery] int pageSize = 10,        // Default page size is set to 10
            [FromQuery] string sortAttribute = "" // For sorting
        )
        {
            var client = _clientFactory.CreateClient();

            try
            {
                var response = await client.GetStringAsync(_countriesApiUrl);
                var countries = JsonSerializer.Deserialize<List<Country>>(response, _serializerOptions);

                // 1. Filter countries by name if searchString is provided
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    countries = _countryService.FilterCountriesByName(countries, searchString).ToList();
                }

                // 1.1 Filter countries by population if populationLessThan is provided
                if (populationLessThan.HasValue && populationLessThan.Value > 0)
                {
                    countries = _countryService.FilterCountriesByPopulation(countries, populationLessThan.Value).ToList();
                }

                // 2. Sort countries by the given attribute if provided
                if (!string.IsNullOrWhiteSpace(sortAttribute))
                {
                    countries = _countryService.SortCountriesByName(countries, sortAttribute).ToList();
                }

                // 3. Paginate the list of countries
                countries = _countryService.GetCountriesByPage(countries, pageNumber, pageSize).ToList();

                return Ok(countries);
            }
            catch (HttpRequestException e)
            {
                return BadRequest($"Error fetching data from REST Countries API: {e.Message}");
            }
        }        
    }
}