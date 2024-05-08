using System.ComponentModel.DataAnnotations;

namespace Ts3era.Dto.AuthanticationDto
{
    public class AddAdminDto
    {



        [Required(ErrorMessage = "!يجب ادخال الاسم الاول")]

        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "!يجب ادخال اسم العائله")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "يجب ادخال اسم المستخدم")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "!يجب ادخال البريد الالكتلروني")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "!يجب ادخال الرقم القومي")]
        //[MaxLength(14, ErrorMessage = "يجب ان لا يزيد عن ١٤ رقم ")]
        [MaxLength(14, ErrorMessage = "يجب ان لا يزيد عن ١٤ رقم"),
        RegularExpression("^[2,3]{1}[0-9]{13}$",
         ErrorMessage = "خطا في الرقم القومي")]
        public string National_Id { get; set; }
        [Required(ErrorMessage = "!يجب ادخال كلمه المرور")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "!كلمه المرور غير متطابقه")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "!يجب ادخال رقم التليفون")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "يرجي ادخال  رقم التليفون بشكل صحيح")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;


    }
}
