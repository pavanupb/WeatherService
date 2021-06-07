using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherService.Common.Models;

namespace WeatherService.Common
{
    public class HttpUtilities : IHttpUtilities
    {
        /// <summary>
        /// A HttpGet Method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="baseUrl"></param>
        /// <param name="headers"></param>
        /// <returns>HttpResponse</returns>
        public async Task<HttpResponse<T>> GetAsync<T>(string url, string baseUrl = null, Dictionary<string, string> headers = null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var getUserUri = new UriBuilder(baseUrl + url);
                string finalUrl = getUserUri.ToString();               

                using var httpRequest = new HttpRequestMessage(HttpMethod.Get, finalUrl);
                if (headers != null)
                {
                    foreach (var (key, value) in headers)
                    {
                        httpRequest.Headers.Add(key, value);
                    }
                }

                HttpResponseMessage response = await httpClient.SendAsync(httpRequest);
                HttpResponse<T> httpResponse = new HttpResponse<T>();
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        httpResponse.Content = JsonConvert.DeserializeObject<T>(jsonResponse);
                        httpResponse.IsSuccess = true;
                        httpResponse.StatusCode = response.StatusCode;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        httpResponse.IsSuccess = false;
                        httpResponse.StatusCode = response.StatusCode;
                    }
                }
                else
                {
                    httpResponse.IsSuccess = false;
                    httpResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                }

                return httpResponse;
            }
        }
    }
}
