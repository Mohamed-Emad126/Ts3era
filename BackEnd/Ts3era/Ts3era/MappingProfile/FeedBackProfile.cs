using AutoMapper;
using Ts3era.Dto.FeedBack_Dto;
using Ts3era.Models;

namespace Ts3era.MappingProfile
{
    public class FeedBackProfile:Profile
    {
        public FeedBackProfile()
        {
            CreateMap<FeedBack, FeedBackDto>()
                .ReverseMap();
        }
    }
}
