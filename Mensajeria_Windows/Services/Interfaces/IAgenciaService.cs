using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.Agencias;
using Mensajeria_Windows.EntityFramework.Models.InfoWhatsApp;

namespace Mensajeria_Windows.Services.Interfaces
{
    public interface IAgenciaService
    {
        Task<int> CreateAgencia (CreateAgenciaRequest model);
        Task<int> UpdateAgencia ( UpdateAgenciaRequest model);
        Task<int> DeleteAgencia (string name);
        Task<Agencia> GetAgenciaByName (string name);
        Task<IEnumerable<Agencia>> GetAllAgencia ( );
        Task<Agencia> GetAgenciaByNameAndToken (string name, string token);
        Task<int> GetAgenciaIdByNameAndToken (string name, string token);
    }

}
