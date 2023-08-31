
using Microsoft.AspNetCore.Routing.Constraints;

namespace Mensajeria_Windows.Business.Models
{
    public class AutorizacionDatos
    {
        public string identificador { get; set; }
        public List<NombreCompleto> viajeroReserva { get; set; }
        public List<NombreCompleto> solicitante { get; set; }
        public List<Trayecto> trayectos { get; set; }
        public DateTime fechalimite { get; set; }
        public float importe { get; set; }
        public string motivo { get; set; }
        public string observaciones { get; set; }
        public string datosRedireccionamiento { get; set; }
    }
}
