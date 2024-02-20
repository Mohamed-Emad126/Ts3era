using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ts3era.Dto.AuthanticationDto;

namespace Ts3era.Services.Role_Services
{
    public class RoleServices : IRoleServices
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<RoleServices> logger;

        public RoleServices
            (
            RoleManager<IdentityRole> roleManager,
            ILogger<RoleServices> logger
            )
        {
            this.roleManager = roleManager;
            this.logger = logger;
        }
        public async Task<string> create(AddRoleDto dto)
        {

            if (await roleManager.RoleExistsAsync(dto.RoleName))
                throw new Exception($"{dto.RoleName}Already Saved !");
            var role = new IdentityRole();
            role.Name = dto.RoleName;
            try
            {
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    var error = string.Empty;
                    foreach (var item in result.Errors)
                    {
                        error += $"{item.Description},";
                        return error;

                    }
                }


            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);


            }
            return $"the role {dto.RoleName} saved ";

        }

    

        public async Task<List<IdentityRole>> GetAll()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return roles;

        }

        public async Task<string> Update(string roleid, AddRoleDto dto)
        {
            try
            {

                var oldrole = await roleManager.Roles.FirstOrDefaultAsync(c => c.Id == roleid);

                oldrole.Name = dto.RoleName;
                var result = await roleManager.UpdateAsync(oldrole);
                if (!result.Succeeded)
                    throw new Exception($"{result.Errors},Invalid Role");


            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            return "Update Role";
        }
        public async Task Delete(string roleid)
        {
            {
                var roles = new IdentityRole();
                if (roles.Id != roleid)
                    throw new Exception("اسم الدور غير موجود");

                try
                {

                    var role = await roleManager.Roles.FirstOrDefaultAsync(c => c.Id == roleid);
                if (role != null)
                {

                    var result = await roleManager.DeleteAsync(role);
                    if (!result.Succeeded)
                    {
                        var errors = string.Empty;
                        foreach (var error in result.Errors)
                        {
                            errors += $"{error.Description},";
                        }
                       
                        throw new Exception(errors);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);


            }





            
        }
    }

    }
}
