using Ts3era.Dto;
using Ts3era.Models;

namespace Ts3era.AuthServices
{
    public interface IAuthServices
    {
        Task<Authmodel> Register(RegisterDto registerDto);
        Task<Authmodel>Login (LoginDto loginDto);
    }
}
