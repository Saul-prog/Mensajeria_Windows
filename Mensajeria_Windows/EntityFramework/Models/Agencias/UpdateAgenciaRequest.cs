namespace Mensajeria_Windows.EntityFramework.Models.Agencias
{
    public class UpdateAgenciaRequest
    {
        public string nombreAgencia { get; set; }
        public string token { get; set; }
        public bool puedeEmail { get; set; }
        public bool puedeTeams { get; set; }
        public bool puedeSMS { get; set; }
        public bool puedeWhatsApp { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
