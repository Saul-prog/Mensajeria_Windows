using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.Controllers.PlantillasC.Models
{
    public class ActualizarPlantilla
    {
        [Required]
        public string nombrePlantilla { get; set; }
        
        public string tipoExtension { get; set; }
        
        public string plantilla { get; set; }
        [Required]
        public string correoAmin { get; set; }
        [Required]
        public string tokenAdmin { get; set; }
    }
}
