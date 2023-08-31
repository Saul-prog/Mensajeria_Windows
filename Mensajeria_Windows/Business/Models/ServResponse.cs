namespace Mensajeria_Windows.Business.Models
{
    public class ServResponse
    {
        /// <summary>
        /// Estado devuelto de la peticion
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Error, si procede, que genera la petición
        /// </summary>
        public string error { get; set; }
    }
}
