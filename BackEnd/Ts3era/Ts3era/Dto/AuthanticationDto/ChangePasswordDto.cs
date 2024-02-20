using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ts3era.Dto.AuthanticationDto
{
    public class ChangePasswordDto
    {
        
        [Required(ErrorMessage ="يجب ادخال اسم المستخدم")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="يجب ادخال كلمه المرور الحاليه")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage ="يجب ادخال كلمه المرور الجديده")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "يجب تاكيد كلمه المرور الجديده")]
        [Compare(nameof(NewPassword),ErrorMessage ="كلمه المرور غير متطابقه ")]
        public string ConfirmPassword { get; set;}




    }
}
