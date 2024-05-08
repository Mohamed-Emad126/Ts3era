using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Ts3era.Dto.AuthanticationDto;
using Ts3era.Models;

namespace Ts3era.MappingProfile
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleDto>()
               .ForMember(c => c.RoleId, c => c.MapFrom(c => c.Id))
               .ForMember(c => c.RoleName, c => c.MapFrom(c => c.Name));

            CreateMap<ApplicationUser, UserInRoleDto>()
                .ForMember(c => c.UserId, c => c.MapFrom(c => c.Id))
                .ForMember(c => c.UserName, c => c.MapFrom(c => c.UserName));

            
        }
    }
}
