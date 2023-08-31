using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.InfoWhatsApp;

namespace Mensajeria_Windows.Business.Interfaces
{
    public interface IInfoWhatsAppBusiness
    {
        Task<int> CreateInfoWhatsApp (CreateInfoWhatsAppRequest model);
        Task<int> DeleteInfoWhatsApp (int id);
        Task<IEnumerable<InfoWhatsApp>> GetAllInfoWhatsApp ( );
        Task<int> UpdateInfoWhatsApp (int id, UpdateInfoWhatsAppRequest model);
        Task<InfoWhatsApp> GetInfoWhatsAppById (int id);

    }
}
