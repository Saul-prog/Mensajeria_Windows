using Microsoft.AspNetCore.Mvc;
using Mensajeria_Windows.Business.Interfaces;
using Mensajeria_Windows.Controllers.Email.Models;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.InfoEmail;

namespace Mensajeria_Windows.Controllers.Email
{
    [ApiController]
    [Route("{version:apiVersion}/[controller]")]

    [Consumes("application/json")]
    [Produces("application/json")]
    public class EmailController
    {

       private IEmailBusiness _infoEmailBusiness;

        public EmailController(IEmailBusiness infoEmailBusiness)
        {
            _infoEmailBusiness = infoEmailBusiness;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult> CreateInfoEmail (CrearDatosEmail model)
        {
            CreateInfoEmailRequest infoEmail = new CreateInfoEmailRequest
            {
                nombreAgencia =model.nombreAgencia,
                tokenAgencia=model.tokenAgencia,
                emailOrigen=model.emailOrigen,
                host = model.host,
                port= model.port,
                emailTokenPassword = model.emailToken,
                emailNombre=model.nombreEmail,
            };
            int infoEmailId = await _infoEmailBusiness.CreateInfoEmail(infoEmail,model.emailAdmin, model.tokenAdmin);

            if (infoEmailId != 0)
            {
                return new ObjectResult(infoEmailId)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new ObjectResult("InfoTeams no se ha creado")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

        }
        
        [HttpGet("{agencia}")]
        public async Task<IActionResult> GetAllInfoEmailByAgencia (string agencia, string? token, string? emailAdmin, string? tokenAdmin)
        {
            IEnumerable<InfoEmail> infoEmail = await _infoEmailBusiness.GetAllInfoEmail(agencia,token, emailAdmin, tokenAdmin);

            return new ObjectResult(infoEmail)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> DeleteIfoEmail (int id, string agencia, string? token, string? emailAdmin, string? tokenAdmin)
        {
            int deveulto = await _infoEmailBusiness.DeleteInfoEmail(id, agencia, token, emailAdmin, tokenAdmin);
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
        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> UpdateInfoEmail (int id, ActualizarDatosEmail model)
        {
            UpdateInfoEmailRequest infoEmail = new UpdateInfoEmailRequest
            {
                emailOrigen=model.emailOrigen,
                host=model.host,
                nombreAgencia=model.nombreAgencia,
                emailNombre = model.nombreEmail,
                port=model.port,
                tokenAgencia=model.tokenAgencia,
                tokenEmail = model.emailToken
            };
            int deveulto = await _infoEmailBusiness.UpdateInfoEmail(id, infoEmail,model.emailAdmin,model.tokenAdmin);
            if (deveulto == 0)
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
        public async Task<IActionResult> SendEmail (EnviarEmail datos)
        {
            switch(datos.plantilla)
            {
                case "ProcesoAutorizacion":
                   
                    if(await _infoEmailBusiness.EnviarProcesoAutorizacion(datos) != 0)
                    {
                        return new ObjectResult("Se ha actualizado correctamente")
                        {
                            StatusCode = StatusCodes.Status200OK
                        };
                    }
                    else
                    {
                        return new ObjectResult("No se ha enviado correctamente")
                        {
                            StatusCode = StatusCodes.Status500InternalServerError
                        };
                        break;
                    }
                   
                    break;
                default:
                    return new ObjectResult("No existe dicha plantilla")
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                    break;
            }
        }

    }
}
