namespace Mensajeria_Windows.EntityFramework.Entities
{
    public class Agencia : Base
    {
            public string nombreAgencia { get; set; }
            public string token {get;set;}
            public bool puedeEmail {get; set;}
            public bool puedeTeams { get; set;}
            public bool puedeSMS { get; set;}
            public bool puedeWhatsApp { get; set;}
            public IEnumerable<InfoTeams>? InfoTeams { get; set; }
            public IEnumerable<InfoWhatsApp>? InfoWhatsApps { get; set; }
            public IEnumerable<InfoEmail>? InfoEmail { get; set; }
            public IEnumerable<InfoSMS>? InfoSMS { get; set; }
    }
}