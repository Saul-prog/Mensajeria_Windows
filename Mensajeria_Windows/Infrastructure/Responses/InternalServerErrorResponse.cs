using System.Net;

namespace Mensajeria_Windows.Infrastructure.Responses
{
    public class InternalServerErrorResponse : BaseErrorResponse
    {
        public InternalServerErrorResponse(
            string title,
            string detail,
            string instance)
            : base(title, detail, instance, HttpStatusCode.InternalServerError, "internal-server-error")
        {
        }
    }
}
