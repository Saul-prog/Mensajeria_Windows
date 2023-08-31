using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.InfoTeam;

namespace Mensajeria_Windows.Business.Interfaces
{
    public interface IInfoTeamsProvider
    {
        /// <summary>
        /// Getter de todos los datos de InfoTeams
        /// </summary>
        /// <returns>Todos los InfoTeams de la base de datos</returns>
        Task<IEnumerable<InfoTeams>> GetAllInfoTeamsAsync();
        /// <summary>
        /// Getter de toda la información de un InfoTeams por id 
        /// </summary>
        /// <param name="id">Parámetro obligatorio mediante el que se busca</param>
        /// <returns>Toda la información de un solo InfoTeams</returns>
        Task<InfoTeams> GetInfoTeamsByIdAsync(int id);
        /// <summary>
        /// Crea un muevo InfoTeams en la base de datos
        /// </summary>
        /// <param name="model">Create InfoTeams Request model</param>
        Task<int> CreateInfoTeams(CreateInfoTeamsRequest model);

        /// <summary>
        /// Actualiza un InfoTeams en la base de datos si ya existe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
         Task UpdateInfoTeams (int id, UpdateInfoTeamsRequest model);
        /// <summary>
        /// Eliminia un único InfoTeams. Lo elimina si existe
        /// </summary>
        /// <param name="id"></param>
        Task<int> DeleteInfoTeams (int id);

    }
}
