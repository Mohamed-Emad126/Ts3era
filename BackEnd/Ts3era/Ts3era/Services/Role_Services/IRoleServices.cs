using Microsoft.AspNetCore.Identity;
using Ts3era.Dto.AuthanticationDto;

namespace Ts3era.Services.Role_Services
{
    public interface IRoleServices
    {
        Task<string> create(AddRoleDto dto);
        Task<List<IdentityRole>> GetAll ();
        Task<string> Update(string roleid, AddRoleDto dto);
        Task Delete (string roleid);
        

    }
}
