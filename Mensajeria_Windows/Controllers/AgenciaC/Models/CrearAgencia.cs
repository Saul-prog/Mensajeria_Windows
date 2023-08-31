using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mensajeria_Windows.Controllers.AgenciaC.Models
{
    public class CrearAgencia
    {
        [Required]
        public string nombreAgencia { get; set; }
        [Required]
        public bool puedeEmail { get; set; }
        [Required]
        public bool puedeTeams { get; set; }
        [Required]
        public bool puedeSMS { get; set; }
        [Required]
        public bool puedeWhatsApp { get; set; }
        public Dictionary<string, string> ExtraData { get; set; }
    }
}
