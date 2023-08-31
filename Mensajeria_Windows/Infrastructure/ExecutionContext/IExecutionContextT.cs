using Mensajeria_Windows.ExecutionContext.Infrastructure.ExecutionContext;
using Mensajeria_Windows.Infrastructure.ErrorMapping;

namespace Mensajeria_Windows.Infrastructure.ExecutionContext
{
    public interface IExecutionContext<out T> : IExecutionContext
    {
        ILogger<T> Logger { get; }

        IErrorMapper ErrorMapper { get; }
    }
}
