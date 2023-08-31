namespace Mensajeria_Windows.Infrastructure.Options
{
    /// <summary>
    /// App Insights configuration class
    /// </summary>
    public class AppInsightsOptions
    {
        /// <summary>
        /// Controls whether the Request Telemetry is going to include Body and Headers information.
        /// Default true.
        /// </summary>
        public bool GetRequestInfo { get; set; } = true;

        /// <summary>
        /// Controls whether the Dependency Telemetry is going to include Body and Headers information
        /// Default true.
        /// </summary>
        public bool GetDependencyInfo { get; set; } = true;

        /// <summary>
        /// Sensitive Data Resolver configuration
        /// </summary>
        public SensitiveDataResolverOptions SensitiveDataResolverConfiguration { get; set; } = new SensitiveDataResolverOptions();

        /// <summary>
        /// Sensitive Data Resolver configuration class
        /// </summary>
        public class SensitiveDataResolverOptions
        {
            /// <summary>
            /// String to be used for masking sensitive data
            /// </summary>
            public string MaskString { get; set; } = "*****";
        }
    }
}
