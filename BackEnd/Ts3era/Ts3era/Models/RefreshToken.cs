using Microsoft.EntityFrameworkCore;

namespace Ts3era.Models
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; }   
        public DateTime ExpireOn { get; set; }
        public bool IsExpire =>DateTime.UtcNow>=ExpireOn;

        public DateTime CreatedOn { get; set; }
        public DateTime? RenokedOn { get; set; }
        public bool IsActive => RenokedOn == null && !IsExpire;


    }
}
