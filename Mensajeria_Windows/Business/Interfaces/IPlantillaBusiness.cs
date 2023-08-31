using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.Plantillas;

namespace Mensajeria_Windows.Business.Interfaces
{
    public interface IPlantillaBusiness
    {
        Task<int> CreateInfoEmail (CreatePlantillaRequest model, string adminEmail, string adminToken);
        Task<IEnumerable<Plantillas>> GetAllPlantillasByName (string nombre, string adminEmail, string adminToken);
        Task<int> UpdatePlantilla (int i, UpdatePlantillaRequest model, string emailAdmin, string tokeAdmin);
        Task<int> DeletePlantilla (int id, string agencia, string token);
    }
}
