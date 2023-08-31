using AutoMapper;
using Mensajeria_Windows.Business.Interfaces;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.InfoWhatsApp;
using Mensajeria_Windows.Services.Interfaces;

namespace Mensajeria_Windows.Business
{
    public class InfoWhatsAppBusiness : IInfoWhatsAppBusiness
    {
        private IInfoWhatsAppService _infoWhatsAppService;
        private IMapper _mapper;
        public InfoWhatsAppBusiness (IInfoWhatsAppService infoWhatsAppService, IMapper mapper)
        {
            _infoWhatsAppService = infoWhatsAppService;
            _mapper = mapper;            
        }


        public async  Task<int> CreateInfoWhatsApp (CreateInfoWhatsAppRequest model)
        {
            return await _infoWhatsAppService.CreateInfoWhatsApp(model);
        }
        public async Task<int> DeleteInfoWhatsApp (int id)
        {
            return await _infoWhatsAppService.DeleteInfoWhatsApp(id);
        }
        public async  Task<IEnumerable<InfoWhatsApp>> GetAllInfoWhatsApp ( )
        {
            return await _infoWhatsAppService.GetAllInfoWhatsApp();
        }
        public async Task<int> UpdateInfoWhatsApp (int id, UpdateInfoWhatsAppRequest model)
        {
            return await _infoWhatsAppService.UpdateInfoWhatsApp(id, model);
        }
        public async Task<InfoWhatsApp> GetInfoWhatsAppById (int id)
        {
           return await _infoWhatsAppService.GetInfoWhatsAppById(id);
        }
    }
}
