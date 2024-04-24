using System.ComponentModel.DataAnnotations.Schema;

namespace Ts3era.Models
{
    public class Ports
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(PortTypes))]
        public int PortType_Id { get; set; }    
        public PortTypes PortTypes { get; set; }
        [ForeignKey(nameof(Governorates))]
        public int Governorate_Id { get; set; }
        public Governorates Governorates { get; set; }

    }
}
