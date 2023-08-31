using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mensajeria_Windows.Infrastructure.Responses;

namespace Mensajeria_Windows.Infrastructure.Middlewares
{
    /// <summary>
    /// Middleware to handle exceptions globally.
    /// </summary>
    public class ApiExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ApiExceptionHandlingMiddleware> logger;
        private readonly IDictionary<Type, Func<HttpContext, Exception, Task>> exceptionHandlers;

        /// <summary>
        /// Contrusctor middleware to handle exceptions globally.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ApiExceptionHandlingMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlingMiddleware> logger)
        {
            exceptionHandlers = new Dictionary<Type, Func<HttpContext, Exception, Task>>
            {
            };

            this.next = next;
            this.logger = logger;
        }

        /// <summary>
        /// Middleware entry for handling exceptions
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            Type type = ex.GetType();
            if (exceptionHandlers.ContainsKey(type))
            {
                await exceptionHandlers[type].Invoke(context, ex);
                return;
            }

            await HandleUnknownException(context, ex);
        }

        private async Task HandleUnknownException(HttpContext context, Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");

            var result = new InternalServerErrorResponse(HttpStatusCode.InternalServerError.ToString(), string.Empty, context.Request.Path);

            context.Response.StatusCode = (int)result.Status;
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}
