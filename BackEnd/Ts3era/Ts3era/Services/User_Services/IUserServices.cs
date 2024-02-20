using Ts3era.Models;

namespace Ts3era.Services.User_Services
{
    public interface IUserServices
    {
        Task<List<ApplicationUser>> GetAll(string? search = "");
        Task<ApplicationUser> GetById(string id);
        Task<ApplicationUser> GetName(string name);
        Task<string> Update(string id, ApplicationUser user);
        Task<string>Delete(string id);
    }
}
