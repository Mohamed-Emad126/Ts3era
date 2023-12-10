using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ts3era.AuthServices;
using Ts3era.Dto;
using Ts3era.Models;

namespace Ts3era.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthServices authServices;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(IAuthServices authServices ,UserManager<ApplicationUser>userManager)
        {
            this.authServices = authServices;
            this.userManager = userManager;
        }

        #region
        //[HttpPost("Register")]
        //public async Task<IActionResult> Register([FromBody]RegisterDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ApplicationUser user = new ApplicationUser();
        //        user.Email = model.Email;
        //        user.FirstName = model.FirstName;
        //        user.LastName = model.LastName;
        //        user.PhoneNumber = model.PhoneNumber;   
        //        user.National_Id = model.National_Id;
        //        user.UserName = model.UsreName;
        //        user.PasswordHash = model.Password;

        //    var result = await userManager.CreateAsync(user,model.Password);
                
        //        if (result.Succeeded)
        //        {
        //            return Ok("Account Add");
        //        }
        //        return BadRequest(result.Errors.FirstOrDefault());
        //    }
        //    return BadRequest(ModelState);
            
        //}
        #endregion


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto model)
        {
            if (ModelState.IsValid)
            {
              var result =  await authServices.Register(model);

                if (!result.IsAuthanticated)
                    return BadRequest(result.Massage);

                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult>Login([FromBody]LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var login = await authServices.Login(model);
                if (!login.IsAuthanticated)
                    return BadRequest(login.Massage);
                return Ok(login);


            }
            return BadRequest(ModelState);
        } 
         

    }
}
