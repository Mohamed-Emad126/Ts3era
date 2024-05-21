using AutoMapper;
using Ts3era.Dto.ComplaintsDto;
using Ts3era.ImageResolver;
using Ts3era.Models;

namespace Ts3era.MappingProfile
{
    public class Complaintprofile:Profile
    {
        public Complaintprofile()
        {

            CreateMap<Complaints, ComplaintDto>()
                .ForMember(c => c.UserName, c => c.MapFrom(c => c.Name))
                .ForMember(c => c.ComplaintAddress, c => c.MapFrom(c => c.Address))
                .ForMember(c => c.complaintDetails, c => c.MapFrom(c => c.Details))
                .ForMember(c => c.PhoneNumber, c => c.MapFrom(c => c.Phone))
                .ForMember(c=>c.AddAtachment,c=>c.Ignore())
                .ReverseMap();

            CreateMap<Complaints, ComplaintDetailsDto>()
                .ForMember(c => c.UserName, c => c.MapFrom(c => c.Name))
                .ForMember(c => c.ComplaintAddress, c => c.MapFrom(c => c.Address))
                .ForMember(c => c.complaintDetails, c => c.MapFrom(c => c.Details))
                .ForMember(c => c.PhoneNumber, c => c.MapFrom(c => c.Phone))
                .ForMember(c => c.Attachment, c => c.MapFrom<ComplaintImageResolver>());
               
        }
    }
}
