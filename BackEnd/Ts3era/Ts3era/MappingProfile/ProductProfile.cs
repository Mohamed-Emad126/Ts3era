using AutoMapper;
using Ts3era.Dto.ProductDto;
using Ts3era.Models;

namespace Ts3era.MappingProfile
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDetailsDto>()
                .ForMember(p => p.ProductName, d => d.MapFrom(c => c.Name))
                .ForMember(p => p.SubCategoryName, d => d.MapFrom(c => c.SubCategory.Name));

            CreateMap<CreateProductDto, Product>()
                .ForMember(c => c.Name, c => c.MapFrom(c => c.ProductName))
                .ForMember(c => c.SubCategory_Id, c => c.MapFrom(c => c.SubCategory_ID))
                .ForMember(c => c.Image, s => s.Ignore());

            CreateMap<SubCategory, SubCategoriesDto>()
            .ForMember(c => c.SubCategoryName, c => c.MapFrom(c => c.Name));
        }
    }
}
