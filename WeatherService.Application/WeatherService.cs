using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WeatherService.Application.Models;
using WeatherService.Application.Models.Dto;
using WeatherService.Common;

namespace WeatherService.Application
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpUtilities _httpUtilities;
        private readonly ILogger<WeatherService> _logger;
        public WeatherService(IHttpUtilities httpUtilities, ILogger<WeatherService> logger)
        {
            _logger = logger;
            _httpUtilities = httpUtilities;

        }

        /// <summary>
        /// Get Weather Details for a Place
        /// </summary>
        /// <param name="placeName"></param>
        /// <returns>WeatherServiceResponseDto</returns>
        public async Task<WeatherServiceResponseDto<WeatherResultsDto>> GetWeatherDetails(string placeName, string searchBy, string countryCode=null)
        {
            WeatherServiceResponseDto<WeatherResultsDto> serviceResponse = new WeatherServiceResponseDto<WeatherResultsDto>();
            try
            {
                //The base URL and AppCode should be moved to appsettings.json. Simplyfing it for the task
                string url = string.IsNullOrEmpty(countryCode) ? $"/data/2.5/weather?{searchBy}={placeName}&appid=fcadd28326c90c3262054e0e6ca599cd&units=metric" : $"/data/2.5/weather?{searchBy}={placeName},{countryCode}&appid=fcadd28326c90c3262054e0e6ca599cd&units=metric";
                var httpResponse = await _httpUtilities.GetAsync<WeatherResults>(url, "api.openweathermap.org");

                if (httpResponse.IsSuccess)
                {
                    //AutoMapper need to be configured here. Simplifying for the task
                    WeatherResultsDto weatherResults = new WeatherResultsDto
                    {
                        CurrentWeather = httpResponse.Content?.WeatherDetails?.Temperature,
                        MaxTemperature = httpResponse.Content?.WeatherDetails?.MaxTemperature,
                        MinTemperature = httpResponse.Content?.WeatherDetails?.MinTemperature,
                        FeelsLike = httpResponse.Content?.WeatherDetails?.FeelsLike,
                        Pressure = httpResponse.Content?.WeatherDetails?.Pressure,
                        Humidity = httpResponse.Content?.WeatherDetails?.Humudity,
                        PlaceName = httpResponse.Content?.PlaceName
                    };
                    serviceResponse.WeatherResults = weatherResults;                   
                }                
                serviceResponse.IsSucess = httpResponse.IsSuccess;
                _logger.LogInformation($"Weather results successfully fetched for city - {placeName}");
                return serviceResponse;
            }
            catch(Exception exception)
            {
                _logger.LogError($"Failed to fetch weather details for place {placeName}", exception);
                serviceResponse.IsSucess = false;
            }

            return serviceResponse;
        }

        /// <summary>
        /// Get 5 Day Forecast For a City
        /// </summary>
        /// <param name="placeName"></param>
        /// <param name="searchBy"></param>
        /// <param name="countryCode"></param>
        /// <returns>WeatherServiceResponseDto</returns>
        public async Task<WeatherServiceResponseDto<WeatherForecastListDto>> GetWeatherForecast(string placeName, string searchBy, string countryCode = null)
        {
            WeatherServiceResponseDto<WeatherForecastListDto> serviceResponse = new WeatherServiceResponseDto<WeatherForecastListDto>();
            try
            {
                //The base URL and AppCode should be moved to appsettings.json. Simplyfing it for the task
                string url = string.IsNullOrEmpty(countryCode) ? $"/data/2.5/forecast?{searchBy}={placeName}&appid=fcadd28326c90c3262054e0e6ca599cd&units=metric" : $"/data/2.5/forecast?{searchBy}={placeName},{countryCode}&appid=fcadd28326c90c3262054e0e6ca599cd&units=metric";
                var httpResponse = await _httpUtilities.GetAsync<WeatherForecastResults>(url, "api.openweathermap.org");

                if(httpResponse.IsSuccess)
                {
                    WeatherForecastListDto forecastList = new WeatherForecastListDto();
                    var fiveDaysForecast = httpResponse.Content.WeatherForecastDetails.GroupBy(x => DateTime.ParseExact(x.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).Date).Select(x => x);
                    foreach(var forecast in fiveDaysForecast)
                    {
                        WeatherForecastResultsDto forecastDetail = new WeatherForecastResultsDto();
                        forecastDetail.Date = forecast.Key;

                        forecastDetail.Temperature = Math.Round(forecast.Average(x => float.Parse(x.WeatherDetails.Tempearure)), 2).ToString();
                        forecastDetail.Humidity = Math.Round(forecast.Average(x => float.Parse(x.WeatherDetails.Humidity)), 2).ToString();
                        forecastDetail.WindSpeed = Math.Round(forecast.Average(x => float.Parse(x.WindDetails.Speed)), 2).ToString();                        
                        forecastList.ForecastResult.Add(forecastDetail);

                    }
                    forecastList.CityName = httpResponse.Content.City.Name;
                    serviceResponse.WeatherResults = forecastList;
                }
                serviceResponse.IsSucess = httpResponse.IsSuccess;
                _logger.LogInformation($"Weather forecast results successfully fetched for city - {placeName}");
                return serviceResponse;

            }
            catch(Exception exception)
            {
                _logger.LogError($"Failed to fetch weather forecast details for place {placeName}", exception);
                serviceResponse.IsSucess = false;
            }

            return serviceResponse;
        }
    }
}
