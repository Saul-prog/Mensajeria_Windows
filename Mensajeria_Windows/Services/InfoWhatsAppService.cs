using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mensajeria_Windows.EntityFramework.Data;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Helpers;
using Mensajeria_Windows.EntityFramework.Models.InfoWhatsApp;
using Mensajeria_Windows.Services.Interfaces;


namespace Mensajeria_Windows.Services
{
    public class InfoWhatsAppService : IInfoWhatsAppService
    {
        private NotificationContext _dbCntext;
        private readonly IMapper _mapper;

        public InfoWhatsAppService (NotificationContext dbCntext, IMapper mapper)
        {
            _dbCntext = dbCntext;
            _mapper = mapper;
        }

        // public async Task<ActionResult> CreateInfoTeams (CreateInfoTeamsRequest model)
        public async Task<int> CreateInfoWhatsApp (CreateInfoWhatsAppRequest model)
        {
            if (await _dbCntext.infoWhatsApp.AnyAsync(x => x.tokenAcceso == model.token))
            {
                return 0;
            }


            //Hace un mapa del modelo InfoTeams
            InfoWhatsApp infoWhatsApp = _mapper.Map<InfoWhatsApp>(model);

            //Guardar InfoTeams
            _dbCntext.infoWhatsApp.Add(infoWhatsApp);
            await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
            if (infoWhatsApp != null)
            {
                return infoWhatsApp.id;
            }

            return 0;
        }

        public async Task<int> DeleteInfoWhatsApp (int id)
        {
            InfoWhatsApp? infoWhatsApp = await _getInfoWhatsAppsById(id);
            if (infoWhatsApp != null)
            {
                _dbCntext?.infoWhatsApp.Remove(infoWhatsApp);
                await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
                return 0;
            }
            else
            {
                throw new RepositoryExceptions($"No existe InfoTeams de usuario con id {id} ya existe.");
            }
        }
        public async Task<IEnumerable<InfoWhatsApp>> GetAllInfoWhatsApp ( )
        {
            //Devuelve todos los InfoTeams
            return await _dbCntext.infoWhatsApp.ToArrayAsync().ConfigureAwait(true);
        }

        public async Task<int> UpdateInfoWhatsApp (int id, UpdateInfoWhatsAppRequest model)
        {
            InfoWhatsApp? infoWhatsApp = await _getInfoWhatsAppsById(id);
            // Validation
            if (model.token != infoWhatsApp.tokenAcceso && await _dbCntext.infoWhatsApp.AnyAsync(x => x.tokenAcceso == model.token))
            {
                throw new RepositoryExceptions($"El token {model.token} ya existe.");
            }               

            _mapper.Map(model, infoWhatsApp);
            _dbCntext.infoWhatsApp.Update(infoWhatsApp);
            return 0;
        }
        public async Task<InfoWhatsApp> GetInfoWhatsAppById (int id)
        {
            InfoWhatsApp? infoWhatsApp = await _dbCntext.infoWhatsApp
                     .AsNoTracking()
                     .Where(x => x.id == id)
                     .FirstOrDefaultAsync().ConfigureAwait(true);

            if (infoWhatsApp == null)
            {
                throw new KeyNotFoundException("infoTeams not found");
            }

            return infoWhatsApp;
        }

        private async Task<InfoWhatsApp> _getInfoWhatsAppsById (int id)
        {
            InfoWhatsApp? infoWhatsApp = await _dbCntext.infoWhatsApp
                .AsNoTracking()
                .Where(x => x.id == id)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            if (infoWhatsApp == null)
            {
                throw new KeyNotFoundException("InfoTeams no se ha encontrado en la base de datos");
            }
            return infoWhatsApp;
        }

    }
}
