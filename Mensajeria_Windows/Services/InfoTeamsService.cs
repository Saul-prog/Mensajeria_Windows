using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mensajeria_Windows.EntityFramework.Data;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.Services.Interfaces;
using Mensajeria_Windows.EntityFramework.Models.InfoTeam;
using Mensajeria_Windows.EntityFramework.Helpers;

namespace Mensajeria_Windows.Services
{
    public class InfoTeamsService : IInfoTeamService
    {
        private NotificationContext _dbCntext;
        private readonly IMapper _mapper;

        public InfoTeamsService (NotificationContext dbCntext, IMapper mapper)
        {
            _dbCntext = dbCntext;
            _mapper = mapper;
        }

       // public async Task<ActionResult> CreateInfoTeams (CreateInfoTeamsRequest model)
        public async Task<int> CreateInfoTeams (CreateInfoTeamsRequest model)
        {
            
            if (await _dbCntext.infoTeams.AnyAsync(x => x.webHook == model.webHook))
            {
                return 0;
            }
              

            //Hace un mapa del modelo InfoTeams
            InfoTeams infoTeams = _mapper.Map<InfoTeams>(model);

            //Guardar InfoTeams
            _dbCntext.infoTeams.Add(infoTeams);
             await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
            if(infoTeams != null)
            {
                return infoTeams.id;
            }

            return 0;
        }

        public async Task<int> DeleteInfoTeams (int id)
        {
            InfoTeams? infoTeams = await _getInfoTeamsById(id);
            if (infoTeams != null)
            {
                _dbCntext?.infoTeams.Remove(infoTeams);
                await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
                return 0;
            }
            else
            {
                throw new RepositoryExceptions($"No existe InfoTeams de usuario con id {id}");
            }
        }
        public async Task<IEnumerable<InfoTeams>> GetAllInfoTeams ( ) {
            //Devuelve todos los InfoTeams
            return await _dbCntext.infoTeams.ToArrayAsync().ConfigureAwait(true);
        }

        public async Task<int> UpdateInfoTeams (int id, UpdateInfoTeamsRequest model)
        {
            InfoTeams? infoTeams = await _getInfoTeamsById(id );
            // Validation
            if (model.webHook != infoTeams.webHook && await _dbCntext.infoTeams.AnyAsync(x => x.webHook == model.webHook))
                throw new RepositoryExceptions($" {model.webHook} ya existe.");

            _mapper.Map(model, infoTeams);
            _dbCntext.infoTeams.Update(infoTeams);
            await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
            return 0;
        }
        public async Task<InfoTeams> GetInfoTeamsById (int id)
        {
            InfoTeams? infoTeams = await _dbCntext.infoTeams
                     .AsNoTracking()
                     .Where(x => x.id == id)
                     .FirstOrDefaultAsync().ConfigureAwait(true);

            if (infoTeams == null)
            {
                throw new KeyNotFoundException("infoTeams not found");
            }

            return infoTeams;
        }

        private async Task<InfoTeams> _getInfoTeamsById(int id)
        {
            InfoTeams? infoTeams = await _dbCntext.infoTeams
                .AsNoTracking()
                .Where(x=> x.id == id)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            if(infoTeams == null)
            {
                throw new KeyNotFoundException("InfoTeams no se ha encontrado en la base de datos");
            }
            return infoTeams;
        }

    }
}
       