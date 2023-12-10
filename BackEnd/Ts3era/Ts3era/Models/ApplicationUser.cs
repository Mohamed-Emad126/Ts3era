using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ts3era.Models
{
    public class ApplicationUser:IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set;}
        [Required]
        [MaxLength(14)]
        public string  National_Id { get; set;}
    }
}
