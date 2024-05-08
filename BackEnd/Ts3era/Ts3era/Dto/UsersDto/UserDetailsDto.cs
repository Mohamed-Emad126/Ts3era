using Ts3era.Dto.FavoriteProduct_Dtos;

namespace Ts3era.Dto.UsersDto
{
    public class UserDetailsDto
    {
        public string UserId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email {  get; set; }
        public string PasswordHash { get; set;}
        public string PhoneNumber { get; set; }
        public string National_Id { get; set; }
        public List<DetailsFavProductDto>FavoriteProducts { get; set; }

    }
}
