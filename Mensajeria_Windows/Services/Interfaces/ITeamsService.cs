using Mensajeria_Windows.Controllers.Teams.Models;

namespace Mensajeria_Windows.Services.Interfaces
{
    public interface ITeamsService
    {
        Task<int> EnviarTeamsSimple (string webhookUrl, string contenido);
    }
}
