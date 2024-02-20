using System.ComponentModel.DataAnnotations;

namespace Ts3era.Dto.AuthanticationDto
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "!يجب ادخال الايميل ")]
        [EmailAddress(ErrorMessage = "!البريد الإلكتروني ليس عنوان بريد إلكتروني صالحًا")]
        public string Email { get; set; }
        public string Token { get; set; }
        [Required(ErrorMessage ="!يجب ادخال كلمه المور الجديده ")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage ="!يجب تاكيد كلمه المرور ")]
        [Compare(nameof(NewPassword),ErrorMessage ="!كلمه المرور غير متطابقه")]
        public string ConfirmPassword { get; set; }
        
    }
}
