namespace Mensajeria_Windows.EntityFramework.Entities
{
    public class InfoWhatsApp : Base
    {
        public string version { get; set; }
        public string numeroTelefonoId { get; set; }
        public string tokenAcceso { get; set; }
        public int agenciaId { get; set; }
        public Agencia agencia { get; set; }
    }
}
