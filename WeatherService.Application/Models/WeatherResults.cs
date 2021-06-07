using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherService.Application.Models
{
    public class WeatherResults
    {
        [JsonProperty("main")]
        public MainWeatherDetails WeatherDetails { get; set; }
        [JsonProperty("name")]
        public string PlaceName { get; set; }

    }

    public class MainWeatherDetails
    {
        [JsonProperty("temp")]
        public string Temperature { get; set; }
        [JsonProperty("feels_like")]
        public string FeelsLike { get; set; }
        [JsonProperty("temp_min")]
        public string MinTemperature { get; set; }
        [JsonProperty("temp_max")]
        public string MaxTemperature { get; set; }
        [JsonProperty("pressure")]
        public string Pressure { get; set; }
        [JsonProperty("humidity")]
        public string Humudity { get; set; }
    }
}
