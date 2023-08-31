namespace Mensajeria_Windows.Infrastructure.Attributes
{
    [System.AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TraceRequestAttribute : Attribute
    {
        /// <summary>
        /// Constructor de si se va a hacer la traza o no
        /// </summary>
        /// <param name="skipTraceResponseBody"></param>
        /// <param name="skipTraceRequestBody"></param>
        public TraceRequestAttribute (bool skipTraceResponseBody = false, bool skipTraceRequestBody = false)
        {
            SkipTraceResponseBody = skipTraceResponseBody;
            SkipTraceRequestBody = skipTraceRequestBody;
        }

        /// <summary>
        /// Si son true no van ser traceados
        /// </summary>
        public bool SkipTraceResponseBody { get; }

        public bool SkipTraceRequestBody { get; }


    }
}
