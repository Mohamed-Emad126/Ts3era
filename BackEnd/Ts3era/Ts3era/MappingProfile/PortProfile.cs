using AutoMapper;
using Ts3era.Dto.PortsDto;
using Ts3era.Dto.PortType_Dto;
using Ts3era.Models;

namespace Ts3era.MappingProfile
{
    public class PortProfile:Profile 
    {
        public PortProfile()
        {
            CreateMap<Ports, PortsDetailsDto>()
               .ForMember(s => s.PortName, d => d.MapFrom(c => c.Name))
               .ForMember(s => s.Type, d => d.MapFrom(c => c.PortTypes.Name))
               .ForMember(s => s.Governorate, c => c.MapFrom(c => c.Governorates.Name));

            CreateMap<Ports, PortDto>()
                .ForMember(s => s.PortName, d => d.MapFrom(c => c.Name))
                .ForMember(s => s.Governorate, d => d.MapFrom(c => c.Governorates.Id))
                .ForMember(s => s.Types, d => d.MapFrom(c => c.PortTypes.Id))
                .ReverseMap();

            CreateMap<Ports, CreatePortDto>()
                .ForMember(c => c.PortName, c => c.MapFrom(c => c.Name))
                .ForMember(c => c.Governorate_Id, c => c.MapFrom(c => c.Governorate_Id))
                .ForMember(c => c.Type_Id, c => c.MapFrom(c => c.PortType_Id))
                .ReverseMap();

           
        }
    }
}
