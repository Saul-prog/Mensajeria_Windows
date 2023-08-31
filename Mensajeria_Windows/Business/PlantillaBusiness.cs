using AutoMapper;
using Mensajeria_Windows.Business.Interfaces;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.InfoEmail;
using Mensajeria_Windows.EntityFramework.Models.Plantillas;
using Mensajeria_Windows.Services.Interfaces;

namespace Mensajeria_Windows.Business
{
    public class PlantillaBusiness : IPlantillaBusiness
    {
        private IPlantillaService _plantillaService;
        private IAdministradoresService _administradoresService;
        private IMapper _mapper;
        public PlantillaBusiness (IPlantillaService plantillaService, IMapper mapper, IAdministradoresService administradoresService)
        {
            _plantillaService = plantillaService;
            _mapper = mapper;
            _administradoresService = administradoresService;
        }

        public async Task<int> CreateInfoEmail (CreatePlantillaRequest model, string adminEmail, string adminToken)
        {
            if (await _administradoresService.IsAdministrador(adminEmail, adminToken))
            {
                return await _plantillaService.CreatePlantilla(model);
            }
            return 0;
        }
        public async Task<IEnumerable<Plantillas>> GetAllPlantillasByName (string nombre,string adminEmail, string adminToken)
        {
            if (await _administradoresService.IsAdministrador(adminEmail, adminToken))
            {
                return await _plantillaService.GetAllPlantillasByName(nombre);
            }
            return new List<Plantillas>();
            
        }
        public async Task<int> UpdatePlantilla (int id, UpdatePlantillaRequest model, string emailAdmin, string tokeAdmin)
        {
            /* if (await _administradoresService.IsAdministrador(emailAdmin, tokeAdmin) ||
                 await _genciaService.GetAgenciaIdByNameAndToken(model.nombreAgencia, model.tokenAgencia) != 0)
             {
                 return await _infoEmailService.UpdateEmailTeams(id, model);
             }
             return 0;*/
            return 1;
        }
        public async Task<int> DeletePlantilla (int id, string agencia, string token)
        {
            if (await _administradoresService.IsAdministrador(agencia, token))
            {
                return await _plantillaService.DeletePlantillaById(id);
            }
            return 1;
        }
    }
}
