namespace Ts3era.Models
{
    public class Governorates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        IEnumerable<Ports> ports { get; set;}
    }
}
