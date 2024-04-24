using AutoMapper;
using Ts3era.Dto.GovernoratesDto;
using Ts3era.Models;

namespace Ts3era.MappingProfile
{
    public class GovernorateProfile:Profile
    {
        public GovernorateProfile()
        {
            CreateMap<Governorates, governorateDto>()
               .ForMember(c => c.GovernorateName, c => c.MapFrom(c => c.Name));
        }
    }
}
