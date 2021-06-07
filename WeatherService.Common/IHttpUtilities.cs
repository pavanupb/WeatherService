using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Common.Models;

namespace WeatherService.Common
{
    public interface IHttpUtilities
    {
        Task<HttpResponse<T>> GetAsync<T>(string url, string baseUrl = null, Dictionary<string, string> headers = null);
    }
}
