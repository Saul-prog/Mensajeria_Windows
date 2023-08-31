using System.Net;

namespace Mensajeria_Windows.Infrastructure.Responses
{
    /// <summary>
    /// Definicion de como se veran las respuestas
    /// de error hacia el exterior
    /// </summary>
    public interface IErrorResponse
    {
        string Title { get; }

        string Detail { get; }

        string Instance { get; }

        HttpStatusCode Status { get; }

        string Type { get; }
    }
}
