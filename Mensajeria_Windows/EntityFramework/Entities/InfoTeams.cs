namespace Mensajeria_Windows.EntityFramework.Entities
{
    public class InfoTeams : Base
    {        
        public string webHook { get; set; }
        public int agenciaId { get; set; }
        public Agencia agencia { get; set; }
    }
}
