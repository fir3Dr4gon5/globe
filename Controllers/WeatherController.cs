using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace WeatherChecker.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly ILogger<WeatherController> _logger;
	private readonly IConfiguration _configuration;
	private readonly string _appId;

        public WeatherController(
	    ILogger<WeatherController> logger,
            IConfiguration configuration
	)
        {
            _logger = logger;
	    _configuration = configuration;
	    _appId = _configuration["Weather:AppId"];
        }


	[HttpGet("[action]/{city}")]
	public async Task<IActionResult> City(string city)
	{
	    using (var client = new HttpClient())
	    {
	        try		
		{

	            client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=c04cf3009a9c6a3cf04ceddd9d4f7f21&units=metric");
                    response.EnsureSuccessStatusCode();
		    
		    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
		    
		    return Ok(new {
	                Temp = rawWeather.Main.Temp,
			FeelsLike = rawWeather.Main.Feels_Like,
	                Summary = string.Join(",",rawWeather.Weather.Select(x=> x.Main + " - " + x.Description)),
                        City = rawWeather.Name
		    });
	        }
	        catch (HttpRequestException httpRequestException)
	        {
	            return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
	    }

	}
	//	
	
    }
}