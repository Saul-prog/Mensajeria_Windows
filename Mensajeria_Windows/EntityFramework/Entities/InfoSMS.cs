namespace Mensajeria_Windows.EntityFramework.Entities
{
    public class InfoSMS : Base
    {
        public string numeroOriginal { get; set; }
        public string region { get; set; }
        public string tipoMensaje { get; set; }
        public string keyWord { get; set; }
        public string idSender { get; set; }
        public string idApp { get; set; }
        public int agenciaId { get; set; }
        public Agencia agencia { get; set; }
    }
}
