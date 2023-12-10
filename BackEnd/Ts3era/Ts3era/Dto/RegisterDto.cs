using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Ts3era.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "!يجب ادخال الاسم الاول")]
        [Display(Name = "الاسم الاول")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "!يجب ادخال اسم العائله")]
        [Display(Name = "اسم العائله")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "يجب ادخال اسم المستخدم")]
        public string UsreName { get; set; }=string.Empty;
        [Required(ErrorMessage ="!يجب ادخال البريد الالكتلروني")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "!يجب ادخال الرقم القومي")]
        [Display(Name = " الرقم القومي")]
        [MaxLength(14,ErrorMessage = "يجب ان لا يزيد عن ١٤ رقم ")]
        public string National_Id {  get; set; }
        [Required(ErrorMessage ="!يجب ادخال كلمه المرور")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password",ErrorMessage = "!كلمه المرور غير متطابقه")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required(ErrorMessage ="!يجب ادخال رقم التليفون")]
        [MaxLength(12)]
        public string PhoneNumber { get; set; } = string.Empty;


    }
}
