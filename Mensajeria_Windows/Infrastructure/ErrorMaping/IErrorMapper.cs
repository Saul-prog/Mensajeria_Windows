using Microsoft.AspNetCore.Http;
using Mensajeria_Windows.Infrastructure.Responses;
using Mensajeria_Windows.Infrastructure.Domain;

namespace Mensajeria_Windows.Infrastructure.ErrorMapping
{
    public interface IErrorMapper
    {
        IErrorResponse GetError(HttpContext ctx, BaseError input);
    }
}
