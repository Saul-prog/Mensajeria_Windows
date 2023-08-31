using Mensajeria_Windows.Business.Models;

namespace Mensajeria_Windows.Controllers.Email.Models
{
    public class EnviarEmail
    {
        public string nombreAgencia { get; set; } 
        public string? tokenAgencia { get; set; }
        public List<DatosEmail> emailsDestino { get; set; }
        public string plantilla { get; set; }
        public AutorizacionDatos? autorizacionDatos { get; set; }
        public string emailOrigen { get; set; }
        public string asunto { get; set; }
        public  List<DatosFichero>? ficheros { get; set; }
        public string? emailAdmin { get; set; }
        public string? tokenAdmin { get; set; }
    }
}
