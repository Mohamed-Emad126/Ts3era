using Ts3era.Dto.FavoriteProduct_Dtos;

namespace Ts3era.Repositories.FavProductServices
{
    public interface IFavoriteServies
    {
        Task<List<DetailsFavProductDto>> GetAllFavProduct(string userid);
        Task<string> AddFavProduct(AddFavProductDto dto);
        Task<string> DeleteFavProduct(AddFavProductDto dto);
    }
}
