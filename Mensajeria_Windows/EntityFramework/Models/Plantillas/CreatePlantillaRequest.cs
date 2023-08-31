using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.EntityFramework.Models.Plantillas
{
    public class CreatePlantillaRequest
    {
        [Required]
        public string nombrePlantilla { get; set; }
        [Required]
        public string tipoExtension { get; set; }
        [Required]
        public string plantilla { get; set; }
       
    }
}
