using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.Business.Interfaces;
using Mensajeria_Windows.Services.Interfaces;
using AutoMapper;
using Mensajeria_Windows.EntityFramework.Models.InfoTeam;

namespace Mensajeria_Windows.Business
{
    public class InfoTeamsBusiness : IInfoTeamsProvider
    {
        private IInfoTeamService _infoTeamService;
        private IMapper _mapper;

        public InfoTeamsBusiness(IInfoTeamService infoTeamService, IMapper mapper)
        {
            _infoTeamService= infoTeamService;
            _mapper= mapper;
        }
        /// <summary>
        /// Getter de todos los datos de InfoTeams
        /// </summary>
        /// <returns>Todos los InfoTeams de la base de datos</returns>
       public  Task<IEnumerable<InfoTeams>> GetAllInfoTeamsAsync ( )
        {
            return _infoTeamService.GetAllInfoTeams ( );
        }
        /// <summary>
        /// Getter de toda la información de un InfoTeams por id 
        /// </summary>
        /// <param name="id">Parámetro obligatorio mediante el que se busca</param>
        /// <returns>Toda la información de un solo InfoTeams</returns>
        public Task<InfoTeams> GetInfoTeamsByIdAsync (int id)
        {
            return _infoTeamService.GetInfoTeamsById (id);
        }
        /// <summary>
        /// Crea un muevo InfoTeams en la base de datos
        /// </summary>
        /// <param name="model">Create InfoTeams Request model</param>
        public Task<int> CreateInfoTeams (CreateInfoTeamsRequest model)
        {
           return _infoTeamService.CreateInfoTeams (model);
        }

        /// <summary>
        /// Actualiza un InfoTeams en la base de datos si ya existe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public Task UpdateInfoTeams (int id, UpdateInfoTeamsRequest model)
        {        
            return _infoTeamService.UpdateInfoTeams(id, model);
        }
        /// <summary>
        /// Eliminia un único InfoTeams. Lo elimina si existe
        /// </summary>
        /// <param name="id"></param>
        public Task<int> DeleteInfoTeams (int id)
        {
           return _infoTeamService.DeleteInfoTeams (id);
        }
    }
}
