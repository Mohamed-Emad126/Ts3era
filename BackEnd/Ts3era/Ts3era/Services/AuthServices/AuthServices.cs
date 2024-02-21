using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ts3era.Dto.AuthanticationDto;
using Ts3era.Heler;
using Ts3era.Models;
using Org.BouncyCastle.Crypto;
using Microsoft.EntityFrameworkCore;

namespace Ts3era.Services.AuthServices
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWT jWT;
        public AuthServices(UserManager<ApplicationUser> userManager,
            IOptions<JWT> _Jwt,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            jWT = _Jwt.Value;
        }

        public async Task<Authmodel> Register(RegisterDto registerDto)
        {
            if (await userManager.FindByEmailAsync(registerDto.Email) != null)
                return new Authmodel { Massage = "(!البريد الإلكتروني قيد التسجيل(موجود بالفعل)" };

            if (await userManager.FindByNameAsync(registerDto.UsreName) != null)
                return new Authmodel { Massage = "(!اسم المستخدم  قيد التسجيل(موجود بالفعل)" };

            var user = new ApplicationUser()
            {
                UserName = registerDto.UsreName,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                National_Id = registerDto.National_Id,
                PhoneNumber = registerDto.PhoneNumber,

            };
            IdentityResult result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var error = string.Empty;
                foreach (var item in result.Errors)
                {
                    error += $"{item.Description},";

                }

                return new Authmodel { Massage = error };
            }
            await userManager.AddToRoleAsync(user, "User");
            var token = await Createtoken(user);

            return new Authmodel()
            {
                Role = new List<string> { "User" },
                Email = registerDto.Email,
                IsAuthanticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
               // Expireon = token.ValidTo,
                UserName = user.UserName

            };
        }
        public async Task<Authmodel> Login(LoginDto loginDto)
        {
            var authmodel = new Authmodel();
            ApplicationUser user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                authmodel.Massage = "!!يوجد خطأ في  البريد الالكتروني او كلمه المرور ";
                return authmodel;
            }

            var token = await Createtoken(user);

            var roleresult = await userManager.GetRolesAsync(user);

            authmodel.IsAuthanticated = true;
            authmodel.Email = loginDto.Email;
           // authmodel.Expireon = token.ValidTo;
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authmodel.Role = roleresult.ToList();
            authmodel.UserName = user.UserName;

            return authmodel;


        }

        public async Task<JwtSecurityToken> Createtoken(ApplicationUser user)
        {

            /*Add claims */
            var claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.Name, user.UserName));
            claim.Add(new Claim(ClaimTypes.Email, user.Email));
            claim.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claim.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            /*AddROle*/
            var role = await userManager.GetRolesAsync(user);
            foreach (var itemrole in role)
            {
                claim.Add(new Claim(ClaimTypes.Role, itemrole));

            }
            /*Add signingCredentials*/

            /*Addkey=>*/
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWT.key));

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            /*Create represent Token */
            var token = new JwtSecurityToken(

                issuer: jWT.Issuer,
                audience: jWT.Audiece,
                expires: DateTime.Now.AddDays(jWT.DurationInDays),
                claims: claim,
                signingCredentials: signingCredentials

                );
            return token;




        }

        public async Task<string> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            
            var user =await userManager.FindByNameAsync(changePasswordDto.UserName);
            if (user == null)
                throw new ArgumentException("اسم المستخدم غير موجود");


            var result = await userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                var error = string.Empty;
                foreach (var errors in result.Errors)
                {
                    error += $"{errors.Description},";

                }
                return error;

            }
            

            return "تم تغير كلمه المرور";

        }

        public async Task<string> AssienRoleToUser(AddRoleToUser dto)
        {
            var user =await userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return "المستخدم غير صالح ";
            if (!await roleManager.RoleExistsAsync(dto.RoleName))
                return "خطا في اسم المهام";
            if (await userManager.IsInRoleAsync(user,dto.RoleName))
                return "المستخدم موجود بالفعل";

            var result =await userManager.AddToRoleAsync(user,dto.RoleName);

            return result.Succeeded ? string.Empty : "لم يحدث شيء";


        }

        public async Task<Authmodel> RefreshToken(string token)
        {
           var authmodel=new Authmodel();   
            
            var user =await userManager.Users.
                FirstOrDefaultAsync(c=>c.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                authmodel.IsAuthanticated = false;
                authmodel.Massage = "Invalid Token";
                return authmodel;
            }

            var refreshtoken = user.RefreshTokens.Single(c => c.Token==token);
            if (!refreshtoken.IsActive)
            {
                authmodel.IsAuthanticated = false;
                authmodel.Massage = "Invalid Token";
                return authmodel;
            }
            refreshtoken.RenokedOn=DateTime.UtcNow;

            var newrefreshtoken = GenerateRefreshToken();

            user.RefreshTokens.Add(newrefreshtoken);

            await userManager.UpdateAsync(user);
            var jwttoken =await  Createtoken(user);
            
            authmodel.IsAuthanticated = true;
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(jwttoken);
            authmodel.Email = user.Email;
            authmodel.UserName = user.UserName;

            var role = await userManager.GetRolesAsync(user);

            authmodel.Role = role.ToList();
            authmodel.RefreshToken = newrefreshtoken.Token;
            authmodel.RefreshTokenExpire = newrefreshtoken.ExpireOn;

            return authmodel;

        }


        public async Task<bool> RevokedToken(string token)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(c => c.RefreshTokens.Any(c => c.Token == token));
            if (user == null)
                return false;
            var refreshtoken = user.RefreshTokens.Single(c => c.Token == token);
            if (!refreshtoken.IsActive)
                return false;
            refreshtoken.RenokedOn= DateTime.UtcNow;

            await userManager.UpdateAsync(user);

            return true;
        }





       public async  Task<Authmodel> AddAdmin(AddAdminDto dto)
        {
          
                if (await userManager.FindByEmailAsync(dto.Email) != null)
                    return new Authmodel { Massage = "(!البريد الإلكتروني قيد التسجيل(موجود بالفعل)" };

              if (await userManager.FindByNameAsync(dto.UsreName) != null)
                return new Authmodel { Massage = "(!اسم المستخدم  قيد التسجيل(موجود بالفعل)" };

            var user = new ApplicationUser
            {
                UserName = dto.UsreName,
                Email = dto.Email,
                PasswordHash = dto.Password
            };

           var result = await userManager.CreateAsync(user,dto.Password);
            
            if (!result.Succeeded)
            {
                var erros = string.Empty;
                foreach (var error in result.Errors)
                {
                    erros += $"{error.Description}&&";                    
                }
                return new Authmodel { Massage= erros};
            }

            await userManager.AddToRoleAsync(user,"admin");

            var jwt =await  Createtoken(user);


            return new Authmodel
            {
                UserName = dto.UsreName,
                Email = dto.Email,
                Role = new List<string> { "admin" },
                IsAuthanticated = true,
                Token=new JwtSecurityTokenHandler().WriteToken(jwt),
            };
        }

        private RefreshToken GenerateRefreshToken()
        {
            var reandomNumber = new byte[32];

            using var generate = new RNGCryptoServiceProvider();
            generate.GetBytes(reandomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(reandomNumber),
                ExpireOn = DateTime.UtcNow.AddDays(30),
                CreatedOn = DateTime.UtcNow
            };

        }

       
    }
}
