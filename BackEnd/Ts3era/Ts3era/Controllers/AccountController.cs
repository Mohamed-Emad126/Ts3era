using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.UA;
using System.Security.Claims;
using Ts3era.Dto.AuthanticationDto;
using Ts3era.Dto.EmailsDto;
using Ts3era.Dto.UsersDto;
using Ts3era.HandleResponseApi;
using Ts3era.Models;
using Ts3era.Services.AuthServices;
using Ts3era.Services.EmailServices;

namespace Ts3era.Controllers
{

    [Route("api/[controller]/[action]")] 
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthServices authServices;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailServices emailServices;

        public AccountController(IAuthServices authServices,
            UserManager<ApplicationUser>userManager,
            IEmailServices emailServices
            )
        {
            this.authServices = authServices;
            this.userManager = userManager;
            this.emailServices = emailServices;
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

       

        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<DisplayUserProfile>> GetCurrentUser2()
        {
            var user = await authServices.GetCurrentUser();
            return Ok( user );
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterDto>> Register([FromBody]RegisterDto model)
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
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginDto>>Login([FromBody]LoginDto model)
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


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(dto.Email);
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var forgetpasswordlink = Url.Action(nameof(ResetPassword), "Account", new { token, Email = user.Email }, Request.Scheme);

                    var email = new EmailRequestDto
                    {
                        EmailTO = dto.Email,
                        Subject = "ForgetPassword",
                        Body = forgetpasswordlink!
                    };

                    await emailServices.SendEmail(email);
                    return Ok($"Password Change Request is send email <{user.Email}> ..please open your Email");




                }
                return StatusCode(StatusCodes.Status400BadRequest,
                    new { Error = "Error", message = " couldn't send email ,please try Again ! " });


            }
            return BadRequest(ModelState);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            if (ModelState.IsValid)
            {
                var model = new ResetPasswordDto { Email = email, Token = token };
                return Ok(new { model });

            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassord([FromForm] ResetPasswordDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = await  userManager.FindByEmailAsync(dto.Email);
                if (user != null)
                {
                    var resultpassword = await userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

                    if (!resultpassword.Succeeded)
                    {
                        foreach (var error in resultpassword.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                            return BadRequest(ModelState);
                        }
                    }
                    return Ok("Password Must Be Changed ");
                }

            }
            return BadRequest(ModelState);
        }



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordDto changePasswordDto)
        {
            if (ModelState.IsValid)
            {
                var user = await authServices.ChangePassword(changePasswordDto);
                

                return Ok(user);

            }
            return BadRequest(ModelState);

        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddUserToRole(AddRoleToUser dto)
        {
            if (ModelState.IsValid)
            {

                var result = await authServices.AssienRoleToUser(dto);

                if (!string.IsNullOrEmpty(result))
                    return BadRequest(result);


                return Ok(dto);

            }
            return BadRequest(ModelState); 
        }


        [HttpGet]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshtoken = Request.Cookies["RefreshToken"];
            var result = await authServices.RefreshToken(refreshtoken);

            if (!result.IsAuthanticated)
                return BadRequest(result);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpire);
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RevokeTokenDto dto)
        {
            var token = dto.Token ?? Request.Cookies["RefreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest("Rquired Token !");
            var result = await authServices.RevokedToken(token);
            if (!result)
                return BadRequest("Invalid Token ! ");
            return Ok("Succeded");

        }



        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult>AddAdmin(AddAdminDto dto)
        {
            if (ModelState.IsValid)
            {
                var admin = await authServices.AddAdmin(dto);
                return Ok(admin);

            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>>Logout()
        {
            if (ModelState.IsValid)
            {
                await authServices.Logout();
                return StatusCode(200,new {Message = "You have already been logged out!" });
            }
            return BadRequest(ModelState);
        }
        private void SetRefreshTokenInCookie(string token,DateTime expires)
        {
            var cookieoption = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
            };
            Response.Cookies.Append("RefreshToken", token, cookieoption);


        }


        [HttpPut]
        [Authorize]
        public async Task<ActionResult>EditProfiel(string userid, [FromBody]EditUserProfileDto dto)
        {
            if (ModelState.IsValid)
            {
                var user =await authServices.EditProfile(userid, dto);
                return Ok(user);

            }
            return BadRequest (ModelState);
        }

        [HttpGet]
        [Authorize("Admin")]
        public async Task<ActionResult>GetCountUsers()
        {
            if (ModelState.IsValid)
            {
                var users = await authServices.GetCountUsers();
                return Ok(users);
            }
            return BadRequest(ModelState);  
        }
        
    }
}
