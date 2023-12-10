using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using Ts3era.Dto;
using Ts3era.Heler;
using Ts3era.Models;

namespace Ts3era.AuthServices
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JWT jWT;
        public AuthServices(UserManager<ApplicationUser>userManager,IOptions<JWT>_Jwt)
        {
            this.userManager = userManager;
             jWT=_Jwt.Value;
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
                PhoneNumber= registerDto.PhoneNumber,

            };
            IdentityResult result  =  await userManager.CreateAsync(user, registerDto.Password);
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
                Expireon = token.ValidTo,
                UserName = user.UserName
                
            };
        }
        public async Task<Authmodel> Login(LoginDto loginDto)
        {
            var authmodel =new Authmodel();
           ApplicationUser user= await userManager.FindByEmailAsync(loginDto.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                authmodel.Massage = "!!يوجد خطأ في  البريد الالكتروني او كلمه المرور " ; 
                return authmodel;
            }

            var token = await Createtoken(user);

            var roleresult = await userManager.GetRolesAsync(user);
            
            authmodel.IsAuthanticated=true;
            authmodel.Email = loginDto.Email;
            authmodel.Expireon=token.ValidTo;
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authmodel.Role=roleresult.ToList();
            authmodel.UserName=user.UserName;



            return authmodel;




            

        }

        public async Task<JwtSecurityToken>Createtoken (ApplicationUser user)
        {

            /*Add claims */
            var claim=new List<Claim> ();
            claim.Add(new Claim(ClaimTypes.Name, user.UserName));
            claim.Add(new Claim(ClaimTypes.Email, user.Email));
            claim.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claim.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            /*AddROle*/
            var role =await  userManager.GetRolesAsync(user);
            foreach (var itemrole in role)
            {
                claim.Add(new Claim(ClaimTypes.Role, itemrole));

            }
            /*Add signingCredentials*/

           /*Addkey=>*/ var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWT  .key));

            SigningCredentials signingCredentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                
            /*Create represent Token */
            var token = new JwtSecurityToken(
                
                issuer:jWT.Issuer,
                audience:jWT.Audiece,
                expires:DateTime.Now.AddDays(jWT.DurationInDays),
                claims:claim,
                signingCredentials:signingCredentials
               
                );
            return token;
            

           

        }






    }
}
