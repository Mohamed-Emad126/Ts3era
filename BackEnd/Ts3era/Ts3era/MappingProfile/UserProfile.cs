using AutoMapper;
using Ts3era.Dto.FavoriteProduct_Dtos;
using Ts3era.Dto.UsersDto;
using Ts3era.Models;

namespace Ts3era.MappingProfile
{
    public class UserProfile:Profile    
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDetailsDto>()
                .ForMember(c => c.UserId, c => c.MapFrom(c => c.Id))
                .ForMember(c => c.FirstName, c => c.MapFrom(c => c.FirstName))
                .ForMember(c => c.LastName, c => c.MapFrom(c => c.LastName))
                .ForMember(c => c.UserName, c => c.MapFrom(c => c.UserName))
                .ForMember(c => c.PhoneNumber, c => c.MapFrom(c => c.PhoneNumber))
                .ForMember(c => c.National_Id, c => c.MapFrom(c => c.National_Id))
                .ForMember(c => c.PasswordHash, c => c.MapFrom(c => c.PasswordHash))
                .ForMember(c => c.FavoriteProducts, c => c.MapFrom(c => c.FavoriteProducts))
                .ReverseMap();

            CreateMap<ApplicationUser, userdto>()
                .ForMember(c => c.UserId, c => c.MapFrom(c => c.Id))
                .ForMember(c => c.UserName, c => c.MapFrom(c => c.UserName));
        }
    }
}
