using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Mensajeria_Windows.Infrastructure.Filters.AppInsights
{
    /// <summary>
    /// Filter to remove telemetry for healthcheck requests
    /// </summary>
    public class HealthCheckFilter : ITelemetryProcessor
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="next">Represents an object used to process telemetry as part of sending it to Application Insights</param>
        public HealthCheckFilter(ITelemetryProcessor next)
        {
            Next = next;
        }

        private ITelemetryProcessor Next { get; set; }

        /// <summary>
        /// Process a collected telemetry item.
        ///     Ignores all telemetry containing "healthcheck" in the Request Uri
        /// </summary>
        /// <param name="item">A collected Telemetry item</param>
        public void Process(ITelemetry item)
        {
            if (item is RequestTelemetry request &&
            request.Url.AbsoluteUri.Contains("healthcheck"))
            {
                return;
            }

            Next.Process(item);
        }
    }
}
