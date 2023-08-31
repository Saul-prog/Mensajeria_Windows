using AutoMapper;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.Agencias;
using Mensajeria_Windows.Services.Interfaces;

namespace Mensajeria_Windows.Business.Interfaces
{
    public interface IAgenciaBusiness
    {
        Task<int> CreateAgencia (CreateAgenciaRequest model, string email, string tokenAdmin);
        Task<int> DeleteAgencia (string name, string adminMail, string adminToken);
        Task<int> UpdateAgencia (UpdateAgenciaRequest model, string adminMail, string adminToken);
        Task<Agencia> GetAgenciaByName (string name, string adminMail, string adminToken);
        Task<IEnumerable<Agencia>> GetAllAgencias (string adminMail, string adminToken);
    }
}
