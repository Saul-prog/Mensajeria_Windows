using AutoMapper;
using Microsoft.OData.UriParser;
using Mensajeria_Windows.Business.Interfaces;
using Mensajeria_Windows.Business.Models;
using Mensajeria_Windows.Controllers.Email.Models;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.InfoEmail;
using Mensajeria_Windows.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Mensajeria_Windows.Business
{
    public class EmailBusiness :IEmailBusiness
    {
        private IInfoEmailService _infoEmailService;
        private IAgenciaService _genciaService;
        private IAdministradoresService _administradoresService;
        private IPlantillaService _plantillaService;
        public  EmailBusiness (IInfoEmailService infoEmailService, IAgenciaService agenciaService,
            IAdministradoresService administradoresService, IPlantillaService plantillaService)        
        {
            _infoEmailService = infoEmailService;            
            _genciaService = agenciaService;
            _administradoresService = administradoresService;
            _plantillaService = plantillaService;   
        }

        public async Task<int> CreateInfoEmail (CreateInfoEmailRequest model, string adminEmail, string adminToken)
        {
            int agenciaId=0;
            bool esAdmin = await _administradoresService.IsAdministrador(adminEmail, adminToken);
           if(esAdmin)
            {
                Agencia agencia = await _genciaService.GetAgenciaByName(model.nombreAgencia);
                agencia.id = agenciaId;
            }
            else
            {
                 agenciaId = await _genciaService.GetAgenciaIdByNameAndToken(model.nombreAgencia, model.tokenAgencia);
            }
            
            if (agenciaId != 0) {
                return await _infoEmailService.CreateInfoEmail(model, agenciaId);
            }
            return 0;
        }
        public async Task<IEnumerable<InfoEmail>> GetAllInfoEmail (string agenciaName, string? token, string? emailAdmin, string? tokenAdmin)
        {
            int agenciaId = 0;
            bool esAdmin = await _administradoresService.IsAdministrador(emailAdmin, tokenAdmin);
            if (esAdmin)
            {
                Agencia agencia = await _genciaService.GetAgenciaByName(agenciaName);
                agencia.id = agenciaId;
            }
            else
            {
                agenciaId = await _genciaService.GetAgenciaIdByNameAndToken(agenciaName, token);
            }
            if ( agenciaId != 0)
            {
                return await _infoEmailService.GetAllInfoEmailByAgencia(agenciaId);
            }
            return new List<InfoEmail>();
        }
        public async Task<InfoEmail> GetInfoEmailById (int id)
        {
            return await _infoEmailService.GetInfoEmailById(id);
        }
        public async Task<int> UpdateInfoEmail (int id, UpdateInfoEmailRequest model, string emailAdmin, string tokeAdmin)
        {
            if( await _administradoresService.IsAdministrador(emailAdmin, tokeAdmin) ||
                await _genciaService.GetAgenciaIdByNameAndToken(model.nombreAgencia, model.tokenAgencia)!=0){
                return await _infoEmailService.UpdateEmailTeams(id, model);
            }
            return 0;
        }
        public async Task<int> DeleteInfoEmail (int id, string agenciaName, string? token, string? emailAdmin, string? tokenAdmin)
        {
            int agenciaId = 0;
            bool esAdmin = await _administradoresService.IsAdministrador(emailAdmin, tokenAdmin);
            if (esAdmin)
            {
                Agencia agencia = await _genciaService.GetAgenciaByName(agenciaName);
                agencia.id = agenciaId;
            }
            else
            {
                agenciaId = await _genciaService.GetAgenciaIdByNameAndToken(agenciaName, token);
            }
            if (agenciaId != 0)
            {
                IEnumerable<InfoEmail> infoEmails = await _infoEmailService.GetAllInfoEmailByAgencia(agenciaId);
                if (infoEmails.Any(infoEmail => infoEmail.id == id))
                return await _infoEmailService.DeleteInfoEmail(id);
            }
            
            return 0;
        }

        public async Task<int> EnviarProcesoAutorizacion(EnviarEmail datos)
        {
            int agenciaId = 0;
            bool esAdmin = await _administradoresService.IsAdministrador(datos.emailAdmin, datos.tokenAdmin);
            if (esAdmin)
            {
                Agencia agencia = await _genciaService.GetAgenciaByName(datos.nombreAgencia);
                agencia.id = agenciaId;
            }
            else
            {
                agenciaId = await _genciaService.GetAgenciaIdByNameAndToken(datos.nombreAgencia, datos.tokenAgencia);
            }
            if (agenciaId != 0)
            {
                InfoEmail infoEmail = await _infoEmailService.GetInfoEmailByAgenciaIdAndTipo(agenciaId,datos.emailOrigen);
                if (infoEmail == null)  return 0; 
                string plantilla = await _plantillaService.GetContenidoPlantillaByNameAndExtension(datos.plantilla, "HTML");
                if(plantilla == null) return 0;
                string plantillaRellena = await RellenarPlantilla( plantilla,  datos.autorizacionDatos);
                int devuelto = await _infoEmailService.EnviarEmailConPlantillaADestino(plantillaRellena, infoEmail, datos.emailsDestino, datos.asunto, datos.ficheros);
                return devuelto;
            }
            return 0;
        }
        private async Task<string> RellenarPlantilla(string plantilla, AutorizacionDatos datos)
        {
            plantilla = plantilla.Replace("{identificador}", datos.identificador);
            plantilla = plantilla.Replace("{viajeReserva}", PintarNombreCompleto(datos.viajeroReserva));
            plantilla = plantilla.Replace("{solicitante}", PintarNombreCompleto(datos.solicitante));
            plantilla = plantilla.Replace("{trayectos}", PintarTrayecto(datos.trayectos));
            plantilla = plantilla.Replace("{fechaLimite}",datos.fechalimite.ToString());
            plantilla = plantilla.Replace("{importe}", datos.importe.ToString());
            plantilla = plantilla.Replace("{observaciones}", datos.observaciones);
            plantilla = plantilla.Replace("{datosRedireccionamiento}", datos.datosRedireccionamiento);
            string plantillaFooter =  await _plantillaService.GetContenidoPlantillaByNameAndExtension("Footer", "HTML");
            string plantillaFinal = plantilla + plantillaFooter;
            return plantillaFinal;

        }

        private static bool IsNullOrEmptyOrAllPropertiesNullAutorizacionDatos (AutorizacionDatos datos)
        {
            if (datos == null)
            {
                return true;
            }

            return string.IsNullOrWhiteSpace(datos.identificador) &&
                   (datos.solicitante == null || !datos.solicitante.Any()) &&
                   (datos.trayectos == null || !datos.trayectos.Any()) &&
                   datos.fechalimite == default(DateTime) &&
                   datos.importe == 0 &&
                   string.IsNullOrWhiteSpace(datos.motivo) &&
                   string.IsNullOrWhiteSpace(datos.observaciones) &&
                   string.IsNullOrWhiteSpace(datos.datosRedireccionamiento);
        }

        private string PintarNombreCompleto (List<NombreCompleto> nombres)
        {
            StringBuilder sb = new StringBuilder();
            foreach (NombreCompleto nombre in nombres)
            {
                sb.Append($"<li><span>Nombre Completo:</span><span>{nombre.nombre}</span><span>{nombre.apellido1}</span><span>{nombre.apellido2}</span></li>");
            }
            return sb.ToString();
        }
        private string PintarTrayecto (List<Trayecto> trayectos)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Trayecto trayecto in trayectos)
            {

                sb.Append($"<ul>" +
                    $"<li>Fecha de Salida: <span>{trayecto.fechaSalida}</span></li>" +
                    $"<li>Origen: <span>{trayecto.origen}</span></li>" +
                    $"<li>Fecha de Llegada: <span>{trayecto.fechaLlegada}</span></li>" +
                    $"<li>Destino: <span>{trayecto.destino}</span></li>" +
                    $"</ul>");
            }
            return sb.ToString();
        }
    }

}
