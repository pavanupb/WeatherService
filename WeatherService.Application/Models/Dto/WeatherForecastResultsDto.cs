using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService.Application.Models.Dto
{
    public class WeatherForecastListDto
    {
        public List<WeatherForecastResultsDto> ForecastResult { get; set; }
            = new List<WeatherForecastResultsDto>();
        public string CityName { get; set; }
    }
    public class WeatherForecastResultsDto
    {        
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string WindSpeed { get; set; }
        public DateTime Date { get; set; }
    }
}
