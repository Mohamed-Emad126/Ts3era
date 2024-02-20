using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Modes;
using Ts3era.Models;

namespace Ts3era.Services.User_Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<UserServices> logger;

        public UserServices(
            UserManager<ApplicationUser> userManager,
            ILogger<UserServices> logger
            )
        {
            this.userManager = userManager;
            this.logger = logger;
        }
        public async Task<List<ApplicationUser>> GetAll(string? search = "")
        {
            var user = await userManager.Users.ToListAsync();
            if (string.IsNullOrEmpty(search))
                return user;
            else
                user = await userManager.Users
                    .Where(c => c.UserName.Trim().ToLower()
                    .Contains(search.Trim().ToLower()))
                    .Where(v => v.Email.Trim().ToLower()
                    .Contains(search.Trim().ToLower())).ToListAsync();
            return user;
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(c => c.Id == id);
            if (user is null)
                throw new Exception("المستخدم غير موجود");

            return user;
        }

        public Task<ApplicationUser> GetName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Update(string id, ApplicationUser user)
        {
            try
            {
                var User = await userManager.Users.FirstOrDefaultAsync(c => c.Id == id);
                if (User != null)
                {
                    User.UserName = user.UserName;
                    User.Email = user.Email;
                    User.PhoneNumber = user.PhoneNumber;
                    User.PasswordHash = user.PasswordHash;
                    User.National_Id = user.National_Id;
                    User.FirstName = user.FirstName;
                    User.LastName = user.LastName;
                    User.NormalizedUserName = user.UserName.ToLower();
                    User.NormalizedEmail = user.Email.ToLower();

                    var result = await userManager.UpdateAsync(User);
                    if (!result.Succeeded)
                        throw new Exception($"{result.Errors},");
                }
            }
            catch (Exception ex)
            {

                logger.LogError(ex, ex.Message);
            }
            return "تم تعديل بيانات المستخدم";

        }

        public async Task<string>Delete(string id)
        {
            var user=new ApplicationUser();
            if (id!=user.Id)
                throw new Exception("المستخدم غير موجود");
            try
            {
                var User = await userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

             
                var result= await userManager.DeleteAsync(user); 
                if (!result.Succeeded)
                {
                    var errors=string .Empty;
                    foreach (var error in result.Errors)
                    {

                        errors += $"{error.Description},";
                        
                        
                    }
                    return errors;
                }
              
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message);
            }

            return "تم حذف المستخدم";
        }

    }
}
