using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Mensajeria_Windows.Controllers.Teams.Models;
using System.Net.Http.Headers;

namespace Mensajeria_Windows.Controllers.Teams
{

    /// <summary>
    /// Controller class for sending card using incoming webhook.
    /// </summary>
    [Route("{version:apiVersion}/")]
    [ApiController]
    public class CardController : ControllerBase
    {
        public static string url = "";

        /// <summary>
        /// Method to send card to team using incoming webhook.
        /// </summary>
        /// <returns></returns>
        [HttpPost("Send")]
        public async Task SendCardAsync(CardEntity cardEntity)
        {
            try
            {
                var adaptiveCard = new JObject(
                    new JProperty("type", "message"),
                    new JProperty("attachments", new JArray(
                        new JObject(
                            new JProperty("contentType", "application/vnd.microsoft.card.adaptive"),
                            new JProperty("content", new JObject(
                                new JProperty("type", "AdaptiveCard"),
                                new JProperty("body", new JArray(
                                    new JObject(
                                        new JProperty("type", "TextBlock"),
                                        new JProperty("text", cardEntity.CardBody)
                                    )
                                )),
                                new JProperty("$schema", "http://adaptivecards.io/schemas/adaptive-card.json"),
                                new JProperty("version", "1.0")
                            ))
                        )
                    ))
                );

                string adaptiveCardJson = JsonConvert.SerializeObject(adaptiveCard, Formatting.Indented);

                var webhookUrl = cardEntity.WebhookUrl; 

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(adaptiveCardJson, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(webhookUrl, content);
           
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to save and send user response.
        /// </summary>
        /// <returns></returns>
        [HttpPost("Save")]
        public async Task SendResponse([FromBody] ResponseEntity entity)
        {
            try
            {
                string cardJson = @"{
            ""@type"": ""MessageCard"",
            ""summary"": ""Response Message"",
            ""sections"": [{ 
            ""activityTitle"": ""Welcome Message"",
            ""text"": ""Submitted response: " + entity.Comment + @"""}]}";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(cardJson, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
