using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Modes;
using Ts3era.Dto.FavoriteProduct_Dtos;
using Ts3era.Dto.UsersDto;
using Ts3era.Models;
using Ts3era.Models.Data;

namespace Ts3era.Services.User_Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<UserServices> logger;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public UserServices(
            UserManager<ApplicationUser> userManager,
            ILogger<UserServices> logger,
            IMapper mapper,
            ApplicationDbContext context
            )
        {
            this.userManager = userManager;
            this.logger = logger;
            this.mapper = mapper;
            this.context = context;
        }
      
      
        

      

       

        public async Task<string>Delete(string id)
        {
         
            var user=await userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user==null)
            {
                throw new Exception("!المستخدم غير موجود");
            }
            else
            {
                try
                {
                  var result =  await userManager.DeleteAsync(user);
                    if (!result.Succeeded)
                    {
                        var errors = string.Empty;
                        foreach (var error  in result.Errors )
                        {
                            errors += $"{error.Description},";
                            
                        }
                        return errors;
                    }else 

                    return "!تم حذف المستخدم ";
                }
                catch (Exception  ex )
                { 
                    logger.LogError( ex.Message);
                    return "";
                }

            }

           
        }

        public async Task<List<userdto>> GetAllUserDropDown()
        {
           var user =await userManager.Users
                .OrderBy(c=>c.UserName).ToListAsync();
           var map=mapper.Map<List<userdto>>(user);
            return map;

        }

        ///
        public async  Task<List<UserDetailsDto>> GetAllUsers(string? search = "")
       {

            var users =await userManager.Users.Include(c=>c.FavoriteProducts).ToListAsync();
           
            var favproduct = await context.FavoriteProducts
                .Include(c => c.product)
                .Include(c => c.product.SubCategory)
                .ToListAsync();
            var usermap =mapper.Map<List<UserDetailsDto>>(users);
            var favmap = mapper.Map<List<DetailsFavProductDto>>(favproduct);

            if (string.IsNullOrEmpty(search))
                return usermap;
            else
            {
                var searchitem = usermap.Where(c => c.UserName.Trim().ToLower()
                          .Contains(search.Trim().ToLower()));
                return searchitem.ToList();
            }



       }
       public async Task<UserDetailsDto> GetDeatailsById(string userid)
        {
            var users = await userManager.Users.Include(c => c.FavoriteProducts).FirstOrDefaultAsync(c=>c.Id==userid);

            var favproduct = await context.FavoriteProducts
                .Include(c => c.product)
                .Include(c => c.product.SubCategory)
                .ToListAsync();
            var usermap = mapper.Map<UserDetailsDto>(users);
            var favmap = mapper.Map<List<DetailsFavProductDto>>(favproduct);

            return usermap;
        }

    }
}
