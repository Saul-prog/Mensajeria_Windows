using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Mensajeria_Windows.Controllers.Teams.Models;
using System.Net.Http.Headers;
using Mensajeria_Windows.Services.Interfaces;

namespace Mensajeria_Windows.Services
{
    public class TeamsService : ITeamsService
    {
        
        public async Task<int> EnviarTeamsSimple (string webhookUrl, string contenido)
        {
            try
            {
                

                string adaptiveCardJson = JsonConvert.SerializeObject(contenido, Formatting.Indented);
;

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(adaptiveCardJson, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(webhookUrl, content);
                return 1;
            }catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
