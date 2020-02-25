using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BackendApi.Helpers_and_Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BackendApi.MW
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CONTENT_TYPE = "application/json";

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (ArgumentNullException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.ContentType = CONTENT_TYPE;
                var dto = new ApiErrorDto("ERR-" + ((int)HttpStatusCode.BadRequest).ToString(), ex.Message, HttpStatusCode.BadRequest);
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(dto));
            }
            catch (ApiException ex)
            {
                httpContext.Response.StatusCode = (int)ex.StatusCode;
                httpContext.Response.ContentType = CONTENT_TYPE;
                var dto = new ApiErrorDto("ERR-" + ((int)ex.StatusCode).ToString(), ex.Message, ex.StatusCode);
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(dto));
            }
            catch(AppException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.ContentType = CONTENT_TYPE;
                var dto = new ApiErrorDto("ERR-" + ((int)HttpStatusCode.BadRequest).ToString(), ex.Message, HttpStatusCode.BadRequest);
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(dto));
            }
            catch (Exception)
            {
                var errorCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.StatusCode = errorCode;
                httpContext.Response.ContentType = CONTENT_TYPE;
                var dto = new ApiErrorDto("ERR-" + errorCode.ToString(), "Unhandled error occurred...", HttpStatusCode.InternalServerError);
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(dto));
            }

            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
