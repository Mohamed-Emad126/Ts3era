using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ts3era.Models
{
    public class ApplicationUser:IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set;}
        [Required]
    
        [MaxLength(14),
        RegularExpression("^[2,3]{1}[0-9]{13}$")]
        public string  National_Id { get; set;}

        public  List<RefreshToken> ?RefreshTokens { get; set; }
    }
}
