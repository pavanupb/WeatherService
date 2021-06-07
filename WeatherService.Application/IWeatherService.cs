using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Application.Models.Dto;

namespace WeatherService.Application
{
    public interface IWeatherService
    {
        Task<WeatherServiceResponseDto<WeatherResultsDto>> GetWeatherDetails(string placeName, string searchBy, string countryCode = null);
        Task<WeatherServiceResponseDto<WeatherForecastListDto>> GetWeatherForecast(string placeName, string searchBy, string countryCode = null);
    }
}
