namespace Mensajeria_Windows.EntityFramework.Entities
{
    public class InfoEmail : Base
    {
        public string host { get; set; }
        public int port { get; set; }
        public string emailOrigen { get; set; }
        public string emailTokenPassword { get; set; }
        public string emailNombre { get; set; }
        public int agenciaId { get; set; }
        public Agencia agencia { get; set; }
    }
}
