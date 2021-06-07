using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherService.Application;

namespace WeatherService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherServiceController : ControllerBase
    {
        private readonly ILogger<WeatherServiceController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherServiceController(ILogger<WeatherServiceController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet]        
        public async Task<IActionResult> GetWeatherByName([FromQuery(Name = "city")] string cityName)
        {
            var weatherResponse = await _weatherService.GetWeatherDetails(cityName, "q");
            if(weatherResponse.IsSucess)
            {
                return Ok(weatherResponse);
            }
            else
            {
                _logger.LogError("Failed to fetch weather details for City - {0}", cityName);
                return StatusCode(404);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherByZip([FromQuery(Name = "zip")] string zipCode, [FromQuery(Name = "code")] string countryCode)
        {
            var weatherResponse = await _weatherService.GetWeatherDetails(zipCode, "zip", countryCode);
            if (weatherResponse.IsSucess)
            {
                return Ok(weatherResponse);
            }
            else
            {
                _logger.LogError("Failed to fetch weather details for City - {0}", zipCode);
                return StatusCode(404);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetForecastByName([FromQuery(Name = "city")] string cityName)
        {
            var weatherResponse = await _weatherService.GetWeatherForecast(cityName, "q");
            if (weatherResponse.IsSucess)
            {
                return Ok(weatherResponse);
            }
            else
            {
                _logger.LogError("Failed to fetch weather details for City - {0}", cityName);
                return StatusCode(404);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetForecastByZip([FromQuery(Name = "zip")] string zipCode, [FromQuery(Name = "code")] string countryCode)
        {
            var weatherResponse = await _weatherService.GetWeatherForecast(zipCode, "zip", countryCode);
            if (weatherResponse.IsSucess)
            {
                return Ok(weatherResponse);
            }
            else
            {
                _logger.LogError("Failed to fetch weather details for City - {0}", zipCode);
                return StatusCode(404);
            }
        }
    }
}
