using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mensajeria_Windows.Business;
using Mensajeria_Windows.Business.Interfaces;
using Mensajeria_Windows.Controllers.PlantillasC.Models;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.InfoEmail;
using Mensajeria_Windows.EntityFramework.Models.Plantillas;

namespace Mensajeria_Windows.Controllers.PlantillasC
{
    [ApiController]
    [Route("{version:apiVersion}/[controller]")]

    [Consumes("application/json")]
    [Produces("application/json")]
    public class PlantillasController
    {
        private IMapper _mapper;
        private IPlantillaBusiness _plantillaBusiness;
        public PlantillasController(IPlantillaBusiness plantillaBusiness, IMapper mapper)
        {
            _mapper = mapper;
            _plantillaBusiness = plantillaBusiness;
        }
        [HttpPost("Crear")]
        public async Task<ActionResult> CrearPlantilla (CrearPlantilla model)
        {
            CreatePlantillaRequest plantilla = new CreatePlantillaRequest
            {
                nombrePlantilla = model.nombrePlantilla,
                tipoExtension = model.tipoExtension,
                plantilla = model.plantilla
            };
            int plantillaId = await _plantillaBusiness.CreateInfoEmail(plantilla, model.correoAmin, model.tokenAdmin);

            if (plantillaId != 0)
            {
                return new ObjectResult(plantillaId)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new ObjectResult("Plantilla no creada")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        [HttpGet("{plantilla}")]
        public async Task<IActionResult> GetAllInfoEmailByAgencia (string plantilla, string email,string token)
        {
            IEnumerable<Plantillas> plantillas = await _plantillaBusiness.GetAllPlantillasByName(plantilla,email, token);

            return new ObjectResult(plantillas)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> UpdatePlantilla (int id,ActualizarPlantilla model)
        {
            UpdatePlantillaRequest infoEmail = new UpdatePlantillaRequest
            {
               nombrePlantilla = model.nombrePlantilla,
               plantilla=model.plantilla,
               tipoExtension = model.tipoExtension,
            };
            int deveulto = await _plantillaBusiness.UpdatePlantilla( id,infoEmail, model.correoAmin, model.tokenAdmin);
            if (deveulto != 0)
            {
                return new ObjectResult("No se ha actualizado correctamente")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return new ObjectResult("Se ha actualizado correctamente")
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> DeletePlantilla(int id, string emailAdmin,string TokenAmin)
        {
            int deveulto = await _plantillaBusiness.DeletePlantilla(id, emailAdmin, TokenAmin);
            if (deveulto == 0)
            {
                return new ObjectResult("No se ha eliminado correctamente")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return new ObjectResult("Se ha eliminado correctamente")
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
