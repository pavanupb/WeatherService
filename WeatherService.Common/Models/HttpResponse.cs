using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService.Common.Models
{
    public class HttpResponse<T>
    {
        public T Content { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
