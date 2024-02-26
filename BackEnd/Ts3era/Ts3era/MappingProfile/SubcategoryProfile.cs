using AutoMapper;
using Ts3era.Dto.SubCategory_Dto;
using Ts3era.Models;

namespace Ts3era.MappingProfile
{
    public class SubcategoryProfile:Profile
    {
        public SubcategoryProfile()
        {

            CreateMap<SubCategory, SubCategoryDetailsDto>()
                .ForMember(s => s.SubCategoryName, d => d.MapFrom(s => s.Name))
                .ForMember(s => s.CategoryName, d => d.MapFrom(s => s.Category.Name))
                .ForMember(s => s.products, d => d.MapFrom(s => s.Products.Select(p => p.Name).ToList()));

            CreateMap<CreateSubCategoryDto, SubCategory>()
                .ForMember(c => c.Name, c => c.MapFrom(c => c.SUbCategoryName))
                .ForMember(c => c.Category_Id, c => c.MapFrom(c => c.Category_ID))
                .ForMember(s => s.Image, s => s.Ignore());

            CreateMap<Category, CategoriesDto>()
                .ForMember(c => c.CategoryName, c => c.MapFrom(c => c.Name));
                
        }
    }
}
