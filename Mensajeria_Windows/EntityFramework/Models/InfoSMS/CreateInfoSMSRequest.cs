using System.ComponentModel.DataAnnotations;

namespace Mensajeria_Windows.EntityFramework.Models.InfoSMS
{
    public class CreateInfoSMSRequest
    {
        [Required]
        public string nombreAgencia { get; set; }
        [Required]
        public string tokenAgencia { get; set; }
        [Required]
        public string numeroOrigen { get; set; }
        [Required]
        public string region { get; set; }
        [Required]
        public string keyWord { get; set; }
        [Required]
        public string idOrigen { get; set; }
        [Required]
        public string idApp { get; set; }
        public DateTime Created { get; set; }

    }
}
