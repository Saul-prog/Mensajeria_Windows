using System;
using System.Threading;

namespace Mensajeria_Windows.ExecutionContext.Infrastructure.ExecutionContext
{
    public interface IExecutionContext
    {
        public string Id { get; }

        public string CorrelationId { get; }

        public string CorrelationCallId { get; }

        public CancellationToken CancellationToken { get; }

        public DateTime ExecutionDate { get;  }
    }
}
