using Mensajeria_Windows.Controllers.Email.Models;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.InfoEmail;

namespace Mensajeria_Windows.Business.Interfaces
{
    public interface IEmailBusiness
    {
        Task<int> CreateInfoEmail (CreateInfoEmailRequest model, string adminEmail,string adminToken);
        Task<IEnumerable<InfoEmail>> GetAllInfoEmail (string agencia, string? token, string? emailAdmin, string? tokenAdmin);
        Task<InfoEmail> GetInfoEmailById (int id);
        Task<int> UpdateInfoEmail (int id, UpdateInfoEmailRequest model, string emailAdmin,string tokeAdmin);
        Task<int> DeleteInfoEmail (int id, string agencia,string? token, string? emailAdmin, string? tokenAdmin);
        Task<int> EnviarProcesoAutorizacion (EnviarEmail datos);
    }
}
