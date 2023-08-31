using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.EntityFramework.Models.Plantillas
{
    public class UpdatePlantillaRequest
    {
        [Required]
        public string nombrePlantilla { get; set; }
        
        public string tipoExtension { get; set; }
        
        public string plantilla { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
