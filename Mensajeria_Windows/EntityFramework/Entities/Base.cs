namespace Mensajeria_Windows.EntityFramework.Entities
{
    public class Base
    {
        public int id { get; set; }
        public DateTime created { get; set; } 
        public DateTime update { get; set; } = DateTime.Now;
    }
}
