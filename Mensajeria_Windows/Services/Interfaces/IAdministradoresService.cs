using Mensajeria_Windows.EntityFramework.Entities;

namespace Mensajeria_Windows.Services.Interfaces
{
    public interface IAdministradoresService
    {
        public Task<Administradores> GetAdministradorByEmail (string email);
        public Task<bool> IsAdministrador (string email, string token);
    }
}
