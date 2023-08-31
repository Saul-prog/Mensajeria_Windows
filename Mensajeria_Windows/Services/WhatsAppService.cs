using System.Net.Http;
using System.Text;
using System;
using System.Threading.Tasks;

namespace Mensajeria_Windows.Services
{
    public class WhatsAppService
    {
        public async Task<int> EnviarTexto ( )
        {
            string version = "vX.X"; // Reemplaza con la versión correcta
            string phoneNumberId = "your-phone-number-id"; // Reemplaza con el ID correcto
            string recipientWaId = "recipient-wa-id"; // Reemplaza con el ID correcto
            string accessToken = "your-access-token"; // Reemplaza con tu token de acceso de Facebook Graph API

            string url = $"https://graph.facebook.com/{version}/{phoneNumberId}/messages?access_token={accessToken}";

            string json = @"
                {
                    ""messaging_product"": ""whatsapp"",
                    ""to"": """ + recipientWaId + @""",
                    ""type"": ""template"",
                    ""template"": {
                        ""name"": ""hello_world"",
                        ""language"": {
                            ""code"": ""en_US""
                        }
                    }
                }";

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Mensaje enviado exitosamente.");
                    return 1;
                }
                else
                {
                    Console.WriteLine($"Error al enviar el mensaje. Código de estado: {response.StatusCode}");
                    return 0;
                }
            }
        }
    }
}
