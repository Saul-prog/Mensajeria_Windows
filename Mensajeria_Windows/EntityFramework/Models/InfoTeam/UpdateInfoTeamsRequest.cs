using Mensajeria_Windows.EntityFramework.Entities;
using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.EntityFramework.Models.InfoTeam
{
    public class UpdateInfoTeamsRequest
    {
        [Required]
        public string nombreAgencia { get; set; }
        [Required]
        public string tokenAgencia { get; set; }
        [Required]
        public string webHook { get; set; }

        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
