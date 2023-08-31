using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mensajeria_Windows.EntityFramework.Data;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.Services.Interfaces;

namespace Mensajeria_Windows.Services
{
    public class AdministradoresService : IAdministradoresService
    {

        private NotificationContext _dbContext;
        private readonly IMapper _mapper;

        public AdministradoresService (NotificationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Administradores> GetAdministradorByEmail (string email)
        {
            Administradores? administradores = await _dbContext.administradores
                .AsNoTracking()
                .Where(x => x.email == email)
                .FirstOrDefaultAsync().ConfigureAwait(true);


            if (administradores == null)
            {
                throw new KeyNotFoundException("infoEmail not found");
            }
            else
            {
                return administradores;
            }
        }
        public async Task<bool> IsAdministrador(string email, string token)
        {
            bool respuesta = await _dbContext.administradores.AnyAsync(x => x.email == email && x.token == token);
            return respuesta;
        } 
    }
}
