using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mensajeria_Windows.Business.Interfaces;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.InfoWhatsApp;

namespace Mensajeria_Windows.Controllers.WhatsApp
{
    [ApiController]
    [Route("{version:apiVersion}/[controller]")]

    [Consumes("application/json")]
    [Produces("application/json")]
    public class WhatsAppController
    {
        private IInfoWhatsAppBusiness _infoWhatsAppBusiness;
        private IMapper _mapper;

        public WhatsAppController (IInfoWhatsAppBusiness infoWhatsAppBusiness, IMapper mapper)
        {
            _infoWhatsAppBusiness = infoWhatsAppBusiness;
            _mapper = mapper;
        }
        /// <summary>
        /// Crea un nuevo InfoWhatsApp
        /// </summary>
        /// <param name="model">CreateInfoWhatsAppRequest</param>
        /// <returns>infoWhatsAppId</returns>
        [HttpPost]
        public async Task<ActionResult> CreateInfoTeams (CreateInfoWhatsAppRequest model)
        {
            int infoWhatsAppId = await _infoWhatsAppBusiness.CreateInfoWhatsApp(model);

            if (infoWhatsAppId != 0)
            {
                return new ObjectResult(infoWhatsAppId)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new ObjectResult("InfoTeams no se ha creado")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

        }

        /// <summary>
        /// Devuelve todos los InfoWhatsApp que hay
        /// </summary>
        /// <param></param>
        /// <returns>IEnumerable<InfoWhatsApp></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllInfoTeams ( )
        {
            IEnumerable<InfoWhatsApp> infoWhatsApps = await _infoWhatsAppBusiness.GetAllInfoWhatsApp();

            return new ObjectResult(infoWhatsApps)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
        /// <summary>
        /// Método para eliminar un registro de InfoWhatsApp por id
        /// </summary>
        /// <param name="id">Identificador de InfoWhatsApp que se quiere eliminar</param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIfoteams (int id)
        {
            int deveulto = await _infoWhatsAppBusiness.DeleteInfoWhatsApp(id);
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
        /// Método para actualizar un registro de InfoWhatsApp
        /// </summary>
        /// <param name="id">Identificador del que se quiere actualizar</param>
        /// <param name="UpdateInfoWhatsAppRequest">Datos para actualizar</param>
        /// <returns>
        ///          Status 200 si ha se ha hecho correctamente
        ///          Status 500 si ha ocurrido algún error
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInfoTeams (int id, UpdateInfoWhatsAppRequest infoWhatsApp)
        {
            int deveulto = await _infoWhatsAppBusiness.UpdateInfoWhatsApp(id, infoWhatsApp);
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
    }
}
