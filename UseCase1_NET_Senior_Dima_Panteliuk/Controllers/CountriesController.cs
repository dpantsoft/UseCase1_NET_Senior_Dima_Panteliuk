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

        public CountriesController(IHttpClientFactory clientFactory, IConfiguration configuration, ICountryService countryService)  // 2. Add the ICountryService in constructor
        {
            _clientFactory = clientFactory;
            _countryService = countryService;   // 3. Assign the country service
            _countriesApiUrl = configuration.GetValue<string>("CountriesApi:Url");  
        }

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries([FromQuery] string searchString)  // 4. Add the searchString query parameter
        {
            var client = _clientFactory.CreateClient();

            try
            {
                var response = await client.GetStringAsync(_countriesApiUrl);
                var countries = JsonSerializer.Deserialize<List<Country>>(response);  // 5. Deserialize the response to a list of countries

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    countries = _countryService.FilterCountriesByName(countries, searchString).ToList();  // 6. Use the service to filter the countries
                }

                return Ok(countries);
            }
            catch (HttpRequestException e)
            {
                return BadRequest($"Error fetching data from REST Countries API: {e.Message}");
            }
        }
    }
}