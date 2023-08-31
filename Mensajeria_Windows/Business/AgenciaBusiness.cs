using AutoMapper;
using Mensajeria_Windows.Business.Interfaces;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.Agencias;
using Mensajeria_Windows.Services.Interfaces;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Mensajeria_Windows.Business
{
    public class AgenciaBusiness : IAgenciaBusiness
    {

        private IAgenciaService _agenciaService;
        private IMapper _mapper;
        private IAdministradoresService _administradoresService;

        public AgenciaBusiness (IAgenciaService agenciaService, IMapper mapper, IAdministradoresService administradoresService)
        {
            _agenciaService = agenciaService;
            _mapper = mapper;
            _administradoresService = administradoresService;   
        }
        public async Task<int> CreateAgencia (CreateAgenciaRequest model, string email, string tokenAdmin)
        {
            
            if (!await _administradoresService.IsAdministrador(email, tokenAdmin))
            {
                return 0;
            }
            string token =tokenAdmin+DateTime.Now.ToString();
            model.token = ComputeSHA3Hash(token);
            return await _agenciaService.CreateAgencia (model);
        }

        public async Task<int> DeleteAgencia (string name, string adminMail, string adminToken)
        {
            if (!await _administradoresService.IsAdministrador(adminMail, adminToken))
            {
                return 0;
            }
            return await _agenciaService.DeleteAgencia(name);
        }

        public async Task<int> UpdateAgencia (UpdateAgenciaRequest model,string adminMail, string adminToken)
        {
            
            if (!await _administradoresService.IsAdministrador(adminMail, adminToken))
            {
                return 0;
            }
            return  await _agenciaService.UpdateAgencia(model);
        }

        public async Task<Agencia> GetAgenciaByName(string  name, string adminMail, string adminToken)
        {
            if (!await _administradoresService.IsAdministrador(adminMail, adminToken))
            {
                return null;
            }
            return await _agenciaService.GetAgenciaByName(name);
        }

        public async Task<IEnumerable<Agencia>> GetAllAgencias (string adminMail, string adminToken)
        {
            if (!await _administradoresService.IsAdministrador(adminMail, adminToken))
            {
                return null;
            }
            return await _agenciaService.GetAllAgencia();
        }

        private static string ComputeSHA3Hash (string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                return  BitConverter.ToString(sha512.ComputeHash(inputBytes)).Replace("-", "").ToLower();
            }
        }
    }
}
