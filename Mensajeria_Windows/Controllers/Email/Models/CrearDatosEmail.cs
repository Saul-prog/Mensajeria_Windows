using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.Controllers.Email.Models
{
    public class CrearDatosEmail
    {

        [Required]
        public string nombreAgencia { get; set; }       
        public string? tokenAgencia { get; set; }
        [Required]
        public string host { get; set; }
        [Required]
        public int port { get; set; }
        [Required]
        public string emailOrigen { get; set; }
        [Required]
        public string emailToken { get; set; }
        [Required]
        public string nombreEmail { get; set; }
        public string? emailAdmin { get; set; }
        public string? tokenAdmin { get; set; }
    }
}
