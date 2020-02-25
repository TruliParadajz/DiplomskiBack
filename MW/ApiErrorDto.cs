using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BackendApi.MW
{
    public class ApiErrorDto
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode ErrorStatusCode { get; set; }

        public ApiErrorDto(string errorCode, string errorMessage, HttpStatusCode errorStatusCode)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ErrorStatusCode = errorStatusCode;
        }

    }
}
