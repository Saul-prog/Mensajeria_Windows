using Mensajeria_Windows.Controllers.Email.Models;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.InfoEmail;

namespace Mensajeria_Windows.Services.Interfaces
{
    public interface IInfoEmailService
    {
        Task<int> CreateInfoEmail (CreateInfoEmailRequest model, int id);
        Task<int> DeleteInfoEmail (int id);
        Task<IEnumerable<InfoEmail>> GetAllInfoEmail ( );
        Task<IEnumerable<InfoEmail>> GetAllInfoEmailByAgencia (int id);
        Task<int> UpdateEmailTeams (int id, UpdateInfoEmailRequest model);
        Task<InfoEmail> GetInfoEmailById (int id);
        Task<InfoEmail> GetInfoEmailByAgenciaIdAndTipo (int id, string emailOrigen);
        Task<int> EnviarEmailConPlantillaADestino (string plantilla, InfoEmail infoEmail, List<DatosEmail> emailsDestino, string titulo, List<DatosFichero> ficheros);
    }
}
