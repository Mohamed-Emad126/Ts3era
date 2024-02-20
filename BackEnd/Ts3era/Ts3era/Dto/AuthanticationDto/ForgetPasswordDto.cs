using System.ComponentModel.DataAnnotations;

namespace Ts3era.Dto.AuthanticationDto
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage ="!يجب ادخال الايميل ")]
        [EmailAddress(ErrorMessage = "!البريد الإلكتروني ليس عنوان بريد إلكتروني صالحًا")]
        public string Email { get; set; }   
    }
}
