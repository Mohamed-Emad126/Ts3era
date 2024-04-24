namespace Ts3era.Models
{
    public class PortTypes
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        IEnumerable<Ports> ports { get; set; } 

    }
}
