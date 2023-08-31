using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mensajeria_Windows.Business.Interfaces;
using Mensajeria_Windows.Controllers.AgenciaC.Models;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.Agencias;

namespace Mensajeria_Windows.Controllers.AgenciaC
{
    [ApiController]
    [Route("{version:apiVersion}/[controller]")]

    [Consumes("application/json")]
    [Produces("application/json")]
    public class AgenciaController
    {
        private IMapper _mapper;
        private IAgenciaBusiness _agenciaBusiness;
        public AgenciaController (IMapper mapper, IAgenciaBusiness agenciaBusiness)
        {
            _mapper = mapper;
            _agenciaBusiness = agenciaBusiness;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateAgencia (CrearAgencia model)
        {


            string email = model.ExtraData["email"];
            string tokenAdmin = model.ExtraData["token"];


            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email))
            {
                return new ObjectResult("Agencia no se ha creado")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            CreateAgenciaRequest agencia = new CreateAgenciaRequest
            {
                nombreAgencia = model.nombreAgencia,
                puedeEmail = model.puedeEmail,
                puedeSMS = model.puedeSMS,
                puedeTeams = model.puedeTeams,
                puedeWhatsApp = model.puedeWhatsApp,
                Created = DateTime.Now,
                token = null
            };

            int agenciaid = await _agenciaBusiness.CreateAgencia(agencia, email, tokenAdmin);

            if (agenciaid != 0)
            {
                return new ObjectResult(agenciaid)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new ObjectResult("Agencia no se ha creado")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> ActualizarAgencia (ActualizarAgencia model)
        {
            string? email = model.ExtraData["email"];
            string tokenAdmin = model.ExtraData["token"];
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email))
            {
                return new ObjectResult("Agencia no se ha creado")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            UpdateAgenciaRequest agencia = new UpdateAgenciaRequest
            {
                nombreAgencia = model.nombreAgencia,
                puedeEmail = model.puedeEmail,
                puedeSMS = model.puedeSMS,
                puedeTeams = model.puedeTeams,
                puedeWhatsApp = model.puedeWhatsApp,
                Updated = DateTime.Now,
                token = model.tokenAgencia
            };
            int agenciaid = await _agenciaBusiness.UpdateAgencia(agencia, email, tokenAdmin);

            if (agenciaid != 0)
            {
                return new ObjectResult(agenciaid)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new ObjectResult("Agencia no se ha creado")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        [HttpPost("Ver")]
        public async Task<IActionResult> VerAgencias (VerAgencia model)
        {
            string? email = model.ExtraData["email"];
            string? tokenAdmin = model.ExtraData["token"];
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email))
            {
                return new ObjectResult("Agencia no se ha creado")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            if (string.IsNullOrEmpty(model.nombreAgencia))
            {
                IEnumerable<Agencia> agencia = await _agenciaBusiness.GetAllAgencias(email, tokenAdmin);
                if (agencia != null)
                {
                    return new ObjectResult(agencia)
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new ObjectResult("Agencia no se ha encontrado")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            else
            {
                Agencia agencia =  await _agenciaBusiness.GetAgenciaByName(model.nombreAgencia,email, tokenAdmin);
                if (agencia != null)
                {
                    return new ObjectResult(agencia)
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new ObjectResult("Agencia no se ha encontrado")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            
        }
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> EliminarAgencia(EliminarAgencia model)
        {
            string email = model.ExtraData["email"];
            string tokenAdmin = model.ExtraData["token"];
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email))
            {
                return new ObjectResult("Agencia no se ha creado")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            int deveulto = await _agenciaBusiness.DeleteAgencia(model.nombreAgencia,  email,  tokenAdmin);
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
