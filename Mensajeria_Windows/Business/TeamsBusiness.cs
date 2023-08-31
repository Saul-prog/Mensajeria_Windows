using Microsoft.Azure.Cosmos.Table;
using Mensajeria_Windows.Business.Interfaces;
using Mensajeria_Windows.Services.Interfaces;

namespace Mensajeria_Windows.Business
{
    public class TeamsBusiness : ITeamsBusiness
    {
        private ITeamsService _teamsService;
        public TeamsBusiness(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        public async Task<int> EnviarTeamsSimple ( string webHookUrl, string titulo,string texto)
        {
            string pathPlantilla = Path.Combine("Services\\PlantillasTeams", "PlantillaBasica.json");
            string plantilla = File.ReadAllText(pathPlantilla);
            plantilla = plantilla.Replace("${title}", titulo);
            plantilla = plantilla.Replace("${description}", titulo);
            return await _teamsService.EnviarTeamsSimple(webHookUrl, plantilla);
        }

    }
}
