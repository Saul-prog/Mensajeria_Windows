using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.EntityFramework.Models.InfoWhatsApp
{
    public class CreateInfoWhatsAppRequest
    {
        [Required]
        public string token { get; set; }
        public string idtelefono { get; set; }
        public string telefono { get; set; }
        public string templateName { get; set; }
        public string idioma { get; set; }
        public DateTime Created { get; set; }
    }
}
