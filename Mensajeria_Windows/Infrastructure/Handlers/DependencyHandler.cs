

namespace Mensajeria_Windows.Infrastructure.Handlers
{
    /// <summary>
    /// Delegating handler to add correlation header to dependency calls
    /// </summary>
    public class DependencyHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="httpContextAccessor">Http context accesor</param>
        public DependencyHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// Adds new correlationcallid for the dependency request
        /// </summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns></returns>
        protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var dependencyCorrelationCallId = Guid.NewGuid().ToString();
            request.Headers?.Add(MagicStrings.Headers.CorrelationCall, dependencyCorrelationCallId);

            if (httpContextAccessor.HttpContext.Items.ContainsKey(MagicStrings.AppInsights.DepenendyCorrelationCallId))
            {
                httpContextAccessor.HttpContext.Items[MagicStrings.AppInsights.DepenendyCorrelationCallId] = dependencyCorrelationCallId;
            }
            else
            {
                httpContextAccessor.HttpContext.Items.Add(MagicStrings.AppInsights.DepenendyCorrelationCallId, dependencyCorrelationCallId);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
