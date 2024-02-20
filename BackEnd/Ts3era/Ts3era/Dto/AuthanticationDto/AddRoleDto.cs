using System.ComponentModel.DataAnnotations;

namespace Ts3era.Dto.AuthanticationDto
{
    public class AddRoleDto
    {
        [Required(ErrorMessage = "!يجب ادخال الاسم")]
        public string RoleName { get; set; }
    }
}
