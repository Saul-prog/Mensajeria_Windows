using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mensajeria_Windows.EntityFramework.Data;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Helpers;
using Mensajeria_Windows.EntityFramework.Models.InfoEmail;
using Mensajeria_Windows.EntityFramework.Models.InfoTeam;
using Mensajeria_Windows.Services.Interfaces;
using System.Net.Mail;
using System.Net;
using Mensajeria_Windows.Controllers.Email.Models;

namespace Mensajeria_Windows.Services
{
    public class InfoEmailService : IInfoEmailService
    {
        private NotificationContext _dbCntext;
        private readonly IMapper _mapper;


        public InfoEmailService (NotificationContext dbCntext, IMapper mapper)
        {
            _dbCntext = dbCntext;
            _mapper = mapper;
        }

        public async Task<int> CreateInfoEmail (CreateInfoEmailRequest model, int id)
        {
            if (await _dbCntext.infoEmail.AnyAsync(x =>  x.emailOrigen == model.emailOrigen))
            {
                return 0;
            }
            InfoEmail infoEmail = _mapper.Map<InfoEmail>(model);
            infoEmail.agenciaId = id;
            infoEmail.created  = DateTime.Now;
            
            _dbCntext.infoEmail.Add(infoEmail);
            try
            {
                await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
                return infoEmail.id;
            }catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> DeleteInfoEmail (int id)
        {
            InfoEmail? infoEmail = await _getInfoEmailById(id);
            if (infoEmail != null)
            {
                _dbCntext?.infoEmail.Remove(infoEmail);
                return  await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
                
            }
            else
            {
                throw new RepositoryExceptions($"No existe InfoEmail de usuario con id {id}");
            }
        }

        public async Task<IEnumerable<InfoEmail>> GetAllInfoEmail ( )
        {
            return await _dbCntext.infoEmail.ToArrayAsync().ConfigureAwait(true);
        }

        public async Task<int> UpdateEmailTeams (int id, UpdateInfoEmailRequest model)
        {
            InfoEmail? infoEmail = await _getInfoEmailById(id);
            // Validation
            if (model.emailOrigen != infoEmail.emailOrigen && await _dbCntext.infoEmail.AnyAsync(x => x.emailOrigen == model.emailOrigen))
                throw new RepositoryExceptions($"{model.emailOrigen} ya existe.");

            _mapper.Map(model, infoEmail);
            _dbCntext.infoEmail.Update(infoEmail);
            return await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
        }
        private async Task<InfoEmail> _getInfoEmailById (int id)
        {
            InfoEmail? infoEmail = await _dbCntext.infoEmail
                .AsNoTracking()
                .Where(x => x.id == id)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            if (infoEmail == null)
            {
                throw new KeyNotFoundException("InfoEmail no se ha encontrado en la base de datos");
            }
            return infoEmail;
        }

        public async Task<InfoEmail> GetInfoEmailById (int id)
        {
            InfoEmail? infoEmail = await _dbCntext.infoEmail
                     .AsNoTracking()
                     .Where(x => x.id == id)
                     .FirstOrDefaultAsync().ConfigureAwait(true);

            if (infoEmail == null)
            {
                throw new KeyNotFoundException("infoEmail not found");
            }
            else
            {
                return infoEmail;
            }


        }
        public Task<IEnumerable<InfoEmail>> GetAllInfoEmailByAgencia (int id)
        {
            return _getAllInfoEmailByAgencia(id);
        }
        public async Task<IEnumerable<InfoEmail>> _getAllInfoEmailByAgencia (int id)
        {
            IEnumerable<InfoEmail>? infoEmail = await _dbCntext.infoEmail
                     .AsNoTracking()
                     .Where(x => x.agenciaId == id)
                     .ToArrayAsync().ConfigureAwait(true);

            if (infoEmail == null)
            {
                throw new KeyNotFoundException("infoEmail not found");
            }
            else
            {
                return infoEmail;
            }


        }
        public async Task<InfoEmail> GetInfoEmailByAgenciaIdAndTipo (int agenciaId, string emailOrigen)
        {
            InfoEmail? infoEmail = await _dbCntext.infoEmail
                .AsNoTracking()
                .Where(x => x.agenciaId == agenciaId && x.emailOrigen == emailOrigen)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            return infoEmail;
            
        }
        public async Task<int> EnviarEmailConPlantillaADestino (string plantilla, InfoEmail infoEmail, List<DatosEmail> emailsDestino, string titulo, List<DatosFichero>? ficheros)
        {
            var client = new SmtpClient();
            client.Host = infoEmail.host;
            client.Port = infoEmail.port;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(infoEmail.emailOrigen, infoEmail.emailTokenPassword);
            foreach (DatosEmail emailDestino in emailsDestino)
            {
                using (var message = new MailMessage(
                   from: new MailAddress(infoEmail.emailOrigen, infoEmail.emailNombre),
                   to: new MailAddress(emailDestino.email, emailDestino.name)
                   ))
                {
                    message.Subject = titulo;
                    message.Body = plantilla;
                    message.IsBodyHtml = true;
                    if (ficheros != null)
                    {
                        foreach (DatosFichero fichero in ficheros)
                        {
                            byte[] Bytes = Convert.FromBase64String(fichero.contenidoFichero);

                            message.Attachments.Add(CrearFicheroPorExtension(Bytes, fichero.nombreFichero, fichero.extension));
                        }
                    }                    
                    try
                    {
                        client.Send(message);
                    }catch (Exception ex)
                    {
                        return 0;
                    }
                    
                }
            }

            return 1;
        }
        private Attachment CrearFicheroPorExtension (byte[] Bytes, string nombre, string extension)
        {
            switch (extension)
            {
                case "pdf":
                    return new Attachment(new MemoryStream(Bytes), nombre, "application/pdf");
                case "xlsx":
                    return new Attachment(new MemoryStream(Bytes), nombre, "application/xlsx");
                case "xls":
                    return new Attachment(new MemoryStream(Bytes), nombre, "application/xls");
                case "zip":
                    return new Attachment(new MemoryStream(Bytes), nombre, "application/zip");
                default:
                    return null;
            }
        }
    }
}
