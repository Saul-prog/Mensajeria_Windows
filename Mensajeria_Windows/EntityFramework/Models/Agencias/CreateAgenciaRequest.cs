using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.EntityFramework.Models.Agencias
{
    public class CreateAgenciaRequest
    {
        [Required]
        public string nombreAgencia { get; set; }
        [Required]
        public string token { get; set; }
        [Required]
        public bool puedeEmail { get; set; }
        [Required]
        public bool puedeTeams { get; set; }
        [Required]
        public bool puedeSMS { get; set; }
        [Required]
        public bool puedeWhatsApp { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
