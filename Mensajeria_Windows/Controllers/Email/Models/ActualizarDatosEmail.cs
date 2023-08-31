using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.Controllers.Email.Models
{
    public class ActualizarDatosEmail
    {
        [Required]
        public string nombreAgencia { get; set; }
        [Required]
        public string tokenAgencia { get; set; }
        
        public string host { get; set; }
       
        public int port { get; set; }
       
        public string emailOrigen { get; set; }
        
        public string emailToken { get; set; }
        
        public string nombreEmail { get; set; }
        public string emailAdmin { get; set; }
        public string tokenAdmin { get; set; }
    }
}
