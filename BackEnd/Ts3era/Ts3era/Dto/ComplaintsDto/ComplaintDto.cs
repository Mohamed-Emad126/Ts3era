using System.ComponentModel.DataAnnotations;

namespace Ts3era.Dto.ComplaintsDto
{
    public class ComplaintDto
    {
        [Required(ErrorMessage ="!يجب ادخال الاسم ")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "!يجب ادخال البريد الالكتروني ")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "!يجب ادخال عنوان الشكوي")]
        public string ComplaintAddress { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage ="!يجب ادخال رقم التليفون")]
           //[RegularExpression(@"^(\\+201|01|00201)[0-2,5]{1}[0-9]{8}")]
           [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"
                 ,ErrorMessage = "! تنسيق الهاتف الذي تم إدخاله غير صالح ")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "!يجب ادخال الرقم القومي")]
        [MaxLength(14, ErrorMessage = "يجب ان لا يزيد عن ١٤ رقم"),
        RegularExpression("^[2,3]{1}[0-9]{13}$",
         ErrorMessage = "خطا في الرقم القومي")]
        public string National_Id { get; set; }
        [Required(ErrorMessage ="!يجب ادخال تفاصيل الشكوي")]
        [StringLength(150,ErrorMessage = " !لقد تعديت الحد الاقصى لكتابه الشكوى")]
        public string complaintDetails { get; set; }

        public IFormFile? AddAtachment {  get; set; }
       
    }
}
