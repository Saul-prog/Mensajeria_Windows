using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mensajeria_Windows.Business.Interfaces;
using Mensajeria_Windows.Controllers.Teams.Models;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.InfoTeam;
using System.Runtime.InteropServices;

namespace Mensajeria_Windows.Controllers.Teams
{

    [ApiController]
    [Route("{version:apiVersion}/[controller]")]
    
    [Consumes("application/json")]
    [Produces("application/json")]
    
    public class TeamsController
    {
        private IInfoTeamsProvider _infoTeamsProvider;
        private IMapper _mapper;
        private ITeamsBusiness _teamsBusiness;
        public TeamsController(IInfoTeamsProvider infoTeamsProvider, IMapper mapper, ITeamsBusiness teamsBusiness)
        {
            _infoTeamsProvider = infoTeamsProvider;
            _mapper= mapper;
            _teamsBusiness = teamsBusiness;
        }

        /// <summary>
        /// Crea un nuevo InfoTeams
        /// </summary>
        /// <param name="model">CreateInfoTeamsRequest</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateInfoTeams(CreateInfoTeamsRequest model)
        {
            int infoTeamsId = await _infoTeamsProvider.CreateInfoTeams(model);

            if (infoTeamsId != 0)
            {
                return new ObjectResult("Se ha creado."+ infoTeamsId)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new ObjectResult("The InfoTeams was not created in the database.")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

        }
        /// <summary>
        /// Devuelve todos los InfoTems que hay
        /// </summary>
        /// <param></param>
        /// <returns>IEnumerable<InfoTeams></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllInfoTeams ( )
        {
            IEnumerable<InfoTeams> infoTeams =  await _infoTeamsProvider.GetAllInfoTeamsAsync();
            
                return new ObjectResult(infoTeams)
                {
                    StatusCode = StatusCodes.Status200OK
                };

        }
        /// <summary>
        /// Método para eliminar un registro de InfoTeams por id
        /// </summary>
        /// <param name="id">Identificador de InfoTeams que se quiere eliminar</param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIfoteams (int id)
        {
            int deveulto = await _infoTeamsProvider.DeleteInfoTeams(id);
            if (deveulto != 0)
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

        /// <summary>
        /// Método para actualizar un registro de InfoTeams
        /// </summary>
        /// <param name="id">Identificador del que se quiere actualizar</param>
        /// <param name="infoTeams">Datos para actualizar</param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInfoTeams(int id, InfoTeams infoTeams)
        {
            int deveulto = await _infoTeamsProvider.DeleteInfoTeams(id);
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

        [HttpPost("Enviar")]
        public async Task<IActionResult> EnviarTeamsConWebHook(TeamsBasico teamsBasico)
        {
            switch (teamsBasico.template)
            {
                case 1:
                    int deveulto = await _teamsBusiness.EnviarTeamsSimple(teamsBasico.webhookurl, teamsBasico.titulo, teamsBasico.cuerpo);
                    if (deveulto != 0)
                    {
                        return new ObjectResult("No se ha enviado correctamente")
                        {
                            StatusCode = StatusCodes.Status500InternalServerError
                        };
                    }
                    return new ObjectResult("Se ha enviado correctamente")
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                    break;
                default:
                    return new ObjectResult("No existe esa plantilla")
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                    break;
            }
        }
    }
}
