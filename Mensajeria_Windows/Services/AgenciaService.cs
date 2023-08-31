using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mensajeria_Windows.EntityFramework.Data;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Helpers;
using Mensajeria_Windows.EntityFramework.Models.Agencias;
using Mensajeria_Windows.Services.Interfaces;

namespace Mensajeria_Windows.Services
{
    public class AgenciaService : IAgenciaService
    {

        private NotificationContext _dbContext;
        private readonly IMapper _mapper;

        public AgenciaService (NotificationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateAgencia (CreateAgenciaRequest model)
        {
            if (await _dbContext.Agencias.AnyAsync(x => x.nombreAgencia == model.nombreAgencia))
            {
                return 0;
            }
            Agencia agencia = _mapper.Map<Agencia>(model);
            _dbContext.Agencias.Add(agencia);
           if( await _dbContext.SaveChangesAsync()!=1 )
            {
                return agencia.id;
            }

            return 1;
        }

        public async Task<int> DeleteAgencia ( string name)
        {
            Agencia? agencia = await _getagenciaByName(name);
            _dbContext.Agencias.Remove(agencia);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAgencia (UpdateAgenciaRequest model)
        {
            Agencia agencia = await _getagenciaByName(model.nombreAgencia);
            _mapper.Map(model, agencia);
            _dbContext.Agencias.Update(agencia);
            return await _dbContext.SaveChangesAsync();
        }


        private async Task<Agencia> _getagenciaById(int id)
        {
            Agencia? agencia = await _dbContext.Agencias
                .AsNoTracking()
                .Where(x => x.id == id).
                FirstAsync().ConfigureAwait(true);
            if(agencia == null)
            {
                throw new RepositoryExceptions("Usuario no encontrado");
            }
            return agencia;
        }

        private async Task<Agencia> _getagenciaByName (string name)
        {
            Agencia? agencia = await _dbContext.Agencias
                .AsNoTracking()
                .Where(x => x.nombreAgencia == name).
                FirstAsync().ConfigureAwait(true);
            if (agencia == null)
            {
                throw new RepositoryExceptions("Usuario no encontrado");
            }
            return agencia;
        }

        public async Task<Agencia> GetAgenciaByName (string name)
        {
            return await _getagenciaByName(name);
        }
        public async Task<IEnumerable<Agencia>> GetAllAgencia ( )
        {
            return await _dbContext.Agencias.ToArrayAsync().ConfigureAwait(true);
        }

        public async Task<Agencia> GetAgenciaAndInfoTeamsByName(string name)
        {
            Agencia? agencia = await _dbContext.Agencias
                   .AsNoTracking()
                   .Where(x => x.nombreAgencia == name)
                   .Include(b => b.InfoTeams)
                   .FirstOrDefaultAsync().ConfigureAwait(true);
            return agencia;
        }
        public async Task<Agencia> GetAgenciaByNameAndToken (string name,string token)
        {
            return await _getagenciaByNameAndToken(name,token);
        }

        public async Task<int> GetAgenciaIdByNameAndToken(string name,string token)
        {
            return await _getAgenciaIdByNameAndToken(name, token);
        }
        private async Task<Agencia> _getagenciaByNameAndToken (string name, string token)
        {
            Agencia? agencia = await _dbContext.Agencias
                .AsNoTracking()
                .Where(x => x.nombreAgencia == name && x.token == token)
                .FirstAsync().ConfigureAwait(true);
            if (agencia == null)
            {
                throw new RepositoryExceptions("Usuario no encontrado");
            }
            return agencia;
        }
        private async Task<int> _getAgenciaIdByNameAndToken(string name,string token)
        {
            int agenciaId = await _dbContext.Agencias
                .AsNoTracking()
                .Where(x => x.nombreAgencia == name && x.token == token)
                .Select(x => x.id)
                .FirstAsync().ConfigureAwait(true);
           return agenciaId;
        }
    }
}
