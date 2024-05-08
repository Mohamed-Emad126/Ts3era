using Ts3era.Dto.UsersDto;
using Ts3era.Models;

namespace Ts3era.Services.User_Services
{
    public interface IUserServices
    {
       
        Task<List<UserDetailsDto>> GetAllUsers(string?search="");
        Task<List<userdto>> GetAllUserDropDown();
        Task<UserDetailsDto> GetDeatailsById(string userid);
        Task<string>Delete(string id);

        ///

       
    }
}
