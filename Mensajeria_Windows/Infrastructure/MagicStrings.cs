namespace Mensajeria_Windows.Infrastructure
{
    internal static class MagicStrings
    {

        internal static class AppInsights
        {
            public const string RequestBodyType = "RequestBodyType";
            public const string SkipTraceResponse = "SkipTraceResponse";
            public const string SkipTraceRequest = "SkipTraceRequest";
            public const string ResponseBodyType = "ResponseBodyType";
            public const string RequestBody = "RequestBody";
            public const string RequestHeaders = "RequestHeaders";
            public const string DependencyRequestBody = "DependencyRequestBody";
            public const string DependencyRequestHeaders = "DependencyRequestHeaders";
            public const string ResponseBody = "ResponseBody";
            public const string SkipTraceResponseBody = "SkipTraceResponseBody";
            public const string SkipTraceRequestBody = "SkipTraceRequestBody";
            public const string ResponseHeaders = "ResponseHeaders";
            public const string DependencyResponseBody = "DependencyResponseBody";
            public const string DependencyResponseHeaders = "DependencyResponseHeaders";
            public const string CorrelationId = "CorrelationId";
            public const string CorrelationCallId = "CorrelationCallId";
            public const string DepenendyCorrelationCallId = "DependencyCorrelationCallId";
            public const string UtcDateTime = "UtcDateTime";
            public const string ApplicationCustomProperty = "ApplicationCustomProperty";
        }

        public const string ConfigurationSupportedCultures = @"App:SupportedCultures";
        public const string BodyTracesSkippedByCode = "Body trace skipped by code";

        internal static class Headers
        {
            public const string Correlation = "correlation-id";
            public const string CorrelationCall = "correlation-call-id";
            public const string Date = "date";
        }

    }
}
