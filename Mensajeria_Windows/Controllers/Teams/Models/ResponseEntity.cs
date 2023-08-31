using Microsoft.Azure.Cosmos.Table;

namespace Mensajeria_Windows.Controllers.Teams.Models
{
    
    /// <summary>
    /// Class for Notes related properties.
    /// </summary>
    public class ResponseEntity : TableEntity
    {
        public string Comment { get; set; }
    }
    
}
