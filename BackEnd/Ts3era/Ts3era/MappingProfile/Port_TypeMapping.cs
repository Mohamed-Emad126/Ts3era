using AutoMapper;
using Ts3era.Dto.PortType_Dto;
using Ts3era.Models;

namespace Ts3era.MappingProfile
{
    public class Port_TypeMapping:Profile
    {
        public Port_TypeMapping()
        {
            CreateMap<PortTypes, PortType_Dto>()
               .ForMember(c => c.Type_Id, c => c.MapFrom(c => c.Id))
               .ForMember(c => c.PortType_Name, c => c.MapFrom(c => c.Name));
        }
    }
}
