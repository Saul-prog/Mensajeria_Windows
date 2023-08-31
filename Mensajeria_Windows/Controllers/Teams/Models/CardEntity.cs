using Microsoft.Azure.Cosmos.Table;
namespace Mensajeria_Windows.Controllers.Teams.Models
{
    public class CardEntity : TableEntity
    {
        public string WebhookUrl { get; set; }

        public string CardBody { get; set; }
    }
}
