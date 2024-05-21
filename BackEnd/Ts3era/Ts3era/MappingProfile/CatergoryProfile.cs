using AutoMapper;
using Microsoft.JSInterop;
using Ts3era.Dto.Category_Dto.Category_Dto;
using Ts3era.ImageResolver;
using Ts3era.Models;

namespace Ts3era.MappingProfile
{
    public class CatergoryProfile:Profile
    {
        public CatergoryProfile()
        {

            CreateMap<Category, CategoryDetailsDto>()  
                .ForMember(c=>c.CategoryName,x=>x.MapFrom(c=>c.Name))
                .ForMember(c => c.SubCategoriesNames,
                c => c.MapFrom(x => x.subCategories.Select(x => x.Name).ToList()))
                .ForMember(c=>c.Image,c=>c.MapFrom<CatgoryImageResolver>())
                .ReverseMap();

            CreateMap<CreateCategoryDto, Category>()
                .ForMember(c => c.Name, c => c.MapFrom(c => c.CategoryName))
                .ForMember(s => s.Image, s => s.Ignore());

        }
    }
}
