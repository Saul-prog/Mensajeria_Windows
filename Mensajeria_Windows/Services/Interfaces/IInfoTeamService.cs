using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.InfoTeam;

namespace Mensajeria_Windows.Services.Interfaces
{
    public interface IInfoTeamService
    {
        Task<int> CreateInfoTeams (CreateInfoTeamsRequest model);

        Task<int> DeleteInfoTeams (int id);

        Task<IEnumerable<InfoTeams>> GetAllInfoTeams ( );

        Task<int> UpdateInfoTeams (int id, UpdateInfoTeamsRequest model);

         Task<InfoTeams> GetInfoTeamsById (int id);
    }
}
