using System.ComponentModel.DataAnnotations;

namespace Ts3era.Dto.PortType_Dto
{
    public class PortType_Dto
    {
        [Display(Name ="TypeId")]
        public int Type_Id { get; set; }
        [Display(Name ="TypeName")]
        public string PortType_Name { get; set; }
    }
}
