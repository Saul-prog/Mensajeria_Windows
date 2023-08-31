using AutoMapper;
using Mensajeria_Windows.Services;
using Mensajeria_Windows.EntityFramework.Entities;
using Mensajeria_Windows.EntityFramework.Models.InfoTeam;
using Mensajeria_Windows.EntityFramework.Models.InfoWhatsApp;
using Mensajeria_Windows.EntityFramework.Models.Agencias;
using Mensajeria_Windows.EntityFramework.Models.InfoSMS;
using Mensajeria_Windows.EntityFramework.Models.InfoEmail;
using Mensajeria_Windows.EntityFramework.Models.Plantillas;

namespace Mensajeria_Windows.EntityFramework.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateAgenciaRequest, Agencia>();
            CreateMap<UpdateAgenciaRequest, Agencia>()
               .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap<CreateInfoTeamsRequest, InfoTeams>();
            CreateMap<UpdateInfoTeamsRequest, InfoTeams>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap<CreateInfoWhatsAppRequest, InfoWhatsApp>();
            CreateMap<UpdateInfoWhatsAppRequest, InfoWhatsApp>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap<CreateInfoSMSRequest, InfoSMS>();
            CreateMap<UpdateInfoSMSRequest, InfoSMS>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap<CreateInfoEmailRequest, InfoEmail>();
            CreateMap<UpdateInfoEmailRequest, InfoEmail>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap<CreatePlantillaRequest, Plantillas>();
            CreateMap<UpdatePlantillaRequest, Plantillas>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));

        }
    }
}
