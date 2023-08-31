using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.Controllers.AgenciaC.Models
{
    public class ActualizarAgencia
    {
        [Required]
        public string nombreAgencia { get; set; }
        public string tokenAgencia { get; set; }
        
        public bool puedeEmail { get; set; }
        
        public bool puedeTeams { get; set; }
        
        public bool puedeSMS { get; set; }
       
        public bool puedeWhatsApp { get; set; }
        public Dictionary<string, string> ExtraData { get; set; }
    }
}
