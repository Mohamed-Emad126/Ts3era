using AutoMapper;
using Org.BouncyCastle.Tls;
using Ts3era.Dto.FavoriteProduct_Dtos;
using Ts3era.Models;

namespace Ts3era.MappingProfile
{
    public class Favoraie_ProductProfile:Profile
    {
        public Favoraie_ProductProfile()
        {

            CreateMap<FavoriteProductUser, AddFavProductDto>()
                .ForMember(c => c.ProductId, c => c.MapFrom(c => c.product.Id))
                .ForMember(c => c.UserId, c => c.MapFrom(c => c.User.Id))
                .ReverseMap();

            CreateMap<FavoriteProductUser, DetailsFavProductDto>()
                .ForMember(c => c.ProductName, c => c.MapFrom(c => c.product.Name))
                .ForMember(c => c.Price_From, c => c.MapFrom(c => c.product.Price_From))
                .ForMember(c => c.Price_To, c => c.MapFrom(c => c.product.Price_TO))
                .ForMember(c => c.LastUpdate, c => c.MapFrom(c => c.product.Last_Update))
                .ForMember(c => c.IsAvailable, c => c.MapFrom(c => c.product.IsAvailable))
                .ForMember(c => c.SubCategoryName, c => c.MapFrom(c => c.product.SubCategory.Name));
            

        }
    }
}
