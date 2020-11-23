using EventYojana.Infrastructure.Core.ExceptionHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.Infrastructure.Core.Middlewares
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger, IHostEnvironment env)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger, env);
            }
        }
    
        private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> Logger, IHostEnvironment env)
        {
            Logger.LogError("{ \"Exception\" : \" " + ex + " \"}");
            
            if (ex is ValidationException validationException)
            {
                var result = JsonConvert.SerializeObject(new
                {
                    validationErrors = validationException.GetErrorMessage()
                });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return context.Response.WriteAsync(result);
            }
            else
            {
                var result = JsonConvert.SerializeObject(new
                {
                    ResponseCode = "",
                    ResponseMessage = (int)HttpStatusCode.InternalServerError
                });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return context.Response.WriteAsync(result);
            }
        }
    }
}
