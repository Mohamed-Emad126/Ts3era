using System.ComponentModel.DataAnnotations;

namespace Ts3era.Dto.AuthanticationDto
{
    public class AddAdminDto
    {
        [Required(ErrorMessage = "يجب ادخال اسم المستخدم")]
        public string UsreName { get; set; } = string.Empty;
        [Required(ErrorMessage = "!يجب ادخال البريد الالكتلروني")]
        public string Email { get; set; }
        [Required(ErrorMessage = "!يجب ادخال كلمه المرور")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "!كلمه المرور غير متطابقه")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
