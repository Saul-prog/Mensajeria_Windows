using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.Controllers.PlantillasC.Models
{
    public class CrearPlantilla
    {
        [Required]
        public string nombrePlantilla { get; set; }
        [Required]
        public string tipoExtension { get; set; }
        [Required]
        public string plantilla { get; set; }
        [Required]
        public string correoAmin { get; set; }
        [Required]
        public string tokenAdmin { get; set; }
    }
}
