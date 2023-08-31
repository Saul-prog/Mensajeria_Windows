using System.Net;

namespace Mensajeria_Windows.Infrastructure.Responses
{
    public class BaseErrorResponse : IErrorResponse
    {
        protected BaseErrorResponse(string title, string detail, string instance, HttpStatusCode status, string type)
        {
            Title = title;
            Status = status;
            Instance = instance;
            Detail = detail;
            Type = type;
        }

        public string Title { get; }

        public string Detail { get; }

        public HttpStatusCode Status { get; }

        public string Instance { get; }

        public string Type { get; set; }
    }
}
