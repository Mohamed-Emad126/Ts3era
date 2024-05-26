using System.ComponentModel.DataAnnotations;

namespace Ts3era.Dto.UsersDto
{
    public class DisplayUserProfile
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UsreName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string National_Id { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
