using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ts3era.Dto.FavoriteProduct_Dtos;
using Ts3era.Models;
using Ts3era.Models.Data;

namespace Ts3era.Repositories.FavProductServices
{
    public class FavoriteServies : IFavoriteServies
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public FavoriteServies
            (
            ApplicationDbContext context,
            IMapper mapper,
            UserManager<ApplicationUser> userManager
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<List<DetailsFavProductDto>> GetAllFavProduct(string userid)
        {
          
            var Product = await context.FavoriteProducts
                .Include(c => c.product)
                .Include(c => c.product.SubCategory)
                .ToListAsync();
            var map =mapper.Map<List<DetailsFavProductDto>>(Product); 
            
            var user =await userManager.Users.FirstOrDefaultAsync(c=>c.Id == userid);

            if (user == null)
                throw new Exception("Not Found User");

            if (user.FavoriteProducts==null )
                throw new Exception("User Don't Have Favorite Product ! ");
            return map; 
       
        }
        public async Task<string> AddFavProduct(AddFavProductDto dto)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(c => c.Id == dto.UserId);
            
            var product =await context.Products.FirstOrDefaultAsync(c=>c.Id==dto.ProductId);

            var fav = await context.FavoriteProducts
                .FirstOrDefaultAsync(c => c.UserId == user.Id && c.productId == product.Id);

            if (fav != null)
                    return null;

            var favProduct = new FavoriteProductUser
            {
                UserId = user.Id,
                productId = product.Id
            };


            await context.FavoriteProducts.AddAsync(favProduct);
            context.SaveChanges();
            return "Product Add  To AFavoriteProduct You  ";
        }

        public async Task<string> DeleteFavProduct(AddFavProductDto dto)
        {
            var message = string.Empty;
            var user =await userManager.Users.SingleOrDefaultAsync(c=>c.Id == dto.UserId);
            var product =await context.Products.FirstOrDefaultAsync(c=>c.Id== dto.ProductId);

            var favproduct=await context.FavoriteProducts.FirstOrDefaultAsync(c=>c.productId==product.Id&&c.UserId==user.Id);
            if (favproduct != null)
            {
                context.FavoriteProducts.Remove(favproduct);
                context.SaveChanges();
                 message= $"Successfully deleted{product.Name} from Favorite Product ";
            }
            else
            {
                message = "Not Found Product in Favorite You !";
            }
            return message;


        }
    }
}
