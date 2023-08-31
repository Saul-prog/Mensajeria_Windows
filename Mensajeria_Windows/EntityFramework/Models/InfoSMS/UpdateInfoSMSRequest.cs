using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.EntityFramework.Models.InfoSMS
{
    public class UpdateInfoSMSRequest
    {
        [Required]
        public string nombreAgencia { get; set; }
        [Required]
        public string tokenAgencia { get; set; }
       
        public string numeroOrigen { get; set; }
       
        public string region { get; set; }
       
        public string keyWord { get; set; }
       
        public string idOrigen { get; set; }
        
        public string idApp { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
