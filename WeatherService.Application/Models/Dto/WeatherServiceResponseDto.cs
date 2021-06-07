using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherService.Application.Models.Dto
{
    public class WeatherServiceResponseDto<T>
    {
        public T WeatherResults { get; set; }
        public bool IsSucess { get; set; }
    }
}
