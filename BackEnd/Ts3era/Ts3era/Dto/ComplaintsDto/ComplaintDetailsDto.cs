using System.ComponentModel.DataAnnotations;

namespace Ts3era.Dto.ComplaintsDto
{
    public class ComplaintDetailsDto
    {
        
        public string UserName { get; set; }
    
        public string Email { get; set; }
        
        public string ComplaintAddress { get; set; }
       
        public string PhoneNumber { get; set; }
      
        public string National_Id { get; set; }
        public string complaintDetails { get; set; }

        public string  Attachment { get; set; }

    }
}
