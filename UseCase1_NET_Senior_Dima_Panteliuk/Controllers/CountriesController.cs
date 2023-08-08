using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly IHttpClientFactory _clientFactory;

    public CountriesController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    [HttpGet("GetCountries")]
    public async Task<IActionResult> GetCountries()
    {
        var client = _clientFactory.CreateClient();

        try
        {
            var response = await client.GetStringAsync("https://restcountries.com/v3.1/all");
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
