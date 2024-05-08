using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Ts3era.Dto.FavoriteProduct_Dtos;
using Ts3era.HandleResponseApi;
using Ts3era.Repositories.FavProductServices;

namespace Ts3era.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FavoriteProductsController : ControllerBase
    {
        private readonly IFavoriteServies favoriteServies;

        public FavoriteProductsController(IFavoriteServies favoriteServies)
        {
            this.favoriteServies = favoriteServies;
        }


        [HttpGet]
   
        public async Task<ActionResult<IEnumerable<DetailsFavProductDto>>> GetAllFavoriteProduct(string userid)
        {

            if (ModelState.IsValid)
            {
                var fav=await favoriteServies.GetAllFavProduct(userid);
                if (fav != null)
                     return Ok(fav);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<ActionResult>AddFavoriteProduct(AddFavProductDto dto)
        {
            if (ModelState.IsValid)
            {
                var fav= await favoriteServies.AddFavProduct(dto);
                if (fav==null )
                    return BadRequest("The Product Already Favorite !!");
                return Ok(fav);

            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult>DeleteProductFromFavorite(AddFavProductDto dto)
        {
            if (ModelState.IsValid)
            {
                var fav =await favoriteServies.DeleteFavProduct(dto);
                if (fav == null)
                    return BadRequest(fav);
                return Ok(fav); 

            }
            return BadRequest();
        }
    }
}
