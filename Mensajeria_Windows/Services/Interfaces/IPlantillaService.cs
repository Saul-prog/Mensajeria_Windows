using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.Plantillas;

namespace Mensajeria_Windows.Services.Interfaces
{
    public interface IPlantillaService
    {
        Task<int> CreatePlantilla (CreatePlantillaRequest model);
        Task<int> DeletePlantillaByNameAndExtension (string name, string extension);
        Task<int> DeletePlantillaById (int id);
        Task<int> UpdatePlantilla (UpdatePlantillaRequest model);
        Task<IEnumerable<Plantillas>> GetAllPlantillasByName (string name);
        Task<Plantillas>  GetPlantillaByNameAndExtension(string name, string extension);
        Task<string> GetContenidoPlantillaByNameAndExtension (string name, string extension);
    }
}
