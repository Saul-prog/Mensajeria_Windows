using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mensajeria_Windows.ExecutionContext.Infrastructure.ExecutionContext;

namespace Mensajeria_Windows.Infrastructure.Middlewares
{
    public class CorrelationHeaderMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="CorrelationHeaderMiddleware"/> class.
        /// </summary>
        /// <param name="next"></param>
        public CorrelationHeaderMiddleware(RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task Invoke(HttpContext context, IExecutionContext executionContext)
        {
            var correlationId = executionContext.CorrelationId;

            // Save correlationId on properties for telemetry purposes
            context.Items.Add(MagicStrings.AppInsights.CorrelationId, correlationId);

            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add(MagicStrings.Headers.Correlation, correlationId);
                return Task.CompletedTask;
            });

            return next(context);
        }
    }
}
