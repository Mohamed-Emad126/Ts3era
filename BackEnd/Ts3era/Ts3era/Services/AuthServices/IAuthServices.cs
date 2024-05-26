using Ts3era.Dto.AuthanticationDto;
using Ts3era.Dto.UsersDto;
using Ts3era.Models;

namespace Ts3era.Services.AuthServices
{
    public interface IAuthServices
    {
        Task<Authmodel> Register(RegisterDto registerDto);
        Task<Authmodel> Login(LoginDto loginDto);
        Task<Authmodel> AddAdmin(AddAdminDto dto );
        Task<string>ChangePassword (ChangePasswordDto changePasswordDto);
        Task<string> AssienRoleToUser(AddRoleToUser dto);

        Task<Authmodel> RefreshToken(string token);

        Task<bool>RevokedToken(string token);
        Task Logout();

        Task<bool> EditProfile(string userid, EditUserProfileDto dto);

        Task<DisplayUserProfile> GetCurrentUser();
        Task<int> GetCountUsers();
    }
}
