using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherService.Application.Models.Dto
{
    public class WeatherResultsDto
    {
        public string CurrentWeather { get; set; }
        public string FeelsLike { get; set; }
        public string MinTemperature { get; set; }
        public string MaxTemperature { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public string PlaceName { get; set; }
        public string WindSpeed { get; set; }
    }
}
