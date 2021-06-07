using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService.Application.Models
{
    public class WeatherForecastResults
    {
        [JsonProperty("list")]
        public List<WeatherForecastDetails> WeatherForecastDetails { get; set; }
        [JsonProperty("city")]
        public City City { get; set; }
    }

    public class WeatherForecastDetails
    {
        [JsonProperty("main")]
        public Weather WeatherDetails { get; set; }
        [JsonProperty("wind")]
        public Wind WindDetails { get; set; }
        [JsonProperty("dt_txt")]
        public string DateTime { get; set; }

    }

    public class Weather
    {
        [JsonProperty("temp")]
        public string Tempearure { get; set; }
        [JsonProperty("humidity")]
        public string Humidity { get; set; }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public string Speed { get; set; }
    }

    public class City
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
