using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.EntityFramework.Models.InfoWhatsApp
{
    public class UpdateInfoWhatsAppRequest
    {
        [Required]
        public string token { get; set; }
        public string idtelefono { get; set; }
        public string telefono { get; set; }
        public string templateName { get; set; }
        public string idioma { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
        public int usuarioId { get; set; }
    }
}
