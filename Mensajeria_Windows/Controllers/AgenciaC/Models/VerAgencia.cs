

using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.Controllers.AgenciaC.Models
{
    public class VerAgencia
    {
        [Required]
        public string nombreAgencia { get; set; }
        public Dictionary<string, string> ExtraData { get; set; }
    }
}
