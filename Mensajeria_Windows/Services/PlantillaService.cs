using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mensajeria_Windows.EntityFramework.Data;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Helpers;
using Mensajeria_Windows.EntityFramework.Models.Plantillas;
using Mensajeria_Windows.Services.Interfaces;
using System.Xml.Linq;

namespace Mensajeria_Windows.Services
{
    public class PlantillaService : IPlantillaService
    {
        private NotificationContext _dbContext;
        private readonly IMapper _mapper;

        public PlantillaService (NotificationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreatePlantilla (CreatePlantillaRequest model)
        {
            
            if(await _dbContext.Plantilla.AnyAsync(x => x.nombrePlantilla == model.nombrePlantilla && x.tipoExtension == model.tipoExtension))
            {
                return 0;
            }              
            
            Plantillas plantilla = _mapper.Map<Plantillas>(model);
            plantilla.created = DateTime.Now;
            _dbContext.Plantilla.Add(plantilla);
            if (await _dbContext.SaveChangesAsync() != 1)
            {
                return plantilla.id;
            }

            return 1;
        }

        public async Task<int> DeletePlantillaByNameAndExtension (string name, string extension)
        {
            Plantillas? plantilla = await _getPlantillaByNameAndExtension(name,extension);
            _dbContext.Plantilla.Remove(plantilla);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> DeletePlantillaById (int id)
        {
            Plantillas? plantilla = await _getPlantillaById(id);
            _dbContext.Plantilla.Remove(plantilla);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdatePlantilla (UpdatePlantillaRequest model)
        {
            Plantillas plantilla = await _getPlantillaByNameAndExtension(model.nombrePlantilla,model.tipoExtension);
            _mapper.Map(model, plantilla);
            _dbContext.Plantilla.Update(plantilla);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Plantillas>> GetAllPlantillasByName(string name)
        {
            return await _getAllPlantillasByName(name);
        }
        private async Task<IEnumerable<Plantillas>> _getAllPlantillasByName(string name)
        {
            return await _dbContext.Plantilla.ToArrayAsync().ConfigureAwait(true);
        }
        private async Task<Plantillas> _getPlantillaById (int id)
        {
            Plantillas? plantilla = await _dbContext.Plantilla
                .AsNoTracking()
                .Where(x => x.id == id).
                FirstAsync().ConfigureAwait(true);
            if (plantilla == null)
            {
                throw new RepositoryExceptions("Usuario no encontrado");
            }
            return plantilla;
        }

        private async Task<Plantillas> _getPlantillaByNameAndExtension (string name, string extension)
        {
            Plantillas? plantilla = await _dbContext.Plantilla
                .AsNoTracking()
                .Where(x => x.nombrePlantilla == name && x.tipoExtension==extension )
                .FirstAsync().ConfigureAwait(true);
            if (plantilla == null)
            {
                throw new RepositoryExceptions("Usuario no encontrado");
            }
            return plantilla;
        }

        public async Task<Plantillas> GetPlantillaByNameAndExtension (string name, string extension)
        { 
            return await _getPlantillaByNameAndExtension(name, extension);
        }
        public async Task<string> GetContenidoPlantillaByNameAndExtension (string name, string extension)
        {
            Plantillas plantilla = await _getPlantillaByNameAndExtension(name,extension);
            return plantilla.plantilla;
        }


    }
}
