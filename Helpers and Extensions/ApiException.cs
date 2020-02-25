using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BackendApi.Helpers_and_Extensions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
        public ApiException() { }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, HttpStatusCode statusCode) : base(message) 
        {
            StatusCode = statusCode;
        }


  
    }
}
