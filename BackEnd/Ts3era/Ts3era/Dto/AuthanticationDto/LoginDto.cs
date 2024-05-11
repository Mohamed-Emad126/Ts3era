using System.ComponentModel.DataAnnotations;

namespace Ts3era.Dto.AuthanticationDto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "!يجب ادخال البريد الالكتلروني")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$",
            ErrorMessage = "(user@example.com)يجب أن يتطابق البريد الإلكتروني مع")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "!يجب ادخال كلمه المرور")]
        public string Password { get; set; } = string.Empty;
    }
}
