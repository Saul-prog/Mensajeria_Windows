using Mensajeria_Windows.ExecutionContext.Infrastructure.ExecutionContext;
using Mensajeria_Windows.Extensions;

namespace Mensajeria_Windows.Infrastructure.ExecutionContext
{
    /// <summary>
    /// Se utiliza para encapsular información importante sobre la ejecución de una operación en la aplicación.
    /// Id: Un identificador único para el contexto de ejecución.
    ///CorrelationId: El identificador de correlación que identifica un flujo de solicitudes.
    ///CorrelationCallId: El identificador de llamada de correlación que identifica una llamada específica dentro de un flujo de solicitudes.
    ///CancellationToken: El token de cancelación asociado con el contexto de ejecución.
    ///ExecutionDate: La fecha y hora en que se creó el contexto de ejecución.
    /// </summary>
    public class ApiExecutionContext : IExecutionContext
    {
        public ApiExecutionContext(IHttpContextAccessor httpContextAccessor)
        {
            ExecutionDate = DateTime.UtcNow;
            var httpContext = httpContextAccessor.HttpContext;

            CancellationToken = httpContext.RequestAborted;

            if (httpContext.Request.Headers.TryGetValue(MagicStrings.Headers.Correlation, out var correlationId))
            {
                var actualCorrelationId = correlationId.ToString().TrimAndAsNullIfEmpty() ?? Guid.NewGuid().ToString();
                httpContext.TraceIdentifier = actualCorrelationId;
                CorrelationId = actualCorrelationId;
            }
            else
            {
                CorrelationId = httpContext.TraceIdentifier;
            }

            if (httpContext.Request.Headers.TryGetValue(MagicStrings.Headers.CorrelationCall, out var correlationCallId))
            {
                CorrelationCallId = correlationCallId.ToString().TrimAndAsNullIfEmpty() ?? Guid.NewGuid().ToString();
            }
            else
            {
                CorrelationCallId = Guid.NewGuid().ToString();
            }

            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; private set; }

        public string CorrelationId { get; private set; }

        public string CorrelationCallId { get; private set; }

        public CancellationToken CancellationToken { get; private set; }

        public DateTime ExecutionDate { get; private set; }
    }

}
