using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.EntityFramework.Models.InfoEmail
{
    public class UpdateInfoEmailRequest
    {
        [Required]
        public string nombreAgencia { get; set; }
        [Required]
        public string tokenAgencia { get; set; }
        
        public string host { get; set; }
        
        public int port { get; set; }
        
        public string emailOrigen { get; set; }
        
        public string tokenEmail { get; set; }
        
        public string emailNombre { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
