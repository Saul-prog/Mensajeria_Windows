using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mensajeria_Windows.Infrastructure.Responses;
using Mensajeria_Windows.Infrastructure.Domain;

namespace Mensajeria_Windows.Infrastructure.ErrorMapping
{
    public class DefaultErrorMapper : IErrorMapper
    {
        private readonly IOptions<ErrorMappingOptions> options;

        public DefaultErrorMapper(IOptions<ErrorMappingOptions> options)
        {
            this.options = options;
        }

        public IErrorResponse GetError(HttpContext ctx, BaseError input)
        {
            return options.Value.Map(ctx, input);
        }
    }
}
