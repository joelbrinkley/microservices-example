using Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Customers.Middleware
{
    public sealed class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILog log;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILog log)
        {
            this.next = next;
            this.log = log;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            this.log.Error("Global Exception Caught", exception);

            var code = HttpStatusCode.InternalServerError; // 500 if unexpected           

            var result = JsonConvert.SerializeObject(new { message = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;


            this.log.Information($"Replying with HttpStatusCode: {code}\r\n {result.ToString()}");

            return context.Response.WriteAsync(result);
        }
    }
}
