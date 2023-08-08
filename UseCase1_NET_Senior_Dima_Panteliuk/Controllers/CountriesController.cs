using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace MyApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _countriesApiUrl;

        public CountriesController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _countriesApiUrl = configuration.GetValue<string>("CountriesApi:Url");  // 2. Fetch URL from configuration
        }

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var client = _clientFactory.CreateClient();

            try
            {
                var response = await client.GetStringAsync(_countriesApiUrl);  // 2. Use the variable instead of hardcoded URL
                var result = JsonSerializer.Deserialize<object>(response);

                // The result variable now holds the parsed data. Do whatever you need with it.
                // For now, just returning it for demonstration.
                return Ok(result);
            }
            catch (HttpRequestException e)
            {
                return BadRequest($"Error fetching data from REST Countries API: {e.Message}");
            }
        }
    }
}